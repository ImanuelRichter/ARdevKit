using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ARdevKit.Model.Project;
using System.Windows.Forms;
using System.Drawing;

namespace EditorTest.Model.Project
{
    [TestClass]
    public class VideoAugmentationTest
    {
        [TestMethod]
        public void PreviewTest1()
        {
            VideoAugmentation vid = new VideoAugmentation();
            vid.ResFilePath = @"C:\Test(Unit)\video.3g2";
            Bitmap preview = vid.getPreview();

            Assert.IsNotNull(preview);
        }

        [TestMethod]
        public void PreviewTest2()
        {
            VideoAugmentation vid = new VideoAugmentation();
            vid.ResFilePath = @"C:\Test(Unit)\video.3g2";
            Bitmap preview = vid.getPreview();

          //  vid.ResFilePath = ;
            Bitmap preview2 = vid.getPreview();
            Assert.IsTrue(this.CompareBitmaps(preview, preview2));
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
