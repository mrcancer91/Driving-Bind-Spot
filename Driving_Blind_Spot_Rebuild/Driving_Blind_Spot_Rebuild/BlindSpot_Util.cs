using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using System.Windows.Forms;
using System.Drawing;

namespace Driving_Blind_Spot_Rebuild
{
    class BlindSpot_Util
    {
        private int _X_hc, _Y_hc, _X_vc, _Y_vc;
        private double teta_x = 0.0f, teta_y = 0.0f;
        private int[] x_axis, y_axis;

        private Rectangle _detectedRegtangle;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="img">Input Image</param>
        /// <param name="threshold">Threshold value (with 8-bit image is from 0 to 255)</param>
        /// <param name="maxGrayVal">Maximum Gray Value</param>
        /// <param name="closingIteration">number of Iterations</param>
        /// <returns>Processed Image</returns>
        public Image<Gray, Single> _imageProcessing(Image<Gray, Single> img, int threshold, int maxGrayVal, int closingIteration)
        {
            img = img.Sobel(0, 1, 3);
            StructuringElementEx element = new StructuringElementEx(3, 3, -1, -1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);
            img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
            img = img.Rotate(-90.0, new Gray(255), false);
            img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
            img = img.Rotate(90.0, new Gray(255), false);
            if (threshold >= 0 && threshold <= maxGrayVal)
            {
                img = img.ThresholdBinary(new Gray(threshold), new Gray(maxGrayVal));
            }
            else
            {
                //MessageBox.Show("Threshold is not appropriate, please choose another!");
                throw new ArgumentOutOfRangeException("Threshold is not appropriate, please choose another threshold value!");
            }
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


            _detectedRegtangle = findRectangle1(_X_hc, _Y_vc);

            //MessageBox.Show(_X_hc + " " + _Y_vc+"\n"+_detectedRegtangle.ToString());
        }

        private Rectangle findRectangle1(int x, int y)
        {
            Rectangle rect = new Rectangle();
            int start = y, end = y;
            while (y_axis[end] >= _X_vc * 0.8 && end < y_axis.Length - 1)
                end++;
            while (y_axis[start] >= _X_vc * 0.8 && start > 0)
                start--;
            rect.Y = start;
            rect.Height = end - start;

            start = x;
            end = x;
            while (x_axis[end] >= _Y_hc * 0.8 && end < x_axis.Length - 1)
                end++;
            while (x_axis[start] >= _Y_hc * 0.8 && start > 0)
                start--;
            rect.X = start;
            rect.Width = end - start;
            return rect;
        }

        private Rectangle findRectange(int x, int y)
        {
            Rectangle temp = new Rectangle();
            int Start = -1, End = -1;
            for (int i = 0; i < x_axis.Length; i++)
            {
                if (x_axis[i] >= y)
                {
                    int xStartTemp = i;
                    while (x_axis[xStartTemp] >= y && xStartTemp < (x_axis.Length - 1))
                        xStartTemp++;
                    if (((xStartTemp - i) > (End - Start)) && (xStartTemp >= x && i <= x))
                    {
                        End = xStartTemp;
                        Start = i;
                    }
                    i = xStartTemp;
                }
            }

            temp.X = Start;
            temp.Width = End - Start;
            Start = End = -1;
            for (int i = 0; i < y_axis.Length; i++)
            {
                if (y_axis[i] >= x)
                {
                    int xStartTemp = i;
                    while (y_axis[xStartTemp] >= x && xStartTemp < (y_axis.Length - 1))
                        xStartTemp++;
                    if (((xStartTemp - i) > (End - Start)) && (xStartTemp >= y && i <= y))
                    {
                        End = xStartTemp;
                        Start = i;
                    }
                    i = xStartTemp;
                }
            }
            temp.Y = Start;
            temp.Height = End - Start;
            //MessageBox.Show(temp.ToString());
            return temp;


        }

        #region Test

