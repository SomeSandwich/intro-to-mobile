package project.example.efriendly.data.model.User;

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
}
