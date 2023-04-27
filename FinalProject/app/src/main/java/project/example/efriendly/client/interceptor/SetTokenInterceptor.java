package project.example.efriendly.client.interceptor;

import androidx.annotation.NonNull;

import java.io.IOException;

import okhttp3.Interceptor;
import okhttp3.Request;
import okhttp3.Response;
import project.example.efriendly.constants.StorageHelper;

public class SetTokenInterceptor implements Interceptor {
    @NonNull
    @Override
    public Response intercept(@NonNull Chain chain) throws IOException {

        String token = StorageHelper.Token;

        System.out.println("DebugToken: " + token);

        if (token != null) {
            Request newRequest = chain.request().newBuilder()
                    .addHeader("Authorization", "Bearer " + StorageHelper.Token)
                    .build();

            return chain.proceed(newRequest);
        }

        return chain.proceed(chain.request());
    }
}
