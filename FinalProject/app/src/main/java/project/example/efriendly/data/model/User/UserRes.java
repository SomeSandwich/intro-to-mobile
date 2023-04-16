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
}
