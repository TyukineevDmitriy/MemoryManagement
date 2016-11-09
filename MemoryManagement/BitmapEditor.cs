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
        IntPtr Ptr;
        int NumBytes;
        byte[] RGBValues;
        public BitmapEditor(Bitmap bitmap)
        {
            Bitmap = bitmap;
            Rectangle rect = new Rectangle(0, 0, Bitmap.Width, Bitmap.Height);
            BmpData = Bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            Ptr = BmpData.Scan0;
            NumBytes = BmpData.Stride * Bitmap.Height;
            RGBValues = new byte[NumBytes];
            Marshal.Copy(Ptr, RGBValues, 0, NumBytes);
            
        }
        public void SetPixel(int x, int y, byte red, byte green, byte blue)
        {
            int coordinates = x * 3 + Bitmap.Width * y * 3;
            RGBValues[coordinates] = blue;
            RGBValues[coordinates + 1] = green;
            RGBValues[coordinates + 2] = red;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Marshal.Copy(RGBValues, 0, Ptr, NumBytes);
                    Bitmap.UnlockBits(BmpData);
                }
                disposedValue = true;
            }
        }
        
        ~BitmapEditor()
        {
            Dispose(false);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
