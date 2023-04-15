package project.example.efriendly.adapter;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.Vector;

import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.ShowPostNewfeelAdapterBinding;


public class PostAdapter extends BaseAdapter {
    private Activity activity;
    private Vector<PostRes> posts = new Vector<>();

    private ShowPostNewfeelAdapterBinding binding;

    public PostAdapter(Activity activity, Vector<PostRes> posts){
        this.activity = activity;
        this.posts = posts;
    }

    @Override
    public int getCount() {
        return posts.size();
    }

    @Override
    public Object getItem(int i) {
        return posts.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        LayoutInflater inflater = activity.getLayoutInflater();
        binding = ShowPostNewfeelAdapterBinding.inflate(inflater, viewGroup,false);
        return binding.getRoot();
    }

}
