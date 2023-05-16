package project.example.efriendly.data.model.Order;

import lombok.Getter;

@Getter
public class OrderDetailReq {
    private int postId;
    private int unitPrice;

    public OrderDetailReq(int postId, int unitPrice) {
        this.postId = postId;
        this.unitPrice = unitPrice;
    }

    public int getPostId() {
        return postId;
    }

    public int getPrice() {
        return unitPrice;
    }
}
