package project.example.efriendly.data.model.Report;

import java.time.LocalDateTime;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ReportRes {

    public Integer id;

    private String reason;

    private LocalDateTime reportAt;

    private Integer postId;

    private Integer userId;
}
