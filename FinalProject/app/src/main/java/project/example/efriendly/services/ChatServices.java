package project.example.efriendly.services;

import android.app.Service;
import android.content.Intent;
import android.os.IBinder;
import android.util.Log;

import project.example.efriendly.client.RetrofitClientGenerator;

public class ChatServices extends Service {

    private static final String TAG = "ChatServices";

    ChatService chatService;
    ConversationService conversationService;

    public ChatServices() {
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        Log.i(TAG, "OnDestroy");
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Log.i(TAG, "OnStart Called");
        chatService = RetrofitClientGenerator.getService(ChatService.class);
        conversationService = RetrofitClientGenerator.getService(ConversationService.class);

        Runnable r = new Runnable() {
            @Override
            public void run() {
                for (int i = 0 ;i<5;i++){
                    long futureTime = System.currentTimeMillis()+5000;
                    while(System.currentTimeMillis() < futureTime){
                        synchronized (this){
                            try{
                                wait(futureTime-System.currentTimeMillis());
                                Log.i(TAG, "Services is running");
                            }
                            catch (Exception exception) {}
                        }
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