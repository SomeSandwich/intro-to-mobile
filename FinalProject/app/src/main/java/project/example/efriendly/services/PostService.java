package project.example.efriendly.services;

import project.example.efriendly.data.model.Post.CommentPostReq;
import project.example.efriendly.data.model.Post.CreatePostReq;
import project.example.efriendly.data.model.Post.PostRes;
import project.example.efriendly.data.model.Post.UpdatePostReq;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.PATCH;
import retrofit2.http.POST;
import retrofit2.http.Path;
import retrofit2.http.Query;

public interface PostService {

    @GET("post/{id}")
    Call<List<PostRes>> GetById(@Path("id") Integer id);

    @GET("post/seller/{sellerId}")
    Call<List<PostRes>> GetBySeller(@Path("sellerId") Integer sellerId);

    @GET("post/newest")
    Call<List<PostRes>> GetNewest(@Query("number") Integer number);

    @GET("post/category/{categoryId}")
    Call<List<PostRes>> GetBySellerId(@Query("categoryId") Integer categoryId);

    @POST("post")
    @FormUrlEncoded
    Call<String> Create(@Body CreatePostReq request);

    @POST("post/{postId}/share-by/{userId}")
    Call<String> AddShareBy(@Path("postId") Integer postId, @Path("userId") Integer userId);

    @DELETE("post/{postId}/share-by/{userId}")
    Call<String> RemoveShareBy(@Path("postId") Integer postId, @Path("userId") Integer userId);

    @POST("post/{id}/comment")
    Call<String> AddComment(@Path("id") Integer id, @Body CommentPostReq request);

    @PATCH("post/{id}")
    Call<String> UpdateInfo(@Path("id") Integer id, @Body UpdatePostReq request);

    @PATCH("post/{id}/toggle-hide")
    Call<String> HideToggle(@Path("id") Integer id);

    @DELETE("post/{id}")
    Call<String> Delete(@Path("id") Integer id);
}
