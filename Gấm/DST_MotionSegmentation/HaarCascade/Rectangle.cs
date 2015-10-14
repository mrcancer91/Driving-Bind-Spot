using System;
using System.Net;
using System.Windows;

namespace FaceDetectionWinPhone
{
    public class RectangleF
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public RectangleF() { }

        public RectangleF(double x, double y, double w, double h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
    }
}
