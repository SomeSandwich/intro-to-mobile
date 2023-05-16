package project.example.efriendly.adapter;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CompoundButton;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;

import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.LoginActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Notifications.NotificationsRes;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.CustomNotificationItemsBinding;
import project.example.efriendly.holder.CartHolder;
import project.example.efriendly.holder.NotificationsHolder;
import project.example.efriendly.services.PostService;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;


public class NotificationsAdapter extends RecyclerView.Adapter<NotificationsHolder> implements DatabaseConnection {
    Context context;
    int size= 0;
    UserService userService;
    PostService postService;

    public NotificationsAdapter( Context context) {
        this.context = context;
    }

    @NonNull
    @Override
    public NotificationsHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        Context context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        CustomNotificationItemsBinding binding = CustomNotificationItemsBinding.inflate(inflater, parent, false);
        NotificationsHolder notificationsHolder = new NotificationsHolder(binding);
        return notificationsHolder;
    }

    @Override
    public void onBindViewHolder(@NonNull NotificationsHolder holder, int position) {
        final int index = holder.getAdapterPosition();
        postService = RetrofitClientGenerator.getService(PostService.class);
        userService = RetrofitClientGenerator.getService(UserService.class);

        Call<UserRes> userResCall = userService.GetSelf();
        userResCall.enqueue(new Callback<UserRes>() {
            @Override
            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                if (response.isSuccessful()) {
                    UserRes userRes = response.body();
                    postService.GetBySeller(userRes.getId()).enqueue(new Callback<List<PostRes>>() {
                        @Override
                        public void onResponse(Call<List<PostRes>> call, Response<List<PostRes>> response) {
                            if(response.isSuccessful() && response.body() != null){
                                List<PostRes> postList = response.body();
                                size = postList.size();
                                for (int i = 0; i < postList.size(); i++) {
                                    if(postList.get(i).getSold()) {
                                        Glide.with(context)
                                                .load(IMAGE_URL + postList.get(i).getMediaPath().get(0)).placeholder(R.drawable.placeholder)
                                                .into(holder.avatar);
                                        holder.txtNotification.setText(postList.get(i).getCaption() + " has been sold");
                                        holder.txtTime.setText("1 minute ago");
                                    }
                                }
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
    }

    @Override
    public int getItemCount() {
        return size;
    }
}
