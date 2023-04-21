package project.example.efriendly.data.model.Post;

import org.jetbrains.annotations.NotNull;

import java.util.List;

import lombok.Getter;
import okhttp3.MultipartBody;

@Getter
public class CreatePostReq {

    @NotNull
    private Integer categoryId;

    @NotNull
    private Integer price;

    @NotNull
    private String caption;

    private String description;

    public CreatePostReq(@NotNull Integer categoryId, @NotNull Integer price, @NotNull String caption, String description) {
        this.categoryId = categoryId;
        this.price = price;
        this.caption = caption;
        this.description = description;
    }
}
