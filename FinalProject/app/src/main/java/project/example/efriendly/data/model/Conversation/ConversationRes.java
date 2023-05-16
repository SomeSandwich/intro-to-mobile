package project.example.efriendly.data.model.Conversation;

import java.util.List;

import lombok.Getter;
import project.example.efriendly.data.model.Message.MessageRes;
import project.example.efriendly.data.model.Participation.ParticipationRes;

@Getter
public class ConversationRes {
    private Integer id;
    private Boolean isLock;
    private List<ParticipationRes> participations;
    private List<MessageRes> messages;

    public Integer getId() {
        return id;
    }
    public Boolean getLock() {
        return isLock;
    }
    public List<ParticipationRes> getParticipations() {
        return participations;
    }
    public List<MessageRes> getMessages() {
        return messages;
    }
}
