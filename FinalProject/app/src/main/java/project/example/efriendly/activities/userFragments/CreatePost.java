package project.example.efriendly.activities.userFragments;

import static android.app.Activity.RESULT_OK;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;

import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;

import android.provider.MediaStore;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Adapter;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Toast;

import java.io.File;
import java.io.InputStream;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

import okhttp3.MediaType;
import okhttp3.MultipartBody;
import okhttp3.RequestBody;
import project.example.efriendly.R;
import project.example.efriendly.activities.LoginActivity;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.CreatePostAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Category.CategoryRes;
import project.example.efriendly.data.model.Post.CreatePostReq;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.FragmentCreatePostBinding;
import project.example.efriendly.databinding.FragmentNewfeelActivityBinding;
import project.example.efriendly.services.CategoryService;
import project.example.efriendly.services.PostService;
import project.example.efriendly.services.UserService;
import project.example.efriendly.ultilities.SpacingItemDecorator;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CreatePost extends Fragment implements DatabaseConnection {
    UserActivity main;
    CreatePostAdapter adapter;
    Context context = null;
    FragmentCreatePostBinding binding;
    CreatePostClickHandler clickHandler;
    List<Uri> imgsList = new ArrayList<>();
    List<MultipartBody.Part> sendList = new ArrayList<>();
    List<CategoryRes> categoryList = new ArrayList<>();
    UserService userService;
    PostService postService;
    CategoryService categoryService;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        try{
            context = getActivity();
            main = (UserActivity) getActivity();
        }
        catch (IllegalStateException err){
            throw new IllegalStateException("MainActivity must implement callbacks");
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        binding = FragmentCreatePostBinding.inflate(inflater, container,false);
        PostService postService = RetrofitClientGenerator.getService(PostService.class);
        clickHandler = new CreatePostClickHandler(context);
        binding.setClickHandler(clickHandler);
        adapter = new CreatePostAdapter(context, imgsList);
        binding.imageReview.setAdapter(adapter);
        SpacingItemDecorator itemDecorator = new SpacingItemDecorator(10);
        binding.imageReview.addItemDecoration(itemDecorator);
        binding.imageReview.setLayoutManager(new LinearLayoutManager(requireContext(), LinearLayoutManager.HORIZONTAL, false));

        binding.processBar.setVisibility(View.VISIBLE);
        binding.createPostLayout.setVisibility(View.INVISIBLE);

        userService = RetrofitClientGenerator.getService(UserService.class);

        categoryService = RetrofitClientGenerator.getService(CategoryService.class);

        addInfo();

        return binding.getRoot();
    }

    void addInfo(){
        Call<UserRes> userResCall = userService.GetSelf();
        userResCall.enqueue(new Callback<UserRes>() {
            @Override
            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                if (response.isSuccessful()){
                    UserRes user = response.body();
                    binding.userName.setText(user.getName());
                    if (user.getAvatar() != null){
                        try{
                            InputStream newUrl = new URL(IMAGE_URL + user.getAvatar()).openStream();
                            Bitmap image = BitmapFactory.decodeStream(newUrl);
                            binding.userAvt.post(new Runnable() {
                                @Override
                                public void run() {
                                    binding.userAvt.setImageBitmap(image);
                                    user.setAvtBitmap(image);
                                }
                            });
                        }
                        catch (Exception e){
                            Log.d("Debug", e.getMessage());
                        }
                    }
                    else binding.userAvt.setImageResource(R.drawable.user);

                    Call<List<CategoryRes>> categoryServiceAll = categoryService.getAll();
                    categoryServiceAll.enqueue(new Callback<List<CategoryRes>>() {
                        @Override
                        public void onResponse(Call<List<CategoryRes>> call, Response<List<CategoryRes>> response) {
                            if (response.isSuccessful()){
                                categoryList = response.body();
                                String[] categoriesName = new String[categoryList.size()];
                                for (int i=0;i<categoryList.size();i++){
                                    categoriesName[i] = categoryList.get(i).getDescription();
                                }
                                ArrayAdapter adapter = new ArrayAdapter<String>(main,R.layout.category_item,categoriesName);
                                adapter.setDropDownViewResource(androidx.appcompat.R.layout.support_simple_spinner_dropdown_item);
                                binding.CategoryBar.setAdapter(adapter);

                            }
                            else {
                                String message = "An error occurred please try again later ...";
                                Toast.makeText(main, message, Toast.LENGTH_LONG).show();
                            }
                        }

                        @Override
                        public void onFailure(Call<List<CategoryRes>> call, Throwable t) {
                            String message = t.getLocalizedMessage();
                            Toast.makeText(main, message, Toast.LENGTH_LONG).show();
                        }
                    });

                    binding.processBar.setVisibility(View.INVISIBLE);
                    binding.createPostLayout.setVisibility(View.VISIBLE);
                }
                else{
                    String message = "An error occurred please try again later ...";
                    Toast.makeText(main, message, Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<UserRes> call, Throwable t) {
                String message = t.getLocalizedMessage();
                Toast.makeText(main, message, Toast.LENGTH_LONG).show();
            }
        });
    }

    public class CreatePostClickHandler{
        Context context;
        public CreatePostClickHandler(Context context){
            this.context = context;
        }
        public void addImageClick(View view){
            Intent intent = new Intent(Intent.ACTION_PICK, MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
            startActivityForResult(intent, 3);
        }
        public void closeClick(View view){
            main.onMsgFromFragToMain("createPost", "close");
        }
        public void postClick(View view){
            if (binding.caption.getText().toString().matches("")){
                String message = "Can't post without caption";
                Toast.makeText(main, message, Toast.LENGTH_LONG).show();
            }
            else if (sendList.size() == 0){
                String message = "Can't post without image";
                Toast.makeText(main, message, Toast.LENGTH_LONG).show();
            }
            else {
                String categoryName = binding.CategoryBar.getSelectedItem().toString();
                Integer categoryID = 0;
                for (int i = 0; i < categoryList.size();i++)
                    if (categoryName.equals(categoryList.get(i).getDescription())) categoryID = categoryList.get(i).getId();
                CreatePostReq req = new CreatePostReq(
                        categoryID,
                        Integer.parseInt(binding.prices.getText().toString()),
                        binding.caption.getText().toString(),
                        binding.des.getText().toString(), sendList);
                System.out.println(categoryID + "\n" +
                        Integer.parseInt(binding.prices.getText().toString()) + "\n" +
                        binding.caption.getText().toString() + "\n" +
                        binding.des.getText().toString() + "\n" +
                        sendList.size());
                postService = RetrofitClientGenerator.getService(PostService.class);
                Call<String> postCall = postService.Create(req);
                postCall.enqueue(new Callback<String>() {
                    @Override
                    public void onResponse(Call<String> call, Response<String> response) {
                        if (response.isSuccessful()){
                            Toast.makeText(main, response.body(), Toast.LENGTH_SHORT).show();
                        }
                        else Toast.makeText(main, "An error occurred please try again later ...", Toast.LENGTH_SHORT).show();
                    }

                    @Override
                    public void onFailure(Call<String> call, Throwable t) {
                        Toast.makeText(main, t.getLocalizedMessage(), Toast.LENGTH_LONG).show();
                    }
                });

            }
        }
    }
    @Override
    public void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(resultCode == RESULT_OK && data != null){
            Uri selectedImage = data.getData();
            imgsList.add(selectedImage);
            adapter.notifyItemInserted(imgsList.size()-1);

            File file = new File(selectedImage.getPath());
            RequestBody requestFile = RequestBody.create(MediaType.parse("multipart/form-data"), file);
            MultipartBody.Part body = MultipartBody.Part.createFormData("image", file.getName(), requestFile);
            sendList.add(body);
        }
    }
}