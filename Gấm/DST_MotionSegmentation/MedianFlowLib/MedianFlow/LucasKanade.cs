using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
namespace LibMotionSegmentation
{
    public static class LucasKanade
    {

        /***********************************************************
         * CONSTANT AND MACRO DEFINITIONS
         ***********************************************************/

        private const int MAX_COUNT = 500;
        private const int MAX_IMG = 2;
        public const double N_A_N = -1.0;
        /**
         * Size of the search window of each pyramid level in cvCalcOpticalFlowPyrLK.
         */
        //private int win_size_lk = 4;
        private static Size win_size_lk = new Size(4, 4);
        private static MCvTermCriteria termCriteria = new MCvTermCriteria(20, 0.03);
        private static PointF[][] points = new PointF[3][];


        private static Image<Gray, float>[] PYR;
        /***********************************************************
 * FUNCTION
 ***********************************************************/
        /**
         * Calculates euclidean distance between the point pairs.
         * @param point1    Array of points. Pairs with point2 at every Position.
         * @param point2    Array of points. Pairs with point1 at every Position.
         * @param match     Output: Contains the result of the distance calculation.
         *                  Must have the length of nPts.
         * @param nPts      Number of pairs.
         */
        /// <summary>
        /// Tính khoảng cách giữa các cặp điểm tương ứng
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="match"></param>
        /// <param name="nPts"></param>
        static void euclideanDistance(PointF[] point1, PointF[] point2, double[] match, int nPts)
        {
            int i;
            for (i = 0; i < nPts; i++)
            {
                match[i] = Math.Sqrt((point1[i].X - point2[i].X) * (point1[i].X - point2[i].X)
                    + (point1[i].Y - point2[i].Y) * (point1[i].Y - point2[i].Y));
            }
        }
        /**
       * Calculates normalized cross correlation for every point.
       * @param imgI      Image 1.
       * @param imgJ      Image 2.
       * @param points0   Array of points of imgI
       * @param points1   Array of points of imgJ
       * @param nPts      Length of array/number of points.
       * @param status    Switch which point pairs should be calculated.
       *                  if status[i] == 1 => match[i] is calculated.
       *                  else match[i] = 0.0
       * @param match     Output: Array will contain ncc values.
       *                  0.0 if not calculated.
       * @param winsize   Size of quadratic area around the point
       *                  which is compared.
       * @param method    Specifies the way how image regions are compared.
       *                  see cvMatchTemplate
       */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgI"></param>
        /// <param name="imgJ"></param>
        /// <param name="points0"></param>
        /// <param name="points1"></param>
        /// <param name="nPts"></param>
        /// <param name="status"></param>
        /// <param name="match"></param>
        /// <param name="winsize"></param>
        /// <param name="method"></param>
        unsafe static void normCrossCorrelation(Image<Gray, Byte> imgI, Image<Gray, Byte> imgJ, PointF[] points0, PointF[] points1,
            int nPts, byte[] status, double[] match, int winsize, TM_TYPE method)
        {
            //IplImage *rec0 = cvCreateImage(cvSize(winsize, winsize), 8, 1);
            //IplImage *rec1 = cvCreateImage(cvSize(winsize, winsize), 8, 1);
            //IplImage *res = cvCreateImage(cvSize(1, 1), IPL_DEPTH_32F, 1);
            Image<Gray, Byte> rec0;
            Image<Gray, Byte> rec1;
            Image<Gray, float> res = new Image<Gray, float>(1, 1);

            //System.Diagnostics.Stopwatch stop = System.Diagnostics.Stopwatch.StartNew();

            int i;
            for (i = 0; i < nPts; i++)
            {
                if (status[i] == 1)
                {
                    int points0X = (int)points0[i].X;
                    int points0Y = (int)points0[i].Y;
                    int points1X = (int)points1[i].X;
                    int points1Y = (int)points1[i].Y;

                    rec0 = imgI.GetSubRect(new Rectangle(points0X, points0Y, Math.Min(winsize, imgI.Width-points0X), Math.Min(winsize, imgI.Height-points0Y)));
                    rec1 = imgI.GetSubRect(new Rectangle(points1X, points1Y, Math.Min(winsize, imgI.Width-points1X), Math.Min(winsize, imgI.Height-points1Y)));

                    try
                    {
                        CvInvoke.cvMatchTemplate(rec0, rec1, res, method);
                    }
                    catch
                    {
                        match[i] = 0;
                    }
                    fixed (float* ptr = res.Data)
                    {
                        match[i] = (double)*ptr;
                    }
                }
                else
                {
                    match[i] = 0.0;
                }
            }
            //stop.Stop();
            //System.Diagnostics.Debug.WriteLine(string.Format("match template in {0} ms", stop.ElapsedMilliseconds));
        }
        /**
         * Needed before start of trackLK and at the end of the program for cleanup.
         * Handles PYR(Pyramid cache) variable.
         */
        public static void initImgs()
        {
            PYR = new Image<Gray, float>[MAX_IMG];
        }
        /**
        * Tracks Points from 1.Image to 2.Image.
        * Need initImgs before start and at the end of the program for cleanup.
        *
        * @param imgI      previous Image source. (isn't changed)
        * @param imgJ      actual Image target. (isn't changed)
        * @param ptsI      points to track from first Image.
        *                  Format [0] = x1, [1] = y1, [2] = x2 ...
        * @param nPtsI     number of Points to track from first Image
        * @param ptsJ      container for calculated points of second Image.
        *                  Must have the length of nPtsI.
        * @param nPtsJ     number of Points
        * @param level     Pyramidlevel, default 5
        * @param fb        forward-backward confidence value.
        *                  (corresponds to euclidean distance between).
        *                  Must have the length of nPtsI: nPtsI * sizeof(double).
        * @param ncc       normCrossCorrelation values. needs as inputlength nPtsI * sizeof(double)
        * @param status    Indicates positive tracks. 1 = PosTrack 0 = NegTrack
        *                  needs as inputlength nPtsI * sizeof(char)
        *
        *
        * Based Matlab function:
        * lk(2,imgI,imgJ,ptsI,ptsJ,Level) (Level is optional)
        */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgI"></param>
        /// <param name="imgJ"></param>
        /// <param name="ptsI"></param>
        /// <param name="nPtsI"></param>
        /// <param name="ptsJ"></param>
        /// <param name="nPtsJ"></param>
        /// <param name="level"></param>
        /// <param name="fb"></param>
        /// <param name="ncc"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int trackLK(Image<Gray, Byte> imgI, Image<Gray, Byte> imgJ, double[] ptsI, int nPtsI,
            double[] ptsJ, int nPtsJ, int level, double[] fb, double[] ncc, byte[] status)
        {
            //TODO: watch NaN cases
            //double nan = std::numeric_limits<double>::quiet_NaN();
            //double inf = std::numeric_limits<double>::infinity();

            // tracking
            int I, J, winsize_ncc;
            //CvSize pyr_sz;
            Size pyr_sz;
            int i;
            //if unused std 5
            if (level == -1)
            {
                level = 5;
            }
            I = 0;
            J = 1;
            winsize_ncc = 10;

            //NOTE: initImgs() must be used correctly or memleak will follow.
            //pyr_sz = cvSize(imgI->width + 8, imgI->height / 3);
            pyr_sz = new Size(imgI.Width + 8, imgI.Height / 3);
            PYR[I] = new Image<Gray, float>(pyr_sz.Width, pyr_sz.Height);
            PYR[J] = new Image<Gray, float>(pyr_sz.Width, pyr_sz.Height);

            // Points
            if (nPtsJ != nPtsI)
            {
                //System.Windows.Forms.MessageBox.Show("Inconsistent input!\n");
                return 0;
            }

            points[0] = new PointF[nPtsI];//template
            points[1] = new PointF[nPtsI];//target
            points[2] = new PointF[nPtsI];//forward-backward

            //TODO:Free
            //char* statusBacktrack = (char*) malloc(nPtsI);
            byte[] statusBacktrack = new byte[nPtsI];


            for (i = 0; i < nPtsI; i++)
            {
                points[0][i].X = (float)ptsI[2 * i];
                points[0][i].Y = (float)ptsI[2 * i + 1];
                points[1][i].X = (float)ptsJ[2 * i];
                points[1][i].Y = (float)ptsJ[2 * i + 1];
                points[2][i].X = (float)ptsI[2 * i];
                points[2][i].Y = (float)ptsI[2 * i + 1];
            }


            //lucas kanade track

            CvInvoke.cvCalcOpticalFlowPyrLK(imgI, imgJ, PYR[I], PYR[J], points[0], points[1],
                nPtsI, win_size_lk, level, status, null, termCriteria,
                LKFLOW_TYPE.CV_LKFLOW_INITIAL_GUESSES);

            //backtrack
            CvInvoke.cvCalcOpticalFlowPyrLK(imgJ, imgI, PYR[J], PYR[I], points[1], points[2],
                nPtsI, win_size_lk, level, statusBacktrack, null, termCriteria,
                LKFLOW_TYPE.CV_LKFLOW_INITIAL_GUESSES | LKFLOW_TYPE.CV_LKFLOW_PYR_A_READY | LKFLOW_TYPE.CV_LKFLOW_PYR_B_READY);

            for (i = 0; i < nPtsI; i++)
            {
                if (status[i] != 0 && statusBacktrack[i] != 0)///TOi uu
                {
                    status[i] = 1;
                }
                else
                {
                    status[i] = 0;
                }
            }
            normCrossCorrelation(imgI, imgJ, points[0], points[1], nPtsI, status, ncc,
                winsize_ncc, TM_TYPE.CV_TM_CCOEFF_NORMED);
            euclideanDistance(points[0], points[2], fb, nPtsI);

            for (i = 0; i < nPtsI; i++)
            {
                if (status[i] == 1)
                {
                    ptsJ[2 * i] = points[1][i].X;
                    ptsJ[2 * i + 1] = points[1][i].Y;
                }
                else //flow for the corresponding feature hasn't been found
                {
                    //Todo: shell realy write N_A_N in it?
                    ptsJ[2 * i] = N_A_N;
                    ptsJ[2 * i + 1] = N_A_N;
                    fb[i] = N_A_N;
                    ncc[i] = N_A_N;
                }
            }
            //for (i = 0; i < 3; i++)
            //{
            //  free(points[i]);
            //  points[i] = 0;
            //}
            //free(statusBacktrack);
            return 1;
        }

        /***********************************************************
         * END OF FILE
         ***********************************************************/

    }
}
