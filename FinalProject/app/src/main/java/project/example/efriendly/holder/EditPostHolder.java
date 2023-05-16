package project.example.efriendly.holder;

import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import org.w3c.dom.Text;

import project.example.efriendly.databinding.ItemPostBinding;

public class EditPostHolder extends RecyclerView.ViewHolder{
    public View view;
    public TextView caption;
    public TextView prices;
    public ImageView clothImg;
    public Button delete;
    public TextView sold;
    public EditPostHolder(@NonNull ItemPostBinding binding) {
        super(binding.getRoot());
        this.caption = binding.caption;
        this.prices = binding.prices;
        this.clothImg = binding.clotheImg;
        this.delete = binding.delete;
        this.sold = binding.sold;

        view = binding.getRoot();
    }
}
