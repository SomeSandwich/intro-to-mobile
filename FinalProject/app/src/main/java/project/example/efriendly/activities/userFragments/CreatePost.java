package project.example.efriendly.activities.userFragments;

import static android.app.Activity.RESULT_OK;

import android.Manifest;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.net.Uri;
import android.os.Bundle;

import androidx.annotation.Nullable;
import androidx.core.app.ActivityCompat;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;

import android.provider.MediaStore;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Toast;

import com.bumptech.glide.Glide;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import okhttp3.MediaType;
import okhttp3.MultipartBody;
import okhttp3.RequestBody;
import project.example.efriendly.R;
import project.example.efriendly.activities.UserActivity;
import project.example.efriendly.adapter.CreatePostAdapter;
import project.example.efriendly.client.RetrofitClientGenerator;
import project.example.efriendly.constants.DatabaseConnection;
import project.example.efriendly.data.model.Category.CategoryRes;
import project.example.efriendly.data.model.SuccessRes;
import project.example.efriendly.data.model.User.UserRes;
import project.example.efriendly.databinding.FragmentCreatePostBinding;
import project.example.efriendly.services.CategoryService;
import project.example.efriendly.services.PostService;
import project.example.efriendly.services.UserService;
import project.example.efriendly.ultilities.RealPathUtil;
import project.example.efriendly.ultilities.SpacingItemDecorator;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CreatePost extends Fragment implements DatabaseConnection {
    private static final int REQUEST_EXTERNAL_STORAGE = 1;
    private static String[] PERMISSIONS_STORAGE = {
            Manifest.permission.READ_EXTERNAL_STORAGE,
            Manifest.permission.WRITE_EXTERNAL_STORAGE
    };
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

    ClickListener clickListener;

    @Override
    public void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        try {
            context = getActivity();
            main = (UserActivity) getActivity();
            // Check if we have write permission
            int permission = ActivityCompat.checkSelfPermission(main, Manifest.permission.WRITE_EXTERNAL_STORAGE);

            if (permission != PackageManager.PERMISSION_GRANTED) {
                // We don't have permission so prompt the user
                ActivityCompat.requestPermissions(
                        main,
                        PERMISSIONS_STORAGE,
                        REQUEST_EXTERNAL_STORAGE
                );
            }
        } catch (IllegalStateException err) {
            throw new IllegalStateException("MainActivity must implement callbacks");
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        binding = FragmentCreatePostBinding.inflate(inflater, container, false);
        PostService postService = RetrofitClientGenerator.getService(PostService.class);
        clickHandler = new CreatePostClickHandler(context);
        binding.setClickHandler(clickHandler);
        clickListener = new ClickListener();
        adapter = new CreatePostAdapter(context, imgsList, clickListener);
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

    void addInfo() {
        Call<UserRes> userResCall = userService.GetSelf();
        userResCall.enqueue(new Callback<UserRes>() {
            @Override
            public void onResponse(Call<UserRes> call, Response<UserRes> response) {
                if (response.isSuccessful()) {
                    UserRes user = response.body();
                    binding.userName.setText(user.getName());
                    if (user.getAvatarPath() != null) {
                        Glide.with(context)
                                .load(IMAGE_URL + user.getAvatarPath())
                                .placeholder(R.drawable.placeholder)
                                .into(binding.userAvt);
                    } else binding.userAvt.setImageResource(R.drawable.user);

                    Call<List<CategoryRes>> categoryServiceAll = categoryService.getAll();
                    categoryServiceAll.enqueue(new Callback<List<CategoryRes>>() {
                        @Override
                        public void onResponse(Call<List<CategoryRes>> call, Response<List<CategoryRes>> response) {
                            if (response.isSuccessful()) {
                                categoryList = response.body();
                                String[] categoriesName = new String[categoryList.size()];
                                for (int i = 0; i < categoryList.size(); i++) {
                                    categoriesName[i] = categoryList.get(i).getDescription();
                                }
                                ArrayAdapter adapter = new ArrayAdapter<String>(main, R.layout.category_item, categoriesName);
                                adapter.setDropDownViewResource(androidx.appcompat.R.layout.support_simple_spinner_dropdown_item);
                                binding.CategoryBar.setAdapter(adapter);

                            } else {
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
                } else {
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

    public class ClickListener{
        public void RemoveImageClick(int position){
            imgsList.remove(position);
            adapter.notifyItemRemoved(position);
        };
    }

    public class CreatePostClickHandler {
        Context context;
        public CreatePostClickHandler(Context context) {
            this.context = context;
        }

        public void addImageClick(View view) {
            Intent intent = new Intent(Intent.ACTION_PICK, MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
            startActivityForResult(intent, 3);
        }

        public void finish(){
            int size = imgsList.size();
            imgsList.clear();
            adapter.notifyItemRangeRemoved(0, size);
            binding.caption.setText("");
            binding.des.setText("");
            binding.prices.setText("");
        }

        public void closeClick(View view) {
            finish();
            main.onMsgFromFragToMain("createPost", "close");
        }

        public void postClick(View view) {
            if (binding.caption.getText().toString().matches("")) {
                String message = "Can't post without caption";
                Toast.makeText(main, message, Toast.LENGTH_LONG).show();
            } else if (imgsList.size() == 0) {
                String message = "Can't post without image";
                Toast.makeText(main, message, Toast.LENGTH_LONG).show();
            } else {
                String categoryName = binding.CategoryBar.getSelectedItem().toString();
                Integer categoryID = 0;
                for (int i = 0; i < categoryList.size(); i++)
                    if (categoryName.equals(categoryList.get(i).getDescription()))
                        categoryID = categoryList.get(i).getId();
                for (int i = 0; i<imgsList.size();i++){
                    File file = new File(RealPathUtil.getRealPath(context, imgsList.get(i)));
                    RequestBody requestFile = RequestBody.create(MediaType.parse("multipart/form-data"), file);
                    MultipartBody.Part body = MultipartBody.Part.createFormData("MediaFiles", file.getName(), requestFile);
                    sendList.add(body);
                }
                postService = RetrofitClientGenerator.getService(PostService.class);
                Call<SuccessRes> postCall = postService.Create(
                        RequestBody.create(MediaType.parse("multipart/form-data"), categoryID.toString()),
                        RequestBody.create(MediaType.parse("multipart/form-data"), binding.prices.getText().toString()),
                        RequestBody.create(MediaType.parse("multipart/form-data"), binding.caption.getText().toString()),
                        RequestBody.create(MediaType.parse("multipart/form-data"), binding.des.getText().toString()),
                        sendList);
                postCall.enqueue(new Callback<SuccessRes>() {
                    @Override
                    public void onResponse(Call<SuccessRes> call, Response<SuccessRes> response) {
                        if (response.isSuccessful()) {
                            Toast.makeText(main, "Success", Toast.LENGTH_SHORT).show();
                        } else {
                            Toast.makeText(main, "An error occurred please try again later...", Toast.LENGTH_SHORT).show();
                        }
                    }
                    @Override
                    public void onFailure(Call<SuccessRes> call, Throwable t) {
                        Log.d("Debug", t.getLocalizedMessage());
                        Toast.makeText(main, t.getLocalizedMessage(), Toast.LENGTH_LONG).show();
                    }
                });
                finish();
                main.onMsgFromFragToMain("createPost", "close");
            }
        }
    }
    @Override
    public void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (resultCode == RESULT_OK && data != null) {
            Uri selectedImage = data.getData();
            imgsList.add(selectedImage);
            adapter.notifyItemInserted(imgsList.size() - 1);
        }
    }
}