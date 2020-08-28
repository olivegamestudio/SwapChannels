using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SwapChannels
{
    public class Shared
    {
        public void SwapChannels(string filename)
        {
            if (Path.GetExtension(filename).ToLowerInvariant() != ".png")
            {
                return;
            }

            if (!File.Exists(filename))
            {
                return;
            }

            var bytes = File.ReadAllBytes(filename);
            var ms = new MemoryStream(bytes);
            Bitmap bitmap = (Bitmap)Image.FromStream(ms);


            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    color = Color.FromArgb(color.A, color.B, color.G, color.R);
                    bitmap.SetPixel(x, y, color);
                }
            }

            bitmap.Save(filename, ImageFormat.Png);
        }
    }
}
