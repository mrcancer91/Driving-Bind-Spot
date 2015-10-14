using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace LibMotionSegmentation
{
    interface IFilter
    {
        void startTracking(Rectangle rec);
        Rectangle Filtering(Image<Bgr, byte> image, Rectangle trackRect);
    }
}
