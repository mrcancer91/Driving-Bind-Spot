using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using LibMotionSegmentation;

namespace MotionSegmentation
{
    class Tracker
    {
        public KalmanFilter KFilter { get; set; }
        public Rectangle CurrentState { get; set; }
        public List<Rectangle> StateHistory { get; set; }
        public FlowTracker flowTracker { get; set; }
        public Image<Bgr, byte> currentFrame { get; set; }
        public void InitNewTracker(Rectangle rectInit)
        {
            CurrentState = rectInit;
            KFilter = new KalmanFilter();
            StateHistory = new List<Rectangle>();
            flowTracker = new MedianFlow(rectInit, currentFrame);
            KFilter.startTracking(rectInit);
            StateHistory.Add(rectInit);
        }

        public void Track()
        {
            flowTracker.track(currentFrame);
            //current state
            flowTracker.winTrack = KFilter.Filtering(currentFrame, flowTracker.winTrack);
            CurrentState = flowTracker.winTrack;
            StateHistory.Add(CurrentState);
        }

        public void DestroyTracker()
        {
            StateHistory.Clear();
        }
    }
}
