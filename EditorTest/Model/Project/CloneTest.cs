using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ARdevKit.Model.Project;
using System.Windows.Forms;
using System.Drawing;

namespace EditorTest.Model.Project
{
    [TestClass]
    public class CloneTest
    {

        [TestMethod]
        public void VideoAugmentationCloneTest()
        {
            VideoAugmentation vid = new VideoAugmentation();
            vid.Height = 100;
            VideoAugmentation vid2 = (VideoAugmentation)vid.Clone();
            Assert.IsTrue(vid.Height == vid2.Height);
            vid2.Height = 300;
            Assert.IsFalse(vid.Height == vid2.Height);
        }

        [TestMethod]
        public void PictureMarkerCloneTest()
        {
            PictureMarker marker = new PictureMarker();
            marker.Size = 100;
            PictureMarker marker2 = (PictureMarker)marker.Clone();
            Assert.IsTrue(marker.Size == marker2.Size);
            marker2.Size = 200;
            Assert.IsFalse(marker.Size == marker2.Size);
        }

        [TestMethod]
        public void ImageTrackableCloneTest()
        {
            ImageTrackable marker = new ImageTrackable();
            marker.Size = 100;
            ImageTrackable marker2 = (ImageTrackable)marker.Clone();
            Assert.IsTrue(marker.Size == marker2.Size);
            marker2.Size = 200;
            Assert.IsFalse(marker.Size == marker2.Size);
        }

        [TestMethod]
        public void ImageAugmentationCloneTest()
        {
            ImageAugmentation aug = new ImageAugmentation();
            aug.Width = 200;
            ImageAugmentation aug2 = (ImageAugmentation)aug.Clone();
            Assert.IsTrue(aug.Width == aug2.Width);
            aug2.Width = 300;
            Assert.IsFalse(aug.Width == aug2.Width);
        }

        [TestMethod]
        public void IDMarkerCloneTest()
        {
            IDMarker marker = new IDMarker(1);
            IDMarker marker2 = (IDMarker)marker.Clone();
            Assert.IsTrue(marker.MatrixID == marker2.MatrixID);
            marker2.MatrixID = 2;
            Assert.IsFalse(marker.MatrixID == marker2.MatrixID);
        }

        [TestMethod]
        public void FileSourceCloneTest()
        {
            FileSource source = new FileSource(@"C:\");
            source.SourceID = "1";
            FileSource source2 = (FileSource)source.Clone();
            Assert.IsTrue(source.SourceID == source2.SourceID);
            source2.SourceID = "2";
            Assert.IsFalse(source.SourceID == source2.SourceID);
        }

        [TestMethod]
        public void DbSourceCloneTest()
        {
            DbSource source = new DbSource(@"C:\");
            DbSource source2 = (DbSource)source.Clone();
            Assert.IsTrue(source.Url == source2.Url);
            source2.Url = @"D:\";
            Assert.IsFalse(source.Url == source2.Url);
        }

        [TestMethod]
        public void ChartCloneTest()
        {
            Chart chart = new Chart();
            chart.Height = 200;
            Chart chart2 = (Chart)chart.Clone();
            Assert.IsTrue(chart.Height == chart2.Height);
            chart2.Height = 300;
            Assert.IsFalse(chart.Height == chart2.Height);
        } 
    }
}
