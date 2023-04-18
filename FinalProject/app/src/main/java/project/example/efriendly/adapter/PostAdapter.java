package project.example.efriendly.adapter;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.Toast;

import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;
import java.util.Vector;

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

        binding.sellerName.setText(post.getAuthor().getName());
        binding.des.setText(post.getDescription());

        Context main = viewGroup.getContext();

        handler = new PostNewFeelClickHandler(main, post, binding);

        binding.clothesImgs.setImageBitmap(post.getImgBitmap().get(0));

        binding.setClickHandler(handler);

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
