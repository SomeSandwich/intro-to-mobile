package project.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.FragmentTransaction;

import android.annotation.SuppressLint;
import android.content.Context;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;

import project.example.efriendly.R;
import project.example.efriendly.activities.userFragments.HomepageActivity;
import project.example.efriendly.activities.userFragments.NewfeelActivity;
import project.example.efriendly.activities.userFragments.NotificationsActivity;
import project.example.efriendly.activities.userFragments.SearchBarCartChatActivity;
import project.example.efriendly.databinding.ActivityUserBinding;

public class UserActivity extends AppCompatActivity{
    FragmentTransaction ft;
    HomepageActivity homepage = new HomepageActivity();
    navBarActivity navbar = new navBarActivity();

    NewfeelActivity newFeel = new NewfeelActivity();

    NotificationsActivity notification = new NotificationsActivity();
    ActivityUserBinding binding;

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

    @SuppressLint("AppCompatMethod")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getSupportActionBar().hide();

        binding = ActivityUserBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        ft = getSupportFragmentManager().beginTransaction();
        ft.replace(R.id.userFragment, new NewfeelActivity()).commit();
        ft = getSupportFragmentManager().beginTransaction();
        ft.replace(R.id.navBarFragment, navbar).commit();
    }

    public void onMsgFromFragToMain(String sender, String strValue) {
        if (sender.equals("nav")) {
            switch (strValue){
                case "1":
                    getSupportFragmentManager().beginTransaction().replace(R.id.userFragment, newFeel).commit();
                    break;
                case "2":
                    getSupportFragmentManager().beginTransaction().replace(R.id.userFragment, homepage).commit();
                    break;
                case "3":
                    getSupportFragmentManager().beginTransaction().replace(R.id.userFragment, notification).commit();
                    break;
                default:
                    break;
            }
        }
    }
}