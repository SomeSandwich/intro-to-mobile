package project.example.efriendly.adapter;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.squareup.picasso.Picasso;

import java.util.ArrayList;

import project.example.efriendly.R;
import project.example.efriendly.activities.MessageActivity;
import project.example.efriendly.activities.Messages;
import project.example.efriendly.activities.userFragments.ChatActivity;
import project.example.efriendly.data.model.User.UserRes;

public class MessagesAdapter extends RecyclerView.Adapter {
    Context context;
    ArrayList<Messages> messagesArrayList;
    UserRes userRes;

    int item_send = 1;
    int item_receive = 2;

    public MessagesAdapter(Context context, ArrayList<Messages> messagesArrayList) {
        this.context = context;
        this.messagesArrayList = messagesArrayList;
    }

    @NonNull
    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        if(viewType == item_send) {
            View view = LayoutInflater.from(context).inflate(R.layout.senderchatlayout, parent, false);
            return new SenderViewHolder(view);
        } else {
            View view = LayoutInflater.from(context).inflate(R.layout.recieverchatlayout, parent, false);
            return new ReceiverViewHolder(view);
        }
    }

    @Override
    public void onBindViewHolder(@NonNull RecyclerView.ViewHolder holder, int position) {
        Messages messages = messagesArrayList.get(position);
        if(holder.getClass() == SenderViewHolder.class) {
            SenderViewHolder viewHolder = (SenderViewHolder) holder;
            viewHolder.textViewMessage.setText(messages.getMessage());
            viewHolder.timeOfMessage.setText(messages.getCurrentTime());
        } else {
            ReceiverViewHolder viewHolder = (ReceiverViewHolder) holder;
            viewHolder.textViewMessage.setText(messages.getMessage());
            viewHolder.timeOfMessage.setText(messages.getCurrentTime());
        }
    }

    @Override
    public int getItemViewType(int position) {
        Messages messages = messagesArrayList.get(position);
        if(userRes.getId().equals(messages.getSenderId())) {
            return item_send;
        } else {
            return item_receive;
        }
    }

    @Override
    public int getItemCount() {
        return messagesArrayList.size();
    }

    public class SenderViewHolder extends RecyclerView.ViewHolder {
        TextView textViewMessage;
        TextView timeOfMessage;
        public SenderViewHolder(@NonNull View itemView) {
            super(itemView);
            textViewMessage = itemView.findViewById(R.id.sendermessage);
            timeOfMessage = itemView.findViewById(R.id.timeofmessage);
        }
    }

    public class ReceiverViewHolder extends RecyclerView.ViewHolder {
        TextView textViewMessage;
        TextView timeOfMessage;
        public ReceiverViewHolder(@NonNull View itemView) {
            super(itemView);
            textViewMessage = itemView.findViewById(R.id.sendermessage);
            timeOfMessage = itemView.findViewById(R.id.timeofmessage);
        }
    }
}