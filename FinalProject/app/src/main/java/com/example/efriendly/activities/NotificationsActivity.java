package com.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;

import com.example.efriendly.R;

public class NotificationsActivity extends AppCompatActivity {

    private ListView notificationList;
    Integer[] avatars = {R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user,
            R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user, R.drawable.user};
    String[] notifications = {"Notice 1", "Notice 2", "Notice 3", "Notice 4", "Notice 5", "Notice 6", "Notice 7", "Notice 8", "Notice 9", "Notice 10",
            "Notice 1", "Notice 2", "Notice 3", "Notice 4", "Notice 5", "Notice 6", "Notice 7", "Notice 8", "Notice 9", "Notice 10"};
    String[] times = {"2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h",
            "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h", "2h"};

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_notifications);

        notificationList = (ListView) findViewById(R.id.NotificationList);

        CustomIconLabelAdapter adapter = new CustomIconLabelAdapter(this, R.layout.custom_notification_items, notifications, times, avatars);
        notificationList.setAdapter(adapter);

        notificationList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                //Danh dau la da xem
            }
        });
    }//onCreate
}