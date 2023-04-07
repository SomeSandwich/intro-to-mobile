package project.example.efriendly.activities.userFragments;


import androidx.fragment.app.Fragment;
import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.databinding.ActivitySearchBarCartChatBinding;

public class SearchBarCartChatActivity extends Fragment {
    ActivitySearchBarCartChatBinding binding;
    Context context = null;
    UserActivity main;

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
        binding = ActivitySearchBarCartChatBinding.inflate(inflater, container,false);
        return binding.getRoot();
    }
}