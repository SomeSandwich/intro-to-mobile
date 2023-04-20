package project.example.efriendly.adapter;

import android.app.Activity;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.HomepageItemAdapterBinding;
import project.example.efriendly.ultilities.HomepageItemList;

public class HomepageItemAdapter extends BaseAdapter {
    private Activity activity;

    private HomepageItemAdapterBinding binding;

    private List<HomepageItemList> posts = new ArrayList<>();

    public HomepageItemAdapter(Activity activity, List<HomepageItemList> posts){
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
        binding = HomepageItemAdapterBinding.inflate(activity.getLayoutInflater(), viewGroup, false);

        HomepageItemList postLine = posts.get(i);

        if (postLine.getRightItem() == null) binding.RightItem.setVisibility(View.INVISIBLE);
        else {
            binding.RightDes.setText(postLine.getRightItem().getCaption());
            binding.RightImage.setImageBitmap(postLine.getRightItem().getImgBitmap().get(0));
            String price = String.valueOf(postLine.getRightItem().getPrice()) + "VND";
            binding.RightPrice.setText(String.format(price));
        }


        return null;
    }
}
