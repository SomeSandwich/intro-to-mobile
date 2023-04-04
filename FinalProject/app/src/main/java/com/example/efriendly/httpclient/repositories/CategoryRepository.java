package com.example.efriendly.httpclient.repositories;

import com.example.efriendly.httpclient.types.Category;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface CategoryRepository {

    @GET("/category")
    public Call<List<Category>> getAllCategory();

    @GET("/category/{id}")
    public Call<Category> getCategoryId(@Path("id") String id);
}
