package com.example.efriendly;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import com.example.efriendly.databinding.ActivityAnonymousHomepageBinding;
import com.example.efriendly.ui.login.LoginActivity;
public class AnonymousHomepageActivity extends AppCompatActivity {
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
    private ActivityAnonymousHomepageBinding binding;
    private AnonymousHomepageActivityClickHandler handlers;

    @SuppressLint("AppCompatMethod")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getSupportActionBar().hide();
        binding = DataBindingUtil.setContentView(this, R.layout.activity_anonymous_homepage);
        handlers = new AnonymousHomepageActivityClickHandler(this);
        binding.setClickHandler(handlers);
    }
    public class AnonymousHomepageActivityClickHandler{
        Context context;
        public AnonymousHomepageActivityClickHandler(Context context){
            this.context = context;
        }
        public void LoginClick(View view){
            Intent myIntent = new Intent(AnonymousHomepageActivity.this, LoginActivity.class);
            startActivity(myIntent);
        }
    }
}