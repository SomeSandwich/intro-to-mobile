package project.example.efriendly.data.model.Message;

import java.time.LocalDateTime;

import lombok.Getter;

@Getter
public class MessageRes {
    
    private Integer id;
    
    private String content;
    
    private Integer userId;
    
    private LocalDateTime createAt;
}
