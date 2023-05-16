package project.example.efriendly.holder;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import project.example.efriendly.databinding.CategoryItemsBinding;

public class CategoryHolder extends RecyclerView.ViewHolder {
    public TextView type;
    public ImageView icon;
    public View view;

    public CategoryHolder(@NonNull CategoryItemsBinding binding) {
        super(binding.getRoot());
        type = binding.type;
        icon = binding.icon;
        view = binding.getRoot();
    }
}
