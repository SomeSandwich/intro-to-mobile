package project.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.Toast;

import project.example.efriendly.R;
import project.example.efriendly.activities.login.LoginActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.data.model.User.CreateUserReq;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityRegisterBinding;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RegisterActivity extends AppCompatActivity {
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

    public ActivityRegisterBinding binding;
    private RegisterActivity.RegisterActivityClickHandler handlers;
    private UserService userService;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getSupportActionBar().hide();

        binding = DataBindingUtil.setContentView(this, R.layout.activity_register);
        handlers = new RegisterActivity.RegisterActivityClickHandler(this);
        binding.setClickHandler(handlers);

        userService = RetrofitClientGenerator.createService(UserService.class);
    }

    public class RegisterActivityClickHandler{
        Context context;

        public RegisterActivityClickHandler(Context context) {this.context = context;}

        public void backClick(View view){
            Intent myIntent = new Intent(RegisterActivity.this, AnonymousHomepageActivity.class);
            startActivity(myIntent);
        }

        public void signUp(View view){
            if(TextUtils.isEmpty(binding.nameInput.getText().toString())
                || TextUtils.isEmpty(binding.emailInput.getText().toString())
                || TextUtils.isEmpty(binding.passInput.getText().toString())
                || TextUtils.isEmpty(binding.passConfirmInput.getText().toString())
                || TextUtils.isEmpty(binding.phone.getText().toString())
                || TextUtils.isEmpty(binding.address.getText().toString()))
            {
                String message = "All inputs required ..";
                Toast.makeText(RegisterActivity.this, message, Toast.LENGTH_LONG).show();
            }

            else if(binding.passInput.getText() != binding.passConfirmInput.getText()) {
                String message = "Passwords does not match ..";
                Toast.makeText(RegisterActivity.this, message, Toast.LENGTH_LONG).show();
            }

            else {
                CreateUserReq userReq = new CreateUserReq(binding.nameInput.toString(),
                        binding.emailInput.toString(),
                        binding.passInput.toString(),
                        binding.phone.toString(),
                        binding.address.toString());

                registerUser(userReq);
            }
        }
    }

    public void registerUser(CreateUserReq userReq) {
            Call<UserRes> userResCall = userService.CreateUser(userReq);
            userResCall.enqueue(new Callback<UserRes>() {
                @Override
                public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                    if (response.isSuccessful()) {
                        String message = "Successful ..";
                        Toast.makeText(RegisterActivity.this, message, Toast.LENGTH_LONG).show();

                        startActivity(new Intent(RegisterActivity.this, LoginActivity.class));
                        finish();
                    } else {
                        String message = "An error occurred please try again later ...";
                        Toast.makeText(RegisterActivity.this, message, Toast.LENGTH_LONG).show();
                    }
                }

                @Override
                public void onFailure(Call<UserRes> call, Throwable t) {
                    Toast.makeText(RegisterActivity.this, "Failed !", Toast.LENGTH_LONG).show();
                }
            });
    }
}