package project.example.efriendly.services;

import java.util.List;

import project.example.efriendly.data.model.Cart.CartRes;
import retrofit2.Call;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface CartService {

    @GET("cart")
    Call<List<CartRes>> GetSelfCart();

    @POST("cart/{postId}")
    Call<String> AddPostToSelfCart(@Path("postId") Integer postId);

    @DELETE("cart/{postId}")
    Call<String> RemovePostFromSelfCart(@Path("postId") Integer postId);
}
