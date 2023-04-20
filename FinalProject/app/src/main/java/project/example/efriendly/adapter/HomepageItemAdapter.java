package project.example.efriendly.adapter;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.Vector;

import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.HomepageItemAdapterBinding;
import project.example.efriendly.ultilities.HomepageItemList;

public class HomepageItemAdapter extends BaseAdapter implements DatabaseConnection {
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
            String price = String.valueOf(postLine.getRightItem().getPrice()) + "VND";
            binding.RightPrice.setText(String.format(price));

            try{
                binding.RightImage.setImageBitmap(postLine.getRightItem().getImgBitmap().get(0));
            }
            catch (NullPointerException err){
                postLine.getRightItem().setImgBitmap(new Vector<>());
                binding.processBar.setVisibility(View.VISIBLE);
                try {
                    URL newUrl = new URL(IMAGE_URL + postLine.getRightItem().getMediaPath().get(0));
                    URLConnection conn = newUrl.openConnection();
                    Bitmap mIcon_val = BitmapFactory.decodeStream(conn.getInputStream());
                    if (mIcon_val != null) {
                        postLine.getRightItem().getImgBitmap().add(mIcon_val);
                        binding.RightImage.setImageBitmap(mIcon_val);
                        binding.processBar.setVisibility(View.INVISIBLE);
                    }
                }
                catch (Exception e) {
                    Log.d("Debug", e.getMessage());
                }
            }
        }

        if (postLine.getLeftItem() == null) binding.LeftItem.setVisibility(View.INVISIBLE);
        else {
            String price = String.valueOf(postLine.getLeftItem().getPrice()) + "VND";
            binding.LeftPrice.setText(String.format(price));
            binding.LeftDes.setText(postLine.getLeftItem().getCaption());
            try{
                binding.LeftImage.setImageBitmap(postLine.getLeftItem().getImgBitmap().get(0));
            }
            catch (NullPointerException e){
                postLine.getLeftItem().setImgBitmap(new Vector<>());
                binding.processBar.setVisibility(View.VISIBLE);
                try {
                    URL newUrl = new URL(IMAGE_URL + postLine.getLeftItem().getMediaPath().get(0));
                    URLConnection conn = newUrl.openConnection();
                    Bitmap mIcon_val = BitmapFactory.decodeStream(conn.getInputStream());
                    if (mIcon_val != null) {
                        postLine.getLeftItem().getImgBitmap().add(mIcon_val);
                        binding.LeftImage.setImageBitmap(mIcon_val);
                        binding.processBar.setVisibility(View.INVISIBLE);
                    }
                } catch (Exception err) {
                    Log.d("Debug", err.getMessage());
                }
            }
        }
        return binding.getRoot();
    }
}
