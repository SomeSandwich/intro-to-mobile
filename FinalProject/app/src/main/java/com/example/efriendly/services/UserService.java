package com.example.efriendly.services;

import com.example.efriendly.data.model.User.CreateUserReq;
import com.example.efriendly.data.model.User.SellerRes;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Query;

public interface UserService {

    @POST("/user")
    @FormUrlEncoded
    Call CreateUser(@Body CreateUserReq request);

    @GET("/user/most-legit")
    Call<List<SellerRes>> GetMostLegit(@Query("number") Integer number);
}
