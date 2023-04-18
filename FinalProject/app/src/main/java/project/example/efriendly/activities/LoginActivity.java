package project.example.efriendly.activities;

import android.annotation.SuppressLint;


import androidx.databinding.DataBindingUtil;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import androidx.appcompat.app.AppCompatActivity;

import android.text.TextUtils;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import project.example.efriendly.R;
import project.example.efriendly.activities.userFragments.ChatActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.data.model.Auth.LoginReq;
import project.example.efriendly.data.model.Auth.LoginRes;
import project.example.efriendly.databinding.ActivityLoginBinding;
import project.example.efriendly.services.AuthService;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LoginActivity extends AppCompatActivity {
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
    private ActivityLoginBinding binding;
    private LoginActivityClickHandler handlers;
    private AuthService authService;


    @SuppressLint("AppCompatMethod")
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getSupportActionBar().hide();


        binding = DataBindingUtil.setContentView(this, R.layout.activity_login);
        handlers = new LoginActivity.LoginActivityClickHandler(this);
        binding.setClickHandler(handlers);
    }

    public class LoginActivityClickHandler{
        Context context;

        public LoginActivityClickHandler(Context context) {this.context = context;}

        public void backClick(View view){
            Intent myIntent = new Intent(LoginActivity.this, AnonymousHomepageActivity.class);
            startActivity(myIntent);
        }

        public void login(View view){
            if(TextUtils.isEmpty(binding.emailInput.getText().toString())
                    || TextUtils.isEmpty(binding.passInput.getText().toString()))
            {
                String message = "All inputs required ..";
                Toast.makeText(LoginActivity.this, message, Toast.LENGTH_LONG).show();
            } else {
                LoginReq loginReq = new LoginReq(binding.emailInput.getText().toString(),
                        binding.passInput.getText().toString());

                loginUser(loginReq);
            }
        }
    }

    public void loginUser(LoginReq loginReq) {
        authService = RetrofitClientGenerator.getService(AuthService.class);
        Call<LoginRes> loginResCall = authService.Login(loginReq);
        loginResCall.enqueue(new Callback<LoginRes>() {
            @Override
            public void onResponse(Call<LoginRes> call, Response<LoginRes> response) {
                if (response.isSuccessful()) {
                    LoginRes loginRes = response.body();
                    startActivity(new Intent(LoginActivity.this, ChatActivity.class).putExtra("data", loginRes));
                    finish();
                } else {
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(LoginActivity.this, message, Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<LoginRes> call, Throwable t) {
                String message = t.getLocalizedMessage();
                Toast.makeText(LoginActivity.this, message, Toast.LENGTH_LONG).show();
            }
        });
    }
}