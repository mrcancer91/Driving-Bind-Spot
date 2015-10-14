using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibMotionSegmentation
{
    public static class bBox
    {

        /***********************************************************
 * FUNCTION
 ***********************************************************/
        /**
         * Creates numM x numN points grid on BBox.
         * Points ordered in 1 dimensional array (x1, y1, x2, y2).
         * @param bb        Bounding box represented through 2 points(x1,y1,x2,y2)
         * @param numM      Number of points in height direction.
         * @param numN      Number of points in width direction.
         * @param margin    margin (in pixel)
         * @param pts       Contains the calculated points in the form (x1, y1, x2, y2).
         *                  Size of the array must be numM * numN * 2.
         */
        /// <summary>
        /// Tạo một lưới các điểm có kích thước numM x numN trong BBox
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="numM"></param>
        /// <param name="numN"></param>
        /// <param name="margin"></param>
        /// <param name="pts"></param>
        /// <returns></returns>
        public static int getFilledBBPoints(double[] bb, int numM, int numN, int margin, double[] pts)
        {
            int pointDim = 2;
            int i;
            int j;
            double[] pbb_local;
            /**
             * gap between points in width direction
             */
            double divN = 0;
            /**
             * gap between points in height direction
             */
            double divM = 0;
            double[] bb_local = new double[4];
            double[] center = new double[2];
            double spaceN;
            double spaceM;
            /*add margin*/
            bb_local[0] = bb[0] + margin;
            bb_local[1] = bb[1] + margin;
            bb_local[2] = bb[2] - margin;
            bb_local[3] = bb[3] - margin;
            pbb_local = bb_local;
            /*  printf("PointArraySize should be: %d\n", numM * numN * pointDim);*/
            /*handle cases numX = 1*/
            if (numN == 1 && numM == 1)
            {
                calculateBBCenter(pbb_local, pts);
                return 1;
            }
            else if (numN == 1 && numM > 1)
            {
                divM = numM - 1;
                divN = 2;
                /*maybe save center coordinate into bb[1] instead of loop again*/
                /*calculate step width*/
                spaceM = (bb_local[3] - bb_local[1]) / divM;
                calculateBBCenter(pbb_local, center);
                /*calculate points and save them to the array*/
                for (i = 0; i < numN; i++)
                {
                    for (j = 0; j < numM; j++)
                    {
                        pts[i * numM * pointDim + j * pointDim + 0] = center[0];
                        pts[i * numM * pointDim + j * pointDim + 1] = bb_local[1] + j * spaceM;
                    }
                }
                return 1;
            }
            else if (numN > 1 && numM == 1)
            {
                divM = 2;
                divN = numN - 1;
                //maybe save center coordinate into bb[1] instead of loop again
                //calculate step width
                spaceN = (bb_local[2] - bb_local[0]) / divN;
                calculateBBCenter(pbb_local, center);
                //calculate points and save them to the array
                for (i = 0; i < numN; i++)
                {
                    for (j = 0; j < numM; j++)
                    {
                        pts[i * numM * pointDim + j * pointDim + 0] = bb_local[0] + i * spaceN;
                        pts[i * numM * pointDim + j * pointDim + 1] = center[1];
                    }
                }
                return 1;
            }
            else if (numN > 1 && numM > 1)
            {
                divM = numM - 1;
                divN = numN - 1;
            }
            //calculate step width
            spaceN = (bb_local[2] - bb_local[0]) / divN;
            spaceM = (bb_local[3] - bb_local[1]) / divM;
            //calculate points and save them to the array
            for (i = 0; i < numN; i++)
            {
                for (j = 0; j < numM; j++)
                {
                    pts[i * numM * pointDim + j * pointDim + 0] = bb_local[0] + i * spaceN;
                    pts[i * numM * pointDim + j * pointDim + 1] = bb_local[1] + j * spaceM;
                }
            }
            return 1;
        }
        /**
        * Calculates center of a Rectangle/Boundingbox.
        * @param bb defined with 2 points x,y,x1,y1
        * @param center point center[0]=x,center[1]=y
        */
        /// <summary>
        /// Tính điểm trung tâm của hộp 
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        private static int calculateBBCenter(double[] bb, double[] center)
        {
            if (bb == null)
            {
                center = null;
                return 0;
            }
            center[0] = 0.5 * (bb[0] + bb[2]);
            center[1] = 0.5 * (bb[1] + bb[3]);
            return 1;
        }
        /***********************************************************
         * END OF FILE
         ***********************************************************/

    }
}
