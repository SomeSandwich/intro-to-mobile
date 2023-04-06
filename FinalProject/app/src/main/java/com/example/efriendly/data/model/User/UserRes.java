package com.example.efriendly.data.model.User;

import com.example.efriendly.constants.enums.UserStatus;

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
}
