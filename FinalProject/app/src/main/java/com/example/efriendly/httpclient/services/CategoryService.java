package com.example.efriendly.httpclient.services;

import com.example.efriendly.httpclient.client.MobileServiceGenerator;
import com.example.efriendly.httpclient.repositories.CategoryRepository;
import com.example.efriendly.httpclient.types.Category;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Response;

public class CategoryService {

    private static CategoryRepository cateRepo = MobileServiceGenerator.createService(CategoryRepository.class);

    public CategoryService() {

    }

    public List<Category> GetAll() {
        List<Category> categories = new ArrayList<Category>();

        Call<List<Category>> callSync = cateRepo.getAllCategory();
        try {
            Response<List<Category>> response = callSync.execute();
            categories = response.body();

        } catch (IOException ignored) {

        }

        return categories;
    }
}
