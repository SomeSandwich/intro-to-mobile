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

    private String AvatarPath;

    private Double legit;

    private String address;

    private UserStatus status;

    private Bitmap avtBitmap;

    public Integer getId() {
        return id;
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

    public String getAvatarPath() {
        return AvatarPath;
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

    public Bitmap getAvtBitmap() {
        return avtBitmap;
    }

    public void setAvtBitmap(Bitmap avtBitmap) {
        this.avtBitmap = avtBitmap;
    }
}
