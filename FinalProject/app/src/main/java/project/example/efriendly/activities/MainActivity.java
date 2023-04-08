package project.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;

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

import project.example.efriendly.activities.userFragments.HomepageActivity;


public class MainActivity extends AppCompatActivity {

//    private static CategoryService categoryService = RetrofitClientGenerator.createService(CategoryService.class);

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

        //setContentView(R.layout.activity_anonymous_homepage);

        Intent myIntent = new Intent(MainActivity.this, UserActivity.class);
        startActivity(myIntent);
        finish();

//        Call<List<CategoryRes>> allCateCall =  categoryService.getAll();
//        allCateCall.enqueue(new Callback<List<CategoryRes>>() {
//            @Override
//            public void onResponse(Call<List<CategoryRes>> call, Response<List<CategoryRes>> response) {
//                List<CategoryRes> cates = response.body();
//
//                ...
//            }
//
//            @Override
//            public void onFailure(Call<List<CategoryRes>> call, Throwable t) {
//
//            }
//        });
    }


}