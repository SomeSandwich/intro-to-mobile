package project.example.efriendly.activities.userFragments;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.CartAdapter;
import project.example.efriendly.databinding.ActivityNotificationsBinding;
import project.example.efriendly.adapter.NotificationsAdapter;

public class NotificationsActivity extends Fragment {
    UserActivity main;
    Context context = null;
    private ActivityNotificationsBinding binding;
    private NotificationsAdapter adapter;

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

        binding = ActivityNotificationsBinding.inflate(inflater, container,false);

        adapter = new NotificationsAdapter(getContext());
        binding.NotificationList.setAdapter(adapter);
        binding.NotificationList.setLayoutManager(new LinearLayoutManager(getContext()));

        return binding.getRoot();
    }
}