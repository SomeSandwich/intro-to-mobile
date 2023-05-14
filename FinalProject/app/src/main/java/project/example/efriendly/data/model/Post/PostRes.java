package project.example.efriendly.data.model.Post;

import android.graphics.Bitmap;

import java.time.LocalDateTime;
import java.util.List;
import java.util.Vector;

import lombok.Getter;
import project.example.efriendly.data.model.Rate.RateRes;
import project.example.efriendly.data.model.Report.ReportRes;
import project.example.efriendly.data.model.User.SellerRes;
import project.example.efriendly.data.model.User.UserRes;

@Getter
public class PostRes {
    private int userId;
    private UserRes user;
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
    public Integer getUserID() {
        return userId;
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
    public int getCurrentIndex(){
        return currentIndex;
    }
    public void setCurrentIndex(int currentIndex){
        this.currentIndex = currentIndex;
    }

    public UserRes getUser() {
        return user;
    }
    public void setUser(UserRes user) {
        this.user = user;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public void setPrice(Integer price) {
        this.price = price;
    }

    public void setCaption(String caption) {
        this.caption = caption;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public void setMediaPath(List<String> mediaPath) {
        this.mediaPath = mediaPath;
    }

    public void setCreatedDate(String createdDate) {
        this.createdDate = createdDate;
    }

    public void setUpdatedDate(String updatedDate) {
        this.updatedDate = updatedDate;
    }

    public void setSold(Boolean sold) {
        isSold = sold;
    }

    public void setRate(RateRes rate) {
        this.rate = rate;
    }

    public void setUserShare(List<Integer> userShare) {
        this.userShare = userShare;
    }

    public void setReports(List<ReportRes> reports) {
        this.reports = reports;
    }
    public RateRes getRate() {
        return rate;
    }
    public List<Integer> getUserShare() {
        return userShare;
    }

    public List<ReportRes> getReports() {
        return reports;
    }
}
