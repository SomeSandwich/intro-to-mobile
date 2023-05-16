package project.example.efriendly.adapter;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CompoundButton;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;

import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.userFragments.CartActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Cart.CartRes;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.CustomCartItemsBinding;
import project.example.efriendly.holder.CartHolder;
import project.example.efriendly.services.PostService;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CartAdapter extends RecyclerView.Adapter<CartHolder> implements DatabaseConnection {
    private LayoutInflater inflater;
    Context context;
    List<CartRes> carts;
    UserService userService;
    PostService postService;
    CartActivity.ClickListener listener;

    public CartAdapter( Context context, List<CartRes> carts,  CartActivity.ClickListener listener) {
        this.context = context;
        this.carts = carts;
        this.listener = listener;
    }

    @NonNull
    @Override
    public CartHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        Context context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        CustomCartItemsBinding binding = CustomCartItemsBinding.inflate(inflater, parent, false);
        CartHolder cartHolder = new CartHolder(binding);
        return cartHolder;
    }
    @Override
    public void onBindViewHolder(@NonNull CartHolder holder, int position) {
        final int index = holder.getAdapterPosition();
        postService = RetrofitClientGenerator.getService(PostService.class);
        userService = RetrofitClientGenerator.getService(UserService.class);
        try {
            holder.checkBox.setChecked(listener.checkList.get(index).equals(Boolean.TRUE));
            holder.checkBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
                @Override
                public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                    try{
                        listener.checkList.set(index, !listener.checkList.get(index));
                        final long currentTotalPrices = Long.parseLong(listener.binding.Prices.getText().toString()
                                .substring(0,listener.binding.Prices.getText().toString().length() - 1));
                        if (holder.checkBox.isChecked()){
                            postService.GetById(carts.get(index).getPostId()).enqueue(new Callback<PostRes>() {
                                @Override
                                public void onResponse(Call<PostRes> call, Response<PostRes> response) {;
                                    if (response.isSuccessful()){
                                        if (!response.body().getSold())
                                            listener.binding.Prices.setText(String.valueOf(currentTotalPrices + response.body().getPrice()) + "đ");
                                    }
                                    else{
                                        Toast.makeText(context, "Can't get post prices", Toast.LENGTH_SHORT).show();
                                    }
                                }
                                @Override
                                public void onFailure(Call<PostRes> call, Throwable t) {
                                    Toast.makeText(context, "Can't connect to server", Toast.LENGTH_SHORT).show();
                                }
                            });
                        }
                        else if (!holder.checkBox.isChecked() && currentTotalPrices > 0){
                            postService.GetById(carts.get(index).getPostId()).enqueue(new Callback<PostRes>() {
                                @Override
                                public void onResponse(Call<PostRes> call, Response<PostRes> response) {;
                                    if (response.isSuccessful()){
                                        listener.binding.Prices.setText(String.valueOf(currentTotalPrices - response.body().getPrice()) + "đ");
                                    }
                                    else{
                                        Toast.makeText(context, "Can't get post prices", Toast.LENGTH_SHORT).show();
                                    }
                                }
                                @Override
                                public void onFailure(Call<PostRes> call, Throwable t) {
                                    Toast.makeText(context, "Can't connect to server", Toast.LENGTH_SHORT).show();
                                }
                            });
                        }
                    }
                    catch (IndexOutOfBoundsException exception){Log.d("CartAdapter", "out of bound");}
                }
            });
        }
        catch (NullPointerException e) {Log.d("CartActivity", "nullpointer");}
        int PostId = carts.get(index).getPostId();
        postService.GetById(PostId).enqueue(new Callback<PostRes>() {
            @Override
            public void onResponse(Call<PostRes> call, Response<PostRes> response) {
                if(response.isSuccessful() && response.body() != null && !response.body().getSold()){
                    PostRes post = response.body();
                    Glide.with(context)
                            .load(IMAGE_URL + post.getMediaPath().get(0)).placeholder(R.drawable.placeholder)
                            .into(holder.productImg);
                    holder.txtProductName.setText(post.getCaption());
                    holder.txtPrice.setText(post.getPrice().toString()+"đ");
                    userService.GetById(post.getUserID()).enqueue(new Callback<UserRes>() {
                        @Override
                        public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                            if (response.isSuccessful()){
                                holder.txtSeller.setText(response.body().getName());
                            }
                            else{
                                String message = "An error occurred please try again later ...";
                                Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                            }
                        }
                        @Override
                        public void onFailure(Call<UserRes> call, Throwable t) {
                            String message = "An error occurred please try again later ...";
                            Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                        }
                    });
                }
                else if (response.isSuccessful() && response.body() != null && response.body().getSold()){
                    holder.view.setEnabled(false);
                }
                else{
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(context, message, Toast.LENGTH_LONG).show();
                }
            }
            @Override
            public void onFailure(Call<PostRes> call, Throwable t) {
                String message = "An error occurred please try again later ...";
                Toast.makeText(context, message, Toast.LENGTH_LONG).show();
            }
        });
    }

    @Override
    public int getItemCount() {
        return carts.size();
    }

}