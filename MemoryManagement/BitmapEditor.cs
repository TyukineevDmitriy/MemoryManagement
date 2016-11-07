using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MemoryManagement
{
    public class BitmapEditor : IDisposable
    {
        private Bitmap Bitmap;
        private BitmapData BmpData;
        public BitmapEditor(Bitmap bitmap)
        {
            Bitmap = bitmap;
            Rectangle rect = new Rectangle(0, 0, Bitmap.Width, Bitmap.Height);
            BmpData = Bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
        }
        public void SetPixel(int x, int y, byte red, byte green, byte blue)
        {
            IntPtr ptr = BmpData.Scan0;
            int numBytes = BmpData.Stride * Bitmap.Height;
            byte[] rgbValues = new byte[numBytes];
            Marshal.Copy(ptr, rgbValues, 0, numBytes);
            int coordinates = x * 3 + Bitmap.Width * y * 3;
            rgbValues[coordinates] = blue;
            rgbValues[coordinates + 1] = green;
            rgbValues[coordinates + 2] = red;
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
        }
        public void Dispose()
        {
            Bitmap.UnlockBits(BmpData);
        }
    }
}
