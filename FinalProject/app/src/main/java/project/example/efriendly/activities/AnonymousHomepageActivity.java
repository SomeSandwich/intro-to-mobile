package project.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.content.res.ResourcesCompat;
import androidx.databinding.DataBindingUtil;

import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.util.Log;
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

import project.example.efriendly.R;
import project.example.efriendly.databinding.ActivityAnonymousHomepageBinding;

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
    public ActivityAnonymousHomepageBinding binding;
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

    String[] category = {
            "All", "Men", "Women", "Kid", "Children", "Elder", "Give a way", "Challenge"
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

        addCategory();
        addItem();
    }
    private LinearLayout createNewItem(String des, Integer img){
        LinearLayout result = new LinearLayout(this);

        result.setLayoutParams(new TableRow.LayoutParams(
                0,
                TableRow.LayoutParams.WRAP_CONTENT, 1.0f));
        result.setGravity(Gravity.FILL);
        result.setOrientation(LinearLayout.VERTICAL);
        result.setBackgroundResource(R.drawable.clothes_frame);

        ImageView clothImg = new ImageView(this);

        clothImg.setLayoutParams(new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WRAP_CONTENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));

        clothImg.setPadding(10, 10, 10, 10);
        clothImg.setImageResource(img);
        clothImg.setBackgroundResource(R.drawable.clothes_frame);

        TextView clothDes = new TextView(this);
        clothDes.setLayoutParams(new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WRAP_CONTENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        clothDes.setGravity(Gravity.CENTER);
        clothDes.setText(des);
        result.addView(clothImg, new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MATCH_PARENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        result.addView(clothDes, new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MATCH_PARENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        return result;
    }
    private void addItem(){
        TableLayout tl = binding.ItemList;
        TableRow tr = tr = new TableRow(this);

        for (int i=0;i<des.length;i++){
            if (i % 2 == 0) tr = new TableRow(this);
            tr.setLayoutParams(new TableRow.LayoutParams(
                    TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT));
            LinearLayout b = createNewItem(des[i], img[i]);
            tr.addView(b);
            if (i%2==1)
                tl.addView(tr, new TableLayout.LayoutParams(
                    TableLayout.LayoutParams.MATCH_PARENT,
                    TableLayout.LayoutParams.WRAP_CONTENT));
        }

        if (des.length % 2 == 1) tl.addView(tr, new TableLayout.LayoutParams(
                TableLayout.LayoutParams.MATCH_PARENT,
                TableLayout.LayoutParams.WRAP_CONTENT));
    }
    private android.widget.Button createCategory(int id, String name, Integer img ){
        android.widget.Button btn = new android.widget.Button(this);

        LinearLayout.LayoutParams btnLayout = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WRAP_CONTENT,
                LinearLayout.LayoutParams.WRAP_CONTENT);
        btnLayout.setMargins(30, 30, 30, 30);
        btn.setLayoutParams(btnLayout);
        btn.setId(id);
        btn.setOnClickListener(handlers.CategoryClick());

        Drawable top = ResourcesCompat.getDrawable(getResources(), R.drawable.likebutton, null);
        btn.setCompoundDrawablesRelativeWithIntrinsicBounds(null, top, null, null);

        btn.setPadding(0, 30, 0,0);
        btn.setBackgroundResource(R.drawable.category_frame);
        btn.setText(name);
        return btn;
    }
    private void addCategory(){
        LinearLayout ll = binding.innerLay;

        for (int i=0;i<category.length;i++) {
            android.widget.Button newCategory = createCategory(i, category[i], R.drawable.likebutton);

            LinearLayout.LayoutParams btnLayout = new LinearLayout.LayoutParams(
                    LinearLayout.LayoutParams.WRAP_CONTENT,
                    LinearLayout.LayoutParams.WRAP_CONTENT);
            btnLayout.setMargins(5, 0, 5, 0);

            ll.addView(newCategory, btnLayout);
        }
    }

    public class AnonymousHomepageActivityClickHandler{
        Context context;
        public AnonymousHomepageActivityClickHandler(Context context){
            this.context = context;
        }

        private ProgressDialog progressBar(){
            ProgressDialog mProgress = new ProgressDialog(context);
            mProgress.setTitle("Processing...");
            mProgress.setMessage("Please wait...");
            mProgress.setCancelable(false);
            mProgress.setIndeterminate(true);
            return mProgress;
        }

        public void LoginClick(View view){
            ProgressDialog mProgress = progressBar();
            mProgress.show();
            Intent myIntent = new Intent(AnonymousHomepageActivity.this, LoginActivity.class);
            startActivity(myIntent);
            mProgress.dismiss();
        }

        public View.OnClickListener CategoryClick(){
            return new View.OnClickListener(){
                @Override
                public void onClick(View view){
                    int id = view.getId();
                    Log.d("Debug", "Click id category " + Integer.toString(id));
                }
            };
        }
    }
}