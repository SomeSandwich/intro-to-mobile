package project.example.efriendly.activities.userFragments;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import com.bumptech.glide.Glide;

import project.example.efriendly.R;
import project.example.efriendly.activities.AnonymousHomepageActivity;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.constants.StorageHelper;
import project.example.efriendly.data.model.SuccessRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityPaymentBinding;
import project.example.efriendly.databinding.ActivityProfileBinding;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PaymentActivity extends Fragment implements DatabaseConnection {

    UserActivity main;
    Context context = null;
    private ActivityPaymentBinding binding;
    private UserService userService;
    private UserRes user;
    public PaymentActivity() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        try{
            context = getActivity();
            main = (UserActivity) getActivity();
        }
        catch (IllegalStateException err){
            throw new IllegalStateException("MainActivity must implement callbacks");
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        binding = ActivityPaymentBinding.inflate(inflater, container,false);
        binding.setClickHandler(new PaymentActivity.ClickHandler(context));

        userService = RetrofitClientGenerator.getService(UserService.class);
        userService.GetSelf().enqueue(new Callback<UserRes>() {
            @Override
            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                if(response.isSuccessful()){
                    user = response.body();
                    binding.userMoney.setText(response.body().getMoney().toString() + "đ");
                }
                else{
                    Toast.makeText(main, "Can't get user money", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<UserRes> call, Throwable t) {
                Toast.makeText(main, "Can't connect to server", Toast.LENGTH_SHORT).show();
            }
        });
        return binding.getRoot();
    }

    public class ClickHandler {
        Context context;
        public ClickHandler(Context context) {this.context = context;}

        public void depositClick(View view){

            if (user != null) {
                user.setMoney(user.getMoney() + Double.parseDouble(binding.money.getText().toString()));
                userService.setMoney(user.getId(), user.getMoney().intValue()).enqueue(new Callback<SuccessRes>() {
                    @Override
                    public void onResponse(Call<SuccessRes> call, Response<SuccessRes> response) {
                        Toast.makeText(main, response.body().getMessage(), Toast.LENGTH_SHORT).show();
                        binding.userMoney.setText(user.getMoney().toString() + "đ");
                    }
                    @Override
                    public void onFailure(Call<SuccessRes> call, Throwable t) {
                        Toast.makeText(main, "Can't connect to server", Toast.LENGTH_SHORT).show();
                    }
                });
            }
            binding.money.setText("");
        }

    }
}