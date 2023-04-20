package project.example.efriendly.data.model.User;

import project.example.efriendly.constants.enums.UserStatus;

import lombok.Getter;

@Getter
public class UserRes {

    private Integer id;

    private String name;

    private String email;

    private String phoneNumber;

    private String avatar;

    private Double legit;

    private String address;

    private UserStatus status;

    public UserRes(Integer id, String name, String avatar) {
        this.id = id;
        this.name = name;
        this.avatar = avatar;
    }

    public Integer getId() {
        return id;
    }
    public String getAvatar() {
        return avatar;
    }
    public String getName() {
        return name;
    }

    public void setId(Integer id) {
        this.id = id;
    }
    public void setAvatar(String avatar) {
        this.avatar = avatar;
    }
    public void setName(String name) {
        this.name = name;
    }
}
