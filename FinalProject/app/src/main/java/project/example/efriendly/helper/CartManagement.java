package project.example.efriendly.helper;

import android.content.Context;
import android.widget.Toast;

import java.util.ArrayList;

import project.example.efriendly.data.model.Cart.CartItem;
import project.example.efriendly.data.model.Order.OrderDetailRes;
import project.example.efriendly.services.ChangeNumberItemsListener;

public class CartManagement {
    private Context context;

    public CartManagement(Context context) {
        this.context = context;
    }

    public void insertProduct(CartItem item) {
        ArrayList<CartItem> listProduct = getListCart();
        boolean exist = false;
        int n = 0;
        for (int i = 0; i < listProduct.size(); i++) {
            if (listProduct.get(i).getProductName().equals(item.getProductName())) {
                exist = true;
                n = i;
                break;
            }
        }

        if (exist) {
            listProduct.get(n).setNumInCart(item.getNumInCart());
        } else {
            listProduct.add(item);
        }

        Toast.makeText(context, "Added To Your Cart", Toast.LENGTH_SHORT).show();
    }

    public ArrayList<CartItem> getListCart() {
        return new ArrayList<>();
    }

    public void plusNumberProduct(ArrayList<CartItem> cartItemArrayList, int position, ChangeNumberItemsListener changeNumberItemsListener) {
        cartItemArrayList.get(position).setNumInCart(cartItemArrayList.get(position).getNumInCart() + 1);
        changeNumberItemsListener.changed();
    }

    public void minusNumberProduct(ArrayList<CartItem> cartItemArrayList, int position, ChangeNumberItemsListener changeNumberItemsListener) {
        if (cartItemArrayList.get(position).getNumInCart() == 1) {
            cartItemArrayList.remove(position);
        } else {
            cartItemArrayList.get(position).setNumInCart(cartItemArrayList.get(position).getNumInCart() - 1);
        }
        changeNumberItemsListener.changed();
    }

    public Integer getTotalMoney() {
        ArrayList<CartItem> listProduct = getListCart();
        int money = 0;
        for(int i = 0; i < listProduct.size(); i++) {
            money = money + (listProduct.get(i).getUnitPrice() * listProduct.get(i).getNumInCart());
        }
        return money;
    }
}
