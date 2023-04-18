package project.example.efriendly.data.model.User;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class SellerRes {

    private Integer id;

    private String name;

    private String avatarPath;

    private Double legit;
    public SellerRes(Integer id, String name, Double legit) {
        this.id = id;
        this.name = name;
        this.legit = legit;
    }

    public Integer getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public Double getLegit() {
        return legit;
    }
}
