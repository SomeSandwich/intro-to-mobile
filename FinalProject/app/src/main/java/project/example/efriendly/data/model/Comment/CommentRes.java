package project.example.efriendly.data.model.Comment;

import java.time.LocalDateTime;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CommentRes {
    
    private Integer id;
    
    private Integer userId;
    
    private Integer postId;
    
    private String content;
    
    private LocalDateTime createAt;
}
