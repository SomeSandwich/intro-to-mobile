package project.example.efriendly.activities;

import static project.example.efriendly.constants.DatabaseConnection.IMAGE_URL;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.FragmentTransaction;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.StaggeredGridLayoutManager;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.target.CustomTarget;
import com.bumptech.glide.request.transition.Transition;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.userFragments.SearchBarCartLoginActivity;
import project.example.efriendly.adapter.AnonymousHomepageAdapter;
import project.example.efriendly.adapter.CategoryItemAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.data.model.Category.CategoryRes;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.ActivityAnonymousHomepageBinding;
import project.example.efriendly.services.CategoryService;
import project.example.efriendly.services.PostService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AnonymousHomepageActivity extends AppCompatActivity {
    ActivityAnonymousHomepageBinding binding;
    FragmentTransaction ft;
    SearchBarCartLoginActivity searchBar = new SearchBarCartLoginActivity();
    private CategoryService categoryService;
    private PostService postService;
    AnonymousHomepageActivity.ClickListener listener = new AnonymousHomepageActivity.ClickListener();

    @Override
    public boolean dispatchTouchEvent(MotionEvent ev) { //Disable keyboard when click around
        View view = getCurrentFocus();
        if (view != null && (ev.getAction() == MotionEvent.ACTION_UP || ev.getAction() == MotionEvent.ACTION_MOVE) && view instanceof EditText && !view.getClass().getName().startsWith("android.webkit.")) {
            int[] scrcoords = new int[2];
            view.getLocationOnScreen(scrcoords);
            float x = ev.getRawX() + view.getLeft() - scrcoords[0];
            float y = ev.getRawY() + view.getTop() - scrcoords[1];
            if (x < view.getLeft() || x > view.getRight() || y < view.getTop() || y > view.getBottom())
                ((InputMethodManager) this.getSystemService(Context.INPUT_METHOD_SERVICE)).hideSoftInputFromWindow((this.getWindow().getDecorView().getApplicationWindowToken()), 0);
        }
        return super.dispatchTouchEvent(ev);
    }

    @SuppressLint("AppCompatMethod")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getSupportActionBar().hide();

        try {
            binding = ActivityAnonymousHomepageBinding.inflate(getLayoutInflater());
            setContentView(binding.getRoot());
            categoryService = RetrofitClientGenerator.getService(CategoryService.class);
            postService = RetrofitClientGenerator.getService(PostService.class);

            binding.processBar.setVisibility(View.VISIBLE);

            ft = getSupportFragmentManager().beginTransaction();
            ft.replace(binding.searchBarFragment.getId(), searchBar).commit();
            addCategory();
            addItems();
            binding.processBar.setVisibility(View.INVISIBLE);
        } catch (IOException e) {
            Toast.makeText(this, "Can't connect to server", Toast.LENGTH_LONG).show();
        }

    }
    private void addItems() throws IOException {
        Response<List<PostRes>> response = postService.GetNewest(15).execute();
        if (response.isSuccessful() && response.body() != null){
            AnonymousHomepageAdapter adapter = new AnonymousHomepageAdapter(response.body(), getApplicationContext(), listener);
            binding.ListItems.setAdapter(adapter);
            binding.ListItems.setLayoutManager(new StaggeredGridLayoutManager(2, StaggeredGridLayoutManager.VERTICAL));
        }
        else{
            Toast.makeText(this, "Can't download item list", Toast.LENGTH_SHORT).show();
        }
    }
    private void addCategory() throws IOException{
        Response<List<CategoryRes>> response = categoryService.getAll().execute();
        if (response.isSuccessful() && response.body() != null){
            List<CategoryRes> category = response.body();
            CategoryItemAdapter adapter = new CategoryItemAdapter(getApplicationContext(), category, listener);
            binding.categories.setAdapter(adapter);
            binding.categories.setLayoutManager(new LinearLayoutManager(getApplicationContext(), LinearLayoutManager.HORIZONTAL, false));
        }
        else {
            Toast.makeText(this, "Can't download category", Toast.LENGTH_SHORT).show();
        }
    }

    public class ClickListener {
        public void Click(PostRes post) {
        }
        public void CategoryClick(int cateId){
            Call<List<PostRes>> postCateCall = postService.GetByCateId(cateId);
            postCateCall.enqueue(new Callback<List<PostRes>>() {
                @Override
                public void onResponse(Call<List<PostRes>> call, Response<List<PostRes>> response) {
                    List<PostRes> posts = response.body();
                    AnonymousHomepageAdapter adapter = new AnonymousHomepageAdapter(posts, getApplicationContext(), listener);
                    binding.ListItems.setAdapter(adapter);
                    binding.ListItems.setLayoutManager(new StaggeredGridLayoutManager(2, StaggeredGridLayoutManager.VERTICAL));
                }
                @Override
                public void onFailure(Call<List<PostRes>> call, Throwable t) {
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                }
            });
        }
    }
}