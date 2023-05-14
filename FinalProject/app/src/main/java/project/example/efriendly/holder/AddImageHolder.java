package project.example.efriendly.holder;

import android.view.View;
import android.widget.ImageView;
import androidx.recyclerview.widget.RecyclerView;

import project.example.efriendly.adapter.CreatePostAdapter;
import project.example.efriendly.databinding.AddImageBinding;


public class AddImageHolder extends RecyclerView.ViewHolder {
    public ImageView clothImg;

    public CreatePostAdapter adapter;

    public AddImageHolder(AddImageBinding binding, CreatePostAdapter adapter){
        super(binding.getRoot());
        clothImg = binding.clotheImg;
        binding.removeImage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                adapter.imgsList.remove(getAdapterPosition());
                adapter.notifyItemRemoved(getAdapterPosition());
            }
        });
    }
}
