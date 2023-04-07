package project.example.efriendly.services;

import project.example.efriendly.data.model.Post.CreatePostReq;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.Post.UpdatePostReq;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.PATCH;
import retrofit2.http.POST;
import retrofit2.http.Path;
import retrofit2.http.Query;

public interface PostService {

    @POST("post")
    @FormUrlEncoded
    Call<String> Create(@Body CreatePostReq request);

    @GET("post")
    Call<List<PostRes>> GetBySellerId(@Query("sellerId") Integer sellerId);

    @GET("post/{id}")
    Call<List<PostRes>> GetById(@Path("id") Integer id);

    @GET("/post/newest")
    Call<List<PostRes>> GetNewest(@Query("number") Integer number);

    @PATCH("/post/{id}")
    Call<String> Update(@Path("id") Integer id, @Body UpdatePostReq request);
}
