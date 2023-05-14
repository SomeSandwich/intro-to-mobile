package project.example.efriendly.activities.userFragments;

import androidx.fragment.app.Fragment;

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

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityShowPostBinding;
import project.example.efriendly.services.CartService;
import project.example.efriendly.services.UserService;
import project.example.efriendly.ultilities.OnSwipeTouchListener;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ShowPost extends Fragment implements DatabaseConnection {
    ActivityShowPostBinding binding;
    Context context = null;
    UserService userService;

    CartService cartService;
    PostRes post;
    int currentIndex=0;
    ShowPostClickHandler handlers;
    UserActivity main;

    public ShowPost(PostRes post){
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
        handlers = new ShowPost.ShowPostClickHandler(context);
        binding.setClickHandler(handlers);
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
        show.setOnTouchListener(new OnSwipeTouchListener(context) {
            public void onSwipeRight() {
                show.setInAnimation(context,R.anim.from_left);
                show.setOutAnimation(context,R.anim.to_right);
                currentIndex++;
                if(currentIndex == (post.getImgBitmap().size()))
                    currentIndex=0;

                Drawable drawable = new BitmapDrawable(getResources(), post.getImgBitmap().get(currentIndex));
                show.setImageDrawable(drawable);
            }
            public void onSwipeLeft() {
                show.setInAnimation(context,R.anim.from_right);
                show.setOutAnimation(context,R.anim.to_left);
                --currentIndex;
                if(currentIndex<0)
                    currentIndex=post.getImgBitmap().size() - 1;

                Drawable drawable = new BitmapDrawable(getResources(), post.getImgBitmap().get(currentIndex));
                show.setImageDrawable(drawable);
            }
        });
        userService = RetrofitClientGenerator.getService(UserService.class);
        cartService = RetrofitClientGenerator.getService(CartService.class);

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

                    if (post.getUser().getAvatar() == null) binding.sellerAvt.setImageResource(R.drawable.user);
                    else{
                        try{
                            InputStream newUrl = new URL(IMAGE_URL + post.getUser().getAvatar()).openStream();
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
                    Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<UserRes> call, Throwable t) {
                String message = t.getLocalizedMessage();
                Toast.makeText(context, message, Toast.LENGTH_LONG).show();
            }
        });

        return binding.getRoot();
    }

    public class ShowPostClickHandler{
        Context context;
        public ShowPostClickHandler(Context context) {
            this.context = context;
        }
        public void addToCartClick(View view){
            int postId = post.getId();
            Call<UserRes> userResCall = userService.GetSelf();
            userResCall.enqueue(new Callback<UserRes>() {
                @Override
                public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                    if (response.isSuccessful()) {
                        cartService.addToCart(response.body().getId(), post.getId()).enqueue(new Callback<String>() {
                            @Override
                            public void onResponse(Call<String> call, Response<String> response) {
                                if (response.isSuccessful()){
                                    Toast.makeText(context, response.body(), Toast.LENGTH_LONG).show();
                                    main.onMsgFromFragToMain("showPost", "close");
                                }
                                else{
                                    String message = "An error occurred please try again later ...";
                                    Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                                }
                            }
                            @Override
                            public void onFailure(Call<String> call, Throwable t) {
                                String message = t.getLocalizedMessage();
                                Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                            }
                        });
                    }
                    else {
                        String message = "An error occurred please try again later ...";
                        Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                    }
                }

                @Override
                public void onFailure(Call<UserRes> call, Throwable t) {
                    String message = t.getLocalizedMessage();
                    Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                }
            });
        }
    }
}