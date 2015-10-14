using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibMotionSegmentation
{
    public static class Function
    {
        private static Random rand = new Random();
        public static double getRand()
        {
            return rand.NextDouble();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private static void elem_swap(double[] a, int i, int j)
        {
            double t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        public static bool isRecEmpty(System.Drawing.Rectangle rect)
        {
            return rect.Width == 0 || rect.Height == 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="widthstep"></param>
        /// <returns></returns>
        public static int sub2idx(double x, double y, double widthstep) 
        { return (int)(Math.Floor((x) + 0.5) + Math.Floor((y) + 0.5) * (widthstep)); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int convertd2i(double a)
        {
            return (int)Math.Floor(a + 0.5);
        }
        public static int convert2i(float a)
        {
            return (int)Math.Floor(a + 0.5);
        }
        /**
         * Returns median of the array. Changes array!
         * @param arr the array
         * @pram n length of array
         *
         *  This Quickselect routine is based on the algorithm described in
         *  "Numerical recipes in C", Second Edition,
         *  Cambridge University Press, 1992, Section 8.5, ISBN 0-521-43108-5
         *  This code by Nicolas Devillard - 1998. Public domain.
         */
        /// <summary>
        /// Tính trung vị của dãy số thực
        /// </summary>
        /// <param name="arr">Dãy số thực</param>
        /// <param name="n">Số phần tử của dãy</param>
        /// <returns>Trung vị</returns>
        public static double getMedianUnmanaged(double[] arr, int n)
        {
            
            int low, hight;
            int median;
            int middle, ll, hh;

            low = 0;
            hight = n - 1;
            if (hight<0) return 0;
            median = (low + hight) / 2;
            for (; ; )
            {
                if (hight <= low) /* One element only */
                    return arr[median];

                if (hight == low + 1)
                { /* Two elements only */
                    if (arr[low] > arr[hight])
                        elem_swap(arr, low, hight);
                    return arr[median];
                }

                /* Find median of low, middle and high items; swap into position low */
                middle = (low + hight) / 2;
                if (arr[middle] > arr[hight])
                    elem_swap(arr, middle, hight);
                if (arr[low] > arr[hight])
                    elem_swap(arr, low, hight);
                if (arr[middle] > arr[low])
                    elem_swap(arr, middle, low);

                /* Swap low item (now in position middle) into position (low+1) */
                elem_swap(arr, middle, low + 1);

                /* Nibble from each end towards middle, swapping items when stuck */
                ll = low + 1;
                hh = hight;
                for (; ; )
                {
                    do
                        ll++;
                    while (arr[low] > arr[ll]);
                    do
                        hh--;
                    while (arr[hh] > arr[low]);

                    if (hh < ll)
                        break;

                    elem_swap(arr, ll, hh);
                }

                /* Swap middle item (in position low) back into correct position */
                elem_swap(arr, low, hh);

                /* Re-set active partition */
                if (hh <= median)
                    low = ll;
                if (hh >= median)
                    hight = hh - 1;
            }

        }


        /// Calculates Median of the array. Don't change array(makes copy).
        /// @param arr the array
        /// @pram n length of array
        /// 

        /// <summary>
        /// TÍnh trung vị của mảng số thực mà không làm thay đổi thứ tự các phần tử trong mảng
        /// </summary>
        /// <param name="arr">Mảng số thực</param>
        /// <param name="n">Số phần tử của mảng</param>
        /// <returns>Trung vị của mảng</returns>
        public static double getMedian(double[] arr, int n)
        {
            double[] temP = new double[n];
            arr.CopyTo(temP, 0);
            
            double median;
            median = getMedianUnmanaged(temP, n);
            return median;
        }


        /***********************************************************
         * END OF FILE
         ***********************************************************/

    }
}