        public  void _testSobelParameters(Image<Gray, Single> img, int threshold, int maxGrayVal, int closingIteration, int cols, int rows, int anchorX, int anchorY, int shape)
        {
            int xOrder = 0, yOrder = 0, appetureSize = 1;
            for (appetureSize = 3; appetureSize < 8; appetureSize += 2)
            {
                for (xOrder = 0; xOrder < appetureSize-1; xOrder++)
                    for (yOrder = 0; yOrder < appetureSize-1; yOrder++)
                    {
                        img = img.Sobel(xOrder, yOrder, appetureSize);
                        StructuringElementEx element;
                        if (shape == 0)
                            element = new StructuringElementEx(cols, rows, anchorX, anchorY, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
                        else if (shape == 1)
                            element = new StructuringElementEx(cols, rows, anchorX, anchorY, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_CROSS);
                        else
                            element = new StructuringElementEx(cols, rows, anchorX, anchorY, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);

                        img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
                        img = img.Rotate(90.0, new Gray(255), false);
                        img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
                        img = img.Rotate(-90.0, new Gray(255), false);
                        if (threshold >= 0 && threshold <= maxGrayVal)
                        {
                            img = img.ThresholdBinary(new Gray(threshold), new Gray(255));
                        }
                        else
                        {
                            MessageBox.Show("Threshold is not appropriate, please choose another!");
                        }
                        img.Bitmap.Save(@"Sobel\" + xOrder + " " + yOrder + " " + appetureSize + ".png");
                    }
            }
        }

        private Image<Gray, Single> _testMorphological(Image<Gray, Single> img, int threshold, int maxGrayVal, int closingIteration, int cols, int rows, int anchorX, int anchorY, int shape)
        {
            img = img.Sobel(0, 1, 3);
            StructuringElementEx element = new StructuringElementEx(cols, rows, anchorX, anchorY, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT); ;
            if (shape == 1)
                element = new StructuringElementEx(cols, rows, anchorX, anchorY, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_CROSS);
            else if (shape == 2)
                element = new StructuringElementEx(cols, rows, anchorX, anchorY, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);

            img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
            img = img.Rotate(90.0, new Gray(255), false);
            img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
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
        public void testClosingParameters(Image<Gray, Single> inputImg)
        {
            int rows, colums, anchorX, anchorY, shape, closingIteration, threshold;
            int count = 1;
            Image<Gray, float> img = inputImg;
            for (closingIteration = 6; closingIteration < 7; closingIteration++)
            {
                for (shape = 0; shape < 1; shape++)
                {
                    for (rows = 5; rows < 7; rows++)
                        for (colums = 5; colums < 7; colums++)
                        {
                            for (anchorX = 0; anchorX < colums; anchorX++)
                                for (anchorY = 0; anchorY < rows; anchorY++)
                                {

                                    for (threshold = 80; threshold < 180; threshold += 20)
                                    {
                                        img = inputImg.Sobel(0, 1, 3);
                                        img = inputImg.Sobel(1, 0, 3);
                                        StructuringElementEx element = new StructuringElementEx(colums, rows, anchorX, anchorY, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT); ;
                                        if (shape == 1)
                                            element = new StructuringElementEx(colums, rows, anchorX, anchorY, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_CROSS);
                                        else if (shape == 2)
                                            element = new StructuringElementEx(colums, rows, anchorX, anchorY, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);

                                        img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
                                        img = img.Rotate(90.0, new Gray(255), false);
                                        img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
                                        img = img.Rotate(-90.0, new Gray(255), false);
                                        if (threshold >= 0 && threshold <= 255)
                                        {
                                            img = img.ThresholdBinary(new Gray(threshold), new Gray(255));
                                        }
                                        else
                                        {
                                            MessageBox.Show("Threshold is not appropriate, please choose another!");
                                        }
                                        string str_shape;
                                        if (shape == 0)
                                            str_shape = "RECTANGLE";
                                        else if (shape == 1)
                                            str_shape = "CROSS";
                                        else str_shape = "ELLIPSE";
                                        img.Bitmap.Save(@"Result\" + closingIteration + "_" + str_shape + "_" + rows + "_" + colums + "_" + anchorX + "_" + anchorY + "_" + threshold + ".bmp");
                                        count++;
                                    }
                                }
                        }
                }
            }
            MessageBox.Show("Done " + count + " images saved!");
        }
        #endregion
    }
}
