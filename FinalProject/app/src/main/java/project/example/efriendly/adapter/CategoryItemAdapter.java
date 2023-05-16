package project.example.efriendly.adapter;

import static project.example.efriendly.constants.DatabaseConnection.IMAGE_URL;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;

import java.util.List;

import project.example.efriendly.R;
import project.example.efriendly.activities.AnonymousHomepageActivity;
import project.example.efriendly.activities.userFragments.HomepageActivity;
import project.example.efriendly.data.model.Category.CategoryRes;
import project.example.efriendly.databinding.CategoryItemsBinding;
import project.example.efriendly.holder.CategoryHolder;

public class CategoryItemAdapter extends RecyclerView.Adapter<CategoryHolder> {
    Context context;
    List<CategoryRes> categories;
    AnonymousHomepageActivity.ClickListener listener;

    public CategoryItemAdapter(Context context, List<CategoryRes> categories, AnonymousHomepageActivity.ClickListener listener) {
        this.context = context;
        this.categories = categories;
        this.listener = listener;

    }
    @NonNull
    @Override
    public CategoryHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        CategoryItemsBinding binding = CategoryItemsBinding.inflate(inflater, parent, false);
        return new CategoryHolder(binding);
    }

    @Override
    public void onBindViewHolder(@NonNull CategoryHolder holder, int position) {
        final int index = holder.getAdapterPosition();
        holder.type.setText(categories.get(index).getDescription());
        if (categories.get(index).getIcon() != null){
            Glide.with(context)
                    .load(IMAGE_URL + categories.get(index).getIcon())
                    .placeholder(R.drawable.likebutton)
                    .into(holder.icon);
        }
        else{
            holder.icon.setImageResource(R.drawable.likebutton);
        }

        holder.view.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            }
        });
    }

    @Override
    public int getItemCount() {
        return categories.size();
    }
}
