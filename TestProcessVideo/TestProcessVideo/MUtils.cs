using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using System.Windows.Forms;
using System.Drawing;

namespace TestProcessVideo
{
    class MUtils
    {
        private int _X_hc, _Y_hc, _X_vc, _Y_vc;
        
        private int[] x_axis, y_axis;

        public Rectangle _detectedRegtangle;
        public List<Rectangle> allDetectedRect;

        public int[] Y_axis
        {
            get { return y_axis; }
            set { y_axis = value; }
        }

        public int[] X_axis
        {
            get { return x_axis; }
            set { x_axis = value; }
        }

        public Rectangle DetectedRegtangle
        {
            get { return _detectedRegtangle; }
            set { _detectedRegtangle = value; }
        }
        public int X_hc
        {
            get { return _X_hc; }
            set { _X_hc = value; }
        }
        public int Y_vc
        {
            get { return _Y_vc; }
            set { _Y_vc = value; }
        }
        
        public  Image<Gray, Single> _imageProcessing(Image<Gray, Single> img, int threshold, int maxGrayVal, int closingIteration)
        {
            #region Sobel filter
            /*-------------
             * ------------*/
            var blurImg = img.SmoothGaussian(3, 3, 34.3, 45.3);
            var mask = img - blurImg;
            mask *= 20;
            img += mask;
            img = img.SmoothBilatral(7, 255, 34);
            img._ThresholdBinaryInv(new Gray(160), new Gray(255));
            img = img.Sobel(1, 2, 5);
            /*-------------
            * ------------*/
            #endregion
           
            #region morphological Processing
            StructuringElementEx element = new StructuringElementEx(3, 3,1, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            img._MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
           
            img = img.Rotate(-90.0, new Gray(255), false);
            img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
            img = img.Rotate(90.0, new Gray(255), false);
            
            #endregion

            //Threshold Binary
            if (threshold >= 0 && threshold <= maxGrayVal)
            {
                img = img.ThresholdBinary(new Gray(threshold), new Gray(255));
            }
            else
            {
                //MessageBox.Show("Threshold is not appropriate, please choose another!");
                throw new ArgumentOutOfRangeException("Threshold is not appropriate, please choose another threshold value!");
            }
            projectImage(img);
            return img;
        }
        public void projectImage(Image<Gray, Single> inputImg)
        {

            float[, ,] imgData = inputImg.Data;
            x_axis = new int[inputImg.Width];
            y_axis = new int[inputImg.Height];
            for (int i = 0; i < inputImg.Width; i++)
                x_axis[i] = 0;
            for (int i = 0; i < inputImg.Height; i++)
                y_axis[i] = 0;

            //MessageBox.Show(imgData[200, 200, 0] + " ");
            int height = inputImg.Height, width = inputImg.Width;
            for (int h = 0; h < height; h++)
                for (int w = 0; w < width; w++)
                {
                    if (imgData[h, w, 0] == 255)
                    {
                        x_axis[w]++;
                        y_axis[h]++;
                    }
                }

            int yTotal = 0, xTotal = 0;

            //Horizontal Projection
            for (int i = 0; i < x_axis.Length; i++)
            {
                yTotal += x_axis[i];
            }
            _Y_hc = yTotal / x_axis.Length;

            _X_hc = 0;
            for (int i = 0; i < x_axis.Length; i++)
                _X_hc += (i * x_axis[i]);

            _X_hc /= yTotal;

            //Vertical Projection
            for (int i = 0; i < y_axis.Length; i++)
            {
                xTotal += y_axis[i];
            }
            _X_vc = xTotal / y_axis.Length;
            _Y_vc = 0;
            for (int i = 0; i < y_axis.Length; i++)
                _Y_vc += (i * y_axis[i]);

            _Y_vc /= xTotal;


            _detectedRegtangle = findRectangle(_X_hc, _Y_vc);
            //allDetectedRect =  findAllRectangle();
            //MessageBox.Show(_X_hc + " " + _Y_vc+"\n"+_detectedRegtangle.ToString());
        }

        private Rectangle findRectangle(int x, int y)
        {
            Rectangle rect = new Rectangle(-1,-1,0,0);
            double alpha = 1.0f;
            int start = y, end = y;
            while (y_axis[end] >= _X_vc * alpha && end < y_axis.Length-1)
                end++;
            while (y_axis[start] >= _X_vc * alpha && start > 0)
                start--;
            rect.Y = start;
            rect.Height = end - start;

            start = x;
            end = x;
            while (x_axis[end] >= _Y_hc * alpha && end < x_axis.Length-1)
                end++;
            while (x_axis[start] >= _Y_hc * alpha && start > 0)
                start--;
            rect.X = start;
            rect.Width = end - start;
            return rect;
        }
        private List<Rectangle> findAllRectangle()
        {
            List<Rectangle> listRect = new List<Rectangle>();
            for(int x=0;x<x_axis.Length;x++)
                for (int y = 0; y < y_axis.Length; y++)
                {
                    Rectangle k = findRectangle(x, y);
                    if (k.Width > 0 && k.Height > 0)
                    {
                        listRect.Add(k);
                        x = k.X + k.Width;
                        y = k.Y + k.Height;
                    }
                }
            return listRect;
        }
    }
}
