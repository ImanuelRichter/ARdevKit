using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ARdevKit.Model.Project;
using System.Drawing;

namespace EditorTest.Model.Project
{
    [TestClass]
    public class PreviewTest
    {

        [TestMethod]
        public void VideoAugmentationPreviewTest1()
        {
            VideoAugmentation vid = new VideoAugmentation();
            vid.ResFilePath = @"C:\Test(Unit)\video.3g2";
            Bitmap preview = vid.getPreview();

            Assert.IsNotNull(preview);
        }

        [TestMethod]
        public void VideoAugmentationPreviewTest2()
        {
            VideoAugmentation vid = new VideoAugmentation();
            vid.ResFilePath = @"C:\Test(Unit)\video.3g2";
            Bitmap preview = vid.getPreview();

            //  vid.ResFilePath = ;
            Bitmap preview2 = vid.getPreview();
            Assert.IsTrue(this.CompareBitmaps(preview, preview2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void VideoAugmentationPreviewTest3()
        {
            VideoAugmentation vid = new VideoAugmentation();
            Bitmap preview = vid.getPreview();
        }

        [TestMethod]
        public void PictureMarkerPreviewTest1()
        {
            PictureMarker marker = new PictureMarker();
            marker.PicturePath = @"C:\Test(Unit)\pictureMarker1.png";
            Bitmap preview = marker.getPreview();

            Assert.IsNotNull(preview);
        }

        [TestMethod]
        public void PictureMarkerPreviewTest2()
        {
            PictureMarker marker = new PictureMarker();
            marker.PicturePath = @"C:\Test(Unit)\pictureMarker1.png";
            Bitmap preview = marker.getPreview();

            marker.PicturePath = @"C:\Test(Unit)\metaioman_target.png";
            Bitmap preview2 = marker.getPreview();

            Assert.IsFalse(this.CompareBitmaps(preview, preview2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PictureMarkerPreviewTest3()
        {
            PictureMarker marker = new PictureMarker();
            Bitmap preview = marker.getPreview();
        }

        [TestMethod]
        public void ImageTrackablePreviewTest1()
        {
            ImageTrackable marker = new ImageTrackable();
            marker.ImagePath = @"C:\Test(Unit)\pictureMarker1.png";
            Bitmap preview = marker.getPreview();

            Assert.IsNotNull(preview);
        }
        
        [TestMethod]
        public void ImageTrackablePreviewTest2()
        {
            ImageTrackable marker = new ImageTrackable();
            marker.ImagePath = @"C:\Test(Unit)\pictureMarker1.png";
            Bitmap preview = marker.getPreview();

            marker.ImagePath = @"C:\Test(Unit)\metaioman_target.png";
            Bitmap preview2 = marker.getPreview();

            Assert.IsFalse(this.CompareBitmaps(preview, preview2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ImageTrackablePreviewTest3()
        {
            ImageTrackable marker = new ImageTrackable();
            Bitmap preview = marker.getPreview();
        }

        [TestMethod]
        public void ImageAugmentationPreviewTest1()
        {
            ImageAugmentation aug = new ImageAugmentation();
            aug.ResFilePath = @"C:\Test(Unit)\pictureMarker1.png";
            Bitmap preview = aug.getPreview();

            Assert.IsNotNull(preview);
        }

        

        [TestMethod]
        public void ImageAugmentationPreviewTest2()
        {
            ImageAugmentation aug = new ImageAugmentation();
            aug.ResFilePath = @"C:\Test(Unit)\pictureMarker1.png";
            Bitmap preview = aug.getPreview();

            aug.ResFilePath = @"C:\Test(Unit)\metaioman_target.png";
            Bitmap preview2 = aug.getPreview();

            Assert.IsFalse(this.CompareBitmaps(preview, preview2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ImageAugmentationPreviewTest3()
        {
            ImageAugmentation aug = new ImageAugmentation();
            Bitmap preview = aug.getPreview();
        }

        [TestMethod]
        public void IDMarkerPreviewTest1()
        {
            IDMarker marker = new IDMarker(1);
            Bitmap preview = marker.getPreview();

            Assert.IsNotNull(preview);
        }

        [TestMethod]
        public void IDMarkerPreviewTest2()
        {
            IDMarker marker = new IDMarker(99);
            Bitmap preview = marker.getPreview();

            marker.MatrixID = 500;
            Bitmap preview2 = marker.getPreview();

            Assert.IsFalse(this.CompareBitmaps(preview, preview2));
        }

        







        private Boolean CompareBitmaps(Image left, Image right)
        {
            if (object.Equals(left, right))
                return true;
            if (left == null || right == null)
                return false;
            if (!left.Size.Equals(right.Size) || !left.PixelFormat.Equals(right.PixelFormat))
                return false;

            Bitmap leftBitmap = left as Bitmap;
            Bitmap rightBitmap = right as Bitmap;
            if (leftBitmap == null || rightBitmap == null)
                return true;

            for (int col = 0; col < left.Width; col++)
            {
                for (int row = 0; row < left.Height; row++)
                {
                    if (!leftBitmap.GetPixel(col, row).Equals(rightBitmap.GetPixel(col, row)))
                        return false;
                }
            }

            return true;
        }
    }
}
