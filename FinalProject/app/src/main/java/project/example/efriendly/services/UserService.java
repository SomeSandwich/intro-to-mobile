package project.example.efriendly.services;

import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.User.CreateUserReq;
import project.example.efriendly.data.model.User.SellerRes;

import java.util.List;

import project.example.efriendly.data.model.User.UserAvatarReq;
import project.example.efriendly.data.model.User.UserRes;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;
import retrofit2.http.Query;

public interface UserService {
    @GET("user/{id}")
    Call<UserRes> GetById(@Path("id") Integer id);

    @GET("user/most-legit")
    Call<List<SellerRes>> GetMostLegit(@Query("number") Integer number);

    @POST("user")
    //@FormUrlEncoded
    Call<UserRes> CreateUser(@Body CreateUserReq request);

    @POST("user/{id}/avatar")
    //@FormUrlEncoded
    Call<String> AddAvatar(@Path("id") Integer id, @Body UserAvatarReq request);

    @DELETE("user/{id}/avatar")
    Call<String> RemoveAvatar(@Path("id") Integer id);
}
