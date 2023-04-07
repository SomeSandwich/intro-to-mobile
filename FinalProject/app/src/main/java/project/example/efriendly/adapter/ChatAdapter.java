package project.example.efriendly.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;

import project.example.efriendly.databinding.CustomChatItemsBinding;


public class ChatAdapter extends ArrayAdapter<String> {
    private LayoutInflater inflater;
    Context context;
    Integer[] avatars;
    String[] name;
    String[] message;

    public ChatAdapter( Context context, int layoutToBeInflated, String[] name, String[] message, Integer[] avatars) {
        super(context, layoutToBeInflated, name);
        this.context = context;
        this.name = name;
        this.message = message;
        this.avatars = avatars;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View result = convertView;
        CustomChatItemsBinding binding;

        if (result == null){
            if (inflater == null)
                inflater = (LayoutInflater) parent.getContext().getSystemService(context.LAYOUT_INFLATER_SERVICE);
            binding = CustomChatItemsBinding.inflate(inflater, parent, false);
            result = binding.getRoot();
            result.setTag(binding);
        }
        else binding = (CustomChatItemsBinding) result.getTag();

        binding.txtName.setText(name[position]);
        binding.txtMessage.setText(message[position]);
        binding.ivAvatar.setImageResource(avatars[position]);
        return (result);
    }
}
