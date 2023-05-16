package project.example.efriendly.holder;

import android.view.View;
import android.widget.ImageView;
import androidx.recyclerview.widget.RecyclerView;

import project.example.efriendly.activities.userFragments.CreatePost;
import project.example.efriendly.adapter.CreatePostAdapter;
import project.example.efriendly.databinding.AddImageBinding;


public class AddImageHolder extends RecyclerView.ViewHolder {
    public ImageView clothImg;

    public ImageView removeImage;


    public AddImageHolder(AddImageBinding binding){
        super(binding.getRoot());
        clothImg = binding.clotheImg;
        removeImage = binding.removeImage;
    }
}
