package project.example.efriendly.holder;

import android.util.Log;
import android.view.View;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;

import project.example.efriendly.adapter.NewfeelAdapter;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.ShowPostNewfeelAdapterBinding;

public class NewfeelPostHolder extends RecyclerView.ViewHolder {
    public View view;
    public ImageView avt;
    public TextView name;
    public TextView time;
    public TextView des;
    public ImageView clothesImg;
    public TextView likeCount;
    public TextView commentCount;
    public TextView shareCount;
    public ProgressBar progressBar;
    public ImageButton nextClick;
    public ImageButton previousClick;
    public LinearLayout post;

    public NewfeelPostHolder(@NonNull ShowPostNewfeelAdapterBinding binding) {
        super(binding.getRoot());

        avt = binding.sellerAvt;
        name = binding.sellerName;
        time = binding.time;
        des = binding.des;
        clothesImg = binding.clothesImgs;
        likeCount = binding.likeCount;
        commentCount = binding.CommentsCount;
        shareCount = binding.shareCount;
        progressBar = binding.processBar;
        post = binding.post;

        nextClick = binding.NextClick;
        previousClick = binding.PreviousClick;
        view = binding.getRoot();

    }
}
