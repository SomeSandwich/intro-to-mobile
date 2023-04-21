package project.example.efriendly.holder;

import android.view.View;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import project.example.efriendly.databinding.PostBinding;

public class PostHolder extends RecyclerView.ViewHolder {
    public TextView des;
    public TextView price;
    public ImageView image;
    public View view;
    public ProgressBar progressBar;

    public PostHolder(PostBinding binding){
        super(binding.getRoot());

        des = binding.Des;
        price = binding.Price;
        image = binding.Image;
        progressBar = binding.processBar;

        this.view = binding.getRoot();

    }
}
