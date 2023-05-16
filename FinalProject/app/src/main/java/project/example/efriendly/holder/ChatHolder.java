package project.example.efriendly.holder;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import project.example.efriendly.databinding.CustomChatItemsBinding;

public class ChatHolder extends RecyclerView.ViewHolder {
    public ImageView avatar;
    public TextView name;
    public View view;
    public ChatHolder(@NonNull CustomChatItemsBinding binding) {
        super(binding.getRoot());
        name = binding.txtName;
        avatar = binding.ivAvatar;

        view = binding.getRoot();
    }
}
