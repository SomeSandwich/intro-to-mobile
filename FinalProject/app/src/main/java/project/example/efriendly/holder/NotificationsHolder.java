package project.example.efriendly.holder;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;
import project.example.efriendly.databinding.CustomNotificationItemsBinding;

public class NotificationsHolder extends RecyclerView.ViewHolder {
    public View view;
    public ImageView avatar;
    public TextView txtNotification;
    public TextView txtTime;
    public NotificationsHolder(CustomNotificationItemsBinding binding){
        super(binding.getRoot());
        this.avatar = binding.ivAvatar;
        this.txtNotification = binding.txtNotification;
        this.txtTime = binding.txtTime;

        this.view = binding.getRoot();
    }
}
