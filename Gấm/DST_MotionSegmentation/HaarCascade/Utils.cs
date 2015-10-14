using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Drawing;
//using System.Drawing.Imaging;

namespace FaceDetectionWinPhone
{
    class Utils
    {
        /// <summary>
        /// Taken from http://stackoverflow.com/questions/671953/converting-from-a-format8bppindexed-to-a-format24bpprgb-in-c-gdi
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <param name="outputFileName"></param>
        //public static Bitmap ConvertTo24(Bitmap bmpIn)
        //{
        //    Bitmap converted = new Bitmap(bmpIn.Width, bmpIn.Height, PixelFormat.Format32bppArgb);
        //    using (Graphics g = Graphics.FromImage(converted))
        //    {
        //        // Prevent DPI conversion
        //        g.PageUnit = GraphicsUnit.Pixel;
        //        // Draw the image
        //        g.DrawImageUnscaled(bmpIn, 0, 0);
        //    }
        //    return converted;
        //}
    }
}
