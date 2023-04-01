package com.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import com.example.efriendly.R;
import com.example.efriendly.databinding.ActivityAnonymousHomepageBinding;
import com.example.efriendly.activities.login.LoginActivity;

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

    String[] des = {
            "Test Description 1", "Test Description 2",
            "Test Description 3", "Test Description 4",
            "Test Description 5", "Test Description 6",
            "Test Description 7", "Test Description 8",
            "Test Description 9"
    };
    Integer[] img = {
            R.drawable.clothes, R.drawable.clothes,
            R.drawable.clothes, R.drawable.clothes,
            R.drawable.clothes, R.drawable.clothes,
            R.drawable.clothes, R.drawable.clothes,
            R.drawable.clothes
    };
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

        addItem();
    }

    private LinearLayout createNewItem(String des, Integer img){
        LinearLayout result = new LinearLayout(this);
        result.setLayoutParams(new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,LinearLayout.LayoutParams.WRAP_CONTENT));
        result.setWeightSum(50);
        result.setOrientation(LinearLayout.VERTICAL);
        result.setBackgroundResource(R.drawable.clothes_frame);

        ImageView clothImg = new ImageView(this);
        clothImg.setLayoutParams(new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.WRAP_CONTENT));
        clothImg.setPadding(10, 10, 10, 10);
        clothImg.setImageResource(R.drawable.clothes);
        clothImg.setBackgroundResource(R.drawable.clothes_frame);

        TextView clothDes = new TextView(this);
        clothDes.setLayoutParams(new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.WRAP_CONTENT));
        clothDes.setGravity(Gravity.CENTER);
        clothDes.setText("Test 123");

        result.addView(clothImg);
        result.addView(clothDes);

        return result;
    }

    private void addItem(){
        TableRow tbrow0 = new TableRow(this);
        LinearLayout Cell1 = createNewItem(des[0], img[0]);
        LinearLayout Cell2 = createNewItem(des[1], img[1]);
        tbrow0.addView(Cell1);
        tbrow0.addView(Cell2);
        TableLayout stk = (TableLayout) findViewById(R.id.ItemList);
        stk.addView(tbrow0, new TableLayout.LayoutParams(TableLayout.LayoutParams.WRAP_CONTENT, TableLayout.LayoutParams.WRAP_CONTENT));
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