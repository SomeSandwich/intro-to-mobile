package project.example.efriendly.activities.userFragments;

import android.content.Context;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.jakewharton.threetenabp.AndroidThreeTen;

import org.threeten.bp.LocalDateTime;
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

public class NewfeelActivity extends Fragment {

    UserActivity main;
    Context context = null;
    FragmentNewfeelActivityBinding binding;

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
        imagePath.add("https://bizweb.dktcdn.net/thumb/1024x1024/100/416/517/products/nguyen-01.png?v=1617871985973");
        imagePath.add("https://bizweb.dktcdn.net/thumb/1024x1024/100/416/517/products/nguyen-01.png?v=1617871985973");
        imagePath.add("https://bizweb.dktcdn.net/thumb/1024x1024/100/416/517/products/nguyen-01.png?v=1617871985973");

        AndroidThreeTen.init(context);

        LocalDateTime dt1 = LocalDateTime.now();

        Vector<PostRes> posts = new Vector<>();

        posts.add(new PostRes(new SellerRes(1,"Hoang Quoc Bao", 4.5),
                1,
                100000,
                "Black t-shirt",
                "This is description",
                imagePath, dt1, dt1, false
                ));
        PostAdapter PostA = new PostAdapter(main, posts);

        binding.newfeelPost.setAdapter(PostA);
        return binding.getRoot();
    }
}