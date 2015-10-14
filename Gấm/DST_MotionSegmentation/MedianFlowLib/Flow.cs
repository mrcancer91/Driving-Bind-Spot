using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace LibMotionSegmentation
{
    public class FlowTracker
    {
        public Image<Bgr, Byte> ActualFrame { get; set; }

        public Image<Bgr, Byte> OpticalFlowFrame { get; set; }
        public Image<Gray, Byte> ActualGrayFrame { get; set; }
        public Image<Gray, Byte> NextGrayFrame { get; set; }
        public PointF[] ActualFeature;
        public PointF[] NextFeature;
        public int npts;
        public MCvTermCriteria termCriteria;

        public Size wsize;
        public Byte[] Status;
        public float[] TrackError;
        public Image<Gray, float> []PYR;
        public Rectangle winTrack;
        virtual public void init()
        {
            PYR = new Image<Gray, float>[2];
            ActualGrayFrame = ActualFrame.Convert<Gray, Byte>();
            wsize = new Size(10, 10);
            termCriteria = new MCvTermCriteria(20, 0.03);
            npts = 100;
            ActualFeature = new PointF[npts];
        }
        public FlowTracker(Rectangle wininit,Image<Bgr,Byte> img)
        {
            winTrack = wininit;
            ActualFrame = img;
            init();
        }
        virtual public Rectangle track(Image<Bgr,Byte> img)
        {

            return Rectangle.Empty;
        }
        public void DrawFlowVectors(int i)
        {

            System.Drawing.Point p = new Point();
            System.Drawing.Point q = new Point();

            p.X = (int)ActualFeature[i].X;
            p.Y = (int)ActualFeature[i].Y;
            q.X = (int)NextFeature[i].X;
            q.Y = (int)NextFeature[i].Y;

            double angle;
            angle = Math.Atan2((double)p.Y - q.Y, (double)p.X - q.X);

            LineSegment2D line = new LineSegment2D(p, q);
            OpticalFlowFrame.Draw(line, new Bgr(255, 0, 0), 1);

            p.X = (int)(q.X + 3 * Math.Cos(angle + Math.PI / 4));
            p.Y = (int)(q.Y + 3 * Math.Sin(angle + Math.PI / 4));
            OpticalFlowFrame.Draw(new LineSegment2D(p, q), new Bgr(Color.Red), 1);
            p.X = (int)(q.X + 3 * Math.Cos(angle - Math.PI / 4));
            p.Y = (int)(q.Y + 3 * Math.Sin(angle - Math.PI / 4));
            OpticalFlowFrame.Draw(new LineSegment2D(p, q), new Bgr(Color.Red), 1);
        }

    }
}