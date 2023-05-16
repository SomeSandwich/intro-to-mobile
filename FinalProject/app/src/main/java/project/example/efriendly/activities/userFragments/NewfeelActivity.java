package project.example.efriendly.activities.userFragments;

import android.content.Context;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;


import com.bumptech.glide.Glide;

import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.NewfeelAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
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

    NewfeelClickHandler clickHandler;
    FragmentNewfeelActivityBinding binding;
    PostService postService;
    ClickListener listener;

    public NewfeelActivity() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        try{
            context = getActivity();
            main = (UserActivity) getActivity();
            listener = new ClickListener();
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
        binding.setClickHandler(new NewfeelClickHandler(context));
        userService.GetSelf().enqueue(new Callback<UserRes>() {
            @Override
            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                if (response.body() != null && response.isSuccessful()){
                    if (response.body().getAvatarPath() != null){
                        Glide.with(context)
                                .load(IMAGE_URL + response.body().getAvatarPath())
                                .placeholder(R.drawable.placeholder)
                                .into(binding.userAvt);
                    }
                    else {
                        binding.userAvt.setImageResource(R.drawable.user);
                    }
                }
                else{
                    Toast.makeText(main, "Can't get avatar", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<UserRes> call, Throwable t) {
                Toast.makeText(main, "Can't get avatar", Toast.LENGTH_SHORT).show();
            }
        });

        binding.processBar.setVisibility(View.VISIBLE);

        addPost();
        return binding.getRoot();
    }
    void addPost(){
        Call<List<PostRes>> postsCallback = postService.GetNewest(100);
        postsCallback.enqueue(new Callback<List<PostRes>>() {
            @Override
            public void onResponse(Call<List<PostRes>> call, Response<List<PostRes>> response) {
                if (response.isSuccessful()) {
                    List<PostRes> posts = response.body();

                    for (int i = 0; i<posts.size();i++){
                        if (response.body().get(i).getSold()) {
                            posts.remove(i);
                            i--;
                        }
                    }

                    NewfeelAdapter adapter = new NewfeelAdapter(posts, main, listener);
                    binding.newfeelPost.setAdapter(adapter);
                    binding.newfeelPost.setLayoutManager(new LinearLayoutManager(main));
                    binding.processBar.setVisibility(View.INVISIBLE);
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
    public class ClickListener{
        public void click(PostRes postRes){
            main.onMsgFromFragToMain(postRes);
        };
    }
    public class NewfeelClickHandler{
        Context context;
        public NewfeelClickHandler(Context context){
            this.context = context;
        }
        public void addPostClick(View view){
            main.onMsgFromFragToMain("newFeel", "CreatePost");
        }
    }
}