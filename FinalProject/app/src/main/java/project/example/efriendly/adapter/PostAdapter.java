package project.example.efriendly.adapter;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.FrameLayout;
import android.widget.ImageSwitcher;
import android.widget.ImageView;
import android.widget.ViewSwitcher;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Vector;

import project.example.efriendly.R;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.ShowPostNewfeelAdapterBinding;
import project.example.efriendly.ultilities.OnSwipeTouchListener;


public class PostAdapter extends BaseAdapter {
    private Activity activity;
    private Vector<PostRes> posts = new Vector<>();

    int count=0;
    int currentIndex=0;

    private ShowPostNewfeelAdapterBinding binding;

    public PostAdapter(Activity activity, Vector<PostRes> posts){
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
        count = post.getMediaPath().size();
        binding.sellerName.setText(post.getAuthor().getName());
        binding.des.setText(post.getDescription());


        ImageSwitcher show = binding.clothesImgs;
        show.setFactory(new ViewSwitcher.ViewFactory() {
            @Override
            public View makeView() {
                ImageView imageView= new ImageView(viewGroup.getContext());
                imageView.setScaleType(ImageView.ScaleType.FIT_CENTER);
                imageView.setLayoutParams(new FrameLayout.LayoutParams(
                        FrameLayout.LayoutParams.MATCH_PARENT
                        ,FrameLayout.LayoutParams.MATCH_PARENT
                ));
                return imageView;
            }
        });

        try {
            URL url = new URL(post.getMediaPath().get(0));
            Bitmap bmp = BitmapFactory.decodeStream(url.openConnection().getInputStream());
            Drawable drawable =new BitmapDrawable(bmp);
            show.setImageDrawable(drawable);
        }
        catch (MalformedURLException e){
            Log.d("Debug", e.toString());
        }
        catch (IOException err){
            Log.d("Debug", err.toString());
        }
        Context main = viewGroup.getContext();
        show.setOnTouchListener(new OnSwipeTouchListener(main) {
            public void onSwipeRight() {
                show.setInAnimation(main, R.anim.from_left);
                show.setOutAnimation(main,R.anim.to_right);
                currentIndex++;
                // condition
                if(currentIndex==count)
                    currentIndex=0;

                try {
                    URL url = new URL(post.getMediaPath().get(currentIndex));
                    Bitmap bmp = BitmapFactory.decodeStream(url.openConnection().getInputStream());
                    Drawable drawable =new BitmapDrawable(bmp);
                    show.setImageDrawable(drawable);
                }
                catch (MalformedURLException e){
                    Log.d("Debug", e.toString());
                }
                catch (IOException err){
                    Log.d("Debug", err.toString());
                }
            }
            public void onSwipeLeft() {
                show.setInAnimation(main,R.anim.from_right);
                show.setOutAnimation(main,R.anim.to_left);
                --currentIndex;
                // condition
                if(currentIndex<0)
                    currentIndex=post.getMediaPath().size()-1;
                try {
                    URL url = new URL(post.getMediaPath().get(currentIndex));
                    Bitmap bmp = BitmapFactory.decodeStream(url.openConnection().getInputStream());
                    Drawable drawable =new BitmapDrawable(bmp);
                    show.setImageDrawable(drawable);
                }
                catch (MalformedURLException e){
                    Log.d("Debug", e.toString());
                }
                catch (IOException err){
                    Log.d("Debug", err.toString());
                }
            }
        });
        return binding.getRoot();
    }

}
