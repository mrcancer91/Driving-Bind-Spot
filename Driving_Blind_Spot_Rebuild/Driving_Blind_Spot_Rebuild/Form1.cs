using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;

namespace Driving_Blind_Spot_Rebuild
{
    public partial class Form1 : Form
    {
        Capture cam;
        Image<Gray, Single> originalImg, currentImg, tempBinary;
        Image<Rgb, byte> currentRGBImage;
        BlindSpot_Util util;
        int maxGrayValue = 255;
        int closingIteration = 8;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            currentImg = new Image<Gray, Single>(Driving_Blind_Spot_Rebuild.Properties.Resources._498);
            currentRGBImage = new Image<Rgb, byte>(Driving_Blind_Spot_Rebuild.Properties.Resources._498);
            originalImg = currentImg;
            setPictureBoxImage(pictureBox1, originalImg);

        }

        private void setPictureBoxImage(PictureBox pictureBox, Image<Gray, Single> img)
        {
            pictureBox.Image = img.Resize(pictureBox.Width, pictureBox.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).ToBitmap();
        }

        private Image<Gray, Single> sobel_Filter(Image<Gray, Single> img)
        {
            int xOrder = Int32.Parse(txtXorder.Text), yOrder = Int32.Parse(txtYorder.Text),
               appetureSize = Int32.Parse(txtAppetureSize.Text);
            //img = img.Sobel(0, 1, 5);
            

            //Image<Gray, float> horImg, verImg;
            //horImg = img.Sobel(xOrder, yOrder, appetureSize);
            //verImg = img.Sobel(yOrder,xOrder, appetureSize);
            //img = horImg + verImg;

            var blurImg = img.SmoothGaussian(3, 3, 34.3, 45.3);
            var mask = img - blurImg;
            mask *= 20;
            img += mask;
            img = img.SmoothBilatral(7, 255, 34);
            img._ThresholdBinaryInv(new Gray(16), new Gray(255));
            img = img.Sobel(xOrder, yOrder, appetureSize);
            

            return img;
        }

        private void resetImage()
        {
            currentImg = originalImg;
        }

        private Image<Gray, Single> morfologyEx(Image<Gray, Single> img)
        {
            StructuringElementEx element = new StructuringElementEx(5, 5, 3, 3, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
            return img;
        }

        private void histogram(Emgu.CV.UI.HistogramBox box, Image<Gray, Single> img)
        {
            box.ClearHistogram();
            box.GenerateHistograms(img, 256);
            box.Refresh();
        }

        private Image<Gray, byte> sobelFilter(Image<Gray, byte> inputImg)
        {
            Bitmap b = inputImg.Bitmap;
            Bitmap bb = inputImg.Bitmap;
            int width = b.Width;
            int height = b.Height;
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            int[,] allPixR = new int[width, height];
            int[,] allPixG = new int[width, height];
            int[,] allPixB = new int[width, height];

            int limit = 128 * 128;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = b.GetPixel(i, j).R;
                    allPixG[i, j] = b.GetPixel(i, j).G;
                    allPixB[i, j] = b.GetPixel(i, j).B;
                }
            }

