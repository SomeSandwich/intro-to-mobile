package project.example.efriendly.adapter;


import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;

import java.io.InputStream;
import java.net.URL;
import java.net.URLConnection;
import java.util.Collections;
import java.util.List;
import java.util.Vector;


import project.example.efriendly.R;
import project.example.efriendly.activities.AnonymousHomepageActivity;
import project.example.efriendly.activities.userFragments.HomepageActivity;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.PostBinding;
import project.example.efriendly.holder.PostHolder;

public class AnonymousHomepageAdapter extends RecyclerView.Adapter<PostHolder> implements DatabaseConnection {
    List<PostRes> posts = Collections.emptyList();
    AnonymousHomepageActivity.ClickListener listener;
    Context context;
    public AnonymousHomepageAdapter(List<PostRes> posts, Context context, AnonymousHomepageActivity.ClickListener listener){
        this.posts = posts;
        this.context = context;
        this.listener = listener;
    }
    @NonNull
    @Override
    public PostHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        Context context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        PostBinding binding = PostBinding.inflate(inflater, parent, false);
        PostHolder postHolder = new PostHolder(binding);
        return postHolder;
    }
    @Override
    public void onBindViewHolder(@NonNull PostHolder holder, int position) {
        final int index = holder.getAdapterPosition();
        holder.des.setText(posts.get(index).getCaption());

        holder.view.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                listener.Click(posts.get(index));
            }
        });

        holder.price.setText(String.valueOf(posts.get(index).getPrice() + "VND"));
        holder.progressBar.setVisibility(View.VISIBLE);

        Glide.with(context)
                .load(IMAGE_URL + posts.get(index).getMediaPath().get(0)).placeholder(R.drawable.placeholder)
                .into(holder.image);
        holder.progressBar.setVisibility(View.INVISIBLE);
    }
    @Override
    public int getItemCount() {
        return posts.size();
    }
}
