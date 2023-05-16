package project.example.efriendly.data.model.Order;

public class OrderDetailRes {
    private Integer orderId;
    private Integer postId;
    private Integer unitPrice;
    private Integer numInCart;

    public Integer getOrderId() {
        return orderId;
    }

    public Integer getPostId() {
        return postId;
    }

    public Integer getUnitPrice() {
        return unitPrice;
    }

    public Integer getNumInCart() {
        return numInCart;
    }

    public void setNumInCart(Integer numInCart) {
        this.numInCart = numInCart;
    }
}
