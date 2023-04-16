package project.example.efriendly.data.model.Category;

import org.jetbrains.annotations.NotNull;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CreateCategoryReq {

    @NotNull
    private String Description;

    private String Icon;
}
