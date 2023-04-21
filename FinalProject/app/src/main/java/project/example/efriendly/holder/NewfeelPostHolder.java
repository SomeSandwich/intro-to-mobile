package project.example.efriendly.holder;

import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

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

        view = binding.getRoot();

    }
}
