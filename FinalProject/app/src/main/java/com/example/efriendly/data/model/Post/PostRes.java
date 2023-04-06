package com.example.efriendly.data.model.Post;

import java.time.LocalDateTime;
import java.util.List;

import lombok.Getter;

@Getter
public class PostRes {

    private Integer id;

    private Integer price;

    private Integer caption;

    private Integer description;

    private List<String> mediaPath;

    private LocalDateTime createdDate;

    private LocalDateTime updatedDate;

    private Boolean isSold;
}
