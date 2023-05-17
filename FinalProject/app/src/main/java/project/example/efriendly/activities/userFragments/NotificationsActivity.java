package project.example.efriendly.activities.userFragments;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.CartAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityNotificationsBinding;
import project.example.efriendly.adapter.NotificationsAdapter;
import project.example.efriendly.services.PostService;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class NotificationsActivity extends Fragment {
    UserActivity main;
    Context context = null;
    UserService userService;
    PostService postService;
    private ActivityNotificationsBinding binding;
    private NotificationsAdapter adapter;

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
        binding = ActivityNotificationsBinding.inflate(inflater, container,false);

        postService = RetrofitClientGenerator.getService(PostService.class);
        userService = RetrofitClientGenerator.getService(UserService.class);
        userService.GetSelf().enqueue(new Callback<UserRes>() {
            @Override
            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                if (response.isSuccessful()) {
                    UserRes userRes = response.body();
                    postService.GetBySeller(userRes.getId()).enqueue(new Callback<List<PostRes>>() {
                        @Override
                        public void onResponse(Call<List<PostRes>> call, Response<List<PostRes>> response) {
                            if(response.isSuccessful() && response.body() != null){
                                List<PostRes> postList = response.body();
                                List<PostRes> notiList = new ArrayList<>();
                                for (int i = 0; i < postList.size(); i++) {
                                    if(postList.get(i).getSold()) {
                                        notiList.add(postList.get(i));
                                    }
                                }
                                adapter = new NotificationsAdapter(main, notiList);
                                binding.NotificationList.setAdapter(adapter);
                                binding.NotificationList.setLayoutManager(new LinearLayoutManager(main));
                            }
                            else{
                                String message = "An error occurred please try again later ...";
                                Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                            }
                        }
                        @Override
                        public void onFailure(Call<List<PostRes>> call, Throwable t) {
                            String message = t.getLocalizedMessage();
                            Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                        }
                    });
                } else {
                    String message = "An error occurred ...";
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
}