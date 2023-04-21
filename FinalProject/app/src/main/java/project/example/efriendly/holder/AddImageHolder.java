package project.example.efriendly.holder;

import android.widget.ImageView;
import androidx.recyclerview.widget.RecyclerView;

import project.example.efriendly.databinding.AddImageBinding;


public class AddImageHolder extends RecyclerView.ViewHolder {
    public ImageView clothImg;

    public AddImageHolder(AddImageBinding binding){
        super(binding.getRoot());
        clothImg = binding.clotheImg;
    }
}
