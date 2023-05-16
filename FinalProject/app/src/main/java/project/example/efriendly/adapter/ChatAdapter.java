package project.example.efriendly.adapter;

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
import project.example.efriendly.activities.userFragments.ChatActivity;
import project.example.efriendly.data.model.User.UserRes;

public class ChatAdapter extends RecyclerView.Adapter<ChatAdapter.viewHolder> {
    ChatActivity chatActivity;
    ArrayList<UserRes> userArrayList;

    public ChatAdapter(ChatActivity chatActivity, ArrayList<UserRes> usersArrayList) {
        this.chatActivity = chatActivity;
        this.userArrayList = usersArrayList;
    }

    @NonNull
    @Override
    public ChatAdapter.viewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(chatActivity).inflate(R.layout.custom_chat_items, parent, false);
        return new viewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ChatAdapter.viewHolder holder, int position) {
        UserRes user = userArrayList.get(position);

        holder.name.setText(user.getName());
        Picasso.get().load(user.getAvatarPath()).into(holder.avatar);

        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(chatActivity, MessageActivity.class);
                intent.putExtra("id", user.getId());
                intent.putExtra("name", user.getName());
                intent.putExtra("avatar", user.getAvatarPath());
                chatActivity.startActivity(intent);
            }
        });
    }

    @Override
    public int getItemCount() {
        return userArrayList.size();
    }

    public class viewHolder extends RecyclerView.ViewHolder {
        ImageView avatar;
        TextView name;
        public viewHolder(@NonNull View itemView) {
            super(itemView);
            avatar = itemView.findViewById(R.id.ivAvatar);
            name = itemView.findViewById(R.id.txtName);
        }
    }
}