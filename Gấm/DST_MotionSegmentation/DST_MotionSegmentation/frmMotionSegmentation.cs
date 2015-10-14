using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using MotionSegmentation;

namespace DST_MotionSegmentation
{
    public partial class frmMotionSegmentation : Form
    {
        //Define motion segmentation for the project
        IMotionSegmentation motionSegmentation = new MotionSegmentationByImageSubtraction();

        //Define Image received from camera
        Image<Bgr, byte> currentImage;

        //Define image for motion segmentation process
        Image<Bgr, byte> segmentImage;

        //Define Capture object to interact with CAMERA
        Capture cam;

        public frmMotionSegmentation()
        {
            InitializeComponent();
        }

        private void frmMotionSegmentation_Load(object sender, EventArgs e)
        {
            //cam = new Emgu.CV.Capture(@"G:\Video Segmentation\BuiDuyHung_BC_TTTN\videoDataSet\video2.mp4");
            cam = new Emgu.CV.Capture();
            Application.Idle += Application_Idle;
            motionSegmentation.InitMotionSegmentation();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            currentImage = cam.QueryFrame();

            segmentImage = currentImage.Copy();

            //Process Motion segmentation by calling object initilized
            motionSegmentation.Segmentation(ref segmentImage);

            //Set image to picture box
            picOrigin.Image = currentImage.Bitmap;
            picAvg.Image = segmentImage.Bitmap;
        }

        private void picOrigin_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                cam = new Emgu.CV.Capture(dialog.FileName);
                Application.Idle += Application_Idle;
            }
        }
    }
}
