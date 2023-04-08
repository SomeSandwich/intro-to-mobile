package project.example.efriendly.activities;

import androidx.fragment.app.Fragment;

import android.annotation.SuppressLint;
import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import android.widget.ImageSwitcher;
import android.widget.ImageView;
import android.widget.ViewSwitcher;

import project.example.efriendly.R;
import project.example.efriendly.databinding.ActivityShowPostBinding;
import project.example.efriendly.ultilities.OnSwipeTouchListener;

public class showPost extends Fragment {
    ActivityShowPostBinding binding;
    UserActivity main;
    Context context = null;

    Integer[] clothImgs = {R.drawable.clothes, R.drawable.clothes,
            R.drawable.clothes, R.drawable.clothes};
    Integer legitPoint = 4;
    String prices = "$300";
    String size = "M";
    String name = "Jasmin Zamora";
    Integer sellerAvt = R.drawable.avatar;
    String clothDes = "98% Cotton, 2% Elastane\n" +
            "Imported\n" +
            "Zipper closure\n" +
            "Machine Wash\n" +
            "These chino pants feature a figure-flattering fabric for versatile, everyday wear\n" +
            "Includes a zip fly with button closure, off-seam side pockets and decorative rear welt pockets\n" +
            "Available in Classic and Curvy fits: Curvy fit\n" +
            " is slightly smaller in the waist and provides more room in the hip than the classic fit. Choose the fit that best matches your body shape.";
    int count=clothImgs.length;
    int currentIndex=0;

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
    @SuppressLint("ClickableViewAccessibility")
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        binding = ActivityShowPostBinding.inflate(inflater, container,false);

        ImageSwitcher show = binding.clothesImgs;
        show.setFactory(new ViewSwitcher.ViewFactory() {
            @Override
            public View makeView() {
                ImageView imageView= new ImageView(context);
                imageView.setScaleType(ImageView.ScaleType.FIT_CENTER);
                imageView.setLayoutParams(new FrameLayout.LayoutParams(
                        FrameLayout.LayoutParams.MATCH_PARENT
                        ,FrameLayout.LayoutParams.MATCH_PARENT
                ));
                return imageView;
            }
        });
        show.setImageResource(clothImgs[0]);
        show.setOnTouchListener(new OnSwipeTouchListener(main) {
            public void onSwipeRight() {
                show.setInAnimation(main,R.anim.from_left);
                show.setOutAnimation(main,R.anim.to_right);
                currentIndex++;
                // condition
                if(currentIndex==count)
                    currentIndex=0;
                show.setImageResource(clothImgs[currentIndex]);
            }
            public void onSwipeLeft() {
                show.setInAnimation(main,R.anim.from_right);
                show.setOutAnimation(main,R.anim.to_left);
                --currentIndex;
                // condition
                if(currentIndex<0)
                    currentIndex=clothImgs.length-1;
                show.setImageResource((clothImgs[currentIndex]));
            }
        });
        binding.legitPoint.setText(Integer.toString(legitPoint));
        binding.sellerAvt.setImageResource(sellerAvt);
        binding.prices.setText(prices);
        binding.ClothesDes.setText(clothDes);
        binding.size.setText(size);
        binding.name.setText(name);
        return binding.getRoot();
    }
}