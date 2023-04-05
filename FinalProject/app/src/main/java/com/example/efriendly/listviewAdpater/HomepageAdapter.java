package com.example.efriendly.listviewAdpater;

import android.app.Activity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;

import com.example.efriendly.R;
import com.example.efriendly.databinding.ItemListHomepageBinding;

import java.util.Vector;

public class HomepageAdapter extends ArrayAdapter<String> {
    private final Activity context;
    private final Vector<String> leftDes;
    private final Vector<Integer> leftImg;
    private final Vector<String> rightDes;
    private final Vector<Integer> rightImg;
    private final int layout;

    private LayoutInflater inflater;

    public HomepageAdapter(Activity context,
                           int layoutToBeInflated,
                           Vector<String> leftDes,
                           Vector<String> rightDes,
                           Vector<Integer> leftImg,
                           Vector<Integer> rightImg) {
        super(context, layoutToBeInflated, leftDes);
        this.context=context;
        this.layout = layoutToBeInflated;

        this.leftDes = leftDes;
        this.rightDes = rightDes;
        this.leftImg = leftImg;
        this.rightImg = rightImg;
    }
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View result = convertView;
        ItemListHomepageBinding binding;
        if (result == null){
            if(inflater == null)
                inflater = (LayoutInflater) parent.getContext().getSystemService(context.LAYOUT_INFLATER_SERVICE);
            binding = ItemListHomepageBinding.inflate(inflater, parent, false);
            result = binding.getRoot();
            result.setTag(binding);
        }
        else binding = (ItemListHomepageBinding) result.getTag();

        binding.LeftItemDes.setText(leftDes.get(position));
        binding.LeftItemPic.setImageResource(leftImg.get(position));

        if (position == 4){
            binding.rightItem.setVisibility(View.INVISIBLE);
        }
        else {
            binding.RightItemDes.setText(rightDes.get(position));
            binding.RightItemPic.setImageResource(rightImg.get(position));
        }

        return result;
    }
}
