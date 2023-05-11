package project.example.efriendly.client.interceptor;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URI;
import java.net.URL;

import okhttp3.Authenticator;
import okhttp3.Request;
import okhttp3.Response;
import okhttp3.Route;
import project.example.efriendly.constants.StorageHelper;

public class TokenAuthenticator implements Authenticator {

    @Nullable
    @Override
    public Request authenticate(@Nullable Route route, @NonNull Response response) throws IOException {
        String token = StorageHelper.Token;
        return null;
    }

    public boolean refreshToken(String url, String token) throws IOException {
        return true;
    }
}
