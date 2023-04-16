package project.example.efriendly.data.model.Post;

import java.time.LocalDateTime;
import java.util.List;

import lombok.Getter;
import project.example.efriendly.data.model.Rate.RateRes;
import project.example.efriendly.data.model.Report.ReportRes;

@Getter
public class PostRes {

    private Integer id;

    private Integer price;

    private Integer caption;

    private Integer description;

    private List<String> mediaPath;

    private LocalDateTime createdDate;

    private LocalDateTime updatedDate;

    private Boolean isSold;

    private RateRes rate;

    private List<Integer> userShare;

    private List<ReportRes> reports;
}
