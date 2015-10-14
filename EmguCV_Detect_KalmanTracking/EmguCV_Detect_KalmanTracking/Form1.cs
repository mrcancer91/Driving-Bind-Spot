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
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace EmguCV_Detect_KalmanTracking
{
    public partial class Form1 : Form
    {
        private Capture cap = null;
        private Image<Bgr, byte> img;
        //private CascadeClassifier haar;
        private HaarCascade haar;
        private List<Rectangle> listRect, newList, detectedList;
        long time, count;
        public Form1()
        {
            InitializeComponent();
            haar = new HaarCascade("cas1.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
        }

        void Idle(object sender, EventArgs e)
        {
            var watch = Stopwatch.StartNew();
            img = cap.QueryFrame();

            if (img != null)
            {
                pictureBox1.Image = img.Bitmap;
                //if (count % 5 == 0)
                {
                    var grayImg = img.Convert<Gray, byte>();
                    //var faces = haar.DetectMultiScale(grayImg, 1.1, 3, Size.Empty);
                   
                    var faces = haar.Detect(grayImg,1.1,3,Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.SCALE_IMAGE,System.Drawing.Size.Empty);
                    newList = new List<Rectangle>();
                    if(faces.Length>0)
                    foreach (var face in faces)
                    {
                       
                            //img.Draw(face, new Bgr(Color.Green), 2);
                            newList.Add(face.rect);
                            detectedList = new List<Rectangle>();
                            if (listRect == null)
                            {
                                listRect = new List<Rectangle>();

                            }
                            else
                                foreach (var rect in listRect)
                                {
                                    int distance = (rect.X - face.rect.X) * (rect.X - face.rect.X) + (rect.Y - face.rect.Y) * (rect.Y - face.rect.Y);
                                    if (distance < 80)
                                    {

                                        //img.Draw(face, new Bgr(Color.Green), 2);
                                        detectedList.Add(face.rect);
                                        break;
                                    }
                                }
                        
                    }
                    listRect = newList;
                }
                if (null!=detectedList)
                {
                    foreach (var rect in detectedList)
                        img.Draw(rect, new Bgr(Color.Green), 2);
                }
                pictureBox2.Image = img.Bitmap;
                watch.Stop();
                label1.Text = watch.ElapsedMilliseconds + "ms";
                time += watch.ElapsedMilliseconds;
                count++;
                label4.Text = time / count + " ms ";
            }
            //var frameRate = cap.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);

            //Thread.Sleep((int)(1000.0 / frameRate));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //if (dialog.ShowDialog() == DialogResult.OK)
            {
                //cap = new Capture(dialog.FileName);
                cap = new Capture();
                //var frameRate = cap.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);
                //label6.Text = frameRate + "fps";
                var resolution = cap.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH) + "x" + cap.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT);
                label8.Text = resolution;

                Application.Idle += Idle;
                time = 0;
                count = 0;
            }
            //cap = new Emgu.CV.Capture(@"D:\Master Degree\Thesis\Driving blind spot\Data\Video\Driving 640 480-Cut.m4v");
            //cap = new Capture();
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, Double.Parse(pictureBox1.Height.ToString()));
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, Double.Parse(pictureBox1.Width.ToString()));

        }
    }
}
