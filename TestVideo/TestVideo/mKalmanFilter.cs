using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using System.Drawing;
namespace TestVideo
{
    class mKalmanFilter
    {
        KalmanFilter kal ;
        
        SyntheticData syntheticData;
        public Matrix<float> processNoise;

        public void initKalmanFilter()
        {
            kal = new KalmanFilter(4, 2, 0);
            syntheticData = new SyntheticData();
                Matrix<float> state = new Matrix<float>(new float[]
                {
                    0.0f, 0.0f, 0.0f, 0.0f
                });
                //kal.CorrectedState = state;
                kal.StatePre.SetTo(state);
                kal.TransitionMatrix.SetTo(syntheticData.transitionMatrix);
                //kal.MeasurementNoiseCovariance = syntheticData.measurementNoise;
                kal.MeasurementNoiseCov.SetTo(syntheticData.measurementNoise);
                //kal.ProcessNoiseCovariance = syntheticData.processNoise;
                kal.ProcessNoiseCov.SetTo(syntheticData.processNoise);
                //kal.ErrorCovariancePost = syntheticData.errorCovariancePost;
                kal.ErrorCovPost.SetTo(syntheticData.errorCovariancePost);
                //kal.MeasurementMatrix = syntheticData.measurementMatrix;
                kal.MeasurementMatrix.SetTo(syntheticData.measurementNoise);
                //kal.MeasurementMatrix = null;
        }
        public void startTracking(Rectangle rect)
        {
            syntheticData.state[0, 1] = rect.X;
            syntheticData.state[1, 1] = rect.Y;
            syntheticData.state[2, 1] = rect.Width;
            syntheticData.state[3, 1] = rect.Height;
            kal.Predict();
            
        }
    }
}
