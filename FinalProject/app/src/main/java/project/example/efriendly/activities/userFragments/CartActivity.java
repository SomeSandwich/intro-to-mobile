package project.example.efriendly.activities.userFragments;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.ScrollView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.data.model.Cart.CartRes;
import project.example.efriendly.databinding.ActivityCartBinding;
import project.example.efriendly.adapter.CartAdapter;
import project.example.efriendly.helper.CartManagement;
import project.example.efriendly.services.CartService;
import project.example.efriendly.services.ChangeNumberItemsListener;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CartActivity extends AppCompatActivity {
    private ActivityCartBinding binding;
    private RecyclerView.Adapter adapter;
    private RecyclerView recyclerViewList;
    TextView txtTotal, txtEmpty;
    private ScrollView scrollView;
    private CartService cartService;
    private CartManagement cartManagement;

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

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Context context = getApplicationContext();
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getSupportActionBar().hide();

        binding = DataBindingUtil.setContentView(this, R.layout.activity_cart);
        cartManagement = new CartManagement(this);

        initView();
        initList();
        calculateCart();
    }//onCreate

    private void initView() {
        txtTotal = binding.txtTotalMoney;
        txtEmpty = binding.txtEmpty;
        scrollView = binding.scrollView;
        recyclerViewList = binding.recyclerview;
    }

    private void initList() {
        LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this, LinearLayoutManager.VERTICAL, false);
        recyclerViewList.setLayoutManager(linearLayoutManager);

        adapter = new CartAdapter(cartManagement.getListCart(), this, new ChangeNumberItemsListener() {
            @Override
            public void changed() {
                calculateCart();
            }
        });

        recyclerViewList.setAdapter(adapter);
        if (cartManagement.getListCart().isEmpty()) {
            txtEmpty.setVisibility(View.VISIBLE);
            scrollView.setVisibility(View.GONE);
        } else {
            txtEmpty.setVisibility(View.GONE);
            scrollView.setVisibility(View.VISIBLE);
        }
    }

    private void calculateCart() {
        txtTotal.setText("$" + cartManagement.getTotalMoney());
    }
}


//public class CartActivity extends AppCompatActivity {
//    private ActivityCartBinding binding;
//    private CartService cartService;
//    private ClickListener listener;
//    private CartActivityClickHandler clickHandler;
//    public CartAdapter adapter;
//
//    @Override
//    public boolean dispatchTouchEvent(MotionEvent ev) { //Disable keyboard when click around
//        View view = getCurrentFocus();
//        if (view != null && (ev.getAction() == MotionEvent.ACTION_UP || ev.getAction() == MotionEvent.ACTION_MOVE) && view instanceof EditText && !view.getClass().getName().startsWith("android.webkit.")) {
//            int[] scrcoords = new int[2];
//            view.getLocationOnScreen(scrcoords);
//            float x = ev.getRawX() + view.getLeft() - scrcoords[0];
//            float y = ev.getRawY() + view.getTop() - scrcoords[1];
//            if (x < view.getLeft() || x > view.getRight() || y < view.getTop() || y > view.getBottom())
//                ((InputMethodManager)this.getSystemService(Context.INPUT_METHOD_SERVICE)).hideSoftInputFromWindow((this.getWindow().getDecorView().getApplicationWindowToken()), 0);
//        }
//        return super.dispatchTouchEvent(ev);
//    }
//
//    @Override
//    protected void onCreate(Bundle savedInstanceState) {
//        super.onCreate(savedInstanceState);
//        Context context = getApplicationContext();
//        //Hide Title
//        requestWindowFeature(Window.FEATURE_NO_TITLE);
//        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
//        getSupportActionBar().hide();
//
//        binding = DataBindingUtil.setContentView(this, R.layout.activity_cart);
//        clickHandler = new CartActivityClickHandler(context);
//        binding.setClickHandler(clickHandler);
//        listener = new ClickListener();
//        cartService = RetrofitClientGenerator.getService(CartService.class);
//
//        cartService.GetSelfCart().enqueue(new Callback<List<CartRes>>() {
//            @Override
//            public void onResponse(Call<List<CartRes>> call, Response<List<CartRes>> response) {
//                if(response.isSuccessful()){
//                    List<Boolean> checkList = new ArrayList<Boolean>(response.body().size());
//                    for (int i = 0; i < response.body().size(); i++) checkList.add(Boolean.FALSE);
//                    listener.setCheckList(checkList);
//                    adapter = new CartAdapter(context, response.body(), listener);
//                    binding.CartList.setAdapter(adapter);
//                    binding.CartList.setLayoutManager(new LinearLayoutManager(getApplicationContext()));
//                }
//                else {
//                    String message = "An error occurred please try again later ...";
//                    Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
//                }
//            }
//
//            @Override
//            public void onFailure(Call<List<CartRes>> call, Throwable t) {
//                String message = "An error occurred please try again later ...";
//                Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
//            }
//        });
//    }
//    public class CartActivityClickHandler{
//        Context context;
//        public CartActivityClickHandler(Context context){
//            this.context = context;
//        }
//        public void back(View view){
//            finish();
//        }
//        public void allCheck(CompoundButton compoundButton, boolean b){
//            for (int i = 0; i < listener.checkList.size();i++) listener.checkList.set(i, binding.cbAll.isChecked());
//            adapter.notifyDataSetChanged();
//        }
//
//        public void DeleteClick(View view){
//            for (int i = 0;i< listener.checkList.size();i++) {
//                if (listener.checkList.get(i).equals(Boolean.TRUE)){
//                    final int position = i;
//                    cartService.RemovePostFromSelfCart(position).enqueue(new Callback<String>() {
//                        @Override
//                        public void onResponse(Call<String> call, Response<String> response) {
//                            if (response.isSuccessful()){
//                                adapter.notifyItemRemoved(position);
//                            }
//                            else {
//                                String message = "An error occurred please try again later ...";
//                                Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
//                            }
//                        }
//
//                        @Override
//                        public void onFailure(Call<String> call, Throwable t) {
//                            String message = "An error occurred please try again later ...";
//                            Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
//                        }
//                    });
//                }
//            }
//        }
//    }
//    public class ClickListener{
//        public List<Boolean> checkList = new ArrayList<Boolean>();
//        public void setCheckList(List<Boolean> checkList) {this.checkList = checkList;}
//
//    }
//}