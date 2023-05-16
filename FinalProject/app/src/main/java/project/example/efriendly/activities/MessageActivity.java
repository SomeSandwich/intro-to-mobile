package project.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;
import androidx.recyclerview.widget.LinearLayoutManager;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.bumptech.glide.Glide;

import java.io.IOException;
import java.time.LocalDateTime;
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
import project.example.efriendly.services.ChatServices;
import project.example.efriendly.services.ConversationService;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
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

    private static final String ACTION_STRING_SERVICE = "ToService";
    private static final String ACTION_STRING_ACTIVITY = "ToActivity";

    private void sendBroadcast() {
        Intent new_intent = new Intent();
        new_intent.setAction(ACTION_STRING_SERVICE);
        sendBroadcast(new_intent);
    }
    ConversationService conversationService;
    UserService userService;
    ChatService chatService;
    int conversationId;
    private ActivityMessageBinding binding;
    private MessagesAdapter adapter;
    Intent realTimeChat;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getSupportActionBar().hide();

        if (activityReceiver != null) {
            IntentFilter intentFilter = new IntentFilter(ACTION_STRING_ACTIVITY);
            registerReceiver(activityReceiver, intentFilter);
        }

        conversationService = RetrofitClientGenerator.getService(ConversationService.class);
        userService = RetrofitClientGenerator.getService(UserService.class);
        chatService = RetrofitClientGenerator.getService(ChatService.class);

        binding = DataBindingUtil.setContentView(this, R.layout.activity_message);
        binding.setClickHandler(new ClickHandler(this.getApplicationContext()));
        binding.processBar.setVisibility(View.VISIBLE);
        if (getIntent().getExtras() != null){
            conversationId = getIntent().getExtras().getInt("conversation_id");
            conversationService.GetByConvId(conversationId).enqueue(new Callback<ConversationRes>() {
                @Override
                public void onResponse(Call<ConversationRes> call, Response<ConversationRes> response) {
                    if (response.isSuccessful()) {
                        final ConversationRes conv = response.body();
                        userService.GetSelf().enqueue(new Callback<UserRes>() {
                            @Override
                            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                                if (response.isSuccessful() && conv != null){
                                    final UserRes sender = response.body();
                                    for (int i = 0; i <  conv.getParticipations().size(); i++) {
                                        userService.GetById(conv.getParticipations().get(i).getUserId()).enqueue(new Callback<UserRes>() {
                                            @Override
                                            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                                                if (response.isSuccessful() && sender != null){
                                                    UserRes receiver = response.body();
                                                    if (receiver.getId().equals(sender.getId())) return;
                                                    if (receiver.getAvatarPath() != null) {
                                                        Glide.with(getApplicationContext())
                                                                .load(IMAGE_URL + receiver.getAvatarPath())
                                                                .placeholder(R.drawable.placeholder)
                                                                .into(binding.ivAvatar);
                                                    }
                                                    else{
                                                        binding.ivAvatar.setImageResource(R.drawable.user);
                                                    }
                                                    binding.username.setText(receiver.getName());
                                                    adapter = new MessagesAdapter(getApplicationContext(), sender, receiver, conv.getMessages());
                                                    binding.recyclerViewOfSpecific.setAdapter(adapter);
                                                    binding.recyclerViewOfSpecific.setLayoutManager(new LinearLayoutManager(getApplicationContext()));
                                                    binding.recyclerViewOfSpecific.getLayoutManager().scrollToPosition(conv.getMessages().size() - 1);
                                                    binding.processBar.setVisibility(View.INVISIBLE);

                                                    realTimeChat = new Intent(MessageActivity.this, ChatServices.class);
                                                    realTimeChat.putExtra("conversation_id", conv.getId());
                                                    startService(realTimeChat);

                                                }
                                                else{
                                                    Toast.makeText(getApplicationContext(), "Can't get receiver", Toast.LENGTH_LONG).show();
                                                }
                                            }
                                            @Override
                                            public void onFailure(Call<UserRes> call, Throwable t) {
                                                Toast.makeText(getApplicationContext(), "Can't connect to server", Toast.LENGTH_LONG).show();
                                            }
                                        });
                                    }
                                }
                                else{ Toast.makeText(getApplicationContext(), "Can't get sender to server", Toast.LENGTH_LONG).show(); }
                            }
                            @Override
                            public void onFailure(Call<UserRes> call, Throwable t) {
                                Toast.makeText(getApplicationContext(), "Can't connect to server", Toast.LENGTH_LONG).show();
                            }
                        });
                    }
                    else{ Toast.makeText(getApplicationContext(), "Can't get conversation by id", Toast.LENGTH_LONG).show(); }

                }
                @Override
                public void onFailure(Call<ConversationRes> call, Throwable t) {
                    Toast.makeText(getApplicationContext(), "Can't connect to server", Toast.LENGTH_LONG).show();
                }
            });
        }
        else Log.d("debug", "Conversation id null");
    }
    @Override
    public void onDestroy() {
        try {
            stopService(realTimeChat);
            unregisterReceiver(activityReceiver);
        }
        catch (RuntimeException exception) {Log.d("Debug", "Can't get Services");}
        super.onDestroy();
    }

    private final BroadcastReceiver activityReceiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            try {
                Response<ConversationRes> res = conversationService.GetByConvId(conversationId).execute();
                if (res.body()!=null && res.isSuccessful()){
                    for (int i = 0; i < res.body().getMessages().size() - adapter.messages.size(); i++){
                        adapter.messages.add(res.body().getMessages().get(adapter.messages.size() + i));
                        adapter.notifyItemInserted(adapter.messages.size() - 1);
                        binding.recyclerViewOfSpecific.getLayoutManager().scrollToPosition(adapter.messages.size() - 1);
                    }
                }
            }
            catch (IOException exception) {Toast.makeText(context, "Can't connect to server", Toast.LENGTH_LONG).show();}
            catch (RuntimeException exception) {Log.d("Debug", "Can't get to server");}
        }
    };
    public class ClickHandler{
        Context context;

        public ClickHandler(Context context){
            this.context = context;
        }

        public void sendClick(View view){
            String mess = binding.getMessage.getText().toString();
            if (mess.equals("")) return;
            chatService.Create(conversationId, new CreateMessageReq(mess)).enqueue(new Callback<Integer>() {
                @Override
                public void onResponse(Call<Integer> call, Response<Integer> response) {
                    if (response.isSuccessful()){
                        binding.getMessage.setText("");
                        Toast.makeText(context, "Sent", Toast.LENGTH_SHORT).show();
                    }
                    else Toast.makeText(context, "Can't send message", Toast.LENGTH_SHORT).show();
                }

                @Override
                public void onFailure(Call<Integer> call, Throwable t) {
                    Toast.makeText(context, "Fail to connect to server", Toast.LENGTH_SHORT).show();
                }
            });
        }

        public void back(View view){
            try {
                stopService(realTimeChat);
                unregisterReceiver(activityReceiver);
            }
            catch (RuntimeException exception) {Log.d("Debug", "Can't get Services");}
            finish();
        }
    }
}