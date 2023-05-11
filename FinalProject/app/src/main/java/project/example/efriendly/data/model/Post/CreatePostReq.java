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

    private List<MultipartBody.Part> mediaFiles;

    public CreatePostReq(@NotNull Integer categoryId, @NotNull Integer price, @NotNull String caption, String description, List<MultipartBody.Part> mediaPart) {
        this.mediaFiles = mediaPart;
        this.categoryId = categoryId;
        this.price = price;
        this.caption = caption;
        this.description = description;
    }
}
