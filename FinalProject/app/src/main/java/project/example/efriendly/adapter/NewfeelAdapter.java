package project.example.efriendly.adapter;


import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.io.InputStream;
import java.net.URL;
import java.net.URLConnection;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.time.temporal.ChronoUnit;
import java.util.Collections;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.userFragments.NewfeelActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ShowPostNewfeelAdapterBinding;
import project.example.efriendly.holder.NewfeelPostHolder;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class NewfeelAdapter extends  RecyclerView.Adapter<NewfeelPostHolder> implements DatabaseConnection {
    private Activity activity;
    List<PostRes> posts = Collections.emptyList();
    Context context;
    ShowPostNewfeelAdapterBinding binding;
    final private UserService userService = RetrofitClientGenerator.getService(UserService.class);
    NewfeelActivity.ClickListener listener;

    public NewfeelAdapter(List<PostRes> posts, Context context, NewfeelActivity.ClickListener listener){
        this.posts = posts;
        this.context = context;
        this.listener = listener;
    }

    @NonNull
    @Override
    public NewfeelPostHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        Context context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        binding = ShowPostNewfeelAdapterBinding.inflate(inflater, parent, false);
        NewfeelPostHolder newfeelPostHolder = new NewfeelPostHolder(binding);

        return newfeelPostHolder;
    }
    @Override
    public void onBindViewHolder(@NonNull NewfeelPostHolder holder, int position) {
        final int index = holder.getAdapterPosition();
        PostRes post = posts.get(index);
        holder.view.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                listener.click(post);
            }
        });

        holder.progressBar.setVisibility(View.VISIBLE);
        holder.post.setVisibility(View.INVISIBLE);

        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
        LocalDateTime updatePost = LocalDateTime.parse(post.getUpdatedDate().substring(0,19), formatter);
        LocalDateTime now = LocalDateTime.now();

        long minutes = ChronoUnit.MINUTES.between(updatePost, now);
        long hour = ChronoUnit.HOURS.between(updatePost, now);
        long day = ChronoUnit.DAYS.between(updatePost, now);

        String time = String.valueOf(minutes) + "minutes";
        if (day > 7){
            time = updatePost.format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        }
        else if (hour > 24){
            minutes -= hour * 60;
            hour -= day * 24;
            time = String.valueOf(day)+" days "+String.valueOf(hour) + " hours " + String.valueOf(minutes) + " minutes";
        }
        else if (minutes > 60) {
            minutes -= hour * 60;
            time = String.valueOf(hour) + " hours " + String.valueOf(minutes) + " minutes";
        }

        holder.time.setText(time);
        holder.likeCount.setText(String.valueOf(10));
        holder.commentCount.setText(String.valueOf(30) + " comments");
        holder.shareCount.setText(String.valueOf(post.getUserShare().size() + " shares"));
        holder.des.setText(post.getDescription());

        try {
            holder.name.setText(post.getUser().getName());
            if (post.getUser().getAvatarPath() != null) holder.avt.setImageBitmap(post.getUser().getAvtBitmap());
            else holder.avt.setImageResource(R.drawable.user);
            holder.post.setVisibility(View.VISIBLE);
        }
        catch (NullPointerException err) {
            Call<UserRes> userResCall = userService.GetById(post.getUserID());
            userResCall.enqueue(new Callback<UserRes>() {
                @Override
                public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                    if (response.isSuccessful()) {
                        UserRes user = response.body();
                        post.setUser(user);
                        if (post.getUser().getAvatarPath() != null) {
                            try {
                                InputStream newUrl = new URL(IMAGE_URL + post.getUser().getAvatarPath()).openStream();
                                Bitmap image = BitmapFactory.decodeStream(newUrl);
                                holder.avt.post(new Runnable() {
                                    @Override
                                    public void run() {
                                        holder.avt.setImageBitmap(image);
                                        post.getUser().setAvtBitmap(image);
                                    }
                                });
                            }
                            catch (Exception e) {Log.d("Debug", e.getMessage());}
                        }
                        holder.name.setText(post.getUser().getName());
                        holder.post.setVisibility(View.VISIBLE);

                    } else {
                        String message = "An error occurred please try again later ...";
                        Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                    }
                }
                @Override
                public void onFailure(Call<UserRes> call, Throwable t) {
                    String message = t.getLocalizedMessage();;
                    Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                }
            });
        }
        holder.progressBar.setVisibility(View.VISIBLE);

        try{
            holder.clothesImg.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
            holder.progressBar.setVisibility(View.INVISIBLE);
        }
        catch (IndexOutOfBoundsException err){
            try {

                InputStream newUrl = new URL(IMAGE_URL + post.getMediaPath().get(post.getCurrentIndex())).openStream();
                Bitmap image = BitmapFactory.decodeStream(newUrl);
                holder.clothesImg.post(new Runnable() {
                    @Override
                    public void run() {
                        post.getImgBitmap().add(image);
                        holder.clothesImg.setImageBitmap(post.getImgBitmap().get(0));
                        holder.progressBar.setVisibility(View.INVISIBLE);
                    }
                });
            }
            catch (Exception e) {
                Log.d("SetImageViewDebug", e.getMessage());
            }
        }

        binding.setClickHandler(new PostNewFeelClickHandler(post, holder));
    }
    @Override
    public int getItemCount() {
        return posts.size();
    }
    public class PostNewFeelClickHandler{
        NewfeelPostHolder holder;
        PostRes post;
        public PostNewFeelClickHandler(PostRes post, NewfeelPostHolder holder){
            this.post = post;
            this.holder = holder;
        }
        public void nextPicClick(View view){
            post.setCurrentIndex(post.getCurrentIndex()+1);
            if (post.getCurrentIndex()>= post.getMediaPath().size()) post.setCurrentIndex(0);

            holder.progressBar.setVisibility(View.VISIBLE);
            try{
                holder.clothesImg.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
                holder.progressBar.setVisibility(View.INVISIBLE);
            }
            catch (ArrayIndexOutOfBoundsException err){
                try {
                    InputStream newUrl = new URL(IMAGE_URL + post.getMediaPath().get(post.getCurrentIndex())).openStream();
                    Bitmap image = BitmapFactory.decodeStream(newUrl);
                    holder.clothesImg.post(new Runnable() {
                        @Override
                        public void run() {
                            post.getImgBitmap().add(image);
                            holder.clothesImg.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
                            holder.progressBar.setVisibility(View.INVISIBLE);
                        }
                    });
                } catch (Exception e) {
                    Log.d("NextButtonDebug", e.getMessage());
                }
            }
        }
        public void previousClick(View view) {
            post.setCurrentIndex(post.getCurrentIndex() - 1);
            if (post.getCurrentIndex() < 0) post.setCurrentIndex(post.getMediaPath().size() - 1);
            holder.progressBar.setVisibility(View.VISIBLE);
            try {
                holder.clothesImg.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
                holder.progressBar.setVisibility(View.INVISIBLE);
            } catch (ArrayIndexOutOfBoundsException err) {
                try {
                    URL newUrl = new URL(IMAGE_URL + post.getMediaPath().get(post.getCurrentIndex()));
                    URLConnection conn = newUrl.openConnection();
                    Bitmap mIcon_val = BitmapFactory.decodeStream(conn.getInputStream());
                    if (mIcon_val != null) {
                        post.getImgBitmap().add(mIcon_val);
                        holder.clothesImg.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
                        holder.progressBar.setVisibility(View.INVISIBLE);
                    }
                } catch (Exception e) {
                    Log.d("NextButtonDebug", e.getMessage());
                }
            }
        }
    }
}