package project.example.efriendly.activities.userFragments;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;
import androidx.fragment.app.Fragment;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.ChatAdapter;
import project.example.efriendly.databinding.ActivityNotificationsBinding;
import project.example.efriendly.adapter.NotificationsAdapter;

public class NotificationsActivity extends Fragment {
    UserActivity main;
    Context context = null;

    private ActivityNotificationsBinding binding;
    
    Integer[] avatars = {R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user,
            R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user};
    String[] notifications = {"Notice 1", "Notice 2", "Notice 3", "Notice 4", "Notice 5", "Notice 6", "Notice 7", "Notice 8", "Notice 9", "Notice 10",
            "Notice 1", "Notice 2", "Notice 3", "Notice 4", "Notice 5", "Notice 6", "Notice 7", "Notice 8", "Notice 9", "Notice 10"};
    String[] times = {"2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h",
            "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h"};

    @Override
    public boolean dispatchTouchEvent(MotionEvent ev) { //Disable keyboard when click around
        View view = getCurrentFocus();
        if (view != null && (ev.getAction() == MotionEvent.ACTION_UP || ev.getAction() == MotionEvent.ACTION_MOVE) && view instanceof EditText && !view.getClass().getName().startsWith("android.webkit.")) {
            int[] scrcoords = new int[2];
            view.getLocationOnScreen(scrcoords);
            float x = ev.getRawX() + view.getLeft() - scrcoords[0];
            float y = ev.getRawY() + view.getTop() - scrcoords[1];
            if (x < view.getLeft() || x > view.getRight() || y < view.getTop() || y > view.getBottom())
                ((InputMethodManager)this.getSystemService(Context.INPUT_METHOD_SERVICE)).hideSoftInputFromWindow((this.getWindow().getDecorView().getApplicationWindowToken()), 0);
        }
        return super.dispatchTouchEvent(ev);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {

        binding = ActivityNotificationsBinding.inflate(inflater, container,false);

        NotificationsAdapter adapter = new NotificationsAdapter(main, R.layout.custom_notification_items, notifications, times, avatars);
        
        binding.NotificationList.setAdapter(adapter);
        binding.NotificationList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                System.out.println("hehe");
            }
        });
    }
}