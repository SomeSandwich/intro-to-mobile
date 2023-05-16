package project.example.efriendly.data.model;

import java.io.Serializable;

import lombok.Getter;

@Getter
public class SuccessRes implements Serializable {

    private String message;

    public String getMessage() {
        return message;
    }
}
