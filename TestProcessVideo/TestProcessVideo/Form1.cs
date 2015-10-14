using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Threading;

namespace TestProcessVideo
{
    public partial class Form1 : Form
    {
        Emgu.CV.Capture cam;
        Image<Bgr, byte> currentImage;
        Image<Gray, Single> grayImg;
        Rectangle choosenPart;
        public Form1()
        {
            InitializeComponent();
            
        }
        int zero = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                cam = new Emgu.CV.Capture(dialog.FileName);
                Application.Idle += Application_Idle;
            }
            //cam = new Emgu.CV.Capture(@"E:\Ca nhac\Bolobala.mp4");
            //Application.Idle += Application_Idle;

        }
        int k = 0;
        void Application_Idle(object sender, EventArgs e)
        {
            currentImage = cam.QueryFrame();
            var originalImage = cam.QueryFrame();
            grayImg = currentImage.Convert<Gray, Single>();

            //Front view
            Rectangle croppedVideoRect = new Rectangle(60, 179, 520, 148);
            //Rear view
            //Rectangle croppedVideoRect = new Rectangle(43, 38, 277, 131);
            //Rectangle croppedVideoRect = choosenPart;
            grayImg.Bitmap = grayImg.Bitmap.Clone(croppedVideoRect, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
            MUtils util = new MUtils();

            pictureBox2.Image = util._imageProcessing(grayImg, 160, 255,10).Bitmap;

            Rectangle rect = util.DetectedRegtangle;

            rect.X += croppedVideoRect.X;
            rect.Y += croppedVideoRect.Y;
            currentImage.Draw(croppedVideoRect, new Bgr(255, 0, 0), 5);
            currentImage.Draw(rect, new Bgr(0, 0, 255), 1);
            currentImage.Draw(new CircleF(new PointF(util.X_hc + croppedVideoRect.X, util.Y_vc + croppedVideoRect.Y), 1), new Bgr(0, 0, 255), 10);

            pictureBox1.Image = currentImage.Bitmap;
            label1.Text = (util.X_hc + croppedVideoRect.X) + " " + (util.Y_vc + croppedVideoRect.Y);
            var frameRate = cam.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);
            #region Save an Image of video

            //if (k++ > 0)
            //{
            //    originalImage.Save(@"Save\" + k + ".png");
            //    //grayImg.Save("Cropped.png");
            //    label2.Text = "Saved";

            //}
            #endregion
            Thread.Sleep((int)(1000.0 / frameRate));

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = map;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        bool isclick = false;

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //isclick = true;
            //choosenPart.X = e.X;
            //choosenPart.Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
           //if (isclick)
           // {
           //     choosenPart.Width = e.X - choosenPart.X;
           //     choosenPart.Height = e.Y - choosenPart.Y;
           // }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //if (isclick)
            //{
            //    Image<Bgr, byte> temp = new Image<Bgr, byte>((Bitmap)pictureBox1.Image);
               
            //    temp.Draw(choosenPart, new Bgr(0, 255, 0), 5);
            //    isclick = false;
            //}
        }
    }
}
