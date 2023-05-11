package project.example.efriendly.activities.userFragments;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;

import java.util.ArrayList;

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.ChatAdapter;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityChatBinding;

public class ChatActivity extends AppCompatActivity {
    private ActivityChatBinding binding;
    private RecyclerView recyclerView;
    private ChatAdapter adapter;
    private ArrayList<UserRes> userArrayList;

    private ChatClickHandler handler;

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

        binding = DataBindingUtil.setContentView(this, R.layout.activity_chat);

        userArrayList = new ArrayList<>();
        userArrayList.add(new UserRes(1, "Truong Trong Khanh", "FinalProject/app/src/main/res/drawable/user.jpg"));
        userArrayList.add(new UserRes(2, "Hoang Quoc Bao", "FinalProject/app/src/main/res/drawable/user.jpg"));
        userArrayList.add(new UserRes(3, "Nguyen Tan Hieu", "FinalProject/app/src/main/res/drawable/user.jpg"));
        userArrayList.add(new UserRes(4, "Kha Vinh Dat", "FinalProject/app/src/main/res/drawable/user.jpg"));
        userArrayList.add(new UserRes(5, "Nguyen Quoc Su", "FinalProject/app/src/main/res/drawable/user.jpg"));
        userArrayList.add(new UserRes(6, "Tran Minh Truong", "FinalProject/app/src/main/res/drawable/user.jpg"));
        userArrayList.add(new UserRes(7, "Nguyen Ho Huu Bang", "FinalProject/app/src/main/res/drawable/user.jpg"));

        recyclerView = binding.recyclerview;
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new ChatAdapter(ChatActivity.this, userArrayList);
        recyclerView.setAdapter(adapter);

        DividerItemDecoration dividerHorizontal =
                new DividerItemDecoration(this, DividerItemDecoration.VERTICAL);

        recyclerView.addItemDecoration(dividerHorizontal);

        handler = new ChatClickHandler(this);
        binding.setClickHandler(handler);

    }

    public class ChatClickHandler{
        Context context;
        public ChatClickHandler(Context context){
            this.context = context;
        }
        public void backClick(View view){
            Intent myIntent = new Intent(ChatActivity.this, UserActivity.class);
            startActivity(myIntent);
        }
    }
}