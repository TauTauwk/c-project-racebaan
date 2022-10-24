using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfZandvoort
{
    public static class DoImage
    {
        private static Dictionary<string, Bitmap> _images = new();

        public static Bitmap GetImage(string ImageLocation)
        {
            if (!_images.ContainsKey(ImageLocation))
            {
                _images.Add(ImageLocation, new Bitmap(ImageLocation));
            }
            return (Bitmap)_images[ImageLocation].Clone();
        }

        public static void ClearImageCache()
        {
            _images.Clear();
        }

        public static Bitmap DrawBitmap(int width, int height)
        {
            string key = "empty";
            if (!_images.ContainsKey(key))
            {
                _images.Add(key, new Bitmap(width,height));
                Graphics graphics = Graphics.FromImage(_images[key]);
                graphics.Clear(System.Drawing.Color.DarkGray);
            }
            return (Bitmap)_images[key].Clone();
            
        }

        //converteerd Bitmap naar BitmapSource dit moet omdat je geen Bitmap kan gebruiken in een WPFwindow
        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

    }
}
