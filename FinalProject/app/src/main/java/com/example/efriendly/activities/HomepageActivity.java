package com.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;

import com.example.efriendly.R;
import com.example.efriendly.databinding.ActivityHomepageBinding;


public class HomepageActivity extends AppCompatActivity {
    private ActivityHomepageBinding binding;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_homepage);
    }
}