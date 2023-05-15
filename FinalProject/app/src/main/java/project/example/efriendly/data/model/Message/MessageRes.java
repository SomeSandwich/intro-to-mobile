package project.example.efriendly.data.model.Message;

import java.time.LocalDateTime;

import lombok.Getter;

@Getter
public class MessageRes {
    private Integer id;
    private String content;
    private Integer userId;
    private String createAt;

    public MessageRes(Integer id, String content, Integer userId, String createAt) {
        this.id = id;
        this.content = content;
        this.userId = userId;
        this.createAt = createAt;
    }

    public String getContent() {
        return content;
    }

    public Integer getUserId() {
        return userId;
    }

    public String getCreateAt() {
        return createAt;
    }
}
