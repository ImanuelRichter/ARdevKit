using ARdevKit;
using ARdevKit.Model.Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace EditorTest.Model.Project
{
    [TestClass]
    public class VideoAugmentationTest
    {
        [TestMethod]
        public void getPreviewTest()
        {
            VideoAugmentation aug = new VideoAugmentation();
            aug.initElement(null);
            Bitmap s = aug.getPreview();
            Debug.WriteLine(s == null);
            Debug.WriteLine(s.Height);
        }
    }
}
