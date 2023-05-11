package project.example.efriendly.activities.userFragments;


import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.fragment.app.Fragment;

import project.example.efriendly.activities.AnonymousHomepageActivity;
import project.example.efriendly.activities.LoginActivity;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.databinding.ActivitySearchBarCartChatBinding;
import project.example.efriendly.databinding.ActivitySearchBarCartLoginBinding;

public class SearchBarCartLoginActivity extends Fragment {
    ActivitySearchBarCartLoginBinding binding;
    Context context = null;
    AnonymousHomepageActivity main;

    SearchBarClickHandler clickHandler;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        try{
            context = getActivity();
            main = (AnonymousHomepageActivity) getActivity();
            clickHandler = new SearchBarClickHandler(context);
        }
        catch (IllegalStateException err){
            throw new IllegalStateException("MainActivity must implement callbacks");
        }
    }
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        binding = ActivitySearchBarCartLoginBinding.inflate(inflater, container,false);
        binding.setClickHandler(clickHandler);
        return binding.getRoot();
    }

    public class SearchBarClickHandler{
        Context context;
        public SearchBarClickHandler(Context context){
            this.context = context;
        }

        public void LoginClick(View view){
            Intent myIntent = new Intent(context, LoginActivity.class);
            startActivity(myIntent);
        }
    }
}