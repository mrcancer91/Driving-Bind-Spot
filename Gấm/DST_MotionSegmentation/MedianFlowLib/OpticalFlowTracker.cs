using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
namespace LibMotionSegmentation
{
    class OpticalFlowTracker : FlowTracker
    {
        public OpticalFlowTracker(Rectangle wininit, Image<Bgr, Byte> frame)
            : base(wininit, frame)
        {
        }
        public override Rectangle track(Image<Bgr, Byte> img)
        {
            PYR[0] = new Image<Gray, float>(winTrack.Size.Width+8,winTrack.Height/3);
            PYR[1] = new Image<Gray, float>(winTrack.Size.Width + 8, winTrack.Height / 3);
            //Khoi tao motion field
            float incx = winTrack.Width * 1.0f / 10;
            float incy = winTrack.Height * 1.0f / 10;
            for (float x = winTrack.X, i = 0; i < 10; i++, x += incx)
                for (float y = winTrack.Y, j = 0; j < 10; j++, y += incy)
                {
                    int index = (int)(i * 10 + j);
                    ActualFeature[index].X = x;
                    ActualFeature[index].Y = y;
                }

            NextGrayFrame = img.Convert<Gray, Byte>();
 
            OpticalFlow.PyrLK(ActualGrayFrame, NextGrayFrame, ActualFeature, wsize, 5, termCriteria, out NextFeature, out Status, out TrackError);
            ActualGrayFrame = NextGrayFrame;

            float avgvx = 0, avgvy = 0;
            OpticalFlowFrame = img.Copy();

            int count = 0;
            for (int i = 0; i < ActualFeature.Length; i++)
            {
                DrawFlowVectors(i);
                
                //OpticalFlowFrame.Draw(winTrack, new Bgr(Color.Yellow), 1);
                
                if (Status[i] == 1)
                {
                    avgvx += NextFeature[i].X - ActualFeature[i].X;
                    avgvy += NextFeature[i].Y - ActualFeature[i].Y;
                    count++;
                }
            }

            if (count > 0)
            {
                avgvx /= count;
                avgvy /= count;
            }
            winTrack.X += (int)avgvx;
            winTrack.Y += (int)avgvy;
            return winTrack;
        }

    }
}
