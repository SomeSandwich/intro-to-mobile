package project.example.efriendly.data.model.Cart;

import java.util.List;

public class CartItem {
    private String productName;

    private String mediaPath;
    private Integer unitPrice;
    private Integer numInCart;

    public String getProductName() {
        return productName;
    }

    public String getMediaPath() {
        return mediaPath;
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
