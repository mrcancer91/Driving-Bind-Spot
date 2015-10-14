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

namespace TestEmguCv
{
    public partial class Form1 : Form
    {
        Image<Rgb, Byte> My_Image,original, currentImg;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                My_Image = new Image<Rgb, byte>(Openfile.FileName);
                original = My_Image;
                currentImg = My_Image;
                My_Image =original.Resize(pictureBox1.Width, pictureBox1.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                pictureBox1.Image = My_Image.ToBitmap();
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {

            if (original != null)
            {
                My_Image = currentImg.Resize(pictureBox1.Width, pictureBox1.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                pictureBox1.Image = My_Image.ToBitmap();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (original != null)
            {
                My_Image = original.Resize(pictureBox1.Width, pictureBox1.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                pictureBox1.Image = My_Image.ToBitmap();
                currentImg = My_Image;
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Image<Gray, byte> grayImg = currentImg.Convert<Gray, byte>();
           
            pictureBox1.Image = currentImg.ToBitmap();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Image<Gray,byte> src,dst;
            StructuringElementEx element = new StructuringElementEx( 3, 3, 1, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_CROSS );
            currentImg = currentImg.MorphologyEx(element, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, 1);
            currentImg= resizeImg(currentImg);
            pictureBox1.Image = currentImg.ToBitmap();
        }



        private Image<Rgb, byte> resizeImg(Image<Rgb, byte> src)
        {
            return src = src.Resize(pictureBox1.Width, pictureBox1.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
        }
    }
}
