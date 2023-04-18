package project.example.efriendly.data.model.Auth;

import lombok.Getter;

@Getter
public class LoginReq {

    private String email;

    private String password;

    public LoginReq(String email, String password) {
        this.email = email;
        this.password = password;
    }
}
