package project.example.efriendly.activities.userFragments;

import androidx.core.content.res.ResourcesCompat;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
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
import java.util.List;
import java.util.Vector;

import lombok.NonNull;
import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Category.CategoryRes;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.databinding.ActivityHomepageBinding;
import project.example.efriendly.services.CategoryService;
import project.example.efriendly.services.PostService;
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
        addItem();
        return binding.getRoot();
    }
    private LinearLayout createNewItem(String des, Bitmap img, String price){
        LinearLayout result = new LinearLayout(context);

        result.setLayoutParams(new TableRow.LayoutParams(0, TableRow.LayoutParams.WRAP_CONTENT, 1.0f));
        result.setGravity(Gravity.FILL);
        result.setOrientation(LinearLayout.VERTICAL);
        result.setBackgroundResource(R.drawable.clothes_frame);

        ImageView clothImg = new ImageView(context);

        clothImg.setLayoutParams(new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WRAP_CONTENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));

        clothImg.setPadding(10, 10, 10, 10);
        clothImg.setImageBitmap(img);
        clothImg.setMaxHeight(450);
        clothImg.setMaxWidth(500);
        clothImg.setScaleType(ImageView.ScaleType.FIT_CENTER);
        clothImg.setAdjustViewBounds(true);

        clothImg.setBackgroundResource(R.drawable.clothes_frame);

        TextView clothDes = new TextView(context);
        clothDes.setLayoutParams(new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WRAP_CONTENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        clothDes.setGravity(Gravity.CENTER);
        clothDes.setText(des);

        TextView prices = new TextView(context);
        prices.setLayoutParams(new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WRAP_CONTENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        prices.setGravity(Gravity.CENTER);
        prices.setText(price);


        result.addView(clothImg, new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MATCH_PARENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        result.addView(clothDes, new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MATCH_PARENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        result.addView(prices, new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MATCH_PARENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        result.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

            }
        });

        return result;
    }
    private void addItem(){
        TableLayout tl = binding.ItemList;

        Call<List<PostRes>> postServiceCall = postService.GetNewest(10);
        postServiceCall.enqueue(new Callback<List<PostRes>>() {
            @Override
            public void onResponse(Call<List<PostRes>> call, Response<List<PostRes>> response) {
                if(response.isSuccessful()) {
                    List<PostRes> posts = response.body();
                    TableRow tr = tr = new TableRow(context);
                    for (int i=0;i<posts.size();i++){
                        Vector<Bitmap> imgBitmap = new Vector<>();
                        for (int j = 0; j < posts.get(i).getMediaPath().size(); j++) {
                            try {
                                URL newUrl = new URL(IMAGE_URL + posts.get(i).getMediaPath().get(j));
                                URLConnection conn = newUrl.openConnection();
                                Bitmap mIcon_val = BitmapFactory.decodeStream(conn.getInputStream());
                                imgBitmap.add(mIcon_val);
                            } catch (Exception err) {
                                Log.d("Debug", err.getMessage());
                            }
                        }
                        posts.get(i).setImgBitmap(imgBitmap);

                        if (i % 2 == 0) tr = new TableRow(context);

                        tr.setLayoutParams(new TableRow.LayoutParams(
                                TableRow.LayoutParams.MATCH_PARENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        LinearLayout b = createNewItem(
                                posts.get(i).getCaption(), posts.get(i).getImgBitmap().get(0), posts.get(i).getPrice().toString() + "VND");
                        tr.addView(b);
                        if (i%2==1)
                            tl.addView(tr, new TableLayout.LayoutParams(
                                    TableLayout.LayoutParams.MATCH_PARENT,
                                    TableLayout.LayoutParams.WRAP_CONTENT));
                    }

                    if (posts.size() % 2 == 1) tl.addView(tr, new TableLayout.LayoutParams(
                            TableLayout.LayoutParams.MATCH_PARENT,
                            TableLayout.LayoutParams.WRAP_CONTENT));
                }
                else{
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(main, message, Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<List<PostRes>> call, Throwable t) {
                String message = "An error occurred please try again later ...";
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