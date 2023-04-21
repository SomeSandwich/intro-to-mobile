package project.example.efriendly.services;

import project.example.efriendly.data.model.Message.CreateMessageReq;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface ChatService {

    @POST("chat/{convId}")
    Call<Integer> Create(@Path("convId") Integer convId, @Body CreateMessageReq req);
}
