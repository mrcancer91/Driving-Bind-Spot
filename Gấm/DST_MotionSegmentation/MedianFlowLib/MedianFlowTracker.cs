using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace LibMotionSegmentation
{
    public class MedianFlow:FlowTracker
    {
        MedianFlowTracker medianflow;
        public MedianFlow(Rectangle wininit, Image<Bgr, Byte> frame)
            : base(wininit, frame)
        {
            medianflow = new MedianFlowTracker();
            winTrack = wininit;
            ActualFrame = frame;
            init();
        }

        public override Rectangle track(Image<Bgr, Byte> img)
        {
            NextGrayFrame=img.Convert<Gray,Byte>();
            OpticalFlowFrame = img.Copy();
            winTrack = medianflow.track(ActualGrayFrame,OpticalFlowFrame, winTrack);
            ActualGrayFrame=NextGrayFrame;
            
            //OpticalFlowFrame.Draw(winTrack,new Bgr(Color.Yellow),1);
            return winTrack;
        }
    }
}
