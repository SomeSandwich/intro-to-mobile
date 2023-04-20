package project.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.cardview.widget.CardView;
import androidx.databinding.DataBindingUtil;
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
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.squareup.picasso.Picasso;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

import project.example.efriendly.R;
import project.example.efriendly.adapter.MessagesAdapter;
import project.example.efriendly.databinding.ActivityMessageBinding;

public class MessageActivity extends AppCompatActivity {
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

    private ImageView avatar;
    private TextView username;
    private EditText getMessage;
    private ImageButton btnSendMessage;
    private CardView sendMessageCardView;
    private Toolbar toolbar;
    private String enteredMessage;
    private Intent intent;
    private String receiverName, senderName;
    private Integer receiverId, senderId, receiverRoom, senderRoom;
    private ImageButton btnBack;
    private RecyclerView recyclerView;

    private String currentTime;
    private Calendar calendar;
    private SimpleDateFormat simpleDateFormat;

    private ActivityMessageBinding binding;
    private MessagesAdapter adapter;
    private ArrayList<Messages> messagesArrayList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);

        binding = DataBindingUtil.setContentView(this, R.layout.activity_message);

        getMessage = binding.getmessage;
        sendMessageCardView = binding.carviewofsendmessage;
        btnSendMessage = binding.imageviewsendmessage;
        toolbar = binding.toolbar;
        avatar = binding.ivAvatar;
        username = binding.username;
        btnBack = binding.backbuttonofspecificchat;

        messagesArrayList = new ArrayList<>();
        recyclerView = binding.recyclerviewofspecific;

        LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
        linearLayoutManager.setStackFromEnd(true);
        recyclerView.setLayoutManager(linearLayoutManager);
        adapter = new MessagesAdapter(MessageActivity.this, messagesArrayList);
        recyclerView.setAdapter(adapter);

        intent = getIntent();

        setSupportActionBar(toolbar);
        toolbar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

            }
        });

        calendar = Calendar.getInstance();
        simpleDateFormat = new SimpleDateFormat("hh:mm a");

        senderId = 1;
        receiverId = intent.getIntExtra("id", 0);
        receiverName = intent.getStringExtra("name");
        senderRoom = receiverRoom = receiverId + senderId;

        btnBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                finish();
            }
        });

        username.setText(receiverName);
        String uri = intent.getStringExtra("avatar");
        Picasso.get().load(uri).into(avatar);

        btnSendMessage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                enteredMessage = getMessage.getText().toString();
                if(enteredMessage.isEmpty()) {
                    Toast.makeText(getApplicationContext(), "Enter message first", Toast.LENGTH_SHORT).show();
                } else {
                    Date date = new Date();
                    currentTime = simpleDateFormat.format(calendar.getTime());
                    Messages messages = new Messages(enteredMessage, senderId, date.getTime(), currentTime);

                }
            }
        });
    }
}