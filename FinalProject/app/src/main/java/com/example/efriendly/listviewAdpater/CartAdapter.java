package com.example.efriendly.listviewAdpater;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;

import com.example.efriendly.databinding.CustomCartItemsBinding;


public class CartAdapter extends ArrayAdapter<String> {
    private LayoutInflater inflater;
    Context context;
    Integer[] product;
    String[] productName;
    String[] price;
    String[] seller;

    public CartAdapter( Context context, int layoutToBeInflated, String[] productName, String[] price, String[] seller, Integer[] product) {
        super(context, layoutToBeInflated, productName);
        this.context = context;
        this.productName = productName;
        this.price = price;
        this.seller = seller;
        this.product = product;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View result = convertView;
        CustomCartItemsBinding binding;

        if (result == null){
            if (inflater == null)
                inflater = (LayoutInflater) parent.getContext().getSystemService(context.LAYOUT_INFLATER_SERVICE);
            binding = CustomCartItemsBinding.inflate(inflater, parent, false);
            result = binding.getRoot();
            result.setTag(binding);
        }
        else binding = (CustomCartItemsBinding) result.getTag();

        binding.txtProductName.setText(productName[position]);
        binding.txtPrice.setText(price[position]);
        binding.txtSeller.setText(seller[position]);
        binding.ivProduct.setImageResource(product[position]);
        return (result);
    }
}
