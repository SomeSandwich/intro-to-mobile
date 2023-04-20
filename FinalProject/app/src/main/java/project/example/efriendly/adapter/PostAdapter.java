package project.example.efriendly.adapter;


import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Handler;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Toast;

import java.net.URL;
import java.net.URLConnection;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.time.temporal.ChronoUnit;
import java.util.ArrayList;
import java.util.List;
import java.util.Vector;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import project.example.efriendly.R;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ShowPostNewfeelAdapterBinding;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PostAdapter extends BaseAdapter implements DatabaseConnection {
    private Activity activity;
    private List<PostRes> posts = new ArrayList<>();

    private ShowPostNewfeelAdapterBinding binding;

    private PostNewFeelClickHandler handler;

    private UserService userService;

    public PostAdapter(Activity activity, List<PostRes> posts, UserService userService){
        this.activity = activity;
        this.posts = posts;
        this.userService = userService;
    }

    @Override
    public int getCount() {
        return posts.size();
    }

    @Override
    public Object getItem(int i) {
        return posts.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        LayoutInflater inflater = activity.getLayoutInflater();
        binding = ShowPostNewfeelAdapterBinding.inflate(inflater, viewGroup,false);
        PostRes post = posts.get(i);
        post.setImgBitmap(new Vector<Bitmap>(post.getMediaPath().size()));
        handler = new PostNewFeelClickHandler(viewGroup.getContext(), post, binding);
        binding.setClickHandler(handler);

        binding.processBar.setVisibility(View.VISIBLE);
        binding.post.setVisibility(View.INVISIBLE);

        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
        LocalDateTime updatePost = LocalDateTime.parse(post.getUpdatedDate().substring(0,19), formatter);
        LocalDateTime now = LocalDateTime.now();

        long minutes = ChronoUnit.MINUTES.between(updatePost, now);
        long hour = ChronoUnit.HOURS.between(updatePost, now);
        long day = ChronoUnit.DAYS.between(updatePost, now);

        String time = String.valueOf(minutes) + "m";

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
        binding.time.setText(time);
        binding.likeCount.setText(String.valueOf(10));
        binding.CommentsCount.setText(String.valueOf(30) + " comments");
        binding.shareCount.setText(String.valueOf(post.getUserShare().size()) + " shares");
        binding.des.setText(post.getDescription());

        try {
            binding.sellerName.setText(post.getUser().getName());
            if (post.getUser().getAvatarPath() != null) binding.sellerAvt.setImageBitmap(post.getUser().getAvtBitmap());
            else binding.sellerAvt.setImageResource(R.drawable.user);
            binding.post.setVisibility(View.VISIBLE);
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
                                URL newUrl = new URL(IMAGE_URL + post.getUser().getAvatarPath());
                                URLConnection conn = newUrl.openConnection();
                                Bitmap mIcon_val = BitmapFactory.decodeStream(conn.getInputStream());
                                if (mIcon_val != null) post.getUser().setAvtBitmap(mIcon_val);
                            }
                            catch (Exception e) {}
                        }

                        binding.sellerName.setText(post.getUser().getName());
                        if (post.getUser().getAvatarPath() != null) binding.sellerAvt.setImageBitmap(post.getUser().getAvtBitmap());
                        else binding.sellerAvt.setImageResource(R.drawable.user);
                        binding.post.setVisibility(View.VISIBLE);

                    } else {
                        String message = "An error occurred please try again later ...";
                        Toast.makeText(viewGroup.getContext(), message, Toast.LENGTH_LONG).show();
                    }
                }
                @Override
                public void onFailure(Call<UserRes> call, Throwable t) {
                    String message = t.getLocalizedMessage();;
                    Toast.makeText(viewGroup.getContext(), message, Toast.LENGTH_LONG).show();
                }
            });
        }
        binding.processBar.setVisibility(View.VISIBLE);

        /*try {
            binding.clothesImgs.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
            binding.processBar.setVisibility(View.INVISIBLE);
        }
        catch (IndexOutOfBoundsException err){
            Thread get = new Thread(new Runnable() {
                @Override
                public void run() {
                    try {
                        URL newUrl = new URL(IMAGE_URL + post.getMediaPath().get(post.getCurrentIndex()));
                        URLConnection conn = newUrl.openConnection();
                        Bitmap mIcon_val = BitmapFactory.decodeStream(conn.getInputStream());
                        if (mIcon_val != null) post.getImgBitmap().add(mIcon_val);
                    }
                    catch (Exception e) {
                        Log.d("SetImageViewDebug", e.getMessage());
                    }
                }
            });
            Thread set = new Thread(new Runnable() {
                @Override
                public void run() {
                    binding.clothesImgs.setImageBitmap(post.getImgBitmap().get(0));
                    binding.processBar.setVisibility(View.INVISIBLE);
                }
            });
            ExecutorService pool = Executors.newFixedThreadPool(2);
            try {
                pool.submit(get).get();
                pool.submit(set).get();
            } catch (Exception e) {Log.d("Get name", e.getMessage());}
            pool.shutdown();
        }*/
        return binding.getRoot();
    }

    public class PostNewFeelClickHandler{
        Context context;
        ShowPostNewfeelAdapterBinding binding;
        PostRes post;
        public PostNewFeelClickHandler(Context context, PostRes post, ShowPostNewfeelAdapterBinding binding){
            this.context = context;
            this.post = post;
            this.binding = binding;
        }
        public void nextPicClick(View view){
            post.setCurrentIndex(post.getCurrentIndex()+1);
            if (post.getCurrentIndex()>= post.getMediaPath().size()) post.setCurrentIndex(0);
            binding.processBar.setVisibility(View.VISIBLE);
            try{
                binding.clothesImgs.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
                binding.processBar.setVisibility(View.INVISIBLE);
            }
            catch (ArrayIndexOutOfBoundsException err){
                Thread get = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            URL newUrl = new URL(IMAGE_URL + post.getMediaPath().get(post.getCurrentIndex()));
                            URLConnection conn = newUrl.openConnection();
                            Bitmap mIcon_val = BitmapFactory.decodeStream(conn.getInputStream());
                            if (mIcon_val != null) {
                                post.getImgBitmap().add(mIcon_val);
                            }
                        } catch (Exception err) {
                            Log.d("NextButtonDebug", err.getMessage());
                        }
                    }
                });
                Thread set = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        binding.clothesImgs.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
                        binding.processBar.setVisibility(View.INVISIBLE);
                    }
                });

                ExecutorService pool = Executors.newFixedThreadPool(2);
                try {
                    pool.submit(get).get();
                    pool.submit(set).get();
                } catch (Exception e) {Log.d("NextButton", e.getMessage());}
                pool.shutdown();
            }
        }
        public void previousClick(View view){
            post.setCurrentIndex(post.getCurrentIndex()-1);
            if (post.getCurrentIndex() < 0) post.setCurrentIndex(post.getMediaPath().size() - 1);
            binding.processBar.setVisibility(View.VISIBLE);

            try{
                binding.clothesImgs.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
                binding.processBar.setVisibility(View.INVISIBLE);
            }
            catch (ArrayIndexOutOfBoundsException err) {
                Thread get = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            URL newUrl = new URL(IMAGE_URL + post.getMediaPath().get(post.getCurrentIndex()));
                            URLConnection conn = newUrl.openConnection();
                            Bitmap mIcon_val = BitmapFactory.decodeStream(conn.getInputStream());
                            if (mIcon_val != null) {
                                post.getImgBitmap().add(mIcon_val);
                            }
                        } catch (Exception err) {
                            Log.d("NextButtonDebug", err.getMessage());
                        }
                    }
                });
                Thread set = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        binding.clothesImgs.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
                        binding.processBar.setVisibility(View.INVISIBLE);
                    }
                });
                ExecutorService pool = Executors.newFixedThreadPool(2);
                try {
                    pool.submit(get).get();
                    pool.submit(set).get();
                } catch (Exception e) {Log.d("PreviousButton", e.getMessage());}
                pool.shutdown();
            }
        }
    }
}
