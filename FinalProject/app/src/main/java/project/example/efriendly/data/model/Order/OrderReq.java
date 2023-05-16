package project.example.efriendly.data.model.Order;

import java.util.List;

import lombok.Getter;

@Getter
public class OrderReq {
    private String deliveryAddress;
    private List<OrderDetailReq> details;
    public OrderReq(String deliveryAddress, List<OrderDetailReq> details) {
        this.deliveryAddress = deliveryAddress;
        this.details = details;
    }
}
