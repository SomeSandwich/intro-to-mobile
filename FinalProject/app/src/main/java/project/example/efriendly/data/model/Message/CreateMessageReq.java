package project.example.efriendly.data.model.Message;

import lombok.Getter;

@Getter
public class CreateMessageReq {

    private String content;

    public CreateMessageReq(String content) {
        this.content = content;
    }

    public String getContent() {
        return content;
    }
}
