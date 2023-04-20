package project.example.efriendly.data.model.Rate;

import java.time.LocalDateTime;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class RateRes {

    private Integer id;

    private LocalDateTime ratingAt;

    private Integer rating;

    private String comment;

    private int userId;

    private int postId;
}
