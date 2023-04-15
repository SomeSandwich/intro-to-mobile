package project.example.efriendly.data.model.User;

import lombok.Getter;

@Getter
public class SellerRes {

    private Integer id;

    private String name;

    private Double legit;
    public SellerRes(Integer id, String name, Double legit) {
        this.id = id;
        this.name = name;
        this.legit = legit;
    }
}
