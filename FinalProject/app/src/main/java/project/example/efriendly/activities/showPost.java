package project.example.efriendly.activities;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import android.widget.ImageSwitcher;
import android.widget.ImageView;
import android.widget.Toast;
import android.widget.ViewSwitcher;

import java.io.InputStream;
import java.net.URL;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.adapter.NewfeelAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityShowPostBinding;
import project.example.efriendly.services.UserService;
import project.example.efriendly.ultilities.OnSwipeTouchListener;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class showPost extends Fragment implements DatabaseConnection {
    ActivityShowPostBinding binding;
    UserActivity main;
    Context context = null;
    UserService userService;
    PostRes post;

    int currentIndex=0;

    public showPost(PostRes post){
        this.post = post;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        try{
            context = getActivity();
            main = (UserActivity) getActivity();
        }
        catch (IllegalStateException err){
            throw new IllegalStateException("MainActivity must implement callbacks");
        }
    }
    @SuppressLint("ClickableViewAccessibility")
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        binding = ActivityShowPostBinding.inflate(inflater, container,false);

        try{
            for (int i=0;i<post.getMediaPath().size();i++){
                InputStream newUrl = new URL(IMAGE_URL + post.getMediaPath().get(i)).openStream();
                Bitmap image = BitmapFactory.decodeStream(newUrl);
                post.getImgBitmap().add(image);
            }
        }
        catch (Exception e){
            Log.d("ShowPostGetImage", e.getMessage());
        }
        currentIndex = 0;

        ImageSwitcher show = binding.clothesImgs;
        show.setFactory(new ViewSwitcher.ViewFactory() {
            @Override
            public View makeView() {
                ImageView imageView= new ImageView(context);
                imageView.setScaleType(ImageView.ScaleType.FIT_CENTER);
                imageView.setLayoutParams(new FrameLayout.LayoutParams(
                        FrameLayout.LayoutParams.MATCH_PARENT
                        ,FrameLayout.LayoutParams.MATCH_PARENT
                ));
                return imageView;
            }
        });

        Drawable drawable = new BitmapDrawable(getResources(), post.getImgBitmap().get(0));
        show.setImageDrawable(drawable);
        show.setOnTouchListener(new OnSwipeTouchListener(main) {
            public void onSwipeRight() {
                show.setInAnimation(main,R.anim.from_left);
                show.setOutAnimation(main,R.anim.to_right);
                currentIndex++;
                if(currentIndex == (post.getImgBitmap().size()))
                    currentIndex=0;

                Drawable drawable = new BitmapDrawable(getResources(), post.getImgBitmap().get(currentIndex));
                show.setImageDrawable(drawable);
            }
            public void onSwipeLeft() {
                show.setInAnimation(main,R.anim.from_right);
                show.setOutAnimation(main,R.anim.to_left);
                --currentIndex;
                if(currentIndex<0)
                    currentIndex=post.getImgBitmap().size() - 1;

                Drawable drawable = new BitmapDrawable(getResources(), post.getImgBitmap().get(currentIndex));
                show.setImageDrawable(drawable);
            }
        });

        userService = RetrofitClientGenerator.getService(UserService.class);

        Call<UserRes> userResCall = userService.GetById(post.getUserID());
        userResCall.enqueue(new Callback<UserRes>() {
            @Override
            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                if (response.isSuccessful()) {
                    UserRes user = response.body();
                    post.setUser(user);


                    binding.ClothesDes.setText(post.getDescription());
                    binding.name.setText(post.getUser().getName());
                    binding.size.setText("M");
                    if (post.getUser().getLegit() != -1) binding.legitPoint.setText(String.valueOf(post.getUser().getLegit()));
                    else binding.legitPoint.setText("0");

                    binding.rating.setOnTouchListener(new View.OnTouchListener() {
                        @Override
                        public boolean onTouch(View view, MotionEvent motionEvent) {
                            if (motionEvent.getAction() == MotionEvent.ACTION_UP) {
                                Log.d("Rating", String.valueOf(binding.rating.getRating()));
                            }
                            return true;
                        }
                    });
                    binding.prices.setText(String.valueOf(post.getPrice() + "VND"));

                    if (post.getUser().getAvatarPath() == null) binding.sellerAvt.setImageResource(R.drawable.user);
                    else{
                        try{
                            InputStream newUrl = new URL(IMAGE_URL + post.getUser().getAvatarPath()).openStream();
                            Bitmap image = BitmapFactory.decodeStream(newUrl);
                            binding.sellerAvt.post(new Runnable() {
                                @Override
                                public void run() {
                                    binding.sellerAvt.setImageBitmap(image);
                                }
                            });
                            post.getUser().setAvtBitmap(image);
                        }
                        catch (Exception e){Log.d("AvatarDownload", e.getMessage());}
                    }

                }
                else {
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(main, message, Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<UserRes> call, Throwable t) {
                String message = t.getLocalizedMessage();
                Toast.makeText(main, message, Toast.LENGTH_LONG).show();
            }
        });

        return binding.getRoot();
    }
}