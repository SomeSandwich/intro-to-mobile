package project.example.efriendly.activities.userFragments;

import android.content.Context;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.databinding.FragmentCreatePostBinding;
import project.example.efriendly.databinding.FragmentNewfeelActivityBinding;
import project.example.efriendly.services.PostService;

public class CreatePost extends Fragment {
    UserActivity main;

    Context context = null;

    FragmentCreatePostBinding binding;

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
        binding = FragmentCreatePostBinding.inflate(inflater, container,false);
        PostService postService = RetrofitClientGenerator.getService(PostService.class);

        return binding.getRoot();
    }
}