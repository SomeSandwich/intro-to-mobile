package project.example.efriendly.adapter;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CompoundButton;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.squareup.picasso.Picasso;

import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.userFragments.CartActivity;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Cart.CartItem;
import project.example.efriendly.data.model.Cart.CartRes;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.CustomCartItemsBinding;
import project.example.efriendly.helper.CartManagement;
import project.example.efriendly.holder.CartHolder;
import project.example.efriendly.services.ChangeNumberItemsListener;
import project.example.efriendly.services.PostService;
import project.example.efriendly.services.UserService;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import java.util.ArrayList;

import project.example.efriendly.R;
import project.example.efriendly.activities.MessageActivity;
import project.example.efriendly.activities.userFragments.CartActivity;
import project.example.efriendly.activities.userFragments.ChatActivity;
import project.example.efriendly.data.model.User.UserRes;

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
        holder.checkBox.setChecked(listener.checkList.get(index).equals(Boolean.TRUE));
        holder.checkBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                listener.checkList.set(index, !listener.checkList.get(index));
            }
        });

        int PostId = carts.get(index).getPostId();
        postService.GetById(PostId).enqueue(new Callback<PostRes>() {
            @Override
            public void onResponse(Call<PostRes> call, Response<PostRes> response) {
                if(response.isSuccessful()){
                    PostRes post = response.body();
                    Glide.with(context)
                            .load(IMAGE_URL + post.getMediaPath().get(0)).placeholder(R.drawable.placeholder)
                            .into(holder.productImg);
                    holder.txtProductName.setText(post.getCaption());
                    holder.txtPrice.setText(post.getPrice().toString());
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

//public class CartAdapter extends RecyclerView.Adapter<CartAdapter.ViewHolder> {
//    private ArrayList<CartItem> cartItemArrayList;
//    private CartManagement cartManagement;
//    private ChangeNumberItemsListener changeNumberItemsListener;
//
//    public CartAdapter(ArrayList<CartItem> cartItemArrayList, Context context, ChangeNumberItemsListener changeNumberItemsListener) {
//        this.cartItemArrayList = cartItemArrayList;
//        this.cartManagement = new CartManagement(context);
//        this.changeNumberItemsListener = changeNumberItemsListener;
//    }
//
//    @Override
//    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
//        View inflate = LayoutInflater.from(parent.getContext()).inflate(R.layout.custom_cart_items, parent, false);
//        return new ViewHolder(inflate);
//    }
//
//    @Override
//    public void onBindViewHolder(@NonNull CartAdapter.ViewHolder holder, int position) {
//        holder.productName.setText(cartItemArrayList.get(position).getProductName());
//        holder.unitPrice.setText(String.valueOf(cartItemArrayList.get(position).getUnitPrice()));
//        holder.totalMoney.setText(String.valueOf(cartItemArrayList.get(position).getNumInCart() * cartItemArrayList.get(position).getUnitPrice()));
//        holder.numInCart.setText(String.valueOf(cartItemArrayList.get(position).getNumInCart()));
//
//        int drawableResourceId = holder.itemView.getContext().getResources().getIdentifier(cartItemArrayList.get(position).getMediaPath(), "drawable",
//                holder.itemView.getContext().getPackageName());
//
//        Glide.with(holder.itemView.getContext())
//                .load(drawableResourceId)
//                .into(holder.ivProduct);
//
//        holder.btnPlus.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                cartManagement.plusNumberProduct(cartItemArrayList, position, new ChangeNumberItemsListener() {
//                    @Override
//                    public void changed() {
//                        notifyDataSetChanged();
//                        changeNumberItemsListener.changed();
//                    }
//                });
//            }
//        });
//
//        holder.btnMinus.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                cartManagement.minusNumberProduct(cartItemArrayList, position, new ChangeNumberItemsListener() {
//                    @Override
//                    public void changed() {
//                        notifyDataSetChanged();
//                        changeNumberItemsListener.changed();
//                    }
//                });
//            }
//        });
//    }
//
//    @Override
//    public int getItemCount() {
//        return cartItemArrayList.size();
//    }
//
//    public class ViewHolder extends RecyclerView.ViewHolder {
//        TextView productName, unitPrice, numInCart, totalMoney;
//        ImageView ivProduct, btnPlus, btnMinus;
//
//        public ViewHolder(@NonNull View itemView) {
//            super(itemView);
//            productName = itemView.findViewById(R.id.txtProductName);
//            unitPrice = itemView.findViewById(R.id.unitPrice);
//            ivProduct = itemView.findViewById(R.id.ivProduct);
//            totalMoney = itemView.findViewById(R.id.txtTotalMoney);
//            numInCart = itemView.findViewById(R.id.txtNumItems);
//            btnPlus = itemView.findViewById(R.id.btnPlusCart);
//            btnMinus = itemView.findViewById(R.id.btnMinusCart);
//        }
//    }
//}