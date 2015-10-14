using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace LibMotionSegmentation
{
    class MedianFlowTracker
    {

        public Rectangle trackerBB;
        double scale;
        /// <summary>
        /// 
        /// </summary>
        public void cleanPreviousData()
        {
            trackerBB = new Rectangle();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prevMat"></param>
        /// <param name="currMat"></param>
        /// <param name="prevBB"></param>
        public Rectangle track(Image<Gray, Byte> prevMat, Image<Bgr, Byte> currMat, Rectangle prevBB)
        {
            if (!Function.isRecEmpty(prevBB))
            {
                double[] bb_tracker = { prevBB.X, prevBB.Y, prevBB.X + prevBB.Width- 1, prevBB.Y + prevBB.Height - 1 };
                ///?????/////////???????????????????????????????????????????????????????
                Image<Gray, Byte> prevImg = prevMat;
                Image<Bgr, Byte> currImg = currMat;

                int success = fb_track.fbtrack(prevImg, currImg, bb_tracker, bb_tracker, ref scale);

                //Extract subimage
                int x, y, w, h;
                x = (int)Math.Floor(bb_tracker[0] + 0.5);
                y = (int)Math.Floor(bb_tracker[1] + 0.5);
                w = (int)Math.Floor(bb_tracker[2] - bb_tracker[0] + 1 + 0.5);
                h = (int)Math.Floor(bb_tracker[3] - bb_tracker[1] + 1 + 0.5); 

                if (success == 0 || x < 0 || y < 0 || w <= 0 || h <= 0 || x + w > currMat.Width || y + h > currMat.Height)
                {
                    trackerBB = new Rectangle();
                    //Leave it empty
                }
                else
                {
                    trackerBB = new Rectangle(x, y, w, h);
                }
                
            }
            return trackerBB;
        }

    } /* namespace tld */



}
