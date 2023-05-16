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
}