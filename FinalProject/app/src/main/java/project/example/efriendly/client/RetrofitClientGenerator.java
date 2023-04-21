package project.example.efriendly.client;

import okhttp3.OkHttpClient;
import okhttp3.logging.HttpLoggingInterceptor;
import project.example.efriendly.client.interceptor.TokenAuthenticator;
import project.example.efriendly.client.interceptor.SetTokenInterceptor;
import project.example.efriendly.constants.DatabaseConnection;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class RetrofitClientGenerator implements DatabaseConnection {

    public static Retrofit getRetrofit() {
        HttpLoggingInterceptor httpLoggingInterceptor = new HttpLoggingInterceptor();
        httpLoggingInterceptor.setLevel(HttpLoggingInterceptor.Level.BODY);

        SetTokenInterceptor setTokenInterceptor = new SetTokenInterceptor();
//        TokenAuthenticator refreshTokenInterceptor = new TokenAuthenticator();

        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .addInterceptor(httpLoggingInterceptor)
                .addInterceptor(setTokenInterceptor)
//                .authenticator(refreshTokenInterceptor)
                .build();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .client(okHttpClient)
                .build();

        return retrofit;
    }

    public static <S> S getService(Class<S> serviceClass) {
        return getRetrofit().create(serviceClass);
    }
}
