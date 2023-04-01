package com.example.efriendly.listviewAdpater;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;

import com.example.efriendly.R;
import com.example.efriendly.databinding.ItemListHomepageBinding;

public class HomepageAdapter extends ArrayAdapter<String> {
    private final Activity context;
    private final String[] des;
    private final Integer[] img;
    private final int layout;


    public HomepageAdapter(Activity context, int layoutToBeInflated, String[] des, Integer[] img) {
        super(context, layoutToBeInflated, des);
        this.context=context;
        this.layout = layoutToBeInflated;
        this.des = des;
        this.img = img;
    }
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View result = convertView;

        return result;
    }
}
