package project.example.efriendly.data.model.Post;

public class SearchPostReq {

    private String query;

    public String getQuery() {
        return query;
    }

    public void setQuery(String query) {
        this.query = query;
    }

    public SearchPostReq(String query) {
        this.query = query;
    }
}