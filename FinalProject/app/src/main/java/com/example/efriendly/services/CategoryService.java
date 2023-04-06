package com.example.efriendly.services;

import com.example.efriendly.data.model.Category.CategoryRes;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface CategoryService {

    @GET("category")
    Call<List<CategoryRes>> getAll();

    @GET("category/{id}")
    Call<CategoryRes> getId(@Path("id") int id);
}
