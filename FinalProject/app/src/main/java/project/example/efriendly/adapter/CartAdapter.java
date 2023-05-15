package project.example.efriendly.adapter;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CompoundButton;
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

//<<<<<<< HEAD
//import java.util.ArrayList;
//
//import project.example.efriendly.R;
//import project.example.efriendly.activities.MessageActivity;
//import project.example.efriendly.activities.userFragments.CartActivity;
//import project.example.efriendly.activities.userFragments.ChatActivity;
//import project.example.efriendly.data.model.User.UserRes;
//
//public class CartAdapter extends RecyclerView.Adapter<CartAdapter.viewHolder> {
//    CartActivity cartActivity;
//    ArrayList<UserRes> userArrayList;
//
//    public CartAdapter(CartActivity cartActivity, ArrayList<UserRes> usersArrayList) {
//        this.cartActivity = cartActivity;
//        this.userArrayList = usersArrayList;
//=======
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
//<<<<<<< HEAD
//    public CartAdapter.viewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
//        View view = LayoutInflater.from(cartActivity).inflate(R.layout.custom_chat_items, parent, false);
//        return new viewHolder(view);
//    }
//
//    @Override
//    public void onBindViewHolder(@NonNull CartAdapter.viewHolder holder, int position) {
//        UserRes user = userArrayList.get(position);
//
//        holder.name.setText(user.getName());
//        Picasso.get().load(user.getAvatar()).into(holder.avatar);
//
//        holder.itemView.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                Intent intent = new Intent(cartActivity, MessageActivity.class);
//                intent.putExtra("id", user.getId());
//                intent.putExtra("name", user.getName());
//                intent.putExtra("avatar", user.getAvatar());
//                cartActivity.startActivity(intent);
//=======
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
//<<<<<<< HEAD
//        return userArrayList.size();
//    }
//
//    public class viewHolder extends RecyclerView.ViewHolder {
//        ImageView avatar;
//        TextView name;
//        public viewHolder(@NonNull View itemView) {
//            super(itemView);
//            avatar = itemView.findViewById(R.id.ivAvatar);
//            name = itemView.findViewById(R.id.txtName);
//        }
//    }
//}
//=======
        return carts.size();
    }

}
