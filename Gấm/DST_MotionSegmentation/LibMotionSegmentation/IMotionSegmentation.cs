using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace MotionSegmentation
{
    public interface IMotionSegmentation
    {
        void InitMotionSegmentation();
        void Segmentation(ref Image<Bgr, byte> image);
    }
}
