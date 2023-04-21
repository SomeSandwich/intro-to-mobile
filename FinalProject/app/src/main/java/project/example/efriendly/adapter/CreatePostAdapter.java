package project.example.efriendly.adapter;

import android.content.Context;
import android.net.Uri;
import android.view.LayoutInflater;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.util.Collections;
import java.util.List;

import project.example.efriendly.databinding.AddImageBinding;
import project.example.efriendly.holder.AddImageHolder;

public class CreatePostAdapter extends RecyclerView.Adapter<AddImageHolder>{
    List<Uri> imgsList = Collections.emptyList();
    Context context;

    public CreatePostAdapter(Context context, List<Uri> imgsList){
        this.context = context;
        this.imgsList = imgsList;
    }

    @NonNull
    @Override
    public AddImageHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        AddImageBinding binding = AddImageBinding.inflate(inflater, parent, false);
        AddImageHolder addImageHolder = new AddImageHolder(binding);
        return addImageHolder;
    }

    @Override
    public void onBindViewHolder(@NonNull AddImageHolder holder, int position) {
        final int index = holder.getAdapterPosition();
        holder.clothImg.setImageURI(imgsList.get(index));
    }

    @Override
    public int getItemCount() {
        return imgsList.size();
    }
}
