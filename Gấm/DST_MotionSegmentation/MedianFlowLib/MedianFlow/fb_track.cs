using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace LibMotionSegmentation
{
    public static class fb_track
    {

        /***********************************************************
         * FUNCTION
         ***********************************************************/

        /**
         * Calculate the bounding box of an Object in a following Image.
         * Imgs aren't changed.
         * @param imgI       Image contain Object with known BoundingBox
         * @param imgJ       Following Image.
         * @param bb         Bounding box of object to track in imgI.
         *                   Format x1,y1,x2,y2
         * @param scaleshift returns relative scale change of bb
         */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgI"></param>
        /// <param name="imgJ"></param>
        /// <param name="bb"></param>
        /// <param name="bbnew"></param>
        /// <param name="scaleshift"></param>
        /// <returns></returns>
        public static int fbtrack(Image<Gray, Byte> imgI, Image<Bgr, Byte> imgJ, double[] bb, double[] bbnew, ref double scaleshift)
        {
            byte level = 5;
            int numM = 10;
            int numN = 10;
            int nPoints = numM * numN;
            int sizePointsArray = nPoints * 2;

            double[] fb;
            double[] ncc;
            byte[] status;
            fb = new double[nPoints];
            ncc = new double[nPoints];
            status = new byte[nPoints];
            double[] pt;
            double[] ptTracked;
            pt = new double[sizePointsArray];
            ptTracked = new double[sizePointsArray];
            int nlkPoints;

            PointF[] startPoints;
            PointF[] targetPoints;

            double[] fbLkCleaned;
            double[] nccLkCleaned;
            int i, M;
            int nRealPoints;
            double medFb;
            double medNcc;
            int nAfterFbUsage;
            bBox.getFilledBBPoints(bb, numM, numN, 5, pt);
            //getFilledBBPoints(bb, numM, numN, 5, &ptTracked);
            //memcpy(ptTracked, pt, sizeof(double) * sizePointsArray);
            pt.CopyTo(ptTracked, 0);
            Image<Gray, Byte> grayJ = imgJ.Convert<Gray, Byte>();
            LucasKanade.initImgs();
            LucasKanade.trackLK(imgI, grayJ, pt, nPoints, ptTracked, nPoints, level, fb, ncc, status);
            LucasKanade.initImgs();

            //  char* status = *statusP;
            nlkPoints = 0;
            for (i = 0; i < nPoints; i++)
            {
                nlkPoints += status[i];
            }
            startPoints = new PointF[nlkPoints];
            targetPoints = new PointF[nlkPoints];
            fbLkCleaned = new double[nlkPoints];
            nccLkCleaned = new double[nlkPoints];

            M = 2;
            nRealPoints = 0;

            for (i = 0; i < nPoints; i++)
            {
                //TODO:handle Missing Points
                //or status[i]==0
                if (ptTracked[M * i] == -1)
                {
                }
                else
                {
                    startPoints[nRealPoints].X = (float)pt[2 * i];
                    startPoints[nRealPoints].Y = (float)pt[2 * i + 1];
                    targetPoints[nRealPoints].X = (float)ptTracked[2 * i];
                    targetPoints[nRealPoints].Y = (float)ptTracked[2 * i + 1];
                    fbLkCleaned[nRealPoints] = fb[i];
                    nccLkCleaned[nRealPoints] = ncc[i];
                    nRealPoints++;
                }
            }
            //assert nRealPoints==nlkPoints
            medFb = Function.getMedian(fbLkCleaned, nlkPoints);
            medNcc = Function.getMedian(nccLkCleaned, nlkPoints);
            /*  printf("medianfb: %f\nmedianncc: %f\n", medFb, medNcc);
             printf("Number of points after lk: %d\n", nlkPoints);*/
            nAfterFbUsage = 0;
            for (i = 0; i < nlkPoints; i++)
            {
                if ((fbLkCleaned[i] <= medFb) & (nccLkCleaned[i] >= medNcc))
                {
                    startPoints[nAfterFbUsage] = startPoints[i];
                    targetPoints[nAfterFbUsage] = targetPoints[i];
                    nAfterFbUsage++;
                }
            }

            for (int ii = 0; ii < nAfterFbUsage; ii++)
            {

                System.Drawing.Point p = new Point();
                System.Drawing.Point q = new Point();

                p.X = (int)startPoints[ii].X;
                p.Y = (int)startPoints[ii].Y;
                q.X = (int)targetPoints[ii].X;
                q.Y = (int)targetPoints[ii].Y;

                double angle;
                angle = Math.Atan2((double)p.Y - q.Y, (double)p.X - q.X);

                LineSegment2D line = new LineSegment2D(p, q);
                imgJ.Draw(line, new Bgr(255, 0, 0), 1);

                p.X = (int)(q.X + 3 * Math.Cos(angle + Math.PI / 4));
                p.Y = (int)(q.Y + 3 * Math.Sin(angle + Math.PI / 4));
                imgJ.Draw(new LineSegment2D(p, q), new Bgr(Color.Red), 1);
                p.X = (int)(q.X + 3 * Math.Cos(angle - Math.PI / 4));
                p.Y = (int)(q.Y + 3 * Math.Sin(angle - Math.PI / 4));
                imgJ.Draw(new LineSegment2D(p, q), new Bgr(Color.Red), 1);
            }

            /*printf("Number of points after fb correction: %d\n", nAfterFbUsage);*/
            //  showIplImage(IMGS[1]);
            // show "OpticalFlow" fb filtered.
            //  drawLinesCvPoint2D32f(imgI, startPoints, nRealPoints, targetPoints,
            //      nRealPoints);
            //  showIplImage(imgI);

            bb_predict.predictbb(bb, startPoints, targetPoints, nAfterFbUsage, bbnew, ref scaleshift);
            /*printf("bbnew: %f,%f,%f,%f\n", bbnew[0], bbnew[1], bbnew[2], bbnew[3]);
             printf("relative scale: %f \n", scaleshift[0]);*/
            //show picture with tracked bb
            //  drawRectFromBB(imgJ, bbnew);
            //  showIplImage(imgJ);

            if (medFb > 10) return 0;
            return 1;

        }
        /***********************************************************
         * END OF FILE
         ***********************************************************/

    }
}
