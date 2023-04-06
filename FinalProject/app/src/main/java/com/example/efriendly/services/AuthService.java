package com.example.efriendly.services;

import com.example.efriendly.data.model.Auth.LoginReq;
import com.example.efriendly.data.model.Auth.LoginRes;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.POST;
import retrofit2.http.Query;

public interface AuthService {

    @POST("login")
    @FormUrlEncoded
    Call<LoginRes> Login(@Body LoginReq request);

    @POST("refresh-token")
    Call<LoginRes> RefreshToken(@Query("token") String token);
}
