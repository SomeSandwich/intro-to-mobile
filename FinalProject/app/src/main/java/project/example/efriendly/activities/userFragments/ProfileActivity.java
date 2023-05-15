package project.example.efriendly.activities.userFragments;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;
import androidx.fragment.app.Fragment;

import android.annotation.SuppressLint;
import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.EditText;

import project.example.efriendly.R;
import project.example.efriendly.adapter.NotificationsAdapter;
import project.example.efriendly.databinding.ActivityNotificationsBinding;
import project.example.efriendly.databinding.ActivityProfileBinding;

public class ProfileActivity extends Fragment {
    private ActivityProfileBinding binding;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {

        binding = ActivityProfileBinding.inflate(inflater, container,false);

        return binding.getRoot();
    }
}