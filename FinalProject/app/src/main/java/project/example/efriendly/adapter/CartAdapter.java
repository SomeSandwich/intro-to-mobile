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
import project.example.efriendly.activities.userFragments.CartActivity;
import project.example.efriendly.activities.userFragments.ChatActivity;
import project.example.efriendly.data.model.User.UserRes;

public class CartAdapter extends RecyclerView.Adapter<CartAdapter.viewHolder> {
    CartActivity cartActivity;
    ArrayList<UserRes> userArrayList;

    public CartAdapter(CartActivity cartActivity, ArrayList<UserRes> usersArrayList) {
        this.cartActivity = cartActivity;
        this.userArrayList = usersArrayList;
    }

    @NonNull
    @Override
    public CartAdapter.viewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(cartActivity).inflate(R.layout.custom_chat_items, parent, false);
        return new viewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull CartAdapter.viewHolder holder, int position) {
        UserRes user = userArrayList.get(position);

        holder.name.setText(user.getName());
        Picasso.get().load(user.getAvatar()).into(holder.avatar);

        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(cartActivity, MessageActivity.class);
                intent.putExtra("id", user.getId());
                intent.putExtra("name", user.getName());
                intent.putExtra("avatar", user.getAvatar());
                cartActivity.startActivity(intent);
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