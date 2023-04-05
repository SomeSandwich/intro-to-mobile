package com.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;

import android.content.Context;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.EditText;

import com.example.efriendly.R;
import com.example.efriendly.databinding.ActivityCartBinding;
import com.example.efriendly.listviewAdpater.CartAdapter;

public class CartActivity extends AppCompatActivity {
    private ActivityCartBinding binding;
    Integer[] product = {R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes,
            R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes, R.drawable.clothes};

    String[] productName = {"Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt",
            "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt", "Black plain T-Shirt"};

    String[] price = {"50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ",
            "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ", "50.000đ"};

    String[] seller = {"Truong Trong Khanh", "Hoang Quoc Bao", "Kha Vinh Dat", "Nguyen Quoc Su", "Tran Minh Truong", "Nguyen Tan Hieu", "Nguyen Ho Hu Bang", "Phan Thanh Sang", "Do Nguyen Hung", "Tran Hong Quan",
            "Truong Trong Khanh", "Hoang Quoc Bao", "Kha Vinh Dat", "Nguyen Quoc Su", "Tran Minh Truong", "Nguyen Tan Hieu", "Nguyen Ho Hu Bang", "Phan Thanh Sang", "Do Nguyen Hung", "Tran Hong Quan"};
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

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getSupportActionBar().hide();

        binding = DataBindingUtil.setContentView(this, R.layout.activity_cart);
        CartAdapter adapter = new CartAdapter(this, R.layout.custom_cart_items, productName, price, seller, product);
        binding.ChatList.setAdapter(adapter);
        binding.ChatList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                System.out.println("hehe");
            }
        });
    }//onCreate
}