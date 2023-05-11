package project.example.efriendly.activities;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.content.res.ResourcesCompat;
import androidx.fragment.app.FragmentTransaction;
import androidx.recyclerview.widget.StaggeredGridLayoutManager;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
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

import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.userFragments.HomepageActivity;
import project.example.efriendly.activities.userFragments.SearchBarCartLoginActivity;
import project.example.efriendly.adapter.AnonymousHomepageAdapter;
import project.example.efriendly.adapter.HomepageAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.data.model.Category.CategoryRes;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.ActivityAnonymousHomepageBinding;
import project.example.efriendly.databinding.ActivityUserBinding;
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

        binding = ActivityAnonymousHomepageBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());
        binding.processBar.setVisibility(View.VISIBLE);

        categoryService = RetrofitClientGenerator.getService(CategoryService.class);
        postService = RetrofitClientGenerator.getService(PostService.class);

        ft = getSupportFragmentManager().beginTransaction();
        ft.replace(binding.searchBarFragment.getId(), searchBar).commit();

        addCategory();
        addAdapter();

        binding.processBar.setVisibility(View.INVISIBLE);

    }

    private void addAdapter() {
        Call<List<PostRes>> postServiceCall = postService.GetNewest(15);
        postServiceCall.enqueue(new Callback<List<PostRes>>() {
            @Override
            public void onResponse(Call<List<PostRes>> call, Response<List<PostRes>> response) {

                if (response.isSuccessful()) {
                    List<PostRes> posts = new ArrayList<>();
                    posts = response.body();

                    AnonymousHomepageAdapter adapter = new AnonymousHomepageAdapter(posts, getApplicationContext(), listener);
                    binding.ListItems.setAdapter(adapter);

                    binding.ListItems.setLayoutManager(
                            new StaggeredGridLayoutManager(2, StaggeredGridLayoutManager.VERTICAL));
                } else {
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<List<PostRes>> call, Throwable t) {
                String message = t.getLocalizedMessage();
                Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
            }
        });

    }

    private android.widget.Button createCategory(int id, String name, Integer img) {
        Context context = getApplicationContext();
        android.widget.Button btn = new android.widget.Button(context);

        LinearLayout.LayoutParams btnLayout = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WRAP_CONTENT,
                LinearLayout.LayoutParams.WRAP_CONTENT);
        btnLayout.setMargins(30, 30, 30, 30);
        btn.setLayoutParams(btnLayout);
        btn.setId(id);
        //btn.setOnClickListener(handlers.CategoryClick());

        Drawable top = ResourcesCompat.getDrawable(getResources(), R.drawable.likebutton, null);
        btn.setCompoundDrawablesRelativeWithIntrinsicBounds(null, top, null, null);

        btn.setPadding(0, 30, 0, 0);
        btn.setBackgroundResource(R.drawable.category_frame);
        btn.setText(name);
        return btn;
    }

    private void addCategory() {
        LinearLayout ll = binding.innerLay;

        Call<List<CategoryRes>> categoryServiceCall = categoryService.getAll();
        categoryServiceCall.enqueue(new Callback<List<CategoryRes>>() {
            @Override
            public void onResponse(Call<List<CategoryRes>> call, Response<List<CategoryRes>> response) {
                if (response.isSuccessful()) {
                    try {
                        List<CategoryRes> category = response.body();
                        int size = category.size();
                        for (int i = 0; i < size; i++) {
                            android.widget.Button newCategory = createCategory(category.get(i).getId(), category.get(i).getDescription(), R.drawable.likebutton);

                            LinearLayout.LayoutParams btnLayout = new LinearLayout.LayoutParams(
                                    LinearLayout.LayoutParams.WRAP_CONTENT,
                                    LinearLayout.LayoutParams.WRAP_CONTENT);
                            btnLayout.setMargins(5, 0, 5, 0);

                            ll.addView(newCategory, btnLayout);
                        }
                    } catch (NullPointerException err) {
                        String message = "An error occurred please try again later ...";
                        Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                    }
                } else {
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<List<CategoryRes>> call, Throwable t) {
                String message = t.getLocalizedMessage();
                Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
            }
        });
    }

    public class ClickListener {
        public void Click(PostRes post) {
        }
    }
}