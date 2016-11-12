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
        private struct Pixel
        {
            public int X;
            public int Y;
            public byte Red;
            public byte Green;
            public byte Blue;

        }

        private Bitmap Bitmap;
        private BitmapData BmpData;
		private List<Pixel> SettedPixels;
        private IntPtr Ptr;
        private byte[] RGBValues;
        public BitmapEditor(Bitmap bitmap)
        {
            Bitmap = bitmap;
            Rectangle rect = new Rectangle(0, 0, Bitmap.Width, Bitmap.Height);
            BmpData = Bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			SettedPixels = new List<Pixel>();
			Ptr = BmpData.Scan0;
            RGBValues = new byte[3];			
        }
        public void SetPixel(int x, int y, byte red, byte green, byte blue)
        {
			SettedPixels.Add(new Pixel {X = x, Y = y, Red = red, Green = green, Blue = blue});
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var pixel in SettedPixels)
                    {
                        int coordinates = (pixel.X + Bitmap.Width * pixel.Y) * 3;
                        Marshal.Copy(Ptr + coordinates, RGBValues, 0, 3);
                        RGBValues[0] = pixel.Blue;
                        RGBValues[1] = pixel.Green;
                        RGBValues[2] = pixel.Red;
                        Marshal.Copy(RGBValues, 0, Ptr + coordinates, 3);
                    }
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
