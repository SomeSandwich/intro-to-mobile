package project.example.efriendly.activities.userFragments;

import androidx.fragment.app.Fragment;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.SuccessRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityEditPostBinding;
import project.example.efriendly.databinding.ActivityPaymentBinding;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class EditPostActivity extends Fragment implements DatabaseConnection {

    UserActivity main;
    Context context = null;
    private ActivityEditPostBinding binding;

    public EditPostActivity() {}

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
        binding = ActivityEditPostBinding.inflate(inflater, container,false);
        binding.setClickHandler(new EditPostActivity.ClickHandler(context));


        return binding.getRoot();
    }

    public class ClickHandler {
        Context context;
        public ClickHandler(Context context) {this.context = context;}

    }
}