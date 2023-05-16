package project.example.efriendly.activities.userFragments;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.StaggeredGridLayoutManager;

import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import java.util.List;

import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.AnonymousHomepageAdapter;
import project.example.efriendly.adapter.EditPostAdapter;
import project.example.efriendly.adapter.NewfeelAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.SuccessRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityEditPostBinding;
import project.example.efriendly.databinding.ActivityPaymentBinding;
import project.example.efriendly.services.PostService;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class EditPostActivity extends Fragment implements DatabaseConnection {

    UserActivity main;
    Context context = null;
    private ActivityEditPostBinding binding;
    List<PostRes> posts;
    UserService userService;
    PostService postService;
    EditPostAdapter adapter;

    public EditPostActivity() {}

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
        binding = ActivityEditPostBinding.inflate(inflater, container,false);
        binding.setClickHandler(new EditPostActivity.ClickHandler(context));
        userService = RetrofitClientGenerator.getService(UserService.class);
        postService = RetrofitClientGenerator.getService(PostService.class);
        userService.GetSelf().enqueue(new Callback<UserRes>() {
            @Override
            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                if (response.isSuccessful()){
                    postService.GetBySeller(response.body().getId()).enqueue(new Callback<List<PostRes>>() {
                        @Override
                        public void onResponse(Call<List<PostRes>> call, Response<List<PostRes>> response) {
                            if (response.isSuccessful()) {
                                posts = response.body();
                                adapter = new EditPostAdapter(posts, new ClickHandler(context), main);
                                binding.posts.setAdapter(adapter);
                                binding.posts.setLayoutManager(new LinearLayoutManager(main));
                            }
                        }
                        @Override
                        public void onFailure(Call<List<PostRes>> call, Throwable t) {
                            Toast.makeText(context, "Can't connect to server", Toast.LENGTH_SHORT).show();
                        }
                    });
                }
            }
            @Override
            public void onFailure(Call<UserRes> call, Throwable t) {
                Toast.makeText(context, "Can't connect to server", Toast.LENGTH_SHORT).show();
            }
        });
        return binding.getRoot();
    }

    public class ClickHandler {
        Context context;
        public ClickHandler(Context context) {this.context = context;}

        public void DeleteClick(int position){
            if (posts.size() == 0) return;
            postService.Delete(posts.get(position).getId()).enqueue(new Callback<SuccessRes>() {
                @Override
                public void onResponse(Call<SuccessRes> call, Response<SuccessRes> response) {
                    if (response.isSuccessful()){
                        Toast.makeText(context, response.body().getMessage(), Toast.LENGTH_SHORT).show();
                        posts.remove(position);
                        adapter.notifyItemRemoved(position);
                    }
                }
                @Override
                public void onFailure(Call<SuccessRes> call, Throwable t) {
                    Toast.makeText(context, "Can't connect to server", Toast.LENGTH_SHORT).show();
                }
            });


        }

    }
}