package project.example.efriendly.services;

import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.CreateUserReq;
import project.example.efriendly.data.model.User.SellerRes;

import java.util.List;

import project.example.efriendly.data.model.User.UserRes;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;
import retrofit2.http.Query;

public interface UserService {
    @POST("user")
    //@FormUrlEncoded
    Call<UserRes> CreateUser(@Body CreateUserReq request);
    @GET("user/{id}")
    Call<UserRes> GetUserById(@Path("id") Integer id);
    @GET("user/most-legit")
    Call<List<SellerRes>> GetMostLegit(@Query("number") Integer number);
}
