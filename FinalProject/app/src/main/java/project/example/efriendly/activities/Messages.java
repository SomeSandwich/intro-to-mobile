package project.example.efriendly.activities;

public class Messages {
    String message;
    Integer senderId;
    long timestamp;
    String currenttime;

    public Messages(String message, Integer senderId, long timestamp, String currentTime) {
        this.message = message;
        this.senderId = senderId;
        this.timestamp = timestamp;
        this.currenttime = currentTime;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public Integer getSenderId() {
        return senderId;
    }

    public void setSenderId(Integer senderId) {
        this.senderId = senderId;
    }

    public long getTimestamp() {
        return timestamp;
    }

    public void setTimestamp(long timestamp) {
        this.timestamp = timestamp;
    }

    public String getCurrentTime() {
        return currenttime;
    }

    public void setCurrentTime(String currenttime) {
        this.currenttime = currenttime;
    }
}
