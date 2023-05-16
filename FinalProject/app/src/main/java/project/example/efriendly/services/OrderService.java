package project.example.efriendly.services;

import project.example.efriendly.data.model.Order.OrderReq;
import project.example.efriendly.data.model.SuccessRes;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface OrderService {
    @POST("order")
    Call<SuccessRes> createOrder(@Body OrderReq orderDetail);
}
