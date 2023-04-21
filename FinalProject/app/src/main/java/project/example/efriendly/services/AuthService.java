package project.example.efriendly.services;

import project.example.efriendly.data.model.Auth.LoginReq;
import project.example.efriendly.data.model.Auth.LoginRes;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;
import retrofit2.http.Query;

public interface AuthService {

    @POST("auth/login")
    Call<LoginRes> Login(@Body LoginReq request);

    @POST("refresh-token")
    Call<LoginRes> RefreshToken(@Query("token") String token);
}
