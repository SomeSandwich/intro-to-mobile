package project.example.efriendly.ultilities;

import project.example.efriendly.data.model.Post.PostRes;

public class HomepageItemList {
    PostRes leftItem;
    PostRes rightItem;
    public HomepageItemList(){ leftItem = null; rightItem = null;}
    public PostRes getLeftItem() {
        return leftItem;
    }
    public PostRes getRightItem() {
        return rightItem;
    }
    public void setLeftItem(PostRes leftItem) {
        this.leftItem = leftItem;
    }
    public void setRightItem(PostRes rightItem) {
        this.rightItem = rightItem;
    }
}
