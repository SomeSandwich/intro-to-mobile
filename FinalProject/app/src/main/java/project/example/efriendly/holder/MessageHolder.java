package project.example.efriendly.holder;

import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import project.example.efriendly.databinding.RecieverchatlayoutBinding;
import project.example.efriendly.databinding.SenderchatlayoutBinding;

public class MessageHolder extends RecyclerView.ViewHolder {
    public TextView message;
    public TextView time;
    public MessageHolder(@NonNull SenderchatlayoutBinding binding) {
        super(binding.getRoot());
        message = binding.senderMessage;
    }
    public MessageHolder(@NonNull RecieverchatlayoutBinding binding){
        super(binding.getRoot());
        message = binding.receiverMessage;
    }
}