            int new_rx = 0, new_ry = 0;
            int new_gx = 0, new_gy = 0;
            int new_bx = 0, new_by = 0;
            int rc, gc, bc;
            for (int i = 1; i < b.Width - 1; i++)
            {
                for (int j = 1; j < b.Height - 1; j++)
                {

                    new_rx = 0;
                    new_ry = 0;
                    new_gx = 0;
                    new_gy = 0;
                    new_bx = 0;
                    new_by = 0;
                    rc = 0;
                    gc = 0;
                    bc = 0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            rc = allPixR[i + hw, j + wi];
                            new_rx += gx[wi + 1, hw + 1] * rc;
                            new_ry += gy[wi + 1, hw + 1] * rc;

                            gc = allPixG[i + hw, j + wi];
                            new_gx += gx[wi + 1, hw + 1] * gc;
                            new_gy += gy[wi + 1, hw + 1] * gc;

                            bc = allPixB[i + hw, j + wi];
                            new_bx += gx[wi + 1, hw + 1] * bc;
                            new_by += gy[wi + 1, hw + 1] * bc;
                        }
                    }
                    if (new_rx * new_rx + new_ry * new_ry > limit || new_gx * new_gx + new_gy * new_gy > limit || new_bx * new_bx + new_by * new_by > limit)
                        bb.SetPixel(i, j, Color.Black);
                    //bb.SetPixel (i, j, Color.FromArgb(allPixR[i,j],allPixG[i,j],allPixB[i,j]));
                    else
                        bb.SetPixel(i, j, Color.White);
                }
            }
            return new Image<Gray, byte>(bb);
        }

        private Image<Gray, Single> threshold_Binary(Image<Gray, Single> img, int threshold, int maxVal)
        {
            if (threshold >= 0 && threshold <= maxVal)
            {
                img = img.ThresholdBinary(new Gray(Int32.Parse(txtThreshold.Text)), new Gray(255));
            }
            else
            {
                MessageBox.Show("Threshold is not approbriate, please choose another!");
            }
            return img;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img">Input Image</param>
        /// <param name="threshold">Threshold value (with 8-bit image is from 0 to 255)</param>
        /// <param name="maxGrayVal">Maximum Gray Value</param>
        /// <param name="closingIteration">number of Iterations</param>
        /// <returns></returns>
        private Image<Gray, Single> _imageProcessing(Image<Gray, Single> img, int threshold, int maxGrayVal, int closingIteration)
        {
            img = img.Sobel(0, 1, 4);
            StructuringElementEx element = new StructuringElementEx(4, 4, 1, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            //MessageBox.Show((int)Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE + "");
            img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
            img = img.Rotate(90.0, new Gray(maxGrayValue), false);
            img = img.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, closingIteration);
            img = img.Rotate(-90.0, new Gray(maxGrayValue), false);
            if (threshold >= 0 && threshold <= maxGrayVal)
            {
                img = img.ThresholdBinary(new Gray(threshold), new Gray(maxGrayValue));
            }
            else
            {
                MessageBox.Show("Threshold is not appropriate, please choose another!");
            }
            return img;
        }


        private Image<Gray, Single> _testMorphological(Image<Gray, Single> img, int threshold, int maxGrayVal, int closingIteration, int cols, int rows, int anchorX, int anchorY, int shape)
        {
            int xOrder = Int32.Parse(txtXorder.Text), yOrder = Int32.Parse(txtYorder.Text),
                appetureSize = Int32.Parse(txtAppetureSize.Text);
            //img = img.Sobel(0, 1, 5);
            //img = img.Sobel(xOrder, yOrder, appetureSize);
            img = sobel_Filter(img);
           
            StructuringElementEx element;
            if(shape==0)
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
            //img.Bitmap.Save(@"Sobel\" + xOrder + " " + yOrder + " " + appetureSize + ".png");
            return img;
        }

        private void Histogram(int[] h, PictureBox box, Color c, int direction)
        {

            //Phân bố

            int max = h[0];
            for (int j = 1; j <= 255; j++)
                if (max < h[j])
                    max = h[j];
            //Vẽ

            int Height;
            Bitmap bmHistogram = new Bitmap(h.Length, 5000);
            Graphics gp = Graphics.FromImage(bmHistogram);
            if (direction == 0)
                Height = box.Height;
            else Height = box.Width;


            gp.FillRectangle(new Pen(Color.White).Brush, new Rectangle(0, 0, bmHistogram.Width, bmHistogram.Height));
            int n;
            int loopNumber = loopNumber = h.Length < box.Width ? h.Length : box.Width; ;
            if (direction != 0)
                loopNumber = h.Length < box.Height ? h.Length : box.Height;
            else
                for (int i = 0; i <= loopNumber; i++)
                {
                    n = (h[i] * Height) / max;
                    if (direction == 0)
                        gp.DrawLine(new Pen(c), i, Height, i, Height - n);
                    else
                        gp.DrawLine(new Pen(c), 0, i, Height - n, i);

                }
            box.Image = bmHistogram;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                currentImg = new Image<Gray, Single>(Openfile.FileName);
                currentRGBImage = new Image<Rgb, byte>(Openfile.FileName);
                originalImg = currentImg;
                tempBinary = null;
                setPictureBoxImage(pictureBox1, originalImg);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (originalImg != null)
            {
                setPictureBoxImage(pictureBox1, currentImg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentImg = originalImg;
            tempBinary = null;
            setPictureBoxImage(pictureBox1, originalImg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentImg = sobel_Filter(currentImg);
            setPictureBoxImage(pictureBox1, currentImg);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentImg = morfologyEx(currentImg);
            setPictureBoxImage(pictureBox1, currentImg);
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {
            //currentImg = currentImg.ThresholdBinary(new Gray(Int32.Parse(txtThreshold.Text)), new Gray(255));
            if (null == tempBinary)
                tempBinary = currentImg;
            else
                currentImg = tempBinary;
            currentImg = threshold_Binary(currentImg, Int32.Parse(txtThreshold.Text), 255);
            setPictureBoxImage(pictureBox1, currentImg);
            //histogram(histogramBox1, currentImg);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //currentImg = currentImg.Rotate(90.0,new Gray(255));
            currentImg = currentImg.Rotate(90.0, new Gray(255), false);
            currentImg = morfologyEx(currentImg);
            currentImg = currentImg.Rotate(-90.0, new Gray(255), false);
            setPictureBoxImage(pictureBox1, currentImg);
        }

        private void histogramBox1_Load(object sender, EventArgs e)
        {

        }

        private void histogramBox2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            currentImg = _imageProcessing(originalImg, Int32.Parse(txtThreshold.Text), 255, 8);

            BlindSpot_Util util = new BlindSpot_Util();
            util.projectImage(currentImg);
            //currentImg.Draw(util.DetectedRegtangle, new Gray(255), 10);
            //Bitmap m = (Bitmap)pictureBox1.Image;
            //Image<Rgb, byte> bolo =new Image<Rgb,byte>(m);

            //Image<Rgb, byte> bolo = originalImg.Convert<Rgb, byte>();
            //currentRGBImage.Draw(util.DetectedRegtangle, new Rgb(255, 0, 0), 5);
            //currentRGBImage.Draw(new CircleF(new PointF(util.X_hc, util.Y_vc), 1), new Rgb(255, 0, 0), 5);
            //pictureBox1.Image = currentRGBImage.Resize(pictureBox1.Width, pictureBox1.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).ToBitmap(); ;
            //lbRect.Text = util.DetectedRegtangle.ToString();
            setPictureBoxImage(pictureBox1, currentImg);

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            util = new BlindSpot_Util();


            int rows, cols, anchorX, anchorY, shape;
            rows = Int32.Parse(txtRows.Text);
            cols = Int32.Parse(txtCols.Text);
            anchorY = Int32.Parse(txtY.Text);
            anchorX = Int32.Parse(txtX.Text);
            shape = cmbShape.SelectedIndex;
            this.closingIteration = Int32.Parse(txtIter.Text);
            //MessageBox.Show(shape + "");

            //Test morphological parameters
            currentImg = _testMorphological(originalImg, Int32.Parse(txtThreshold.Text), maxGrayValue, this.closingIteration, cols, rows, anchorX, anchorY, shape);


            //Test sobel Parameter then save to images
            //util._testSobelParameters(originalImg, Int32.Parse(txtThreshold.Text), maxGrayValue, this.closingIteration, cols, rows, anchorX, anchorY, shape);

            //util.projectImage(currentImg);
            pictureBox1.Image = currentImg.Bitmap;

            /*
            //Histogram(util.X_axis, pictureBox2, Color.Red, 0);
            //Histogram(util.Y_axis, pictureBox3, Color.Green, 1);
            */

            //util.testParameters(currentImg);

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Image<Rgb, byte> bolo = originalImg.Convert<Rgb, byte>();
            bolo.Draw(util.DetectedRegtangle, new Rgb(255, 0, 0), 5);
            bolo.Draw(new CircleF(new PointF(util.X_hc, util.Y_vc), 1), new Rgb(255, 0, 0), 5);
            //pictureBox1.Image = currentRGBImage.Resize(pictureBox1.Width, pictureBox1.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).ToBitmap(); ;
            lbRect.Text = util.DetectedRegtangle.ToString();
            pictureBox1.Image = bolo.Bitmap;
        }

        private void cmbShape_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
