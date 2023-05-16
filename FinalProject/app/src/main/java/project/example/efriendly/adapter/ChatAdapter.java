package project.example.efriendly.adapter;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.squareup.picasso.Picasso;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.MessageActivity;
import project.example.efriendly.activities.userFragments.ChatActivity;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Conversation.ConversationRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityChatBinding;
import project.example.efriendly.databinding.CustomChatItemsBinding;
import project.example.efriendly.holder.ChatHolder;
import project.example.efriendly.services.ConversationService;
import retrofit2.Response;

public class ChatAdapter extends RecyclerView.Adapter<ChatHolder> implements DatabaseConnection {
    ChatActivity chatActivity;
    List<UserRes> userArrayList;

    ConversationService conversationService;

    Context context;

    public ChatAdapter(ChatActivity chatActivity, List<UserRes> usersArrayList) {
        this.context = chatActivity.getApplicationContext();
        this.chatActivity = chatActivity;
        this.userArrayList = usersArrayList;
    }

    @NonNull
    @Override
    public ChatHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);

        CustomChatItemsBinding binding = CustomChatItemsBinding.inflate(inflater, parent, false);
        ChatHolder chatHolder = new ChatHolder(binding);
        return chatHolder;
    }

    @Override
    public void onBindViewHolder(@NonNull ChatHolder holder, int position) {
        final int index = holder.getAdapterPosition();
        UserRes user = userArrayList.get(index);

        holder.name.setText(user.getName());
        if (user.getAvatarPath() != null) {
            Glide.with(context)
                    .load(IMAGE_URL + user.getAvatarPath())
                    .placeholder(R.drawable.placeholder)
                    .into(holder.avatar);
        }
        else {holder.avatar.setImageResource(R.drawable.user);}

        holder.view.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                try {
                    Response<List<ConversationRes>> conversations = conversationService.GetBySelf().execute();
                    if(conversations.isSuccessful() && conversations.body() != null){
                        Intent intent = new Intent(chatActivity, MessageActivity.class);
                        intent.putExtra("conversation_id", conversations.body().get(index).getId());
                        chatActivity.startActivity(intent);
                    }
                    else {
                        Toast.makeText(context, "Cant' download conversation list", Toast.LENGTH_LONG).show();
                    }
                }
                catch (IOException e) {
                    Toast.makeText(context, "Can't connect to server", Toast.LENGTH_LONG).show();
                }
            }
        });
    }
    @Override
    public int getItemCount() {
        return userArrayList.size();
    }
}