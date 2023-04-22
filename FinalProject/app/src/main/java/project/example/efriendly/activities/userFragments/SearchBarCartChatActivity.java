package project.example.efriendly.activities.userFragments;


import androidx.fragment.app.Fragment;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import project.example.efriendly.activities.AnonymousHomepageActivity;
import project.example.efriendly.activities.MainActivity;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.databinding.ActivitySearchBarCartChatBinding;

public class SearchBarCartChatActivity extends Fragment {
    ActivitySearchBarCartChatBinding binding;
    Context context = null;
    UserActivity main;

    SearchBarClickHandler handler;

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

        handler = new SearchBarClickHandler(main);

        binding.setClickHandler(handler);

        return binding.getRoot();
    }

    public class SearchBarClickHandler{
        Context context;
        public SearchBarClickHandler(Context context) {
            this.context = context;
        }
        public void chatClick(View view){
            Intent myIntent = new Intent(context, ChatActivity.class);
            startActivity(myIntent);
        }
        public void cartClick(View view){
            Intent intent = new Intent(context, CartActivity.class);
            startActivity(intent);
        }
    }
}