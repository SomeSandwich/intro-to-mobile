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
import android.widget.Toast;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.ChatAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityChatBinding;
import project.example.efriendly.services.ConversationService;
import retrofit2.Response;

public class ChatActivity extends AppCompatActivity {
    private ActivityChatBinding binding;
    private RecyclerView recyclerView;
    private ChatAdapter adapter;
    private List<UserRes> userArrayList;
    private ChatClickHandler handler;

    ConversationService conversationService;


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

        conversationService = RetrofitClientGenerator.getService(ConversationService.class);
        try{
            Response<List<UserRes>> response = conversationService.GetUserBySelf().execute();
            if(response.isSuccessful() && response.body() != null){
                userArrayList = response.body();
            }
            else{
                Toast.makeText(this, "Error when get conversation list", Toast.LENGTH_SHORT).show();
            }
        }
        catch (IOException e){
            Toast.makeText(this, "Can't connect to server", Toast.LENGTH_LONG).show();
        }
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