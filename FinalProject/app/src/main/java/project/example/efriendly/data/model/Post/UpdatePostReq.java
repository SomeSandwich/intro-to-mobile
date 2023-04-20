package project.example.efriendly.data.model.Post;

import java.util.List;

import lombok.Getter;
import lombok.Setter;
import okhttp3.MultipartBody;

@Getter
@Setter
public class UpdatePostReq {

    private Integer price;

    private String caption;

    private String description;

    private List<String> mediaFilesDelete;

    private List<MultipartBody.Part> mediaFilesAdd;
}
