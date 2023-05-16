package project.example.efriendly.adapter;


import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;

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

public class NewfeelAdapter extends RecyclerView.Adapter<NewfeelPostHolder> implements DatabaseConnection {
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
        holder.view.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                listener.click(posts.get(index));
            }
        });
        holder.nextClick.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                posts.get(index).setCurrentIndex(posts.get(index).getCurrentIndex()+1);
                if (posts.get(index).getCurrentIndex()>= posts.get(index).getMediaPath().size()) posts.get(index).setCurrentIndex(0);
                holder.progressBar.setVisibility(View.VISIBLE);
                Glide.with(context)
                        .load(IMAGE_URL + posts.get(index).getMediaPath().get(posts.get(index).getCurrentIndex()))
                        .into(holder.clothesImg);
                holder.progressBar.setVisibility(View.INVISIBLE);
            }
        });
        holder.previousClick.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                posts.get(index).setCurrentIndex( posts.get(index).getCurrentIndex() - 1);
                if ( posts.get(index).getCurrentIndex() < 0)  posts.get(index).setCurrentIndex( posts.get(index).getMediaPath().size() - 1);
                holder.progressBar.setVisibility(View.VISIBLE);
                Glide.with(context)
                        .load(IMAGE_URL +  posts.get(index).getMediaPath().get( posts.get(index).getCurrentIndex()))
                        .into(holder.clothesImg);
                holder.progressBar.setVisibility(View.INVISIBLE);
            }
        });
        holder.progressBar.setVisibility(View.VISIBLE);
        holder.post.setVisibility(View.INVISIBLE);
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
        LocalDateTime updatePost = LocalDateTime.parse(posts.get(index).getUpdatedDate().substring(0,19), formatter);
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
        holder.shareCount.setText(String.valueOf(posts.get(index).getUserShare().size() + " shares"));
        holder.des.setText(posts.get(index).getDescription());

        Call<UserRes> userResCall = userService.GetById(posts.get(index).getUserID());
        userResCall.enqueue(new Callback<UserRes>() {
            @Override
            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                if (response.isSuccessful()) {
                    UserRes user = response.body();
                    posts.get(index).setUser(user);
                    if (posts.get(index).getUser().getAvatarPath() != null) {
                        try {
                            Glide.with(context)
                                    .load(IMAGE_URL + "/avatar/" +posts.get(index).getUser().getAvatarPath()).placeholder(R.drawable.placeholder)
                                    .into(holder.avt);
                            holder.progressBar.setVisibility(View.INVISIBLE);
                        }
                        catch (Exception e) {Log.d("Debug", e.getMessage());}
                    }
                    holder.name.setText(posts.get(index).getUser().getName());
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
        holder.progressBar.setVisibility(View.VISIBLE);

        Glide.with(context)
                .load(IMAGE_URL + posts.get(index).getMediaPath().get(posts.get(index).getCurrentIndex()))
                .placeholder(R.drawable.placeholder)
                .into(holder.clothesImg);
        holder.progressBar.setVisibility(View.INVISIBLE);
    }
    @Override
    public int getItemCount() {
        return posts.size();
    }
}