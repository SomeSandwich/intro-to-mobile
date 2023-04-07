package project.example.efriendly.activities.userFragments;

import androidx.fragment.app.Fragment;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;;

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.databinding.ActivityNotificationsBinding;
import project.example.efriendly.adapter.NotificationsAdapter;

public class NotificationsActivity extends Fragment {
    UserActivity main;
    Context context = null;
    String message = "";
    private ActivityNotificationsBinding binding;
    Integer[] avatars = {R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user,
            R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user};
    String[] notifications = {"Notice 1", "Notice 2", "Notice 3", "Notice 4", "Notice 5", "Notice 6", "Notice 7", "Notice 8", "Notice 9", "Notice 10",
            "Notice 1", "Notice 2", "Notice 3", "Notice 4", "Notice 5", "Notice 6", "Notice 7", "Notice 8", "Notice 9", "Notice 10"};
    String[] times = {"2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h",
            "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h"};

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

        NotificationsAdapter adapter = new NotificationsAdapter(context, R.layout.custom_notification_items, notifications, times, avatars);
        binding.NotificationList.setAdapter(adapter);
        binding.NotificationList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                System.out.println("hehe");
            }
        });
        return binding.getRoot();
    }
}