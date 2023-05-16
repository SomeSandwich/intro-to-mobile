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
import project.example.efriendly.activities.MessageActivity;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.ChatAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.data.model.Conversation.ConversationRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityChatBinding;
import project.example.efriendly.services.ConversationService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ChatActivity extends AppCompatActivity {
    private ActivityChatBinding binding;
    private RecyclerView recyclerView;
    private ChatAdapter adapter;
    private List<UserRes> userArrayList;
    private ChatClickHandler handler;

    ConversationService conversationService;
    ChatActivity.ClickListener listener;


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
        listener = new ClickListener();

        conversationService = RetrofitClientGenerator.getService(ConversationService.class);
        binding.processBar.setVisibility(View.VISIBLE);
        conversationService.GetUserBySelf().enqueue(new Callback<List<UserRes>>() {
            @Override
            public void onResponse(Call<List<UserRes>> call, Response<List<UserRes>> response) {
                if(response.isSuccessful() && response.body() != null){
                    userArrayList = response.body();
                    recyclerView = binding.recyclerview;
                    recyclerView.setLayoutManager(new LinearLayoutManager(getApplicationContext()));
                    adapter = new ChatAdapter(ChatActivity.this, userArrayList, listener);
                    recyclerView.setAdapter(adapter);
                    DividerItemDecoration dividerHorizontal = new DividerItemDecoration(getApplicationContext(), DividerItemDecoration.VERTICAL);
                    recyclerView.addItemDecoration(dividerHorizontal);
                    binding.processBar.setVisibility(View.INVISIBLE);
                }
                else{
                    Toast.makeText(getApplicationContext(), "Error when get conversation list", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<List<UserRes>> call, Throwable t) {
                Toast.makeText(getApplicationContext(), "Can't connect to server", Toast.LENGTH_LONG).show();
            }
        });
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
    public class ClickListener{
        public void ConvClick(int index){
            binding.processBar.setVisibility(View.VISIBLE);
            conversationService.GetBySelf().enqueue(new Callback<List<ConversationRes>>() {
                @Override
                public void onResponse(Call<List<ConversationRes>> call, Response<List<ConversationRes>> response) {
                    if(response.isSuccessful() && response.body() != null){
                        Intent intent = new Intent(ChatActivity.this, MessageActivity.class);
                        intent.putExtra("conversation_id", response.body().get(index).getId());
                        startActivity(intent);
                        binding.processBar.setVisibility(View.INVISIBLE);
                    }
                    else {
                        Toast.makeText(getApplicationContext(), "Cant' download conversation list", Toast.LENGTH_LONG).show();
                    }
                }
                @Override
                public void onFailure(Call<List<ConversationRes>> call, Throwable t) {
                    Toast.makeText(getApplicationContext(), "Can't connect to server", Toast.LENGTH_LONG).show();
                }
            });
        }
    }
}