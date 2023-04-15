package project.example.efriendly.data.model.Post;

import java.time.LocalDateTime;
import java.util.List;

import lombok.Getter;
import lombok.Setter;
import project.example.efriendly.data.model.User.SellerRes;

@Getter
public class PostRes {
    private SellerRes author;

    private Integer id;

    private Integer price;

    private String caption;

    private String description;

    private List<String> mediaPath;

    private LocalDateTime createdDate;

    private LocalDateTime updatedDate;

    private Boolean isSold;

    public PostRes(SellerRes author, Integer id, Integer price, String caption, String description, List<String> mediaPath, LocalDateTime createdDate, LocalDateTime updatedDate, Boolean isSold) {
        this.author = author;
        this.id = id;
        this.price = price;
        this.caption = caption;
        this.description = description;
        this.mediaPath = mediaPath;
        this.createdDate = createdDate;
        this.updatedDate = updatedDate;
        this.isSold = isSold;
    }

}
