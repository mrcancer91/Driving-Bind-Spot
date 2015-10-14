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
    public class MotionSegmentationByImageSubtraction : IMotionSegmentation
    {

        #region Define variables
        
        Emgu.CV.VideoSurveillance.BlobTrackerAutoParam<Bgr> param;

        //Forground image
        private Image<Bgr, byte> forgroundImage;
        
        //Current image received from current process
        private Image<Bgr, byte> currentImage;


        //Status of segmentation engine
        public bool TrackingStarted { get; set; }

        //Frame with, frame height
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }

        //index of image
        public int Index { get; set; }

        private Rectangle detectingRectangle;

        private List<Tracker> listObject;

        #endregion

        /// <summary>
        /// Motion segmentation approaches multiple target tracking
        /// </summary>
        public MotionSegmentationByImageSubtraction()
        {
            TrackingStarted = false;
        }

        /// <summary>
        /// Initialized motion segmentation
        /// </summary>
        public void InitMotionSegmentation()
        {
            param = new Emgu.CV.VideoSurveillance.BlobTrackerAutoParam<Bgr>();
            param.FGDetector = new Emgu.CV.VideoSurveillance.FGDetector<Bgr>(Emgu.CV.CvEnum.FORGROUND_DETECTOR_TYPE.MOG);
            param.BlobTracker = new Emgu.CV.VideoSurveillance.BlobTracker(Emgu.CV.CvEnum.BLOBTRACKER_TYPE.CCMSPF);

            //Frame saved to history
            param.FGTrainFrames = 10;
            listObject = new List<Tracker>();
            
        }

        /// <summary>
        /// Segments
        /// </summary>
        /// <param name="image"></param>
        public void Segmentation(ref Image<Bgr, byte> image)
        {
            if (detectingRectangle == null && image != null)
                initRectangle(image);

            //May not assigned
            currentImage = image;

            if (image != null)
            {
                //Tracking
                for (int i = 0; i < listObject.Count; i++)
                {
                    listObject[i].currentFrame = currentImage;
                    listObject[i].Track();
                }

                param.FGDetector.Update(image);

                Image<Gray, byte> imgForground = param.FGDetector.ForgroundMask;

                imgForground = imgForground.SmoothMedian(5);
                
                List<Rectangle> ListBlobls = Utilities.BlobsCounter(imgForground, 0.001, 0.1);

                //Prunning duplicate objects
                List<Rectangle> tempObj = new List<Rectangle>();
                List<Point> tempPnt = new List<Point>();

                for (int i = listObject.Count-1; i >= 0 ; i--)
                {
                    bool isExisted = false;
                    for (int j = 0; j < tempObj.Count; j++)
                    {
                        double distance = 0.0;
                        Point centerRect = new Point((tempObj[j].X+tempObj[j].Width)/2, (tempObj[j].Y+tempObj[j].Height)/2);
                        Point centerPoint = new Point((listObject[i].CurrentState.X+listObject[i].CurrentState.Width)/2, (listObject[i].CurrentState.Y+listObject[i].CurrentState.Height)/2);
                        distance = Math.Pow(centerRect.X - centerPoint.X, 2) + Math.Pow(centerRect.Y - centerPoint.Y, 2);
                        if (distance <= 50 * 50)
                        {
                            //loai bo 1 doi tuong
                            isExisted = true;
                            break;
                        }
                            
                    }

                    if(isExisted)
                        listObject.RemoveAt(i);
                    else
                        tempObj.Add(listObject[i].CurrentState);
            }

                
                //Prunning the false object
                bool stop = false;
                for (int i = 0; i < ListBlobls.Count; i++)
                {
                    if(!stop)
                    for (int j = 0; j < listObject.Count; j++)
                    {
                        if (!stop)
                        if (Utilities.getCoverArea(listObject[j].CurrentState, ListBlobls[i]) >= 0.3) //Blob i_th counted actually is Object[j] being tracked
                        {
                            ListBlobls.RemoveAt(i);
                            if (i >= 1) i--;
                            if (ListBlobls.Count == 0 || listObject.Count == 0) stop = true;
                        }
                    }
                }

                //Nhét hết các đối tượng vào bộ tracker mới
                foreach (Rectangle rect in ListBlobls)
                {
                    Tracker newTrack = new Tracker();
                    newTrack.currentFrame = currentImage;
                    newTrack.InitNewTracker(rect);
                    newTrack.KFilter.Filtering(currentImage, rect);
                    listObject.Add(newTrack);
                }

                //Khi đối tượng xuất hiện ở trong khung RECT main thì tóm lấy đối tượng, thêm khởi tạo mới

                for (int i = 0; i < listObject.Count; i++)
                {
                    image.Draw(listObject[i].CurrentState, new Bgr(Color.Blue), 2);
                }
                foreach (Rectangle item in ListBlobls)
                {
                    image.Draw(item, new Bgr(Color.Red), 2);
                }
            }
        }

        private void initRectangle(Image<Bgr, byte> img)
        {
            int _x, _y, _w, _h;

            _x = img.Width/10;
            _y = img.Height/10;
            _w = (8 * img.Width) / 10;
            _h = (8 * img.Height) / 10;

            detectingRectangle = new Rectangle(_x, _y, _w, _h);
        }
    }
}
