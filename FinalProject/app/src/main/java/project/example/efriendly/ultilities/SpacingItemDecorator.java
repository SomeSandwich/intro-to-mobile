package project.example.efriendly.ultilities;

import android.graphics.Rect;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

public class SpacingItemDecorator extends RecyclerView.ItemDecoration {
    private final int spacing;

    @Override
    public void getItemOffsets(@NonNull Rect outRect, int itemPosition, @NonNull RecyclerView parent) {
        outRect.left =outRect.right = spacing;
    }

    public SpacingItemDecorator(int spacing)
    {
        this.spacing = spacing;
    }
}
