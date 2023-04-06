package com.example.efriendly.data.model.Post;

import lombok.Getter;

@Getter
public class CreatePostReq {

    private Integer categoryId;

    private Integer price;

    private String caption;

    private String description;
}
