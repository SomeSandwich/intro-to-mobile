package project.example.efriendly.activities.userFragments;

import androidx.appcompat.app.AlertDialog;
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
import project.example.efriendly.activities.LoginActivity;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.constants.StorageHelper;
import project.example.efriendly.data.model.Auth.LoginReq;
import project.example.efriendly.data.model.Auth.LoginRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityProfileBinding;
import project.example.efriendly.services.AuthService;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ProfileActivity extends Fragment implements DatabaseConnection {
    UserActivity main;
    Context context = null;
    private ActivityProfileBinding binding;
    private UserService userService;
    public ProfileActivity() {}

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
        binding = ActivityProfileBinding.inflate(inflater, container,false);
        binding.setClickHandler(new ProfileActivityClickHandler(context));

        userService = RetrofitClientGenerator.getService(UserService.class);
        Call<UserRes> userResCall = userService.GetSelf();
        userResCall.enqueue(new Callback<UserRes>() {
            @Override
            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                if (response.isSuccessful()) {
                    UserRes userRes = response.body();
                    binding.txtUsername.setText(userRes.getName());
                    binding.txtEmail.setText(userRes.getEmail());
                    binding.txtAddress.setText(userRes.getAddress());
                    if (userRes.getAvatarPath() != null) {
                        Glide.with(getActivity())
                                .load(IMAGE_URL + userRes.getAvatarPath())
                                .placeholder(R.drawable.placeholder)
                                .into(binding.userAvatar);
                    }
                    else{
                        binding.userAvatar.setImageResource(R.drawable.user);
                    }
                } else {
                    String message = "An error occurred ...";
                    Toast.makeText(getActivity(), message, Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<UserRes> call, Throwable t) {
                String message = t.getLocalizedMessage();
                Toast.makeText(getActivity(), message, Toast.LENGTH_LONG).show();
            }
        });

        return binding.getRoot();
    }

    public class ProfileActivityClickHandler {
        Context context;
        public ProfileActivityClickHandler(Context context) {this.context = context;}
        public void logout(View view){
            logoutMenu(ProfileActivity.this);
        }
        private void logoutMenu(ProfileActivity profileActivity) {
            AlertDialog.Builder builder = new AlertDialog.Builder(profileActivity.context);
            builder.setTitle("Logout");
            builder.setMessage("Are you sure you want to logout?");
            builder.setPositiveButton("Yes", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    StorageHelper.Token = null;
                    startActivity(new Intent(getActivity(), AnonymousHomepageActivity.class));
                }
            });
            builder.setNegativeButton("No", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                }
            });
            builder.show();
        }
        public void paymentClick(View view){
            main.onMsgFromFragToMain("profile", "payment");
        }
        public void editPostClick(View view){
            main.onMsgFromFragToMain("profile", "editPost");
        }
    }
}