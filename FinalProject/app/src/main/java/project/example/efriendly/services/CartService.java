package project.example.efriendly.services;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;
import retrofit2.http.Path;
public interface CartService {
    @POST("cart/{id}")
    Call<String> addToCart(@Path("id") Integer userId, @Body Integer postId);
}
