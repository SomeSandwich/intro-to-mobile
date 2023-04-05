package com.example.efriendly.httpclient.services;

import com.example.efriendly.httpclient.client.MobileServiceGenerator;
import com.example.efriendly.httpclient.repositories.CategoryRepository;
import com.example.efriendly.httpclient.types.CategoryRes;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Response;

public class CategoryService {

    private static CategoryRepository cateRepo = MobileServiceGenerator.createService(CategoryRepository.class);

    public CategoryService() {

    }

    public List<CategoryRes> GetAll() {
        List<CategoryRes> categories = new ArrayList<CategoryRes>();

        Call<List<CategoryRes>> callSync = cateRepo.getAllCategory();
        try {
            Response<List<CategoryRes>> response = callSync.execute();
            categories = response.body();

        } catch (IOException ignored) {

        }

        return categories;
    }

    public CategoryRes GetById(int id) {
        CategoryRes cate = null;

        Call<CategoryRes> callSync = cateRepo.getCategoryId(id);
        try {
            Response<CategoryRes> response = callSync.execute();
            cate = response.body();
        } catch (IOException ignored) {

        }

        return cate;
    }
}
