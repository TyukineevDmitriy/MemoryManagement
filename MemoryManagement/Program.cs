using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace MemoryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var bitmap = (Bitmap)Bitmap.FromFile(@"..\..\kok.bmp");
            var timer = new Timer();
            using (timer.Start())
            {
                using (var bitmapEditor = new BitmapEditor(bitmap))
                {
                    for (int i = 20; i < 80; i++)
                    {
                        for (int j = 0; j < 5; j++)
                            bitmapEditor.SetPixel(i, 20 + j, 102, 182, 32);
                    }
                }
            }
            Console.WriteLine(timer.ElapsedMilliseconds);
            Console.WriteLine(timer.ElapsedTicks);
            using (timer.Continue())
            {
                using (var bitmapEditor = new BitmapEditor(bitmap))
                {
                    for (int i = 20; i < 80; i++)
                    {
                        for (int j = 0; j < 5; j++)
                            bitmapEditor.SetPixel(i, 40 + j, 35, 11, 232);
                    }
                }
            }
            Console.WriteLine(timer.ElapsedMilliseconds);
            Console.WriteLine(timer.ElapsedTicks);
            bitmap.Save(@"..\..\image2.png", ImageFormat.Png);
        }
    }
}
