package project.example.efriendly.data.model.User;

import lombok.Getter;
import lombok.Setter;

@Getter
public class CreateUserReq {

    private String name;

    private String email;

    private String password;

    private String phoneNumber;

    private String address;

    public CreateUserReq(String name, String email, String password, String phone, String address) {
        this.name = name;
        this.email = email;
        this.password = password;
        this.phoneNumber = phone;
        this.address = address;
    }
}
