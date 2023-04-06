package com.example.efriendly.data.model.User;

import lombok.Getter;

@Getter
public class CreateUserReq {

    private String name;

    private String email;

    private String Password;

    private String phoneNumber;

    private String address;
}
