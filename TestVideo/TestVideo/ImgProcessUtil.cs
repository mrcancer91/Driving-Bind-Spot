using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TestVideo
{
    class ImgProcessUtil
    {
        private Image<Gray, Single> _imageProcessing(Image<Gray, Single> img, int threshold, int maxGrayVal, int closingIteration)
        {
            img = img.Sobel(0, 1, 3);
            //StructuringElementEx element = new StructuringElementEx(5, 5, 1, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            //img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
            IInputArray arr = (IInputArray) new InputArray(new IntPtr());
            int[,] numbers = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            UMat m = new UMat();
            
            img.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Close,m , new System.Drawing.Point(-1, -1), closingIteration, Emgu.CV.CvEnum.BorderType.Constant, new MCvScalar(1.0f));
            img = img.Rotate(90.0, new Gray(255), false);
            //img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
            img = img.Rotate(-90.0, new Gray(255), false);
            if (threshold >= 0 && threshold <= maxGrayVal)
            {
                img = img.ThresholdBinary(new Gray(threshold), new Gray(255));
            }
            else
            {
                MessageBox.Show("Threshold is not appropriate, please choose another!");
            }
            return img;
        }
    }
}
