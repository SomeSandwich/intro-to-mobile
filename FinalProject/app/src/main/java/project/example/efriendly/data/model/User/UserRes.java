package project.example.efriendly.data.model.User;

import android.graphics.Bitmap;

import lombok.Setter;
import project.example.efriendly.constants.enums.UserStatus;

import lombok.Getter;

@Getter
@Setter
public class UserRes {

    private Integer id;

    private String name;

    private String email;

    private String phoneNumber;

    private String avatarPath;

    private Double legit;

    private String address;

    private UserStatus status;

    public UserRes(Integer id, String name, String avatar) {
        this.id = id;
        this.name = name;
        this.avatarPath = avatar;
    }

    public Integer getId() {
        return id;
    }
    public String getAvatarPath() {
        return avatarPath;
    }

    public String getName() {
        return name;
    }

    public String getEmail() {
        return email;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public Double getLegit() {
        return legit;
    }

    public String getAddress() {
        return address;
    }

    public UserStatus getStatus() {
        return status;
    }
}
