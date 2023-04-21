package project.example.efriendly.data.model.Auth;

import java.io.Serializable;

import lombok.Getter;

@Getter
public class LoginRes implements Serializable {

    private String token;

    public String getToken() {
        return token;
    }
}
