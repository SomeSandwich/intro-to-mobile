package project.example.efriendly.activities.userFragments;


import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.appcompat.widget.SearchView;
import androidx.fragment.app.Fragment;

import project.example.efriendly.activities.AnonymousHomepageActivity;
import project.example.efriendly.activities.LoginActivity;
import project.example.efriendly.databinding.ActivitySearchBarLoginBinding;

public class SearchBarLoginActivity extends Fragment {
    ActivitySearchBarLoginBinding binding;
    Context context = null;
    AnonymousHomepageActivity anonymousActivity;

    SearchBarClickHandler clickHandler;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        try {
            context = getActivity();
            anonymousActivity = (AnonymousHomepageActivity) getActivity();
            clickHandler = new SearchBarClickHandler(context);
        } catch (IllegalStateException err) {
            throw new IllegalStateException("MainActivity must implement callbacks");
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        binding = ActivitySearchBarLoginBinding.inflate(inflater, container,false);

        binding.setClickHandler(clickHandler);

        binding.SearchBar.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                anonymousActivity.FetchSearchListPost(query);

                return false;
            }

            @Override
            public boolean onQueryTextChange(String newText) {
                return false;
            }
        });

        return binding.getRoot();
    }

    public class SearchBarClickHandler {
        Context context;

        public SearchBarClickHandler(Context context) {
            this.context = context;
        }

        public void LoginClick(View view) {
            Intent myIntent = new Intent(context, LoginActivity.class);
            startActivity(myIntent);
        }
    }
}