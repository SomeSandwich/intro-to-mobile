package project.example.efriendly.activities.userFragments;

import androidx.core.content.res.ResourcesCompat;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import android.widget.Toast;

import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;
import java.util.Vector;

import lombok.NonNull;
import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.HomepageItemAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Category.CategoryRes;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.ActivityHomepageBinding;
import project.example.efriendly.services.CategoryService;
import project.example.efriendly.services.PostService;
import project.example.efriendly.ultilities.HomepageItemList;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class HomepageActivity extends Fragment implements DatabaseConnection {
    UserActivity main;
    Context context = null;
    FragmentTransaction ft;

    public ActivityHomepageBinding binding;
    private ActivityHomepageClickHandler handlers;

    private CategoryService categoryService;

    private PostService postService;

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
        binding = ActivityHomepageBinding.inflate(inflater, container, false);
        handlers = new ActivityHomepageClickHandler(context);
        SearchBarCartChatActivity searchbar = new SearchBarCartChatActivity();
        ft = getParentFragmentManager().beginTransaction();
        ft.replace(R.id.searchBarFragment, searchbar).commit();
        categoryService = RetrofitClientGenerator.getService(CategoryService.class);
        postService = RetrofitClientGenerator.getService(PostService.class);

        addCategory();
        addAdapter();

        return binding.getRoot();
    }
    private void addAdapter(){
        Call<List<PostRes>> postServiceCall = postService.GetNewest(15);
        postServiceCall.enqueue(new Callback<List<PostRes>>() {
            @Override
            public void onResponse(Call<List<PostRes>> call, Response<List<PostRes>> response) {

                if (response.isSuccessful()){
                    List<PostRes> posts = new ArrayList<>();
                    posts = response.body();

                    List<HomepageItemList> postListView = new ArrayList<>();

                    for (int i = 0;i<posts.size();i+=2){
                        HomepageItemList pairPost = new HomepageItemList();
                        try{
                            pairPost.setLeftItem(posts.get(i));
                            pairPost.setRightItem(posts.get(i+1));
                        }
                        catch (IndexOutOfBoundsException err) {Log.d("debug", err.getLocalizedMessage());}
                        postListView.add(pairPost);
                    }
                    HomepageItemAdapter adapter = new HomepageItemAdapter(main, postListView);
                    binding.ListItems.setAdapter(adapter);
                }
                else {
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(main, message, Toast.LENGTH_LONG).show();
                }
            }
            @Override
            public void onFailure(Call<List<PostRes>> call, Throwable t) {
                String message = t.getLocalizedMessage();
                Toast.makeText(main, message, Toast.LENGTH_LONG).show();
            }
        });

    }

    private android.widget.Button createCategory(int id, String name, Integer img ){
        android.widget.Button btn = new android.widget.Button(context);

        LinearLayout.LayoutParams btnLayout = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WRAP_CONTENT,
                LinearLayout.LayoutParams.WRAP_CONTENT);
        btnLayout.setMargins(30, 30, 30, 30);
        btn.setLayoutParams(btnLayout);
        btn.setId(id);
        btn.setOnClickListener(handlers.CategoryClick());

        Drawable top = ResourcesCompat.getDrawable(getResources(), R.drawable.likebutton, null);
        btn.setCompoundDrawablesRelativeWithIntrinsicBounds(null, top, null, null);

        btn.setPadding(0, 30, 0,0);
        btn.setBackgroundResource(R.drawable.category_frame);
        btn.setText(name);
        return btn;
    }
    private void addCategory(){
        LinearLayout ll = binding.innerLay;

        Call<List<CategoryRes>> categoryServiceCall = categoryService.getAll();
        categoryServiceCall.enqueue(new Callback<List<CategoryRes>>() {
            @Override
            public void onResponse(Call<List<CategoryRes>> call, Response<List<CategoryRes>> response) {
                if (response.isSuccessful()){
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
                    }
                    catch (NullPointerException err){
                        String message = "An error occurred please try again later ...";
                        Toast.makeText(main, message, Toast.LENGTH_LONG).show();
                    }
                }
                else {
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(main, message, Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<List<CategoryRes>> call, Throwable t) {
                String message = t.getLocalizedMessage();
                Toast.makeText(main, message, Toast.LENGTH_LONG).show();
            }
        });
    }
    public class ActivityHomepageClickHandler{
        Context context;
        public ActivityHomepageClickHandler(Context context){
            this.context = context;
        }
        public View.OnClickListener CategoryClick(){
            return new View.OnClickListener(){
                @Override
                public void onClick(View view){
                    int id = view.getId();
                    Log.d("Debug", "Click id category " + Integer.toString(id));
                }
            };
        }
    }
}