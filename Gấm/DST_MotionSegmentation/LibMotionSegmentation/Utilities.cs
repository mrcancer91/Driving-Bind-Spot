using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace MotionSegmentation
{
    /*
     * Class contains functions for solving common problem such as:
     *      + Blob counting
     *      + Image manipulating...
     * Writen by Hung D. Bui - 2012
     * Email: duyhunghd6@gmail.com - dst06@googlegroups.com
     * Website: http://dst03.net
     */
    public static class Utilities
    {
        /// <summary>
        /// Calculates the distance from two Rectangles
        /// </summary>
        /// <param name="rect1">Rectangle 1</param>
        /// <param name="rect2">Rectangle 2</param>
        /// <returns>Distance between 2 Rects</returns>
        public static double getDistance(Rectangle rect1, Rectangle rect2)
        {
            Point c1, c2;
            c1 = new Point((rect1.X + rect1.Width) / 2, (rect1.Y + rect1.Height) / 2);
            c2 = new Point((rect2.X + rect2.Width) / 2, (rect2.Y + rect2.Height) / 2);
            return Math.Sqrt(Math.Pow(c1.X - c2.X, 2) + Math.Pow(c1.Y - c2.Y, 2));
        }

        /// <summary>
        /// Get the area that 2 Rectangle both cover
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        public static double getCoverArea(Rectangle rect1, Rectangle rect2)
        {
            double area1 = rect1.Width*1.0*rect1.Height;
            double area2 = rect2.Width*1.0*rect2.Height;
            rect1.Intersect(rect2);
            double areaCalc = rect1.Height*1.0*rect1.Width;
            return areaCalc/Math.Max(area1, area2);
        }

        /// <summary>
        /// Count the blobs from a gray image.
        /// </summary>
        /// <param name="img">Image in gray</param>
        /// <param name="minRate">Min % area accepting the blob</param>
        /// <param name="maxRate">Max % area accepting the blob</param>
        /// <returns>Rectanges boundaries the blobs</returns>
        public static List<Rectangle> BlobsCounter(Image<Gray, byte> img, double minRate, double maxRate)
        {
            List<Rectangle> ListHand = new List<Rectangle>();

            int frameWidth = img.Width;
            int frameHeight = img.Height;

            using (MemStorage storage = new MemStorage())
            {
                MCvBox2D handBox = new MCvBox2D();
                Contour<Point> contours = img.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST, storage);
                Contour<Point> biggestContour = null;
                if (minRate <= 0) minRate = 0.001;
                if (maxRate <= 0) maxRate = 0.1;

                double areaMin = minRate * frameHeight * frameWidth;
                double areaMax = maxRate * frameHeight * frameWidth;
                while (contours != null)
                {
                    biggestContour = contours;
                    double area = biggestContour.Area;

                    if (biggestContour != null && area >= areaMin && area <= areaMax)
                    {
                        Contour<Point> currentContour = biggestContour.ApproxPoly(biggestContour.Perimeter * 0.0025, storage);
                        biggestContour = currentContour;
                        handBox = biggestContour.GetMinAreaRect();
                        PointF[] points = handBox.GetVertices();
                        ListHand.Add(biggestContour.BoundingRectangle);

                        Seq<Point> hulles = biggestContour.GetConvexHull(Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE);

                        Seq<MCvConvexityDefect> defectsList = biggestContour.GetConvexityDefacts(storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE);
                    }
                    contours = contours.HNext;
                }
            }
            return ListHand;
        }
    }
}
