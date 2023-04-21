package project.example.efriendly.services;

import java.util.List;

import project.example.efriendly.data.model.Conversation.ConversationRes;
import project.example.efriendly.data.model.User.UserRes;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface ConversationService {

    @GET("conversation/{convId}")
    Call<ConversationRes> GetByConvId(@Path("convId") Integer convId);

    @GET("conversation/self/all")
    Call<List<ConversationRes>> GetBySelf();

    @GET("conversation/self/user/all")
    Call<List<UserRes>> GetUserBySelf();

    @POST("conversation/{userId}")
    Call<Integer> Create(@Path("userId") Integer userId);
}
