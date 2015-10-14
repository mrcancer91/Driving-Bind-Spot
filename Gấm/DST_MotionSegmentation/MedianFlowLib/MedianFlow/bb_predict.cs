using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;


namespace LibMotionSegmentation
{
    public static class bb_predict
    {
        /***********************************************************
         * FUNCTION
         ***********************************************************/
        /**
 * Returns width of Boundingbox.
 * @param bb Boundingbox
 */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bb"></param>
        /// <returns></returns>
        private static double getBbWidth(double[] bb)
        {
            return Math.Abs(bb[2] - bb[0]) + 1;
        }

        /**
         * Returns hight of Boundingbox.
         * @param bb Boundingbox
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bb"></param>
        /// <returns></returns>
        private static double getBbHeight(double[] bb)
        {
            return Math.Abs(bb[3] - bb[1]) + 1;
        }
        /**
         * Calculates the new (moved and resized) Bounding box.
         * Calculation based on all relative distance changes of all points
         * to every point. Then the Median of the relative Values is used.
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bb0"></param>
        /// <param name="pt0"></param>
        /// <param name="pt1"></param>
        /// <param name="nPts"></param>
        /// <param name="bb1"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        public static int predictbb(double[] bb0, PointF[] pt0, PointF[] pt1, int nPts,
            double[] bb1, ref double shift)
        {
            double[] ofx = new double[nPts];
            double[] ofy = new double[nPts];
            int i;
            int j;
            int d = 0;
            double dx, dy;
            int lenPdist;
            double[] dist0;
            double[] dist1;
            double s0, s1;
            for (i = 0; i < nPts; i++)
            {
                ofx[i] = pt1[i].X - pt0[i].X;
                ofy[i] = pt1[i].Y - pt0[i].Y;
            }
            dx = Function.getMedianUnmanaged(ofx, nPts);
            dy = Function.getMedianUnmanaged(ofy, nPts);

            //m(m-1)/2
            lenPdist = (int)(nPts * (nPts - 1) * 1.0 / 2);

            dist0 = new double[lenPdist];
            dist1 = new double[lenPdist];
            for (i = 0; i < nPts; i++)
            {
                for (j = i + 1; j < nPts; j++, d++)
                {
                    dist0[d] = Math.Sqrt(Math.Pow(pt0[i].X - pt0[j].X, 2) + Math.Pow(pt0[i].Y - pt0[j].Y, 2));
                    dist1[d] = Math.Sqrt(Math.Pow(pt1[i].X - pt1[j].X, 2) + Math.Pow(pt1[i].Y - pt1[j].Y, 2));
                    dist0[d] = dist1[d] / dist0[d];
                }
            }
            //The scale change is the median of all changes of distance.
            //same as s = median(d2./d1) with above
            shift = Function.getMedianUnmanaged(dist0, lenPdist);
            s0 = 0.5 * (shift - 1) * getBbWidth(bb0);
            s1 = 0.5 * (shift - 1) * getBbHeight(bb0);

            //Apply transformations (translation& scale) to old Bounding Box
            bb1[0] = bb0[0] - s0 + dx;
            bb1[1] = bb0[1] - s1 + dy;
            bb1[2] = bb0[2] + s0 + dx;
            bb1[3] = bb0[3] + s1 + dy;

            //return absolute scale change
            //  shift[0] = s0;
            //  shift[1] = s1;

            return 1;
        }
        /***********************************************************
         * END OF FILE
         ***********************************************************/

    }
}
