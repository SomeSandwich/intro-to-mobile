package project.example.efriendly.activities.userFragments;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.DataBindingUtil;
import androidx.recyclerview.widget.LinearLayoutManager;

import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.Toast;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.data.model.Cart.CartRes;
import project.example.efriendly.data.model.Order.OrderDetailReq;
import project.example.efriendly.data.model.Order.OrderReq;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.SuccessRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.ActivityCartBinding;
import project.example.efriendly.adapter.CartAdapter;
import project.example.efriendly.services.CartService;
import project.example.efriendly.services.OrderService;
import project.example.efriendly.services.PostService;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CartActivity extends AppCompatActivity {
    private ActivityCartBinding binding;
    private CartService cartService;
    private PostService postService;
    private OrderService orderService;
    private UserService userService;
    private ClickListener listener;
    private CartActivityClickHandler clickHandler;
    public CartAdapter adapter;
    public List<CartRes> cartList;
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
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Context context = getApplicationContext();
        //Hide Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getSupportActionBar().hide();

        binding = DataBindingUtil.setContentView(this, R.layout.activity_cart);
        clickHandler = new CartActivityClickHandler(context);
        binding.setClickHandler(clickHandler);
        listener = new ClickListener(binding);

        cartService = RetrofitClientGenerator.getService(CartService.class);
        postService = RetrofitClientGenerator.getService(PostService.class);
        orderService = RetrofitClientGenerator.getService(OrderService.class);
        userService = RetrofitClientGenerator.getService(UserService.class);

        binding.processBar.setVisibility(View.VISIBLE);

        cartService.GetSelfCart().enqueue(new Callback<List<CartRes>>() {
            @Override
            public void onResponse(Call<List<CartRes>> call, Response<List<CartRes>> response) {
                if(response.isSuccessful()){
                    cartList = response.body();
                    if (response.body().size() == 0) {
                        binding.processBar.setVisibility(View.INVISIBLE);
                        return;
                    }

                    List<Boolean> checkList = new ArrayList<Boolean>(cartList.size());
                    for (int i = 0; i < cartList.size(); i++) checkList.add(Boolean.FALSE);

                    listener.setCheckList(checkList);
                    adapter = new CartAdapter(context, cartList, listener);
                    binding.CartList.setAdapter(adapter);
                    binding.CartList.setLayoutManager(new LinearLayoutManager(getApplicationContext()));
                    binding.Prices.setText("0đ");
                    binding.processBar.setVisibility(View.INVISIBLE);
                }
                else {
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<List<CartRes>> call, Throwable t) {
                String message = "An error occurred please try again later ...";
                Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
            }
        });
    }
    public class CartActivityClickHandler{
        Context context;
        public CartActivityClickHandler(Context context){
            this.context = context;
        }
        public void back(View view){
            finish();
        }
        public void allCheck(CompoundButton compoundButton, boolean b){
            for (int i = 0; i < listener.checkList.size();i++) {
                listener.checkList.set(i, b);
                adapter.notifyItemChanged(i);
            }
            adapter.notifyDataSetChanged();
        }
        public void DeleteClick(View view){
            if(cartList.size() == 0) return;
            if (binding.cbAll.isChecked()){
                for (int i = 0;i<listener.checkList.size();i++){
                    try {
                        Response<SuccessRes> res = cartService.RemovePostFromSelfCart(cartList.get(i).getPostId()).execute();
                        if (res.isSuccessful()){
                            Toast.makeText(getApplicationContext(),"Success", Toast.LENGTH_LONG).show();
                            listener.checkList.remove(i);
                            cartList.remove(i);
                            adapter.notifyItemRemoved(i);
                            binding.Prices.setText("0đ");
                            i--;
                        }
                        else {
                            String message = "There is an error when deleting.";
                            Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                        }
                    }
                    catch (IOException exception){
                        String message = "There is an error when deleting.";
                        Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                    }
                }
                return;
            }
            for (int i = 0;i<listener.checkList.size();i++){
                if (listener.checkList.get(i).equals(Boolean.TRUE)){
                    try {
                        Response<SuccessRes> res = cartService.RemovePostFromSelfCart(cartList.get(i).getPostId()).execute();
                        if (res.isSuccessful()){
                            Toast.makeText(getApplicationContext(),"Success", Toast.LENGTH_LONG).show();
                            Response<PostRes> post = postService.GetById(cartList.get(i).getPostId()).execute();
                            if (post.isSuccessful()){
                                long updatePrices =
                                        Long.parseLong(binding.Prices.getText().toString().substring(0, binding.Prices.getText().toString().length() - 1))
                                                - post.body().getPrice();
                                binding.Prices.setText(String.valueOf(updatePrices) + "đ");
                            }
                            else {
                                String message = "Can't update total price.";
                                Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                            }
                            listener.checkList.remove(i);
                            cartList.remove(i);
                            adapter.notifyItemRemoved(i);
                            i--;
                        }
                        else {
                            String message = "There is an error when deleting.";
                            Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                        }
                    }
                    catch (IOException exception){
                        String message = "There is an error when deleting.";
                        Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                    }
                }
                finish();
           }
        }
        public void buyClick(View view){
            if (cartList.size() == 0) return;
            binding.processBar.setVisibility(View.VISIBLE);
            userService.GetSelf().enqueue(new Callback<UserRes>() {
                @Override
                public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                    if (response.isSuccessful()){
                        try{
                            List<OrderDetailReq> orderDetailReqList = new ArrayList<OrderDetailReq>();
                            for (int i = 0; i < listener.checkList.size(); i++){
                                if (listener.checkList.get(i)){
                                    Response<PostRes> postResResponse = postService.GetById(cartList.get(i).getPostId()).execute();
                                    if (postResResponse.isSuccessful() && postResResponse.body() != null) {
                                        orderDetailReqList.add(new OrderDetailReq(cartList.get(i).getPostId(), postResResponse.body().getPrice()));
                                    }
                                    else{
                                        Toast.makeText(context, "Can't get post from server", Toast.LENGTH_SHORT).show();
                                    }
                                }
                            }
                            orderService.createOrder(new OrderReq(response.body().getPhoneNumber(), orderDetailReqList)).enqueue(new Callback<SuccessRes>() {
                                @Override
                                public void onResponse(Call<SuccessRes> call, Response<SuccessRes> response) {
                                    if (response.isSuccessful() && response.body() != null){
                                        Toast.makeText(CartActivity.this, response.body().getMessage(), Toast.LENGTH_SHORT).show();
                                        finish();
                                    }
                                    else {
                                        Toast.makeText(context, "Can't buy item", Toast.LENGTH_SHORT).show();
                                        binding.processBar.setVisibility(View.INVISIBLE);
                                    }
                                }
                                @Override
                                public void onFailure(Call<SuccessRes> call, Throwable t) {
                                    Toast.makeText(context, "Can't connect to server", Toast.LENGTH_SHORT).show();
                                }
                            });


                        }
                        catch (IOException exception) {Toast.makeText(context, "Can't connect to server", Toast.LENGTH_SHORT).show();}
                    }
                    else{
                        Toast.makeText(context, "Can't get self", Toast.LENGTH_SHORT).show();
                    }
                }
                @Override
                public void onFailure(Call<UserRes> call, Throwable t) {
                    Toast.makeText(context, "Can't connect to server", Toast.LENGTH_SHORT).show();
                }
            });
        }
    }
    public class ClickListener{
        public List<Boolean> checkList = new ArrayList<Boolean>();
        public ActivityCartBinding binding;
        public ClickListener(ActivityCartBinding binding){
            this.binding = binding;
        }
        public void setCheckList(List<Boolean> checkList) {this.checkList = checkList;}

    }
}