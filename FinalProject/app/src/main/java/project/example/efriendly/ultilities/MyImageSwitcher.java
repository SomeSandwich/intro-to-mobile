package project.example.efriendly.ultilities;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.AttributeSet;
import android.util.Log;
import android.widget.ImageSwitcher;
import android.widget.ImageView;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;

public class MyImageSwitcher extends ImageSwitcher {

    public MyImageSwitcher(Context context) {
        super(context);
    }

    public MyImageSwitcher(Context context, AttributeSet attrs) {
        super(context, attrs);
    }

    public void setImageUrl(String link) {
        ImageView image = (ImageView) this.getNextView();
        try {
            URL url = new URL(link);
            Bitmap bmp = BitmapFactory.decodeStream(url.openConnection().getInputStream());
            image.setImageBitmap(bmp);
        }
        catch (MalformedURLException e){
            Log.d("Debug", e.toString());
        }
        catch (IOException err){
            Log.d("Debug", err.toString());
        }
        showNext();;
    }
}
