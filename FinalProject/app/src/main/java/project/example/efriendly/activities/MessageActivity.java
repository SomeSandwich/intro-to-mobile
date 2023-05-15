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
import android.util.Log;
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

import com.bumptech.glide.Glide;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.adapter.MessagesAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Conversation.ConversationRes;
import project.example.efriendly.data.model.Message.CreateMessageReq;
import project.example.efriendly.data.model.Message.MessageRes;
import project.example.efriendly.data.model.Participation.ParticipationRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityMessageBinding;
import project.example.efriendly.services.ChatService;
import project.example.efriendly.services.ConversationService;
import project.example.efriendly.services.UserService;
import retrofit2.Response;

public class MessageActivity extends AppCompatActivity implements DatabaseConnection {
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
    ConversationService conversationService;
    UserService userService;
    ChatService chatService;
    int conversationId;
    UserRes sender;
    UserRes receiver;
    private ActivityMessageBinding binding;
    private MessagesAdapter adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getSupportActionBar().hide();

        conversationService = RetrofitClientGenerator.getService(ConversationService.class);
        userService = RetrofitClientGenerator.getService(UserService.class);
        chatService = RetrofitClientGenerator.getService(ChatService.class);

        binding = DataBindingUtil.setContentView(this, R.layout.activity_message);
        binding.setClickHandler(new ClickHandler(this.getApplicationContext()));
        if (getIntent().getExtras() != null){
            conversationId = getIntent().getExtras().getInt("conversation_id");
            try{
                Response<ConversationRes> res = conversationService.GetByConvId(conversationId).execute();
                Response<UserRes> getSender = userService.GetSelf().execute();

                if (res.isSuccessful() && res.body() != null && getSender.isSuccessful() && getSender.body() != null){
                    this.sender = getSender.body();
                    List<ParticipationRes> participationResList = res.body().getParticipations();
                    for (int i = 0; i < participationResList.size(); i++) {
                        Response<UserRes> getReceiver = userService.GetById(participationResList.get(i).getUserId()).execute();
                        if (getReceiver.isSuccessful() && getReceiver.body() != null && !getReceiver.body().getId().equals(sender.getId())) {
                            this.receiver = getReceiver.body();
                        }
                    }

                    Log.d("Debug", IMAGE_URL + receiver.getAvatarPath());

                    Glide.with(this)
                            .load(IMAGE_URL + receiver.getAvatarPath())
                            .placeholder(R.drawable.placeholder)
                            .into(binding.ivAvatar);
                    binding.username.setText(receiver.getName());
                    adapter = new MessagesAdapter(this, sender, receiver, res.body().getMessages());
                    binding.recyclerViewOfSpecific.setAdapter(adapter);
                    binding.recyclerViewOfSpecific.setLayoutManager(new LinearLayoutManager(this));
                }
                else{
                    Toast.makeText(this, "Can't get conversation by id", Toast.LENGTH_LONG).show();
                }
            }
            catch (IOException exception) {Toast.makeText(this, "Can't connect to server", Toast.LENGTH_LONG).show();}
        }
        else Log.d("debug", "Conversation id null");
    }
    public class ClickHandler{
        Context context;

        public ClickHandler(Context context){
            this.context = context;
        }

        public void sendClick(View view){
            LocalDateTime SendTime = LocalDateTime.now();
            String mess = binding.getMessage.getText().toString();
            adapter.messages.add(new MessageRes(0, mess, sender.getId(), SendTime.toString()));
            adapter.notifyItemInserted(0);
            try {
                Response<Integer> res = chatService.Create(conversationId, new CreateMessageReq(mess)).execute();
                if (res.isSuccessful()){
                    Toast.makeText(context, "Sent", Toast.LENGTH_SHORT).show();
                }
                else{
                    Toast.makeText(context, "Can't send message", Toast.LENGTH_SHORT).show();
                }
            }
            catch (IOException exception) { Toast.makeText(context, "Fail to connect to server", Toast.LENGTH_SHORT).show();}
        }

        public void back(View view){
            finish();
        }
    }
}