package project.example.efriendly.adapter;


import android.app.Activity;
import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.time.temporal.ChronoUnit;
import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.ShowPostNewfeelAdapterBinding;

public class PostAdapter extends BaseAdapter {
    private Activity activity;
    private List<PostRes> posts = new ArrayList<>();

    private ShowPostNewfeelAdapterBinding binding;

    private PostNewFeelClickHandler handler;

    public PostAdapter(Activity activity, List<PostRes> posts){
        this.activity = activity;
        this.posts = posts;
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
        handler = new PostNewFeelClickHandler(viewGroup.getContext(), post, binding);

        try {
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
            if (post.getSellerAvt() != null) binding.sellerAvt.setImageBitmap(post.getSellerAvt());
            else binding.sellerAvt.setImageResource(R.drawable.user);

            binding.likeCount.setText(String.valueOf(10));

            binding.CommentsCount.setText(String.valueOf(30) + " comments");

            binding.shareCount.setText(String.valueOf(post.getUserShare().size()) + " shares");



            binding.processBar.setVisibility(View.INVISIBLE); binding.post.setVisibility(View.VISIBLE);
            binding.sellerName.setText(post.getUser().getName());
            binding.des.setText(post.getDescription());
            binding.clothesImgs.setImageBitmap(post.getImgBitmap().get(0));
            binding.setClickHandler(handler);
        }catch (NullPointerException err){binding.processBar.setVisibility(View.VISIBLE); binding.post.setVisibility(View.INVISIBLE);}

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

            Log.d("Debug", post.getImgBitmap().get(post.getCurrentIndex()).toString());
            binding.clothesImgs.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
        }
        public void previousClick(View view){
            post.setCurrentIndex(post.getCurrentIndex()-1);
            if (post.getCurrentIndex() < 0) post.setCurrentIndex(post.getMediaPath().size() - 1);
            Log.d("Debug", post.getImgBitmap().get(post.getCurrentIndex()).toString());
            binding.clothesImgs.setImageBitmap(post.getImgBitmap().get(post.getCurrentIndex()));
        }
    }

}
