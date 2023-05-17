package project.example.efriendly.adapter;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.time.temporal.ChronoUnit;
import java.util.List;

import project.example.efriendly.data.model.Message.MessageRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.RecieverchatlayoutBinding;
import project.example.efriendly.databinding.SenderchatlayoutBinding;
import project.example.efriendly.holder.MessageHolder;

public class MessagesAdapter extends RecyclerView.Adapter<MessageHolder> {

    Context context;

    UserRes sender;

    UserRes receiver;

    public List<MessageRes> messages;

    public MessagesAdapter(Context context, UserRes sender, UserRes receiver, List<MessageRes> messages) {
        this.context = context;
        this.sender = sender;
        this.receiver = receiver;
        this.messages = messages;
    }

    @NonNull
    @Override
    public MessageHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        if (viewType == 1){
            RecieverchatlayoutBinding binding = RecieverchatlayoutBinding.inflate(inflater, parent, false);
            return new MessageHolder(binding);
        }
        SenderchatlayoutBinding binding = SenderchatlayoutBinding.inflate(inflater, parent, false);
        return new MessageHolder(binding);
    }

    @Override
    public void onBindViewHolder(@NonNull MessageHolder holder, int position) {
        final int index = holder.getAdapterPosition();
        holder.message.setText(messages.get(index).getContent());


        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
        LocalDateTime updatePost = LocalDateTime.parse(messages.get(index).getCreateAt().substring(0,19), formatter);
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

        holder.time.setText(time);
    }

    @Override
    public int getItemViewType(int position) {
        if (messages.get(position).getUserId().equals(receiver.getId())) return 1;
        return 0;
    }

    @Override
    public int getItemCount() {
        return messages.size();
    }
}