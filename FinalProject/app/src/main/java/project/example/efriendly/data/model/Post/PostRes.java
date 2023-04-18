package project.example.efriendly.data.model.Post;

import android.graphics.Bitmap;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Vector;

import lombok.Getter;
import project.example.efriendly.data.model.Rate.RateRes;
import project.example.efriendly.data.model.Report.ReportRes;
import project.example.efriendly.data.model.User.SellerRes;

@Getter
public class PostRes {
    private SellerRes author;
    private Integer id;
    private Integer price;
    private String caption;
    private String description;
    private List<String> mediaPath;
    private String createdDate;
    private String updatedDate;
    private Boolean isSold;

    private RateRes rate;

    private List<Integer> userShare;

    private List<ReportRes> reports;


    public SellerRes getAuthor() {
        return author;
    }

    public Integer getId() {
        return id;
    }

    public Integer getPrice() {
        return price;
    }

    public String getCaption() {
        return caption;
    }

    public String getDescription() {
        return description;
    }

    public List<String> getMediaPath() {
        return mediaPath;
    }

    public String getCreatedDate() {
        return createdDate;
    }

    public String getUpdatedDate() {
        return updatedDate;
    }

    public Boolean getSold() {
        return isSold;
    }

    private int currentIndex = 0;

    private Vector<Bitmap> imgBitmap;

    public int getCurrentIndex(){
        return currentIndex;
    }
    public Vector<Bitmap> getImgBitmap(){return this.imgBitmap;}
    public void setImgBitmap(Vector<Bitmap> imgBitmapInput){this.imgBitmap = imgBitmapInput;}
    public void setCurrentIndex(int currentIndex){
        this.currentIndex = currentIndex;
    }
}
