using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing;

namespace LibMotionSegmentation
{
    public class KalmanFilter
    {
        PointF lastPoint;
        PointF predictPoint, measurePoint;

        Matrix<float> measurement;
        Matrix<float> realPosition;
        Matrix<float> prediction;
        Rectangle predictTrackWin;
        private Rectangle trackWindows;

        private Rectangle selection;
        float scale;

        Kalman kalmanFilter;
        
        public bool isTrack { get; set; }

        public KalmanFilter()
        {
            kalmanFilter = initKalmanFilter();
            measurement = new Matrix<float>(2, 1);
            realPosition = new Matrix<float>(4, 1);
        }

        public void startTracking(Rectangle rec)
        {
            selection = rec;
            trackWindows = rec;
            isTrack = false;
            scale = 1.0f * selection.Height / selection.Width;
        }

        /// <summary>
        /// Theo vết đối tượng
        /// </summary>
        /// <param name="image">Ảnh đã tách màu da lấy từ các frame tuần tự</param>
        /// <returns>Hình chữ nhật chứa vùng bàn tay di chuyển</returns>
        public Rectangle Filtering(Image<Bgr, byte> image, Rectangle trackRect)
        {
            if (image != null) //Nếu ảnh truyền vào tracking khác null
            {
                if (isTrack == false)       //Nếu chế độ track chưa được khởi động
                {

                    if (trackWindows.Width == 0 || trackWindows.Height == 0) return Rectangle.Empty;

                    isTrack = true;

                    lastPoint = predictPoint = new PointF(trackWindows.X + trackWindows.Width / 2,
                                                            trackWindows.Y + trackWindows.Height / 2);

                    getCurrentState(kalmanFilter, lastPoint, predictPoint); //input curent state

                    return trackWindows;
                }
                else   //isTrack = true => Đã khởi tạo chế độ track
                {

                    prediction = kalmanFilter.Predict();
                    predictPoint = calc_point(prediction);

                    predictTrackWin = new Rectangle((int)(predictPoint.X - (trackWindows.Width / 2)),
                        (int)(predictPoint.Y - trackWindows.Height / 2), trackWindows.Width, trackWindows.Height);

                    trackWindows = changeRect(new Rectangle(0, 0, image.Width, image.Height), predictTrackWin, trackWindows);

                    trackWindows.Height = (int)(scale * trackWindows.Width);


                    trackWindows = trackRect;
                    measurePoint = new PointF(trackWindows.X + trackWindows.Width / 2,
                                            trackWindows.Y + trackWindows.Height / 2);

                    //Update from measurement
                    realPosition[0, 0] = measurePoint.X;
                    realPosition[1, 0] = measurePoint.Y;
                    realPosition[2, 0] = trackWindows.Width;
                    realPosition[3, 0] = trackWindows.Height;
                    lastPoint = measurePoint;

                    measurement = kalmanFilter.MeasurementMatrix.Mul(realPosition).Add(kalmanFilter.MeasurementNoiseCovariance);
                    //kalmanFilter.MeasurementMatrix.Mul(kalmanFilter).Add(kalmanFilter.MeasurementNoiseCovariance);

                    kalmanFilter.Correct(measurement);
                    kalmanFilter.CorrectedState = kalmanFilter.TransitionMatrix.Mul(kalmanFilter.CorrectedState);

                    //if (image.MIplImage.origin != 0)
                    //    trackBox.angle = -trackBox.angle;

                    return trackWindows;
                }
            } //image != null
            return Rectangle.Empty;
        }

        private Kalman initKalmanFilter()
        {
            Kalman kalman = new Kalman(4, 1, 0);                //Mo hinh co gia toc

            //Transition matrix
            Matrix<float> F = new Matrix<float>(new float[,]{   {1.0f,  0.0f,  0.1f,  0.0f},
                                                                {0.0f,  1.0f,  0.0f,  0.1f},
                                                                {0.0f,  0.0f,  1.0f,  0.0f},
                                                                {0.0f,  0.0f,  0.0f,  1.0f}
                                                            });
            F.CopyTo(kalman.TransitionMatrix);

            CvInvoke.cvSetIdentity(kalman.MeasurementMatrix,            new MCvScalar(1));      //H
            CvInvoke.cvSetIdentity(kalman.ProcessNoiseCovariance,       new MCvScalar(1e-4));   //Q w
            CvInvoke.cvSetIdentity(kalman.MeasurementNoiseCovariance,   new MCvScalar(1e-6));   //R v
            CvInvoke.cvSetIdentity(kalman.ErrorCovariancePost,          new MCvScalar(1));      //P
            return kalman;
        }

        private void getCurrentState(Kalman currentKalman, PointF point1, PointF point2)
        {
            Matrix<float> input = new Matrix<float>(new float[] { point2.X, point2.Y, point2.X - point1.X, point2.Y - point1.Y });//currentstate
            input.CopyTo(currentKalman.CorrectedState);
        }

        private PointF calc_point(Matrix<float> matrix)
        {
            return new PointF(matrix[0, 0], matrix[1, 0]);
        }

        private Rectangle changeRect(Rectangle A, Rectangle B, Rectangle C)  //Kiem tra xem B co nam trong A khong
        {                                                                   //neu khong thi sua B theo C
            if (B.X < A.X || B.X + B.Width > A.Width || B.Width <= 0 || B.Y < A.Y || B.Y + B.Height > A.Height || B.Height <= 0) return (C);
            return (B);
        }

        private Rectangle changeRect(Rectangle A, Rectangle B)
        {
            if (B.X < A.X)
                B.X = A.X;
            if (B.X + Math.Abs(B.Width) > A.Width)
                B.Width = A.Width - B.X;
            if (B.Y < A.Y)
                B.Y = A.Y;
            if (B.Y + Math.Abs(B.Height) > A.Height)
                B.Height = A.Height - B.Y;
            return (B);
        }
    }
}
