package project.example.efriendly.holder;

import android.view.View;
import android.widget.CheckBox;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;
import project.example.efriendly.databinding.CustomCartItemsBinding;

public class CartHolder extends RecyclerView.ViewHolder {
    public View view;
    public ImageView productImg;
    public TextView txtProductName;
    public TextView txtPrice;
    public TextView txtSeller;
    public CheckBox checkBox;
    public CartHolder(CustomCartItemsBinding binding){
        super(binding.getRoot());
        this.productImg = binding.ivProduct;
        this.txtProductName = binding.txtProductName;
//        this.txtPrice = binding.txtPrice;
//        this.txtSeller = binding.txtSeller;
//        this.checkBox = binding.cbCart;

        this.view = binding.getRoot();
    }
}
