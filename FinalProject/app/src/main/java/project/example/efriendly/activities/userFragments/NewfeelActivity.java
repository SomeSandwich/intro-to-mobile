package project.example.efriendly.activities.userFragments;

import android.content.Context;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import java.io.FileInputStream;
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

    Vector<PostRes> posts = new Vector<>();

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

        //PostAdapter posts = new PostAdapter(main, posts);
        return binding.getRoot();
    }
}