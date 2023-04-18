package project.example.efriendly.data.model.Comment;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CreateCommentReq {
    
    private Integer userId;
    
    private Integer postId;
    
    private String Content;
}
