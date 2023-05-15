package project.example.efriendly.services;

import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.IBinder;
import android.util.Log;
import android.widget.Toast;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.data.model.Conversation.ConversationRes;
import project.example.efriendly.data.model.Message.MessageRes;
import retrofit2.Response;

public class ChatServices extends Service {
    private static final String ACTION_STRING_SERVICE = "ToService";
    private static final String ACTION_STRING_ACTIVITY = "ToActivity";
    private static final String TAG = "ChatServices";
    private int conversationId;

    ConversationService conversationService;
    private final BroadcastReceiver serviceReceiver  = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {

        }
    };
    private void sendBroadcast() {
        Intent new_intent = new Intent();
        new_intent.setAction(ACTION_STRING_ACTIVITY);
        sendBroadcast(new_intent);
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        Log.i(TAG, "OnDestroy");
        unregisterReceiver(serviceReceiver);
    }

    @Override
    public void onCreate() {
        super.onCreate();
        Log.d(TAG, "onCreate");
        IntentFilter intentFilter = new IntentFilter(ACTION_STRING_SERVICE);
        registerReceiver(serviceReceiver, intentFilter);
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        conversationId = intent.getExtras().getInt("conversation_id");
        Log.i(TAG, "OnStart Called");
        conversationService = RetrofitClientGenerator.getService(ConversationService.class);

        Runnable r = new Runnable() {
            @Override
            public void run() {
                List<MessageRes> messageResList = new ArrayList<MessageRes>();
                while(true){
                    try{
                        Thread.sleep(3000);
                        Response<ConversationRes> res = conversationService.GetByConvId(conversationId).execute();
                        if (res.isSuccessful() && res.body() != null){
                            List<MessageRes> currentList = res.body().getMessages();
                            if (currentList.size() > messageResList.size()) {
                                messageResList = currentList;
                                sendBroadcast();
                            }
                        }
                    }
                    catch (InterruptedException e){
                    }
                    catch (IOException exception){
                        Toast.makeText(ChatServices.this, "Can't connect to server", Toast.LENGTH_SHORT).show();
                    }
                }
            }
        };
        Thread thread = new Thread(r);
        thread.start();

        return Service.START_STICKY;
    }

    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }
}