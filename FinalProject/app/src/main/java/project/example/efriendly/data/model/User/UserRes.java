package project.example.efriendly.data.model.User;

import project.example.efriendly.constants.enums.UserStatus;

import lombok.Getter;

@Getter
public class UserRes {

    private Integer id;

    private String name;

    private String email;

    private String phoneNumber;

    private Double legit;

    private String address;

    private UserStatus status;

    public UserRes(Integer id, String name, String email, String phoneNumber, Double legit, String address, UserStatus status) {
        this.id = id;
        this.name = name;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.legit = legit;
        this.address = address;
        this.status = status;
    }
}
