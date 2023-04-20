package project.example.efriendly.activities.userFragments;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.os.Handler;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;
import java.util.Vector;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import project.example.efriendly.R;
import project.example.efriendly.activities.LoginActivity;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.PostAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Auth.LoginRes;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.FragmentNewfeelActivityBinding;
import project.example.efriendly.services.PostService;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class NewfeelActivity extends Fragment implements DatabaseConnection {

    UserActivity main;
    Context context = null;
    private UserService userService;
    FragmentNewfeelActivityBinding binding;

    PostService postService;

    public NewfeelActivity() {}

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

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        binding = FragmentNewfeelActivityBinding.inflate(inflater, container,false);
        SearchBarCartChatActivity searchbar = new SearchBarCartChatActivity();
        getParentFragmentManager().beginTransaction().replace(R.id.searchBarFragment, searchbar).commit();

        postService = RetrofitClientGenerator.getService(PostService.class);
        userService = RetrofitClientGenerator.getService(UserService.class);

        Call<List<PostRes>> postsCallback = postService.GetNewest(10);
        Thread get = new Thread(new Runnable() {
            @Override
            public void run() {
                postsCallback.enqueue(new Callback<List<PostRes>>() {
                    @Override
                    public void onResponse(Call<List<PostRes>> call, Response<List<PostRes>> response) {
                        if (response.isSuccessful()) {
                            List<PostRes> posts = response.body();
                            binding.processBar.setVisibility(View.INVISIBLE);

                            PostAdapter postAdapter = new PostAdapter(main, posts, userService);
                            binding.newfeelPost.setAdapter(postAdapter);
                        }
                        else {
                            String message = "An error occurred please try again later ...";
                            Toast.makeText(main, message, Toast.LENGTH_LONG).show();
                        }
                    }
                    @Override
                    public void onFailure(Call<List<PostRes>> call, Throwable t) {
                        String message = t.getLocalizedMessage();
                        Toast.makeText(main, message, Toast.LENGTH_LONG).show();
                    }
                });
            }
        });

        Thread set = new Thread(new Runnable() {
            @Override
            public void run() {
                binding.processBar.setVisibility(View.VISIBLE);
            }
        });
        ExecutorService pool = Executors.newFixedThreadPool(2);
        try {
            pool.submit(get).get();
            pool.submit(set).get();
        } catch (Exception e) {Log.d("Get name", e.getMessage());}

        pool.shutdown();
        return binding.getRoot();
    }
}