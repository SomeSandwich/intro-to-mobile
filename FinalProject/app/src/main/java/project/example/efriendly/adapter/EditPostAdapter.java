package project.example.efriendly.adapter;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;

import java.util.Collections;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.userFragments.EditPostActivity;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.ItemPostBinding;
import project.example.efriendly.holder.EditPostHolder;

public class EditPostAdapter extends RecyclerView.Adapter<EditPostHolder> implements DatabaseConnection {

    List<PostRes> posts = Collections.emptyList();
    Context context;
    EditPostActivity.ClickHandler listener;

    public EditPostAdapter(List<PostRes> posts, EditPostActivity.ClickHandler listener, Context context) {
        this.context = context;
        this.posts = posts;
        this.listener = listener;
    }

    @NonNull
    @Override
    public EditPostHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        Context context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        ItemPostBinding binding = ItemPostBinding.inflate(inflater, parent, false);
        EditPostHolder editPostHolder = new EditPostHolder(binding);

        return editPostHolder;
    }

    @Override
    public void onBindViewHolder(@NonNull EditPostHolder holder, int position) {
        final int index = holder.getAdapterPosition();

        holder.caption.setText(posts.get(index).getCaption());
        holder.prices.setText(posts.get(index).getPrice() + "đ");
        Glide.with(context)
                .load(IMAGE_URL + posts.get(index).getMediaPath().get(0))
                .placeholder(R.drawable.placeholder)
                .into(holder.clothImg);
        if (posts.get(index).getSold()) holder.sold.setText("Sold");

        holder.delete.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                listener.DeleteClick(index);
            }
        });

    }

    @Override
    public int getItemCount() {
        return posts.size();
    }
}
