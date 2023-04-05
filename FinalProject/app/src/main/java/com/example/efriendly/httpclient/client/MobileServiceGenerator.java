package com.example.efriendly.httpclient.client;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class MobileServiceGenerator {

    private static final String BASE_URL = "https://mobile.hieucckha.me/";

    private static final Retrofit.Builder buider = new Retrofit.Builder()
            .baseUrl(BASE_URL)
            .addConverterFactory(GsonConverterFactory.create());

    private static final Retrofit retrofit = buider.build();

    private static final OkHttpClient.Builder httpClient = new OkHttpClient.Builder();

    public static <S> S createService(Class<S> serviceClass) {
        return retrofit.create(serviceClass);
    }
}
