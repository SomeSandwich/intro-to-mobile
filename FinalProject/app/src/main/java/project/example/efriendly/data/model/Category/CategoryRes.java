package project.example.efriendly.data.model.Category;

import lombok.Getter;

@Getter
public class CategoryRes {

    private Integer id;

    private String description;

    private String Icon;

    public Integer getId() {
        return id;
    }

    public String getDescription() {
        return description;
    }

    public String getIcon() {
        return Icon;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public void setIcon(String icon) {
        Icon = icon;
    }
}
