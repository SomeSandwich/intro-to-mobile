package project.example.efriendly.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;

import project.example.efriendly.databinding.CustomNotificationItemsBinding;


public class NotificationsAdapter extends ArrayAdapter<String> {
    private LayoutInflater inflater;
    Context context;
    Integer[] avatars;
    String[] notifications;
    String[] times;
    public NotificationsAdapter( Context context, int layoutToBeInflated, String[] notifications, String[] times, Integer[] avatars) {
        super(context, layoutToBeInflated, notifications);
        this.context = context;
        this.notifications = notifications;
        this.times = times;
        this.avatars = avatars;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View result = convertView;
        CustomNotificationItemsBinding binding;

        if (result == null){
            if (inflater == null)
                inflater = (LayoutInflater) parent.getContext().getSystemService(context.LAYOUT_INFLATER_SERVICE);
            binding = CustomNotificationItemsBinding.inflate(inflater, parent, false);
            result = binding.getRoot();
            result.setTag(binding);
        }
        else binding = (CustomNotificationItemsBinding) result.getTag();

        binding.txtNotification.setText(notifications[position]);
        binding.txtTime.setText(times[position]);
        binding.ivAvatar.setImageResource(avatars[position]);
        return (result);
    }
}
