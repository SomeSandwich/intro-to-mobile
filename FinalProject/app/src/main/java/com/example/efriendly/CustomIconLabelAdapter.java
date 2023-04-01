package com.example.efriendly;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

class CustomIconLabelAdapter extends ArrayAdapter<String> {
    Context context;
    Integer[] avatars;
    String[] notifications;
    String[] times;
    public CustomIconLabelAdapter( Context context, int layoutToBeInflated, String[] notifications, String[] times, Integer[] avatars) {
        super(context, layoutToBeInflated, notifications);
        this.context = context;
        this.notifications = notifications;
        this.times = times;
        this.avatars = avatars;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = ((Activity) context).getLayoutInflater();
        View row = inflater.inflate(R.layout.custom_notification_items, null);

        TextView notice = (TextView) row.findViewById(R.id.txtNotification);
        TextView time = (TextView) row.findViewById(R.id.txtTime);
        ImageView avatar = (ImageView) row.findViewById(R.id.ivAvatar);

        notice.setText(notifications[position]);
        time.setText(times[position]);
        avatar.setImageResource(avatars[position]);
        return (row);
    }
} // CustomAdapter
