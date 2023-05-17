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

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.time.temporal.ChronoUnit;
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

    List<PostRes> postList;

    public NotificationsAdapter(Context context, List<PostRes> postList) {
        this.context = context;
        this.postList = postList;
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
        Glide.with(context)
                .load(IMAGE_URL + postList.get(index).getMediaPath().get(0)).placeholder(R.drawable.placeholder)
                .into(holder.avatar);
        holder.txtNotification.setText(postList.get(index).getCaption() + " has been sold");

        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
        LocalDateTime updatePost = LocalDateTime.parse(postList.get(index).getUpdatedDate().substring(0,19), formatter);
        LocalDateTime now = LocalDateTime.now();

        long minutes = ChronoUnit.MINUTES.between(updatePost, now);
        long hour = ChronoUnit.HOURS.between(updatePost, now);
        long day = ChronoUnit.DAYS.between(updatePost, now);

        String time = String.valueOf(minutes) + " minutes";
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

        holder.txtTime.setText(time + " ago");
    }


    @Override
    public int getItemCount() {
        return postList.size();
    }
}
