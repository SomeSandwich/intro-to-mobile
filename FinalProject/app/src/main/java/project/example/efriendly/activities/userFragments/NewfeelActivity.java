package project.example.efriendly.activities.userFragments;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.jakewharton.threetenabp.AndroidThreeTen;

import org.threeten.bp.LocalDateTime;

import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;
import java.util.Vector;

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.PostAdapter;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.SellerRes;
import project.example.efriendly.databinding.ActivityHomepageBinding;
import project.example.efriendly.databinding.FragmentNewfeelActivityBinding;
import project.example.efriendly.services.PostService;

public class NewfeelActivity extends Fragment {

    UserActivity main;
    Context context = null;
    FragmentNewfeelActivityBinding binding;

    PostService service;

    public NewfeelActivity() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        try{
            context = getActivity();
            main = (UserActivity) getActivity();
        }
        catch (IllegalStateException err){
            throw new IllegalStateException("MainActivity must implement callbacks");
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        binding = FragmentNewfeelActivityBinding.inflate(inflater, container,false);
        SearchBarCartChatActivity searchbar = new SearchBarCartChatActivity();
        getParentFragmentManager().beginTransaction().replace(R.id.searchBarFragment, searchbar).commit();

        List<String> imagePath = new ArrayList<>();
        imagePath.add("https://bizweb.dktcdn.net/thumb/1024x1024/100/416/517/products/nguyen-01.png?v=1617871985973");
        imagePath.add("https://s.imgur.com/images/logo-1200-630.jpg?2");
        imagePath.add("https://bizweb.dktcdn.net/thumb/1024x1024/100/416/517/products/nguyen-01.png?v=1617871985973");
        imagePath.add("https://www.vectortemplates.com/raster/batman-logo-big.gif");

        Vector<Bitmap> imgBitmap = new Vector<>();


        AndroidThreeTen.init(context);

        LocalDateTime dt1 = LocalDateTime.now();

        Vector<PostRes> posts = new Vector<>();

        for(int i=0;i<imagePath.size();i++){
            try {
                URL newUrl = new URL(imagePath.get(i));
                URLConnection conn = newUrl.openConnection();
                Bitmap mIcon_val = BitmapFactory.decodeStream(conn.getInputStream());
                imgBitmap.add(mIcon_val);
            }
            catch (Exception err) {
                Log.d("Debug", err.getMessage());}
        }

        posts.add(new PostRes(new SellerRes(1,"Hoang Quoc Bao", 4.5),
                1,
                100000,
                "Black t-shirt",
                "This is description",
                imagePath, dt1, dt1, false
                ));
        posts.add(new PostRes(new SellerRes(1,"Hoang Quoc Bao", 4.5),
                1,
                100000,
                "Black t-shirt",
                "This is description",
                imagePath, dt1, dt1, false
        ));
        posts.add(new PostRes(new SellerRes(1,"Hoang Quoc Bao", 4.5),
                1,
                100000,
                "Black t-shirt",
                "This is description",
                imagePath, dt1, dt1, false
        ));
        posts.add(new PostRes(new SellerRes(1,"Hoang Quoc Bao", 4.5),
                1,
                100000,
                "Black t-shirt",
                "This is description",
                imagePath, dt1, dt1, false
        ));
        posts.add(new PostRes(new SellerRes(1,"Hoang Quoc Bao", 4.5),
                1,
                100000,
                "Black t-shirt",
                "This is description",
                imagePath, dt1, dt1, false
        ));
        posts.add(new PostRes(new SellerRes(1,"Hoang Quoc Bao", 4.5),
                1,
                100000,
                "Black t-shirt",
                "This is description",
                imagePath, dt1, dt1, false
        ));
        for(int i = 0;i<posts.size();i++){
            posts.get(i).setImgBitmap(imgBitmap);
        }

        PostAdapter PostA = new PostAdapter(main, posts);
        binding.newfeelPost.setAdapter(PostA);

        return binding.getRoot();
    }
}