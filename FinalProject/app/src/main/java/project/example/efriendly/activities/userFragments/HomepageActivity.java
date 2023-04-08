package project.example.efriendly.activities.userFragments;

import androidx.core.content.res.ResourcesCompat;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import android.app.ProgressDialog;
import android.content.Context;
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

import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.databinding.ActivityHomepageBinding;
public class HomepageActivity extends Fragment {
    UserActivity main;
    Context context = null;
    String message = "";

    FragmentTransaction ft;

    public ActivityHomepageBinding binding;
    private ActivityHomepageClickHandler handlers;
    String[] des = {
            "Test Description 1 Test Description 1 Test Description 1 Test Description 1 Test Description 1 ", "Test Description 2",
            "Test Description 3", "Test Description 4",
            "Test Description 5", "Test Description 6",
            "Test Description 7", "Test Description 8",
            "Test Description 9"
    };
    Integer[] img = {
            R.drawable.clothes, R.drawable.clothes,
            R.drawable.clothes, R.drawable.clothes,
            R.drawable.clothes, R.drawable.clothes,
            R.drawable.clothes, R.drawable.clothes,
            R.drawable.clothes
    };

    String[] category = {
            "All", "Men", "Women", "Kid", "Children", "Elder", "Give a way", "Challenge"
    };

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
        binding = ActivityHomepageBinding.inflate(inflater, container,false);
        handlers = new ActivityHomepageClickHandler(context);
        SearchBarCartChatActivity searchbar = new SearchBarCartChatActivity();
        ft = getParentFragmentManager().beginTransaction();
        ft.replace(R.id.searchBarFragment, searchbar).commit();

        addCategory();
        addItem();
        return binding.getRoot();
    }
    private LinearLayout createNewItem(String des, Integer img){
        LinearLayout result = new LinearLayout(context);

        result.setLayoutParams(new TableRow.LayoutParams(
                0,
                TableRow.LayoutParams.WRAP_CONTENT, 1.0f));
        result.setGravity(Gravity.FILL);
        result.setOrientation(LinearLayout.VERTICAL);
        result.setBackgroundResource(R.drawable.clothes_frame);

        ImageView clothImg = new ImageView(context);

        clothImg.setLayoutParams(new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WRAP_CONTENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));

        clothImg.setPadding(10, 10, 10, 10);
        clothImg.setImageResource(img);
        clothImg.setBackgroundResource(R.drawable.clothes_frame);

        TextView clothDes = new TextView(context);
        clothDes.setLayoutParams(new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.WRAP_CONTENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        clothDes.setGravity(Gravity.CENTER);
        clothDes.setText(des);
        result.addView(clothImg, new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MATCH_PARENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        result.addView(clothDes, new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MATCH_PARENT,
                LinearLayout.LayoutParams.WRAP_CONTENT));
        return result;
    }
    private void addItem(){
        TableLayout tl = binding.ItemList;
        TableRow tr = tr = new TableRow(context);

        for (int i=0;i<des.length;i++){
            if (i % 2 == 0) tr = new TableRow(context);
            tr.setLayoutParams(new TableRow.LayoutParams(
                    TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT));
            LinearLayout b = createNewItem(des[i], img[i]);
            tr.addView(b);
            if (i%2==1)
                tl.addView(tr, new TableLayout.LayoutParams(
                        TableLayout.LayoutParams.MATCH_PARENT,
                        TableLayout.LayoutParams.WRAP_CONTENT));
        }

        if (des.length % 2 == 1) tl.addView(tr, new TableLayout.LayoutParams(
                TableLayout.LayoutParams.MATCH_PARENT,
                TableLayout.LayoutParams.WRAP_CONTENT));
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

        for (int i=0;i<category.length;i++) {
            android.widget.Button newCategory = createCategory(i, category[i], R.drawable.likebutton);

            LinearLayout.LayoutParams btnLayout = new LinearLayout.LayoutParams(
                    LinearLayout.LayoutParams.WRAP_CONTENT,
                    LinearLayout.LayoutParams.WRAP_CONTENT);
            btnLayout.setMargins(5, 0, 5, 0);

            ll.addView(newCategory, btnLayout);
        }
    }
    public class ActivityHomepageClickHandler{
        Context context;
        public ActivityHomepageClickHandler(Context context){
            this.context = context;
        }

        private ProgressDialog progressBar(){
            ProgressDialog mProgress = new ProgressDialog(context);
            mProgress.setTitle("Processing...");
            mProgress.setMessage("Please wait...");
            mProgress.setCancelable(false);
            mProgress.setIndeterminate(true);
            return mProgress;
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