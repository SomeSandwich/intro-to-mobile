package project.example.efriendly.data.model.User;

import lombok.Getter;
import lombok.Setter;
import okhttp3.MultipartBody;

@Getter
@Setter
public class UserAvatarReq {

    private MultipartBody.Builder file;
}
