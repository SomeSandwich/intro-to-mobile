package project.example.efriendly.activities;

import androidx.fragment.app.Fragment;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import project.example.efriendly.databinding.ActivityNavBarBinding;

public class navBarActivity extends Fragment {
    ActivityNavBarBinding binding;
    Context context = null;
    UserActivity main;
    public NavbarClickHandler handlers;

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
        binding = ActivityNavBarBinding.inflate(inflater, container,false);
        handlers = new NavbarClickHandler(context);
        binding.setClickHandler(handlers);

        return binding.getRoot();
    }
    public class NavbarClickHandler{
        Context context;
        public NavbarClickHandler(Context context){
            this.context = context;
        }
        public void homeClick(View view){
            main.onMsgFromFragToMain("nav", "2");
        }
        public void notificationClick(View view){ main.onMsgFromFragToMain("nav", "3"); }
    }
}