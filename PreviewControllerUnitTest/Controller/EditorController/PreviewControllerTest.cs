using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ARdevKit.Model.Project;
using ARdevKit;
using System.Drawing;

namespace Tests
{
    [TestClass()]
    public class PreviewControllerTest
    {
        [TestMethod()]
        public void constructorTest01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();

            //act
            PreviewController prevcontr = new PreviewController(ew);

            //assert
            Assert.IsNotNull(prevcontr);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void constructorTest02()
        {
            //arrange

            //act
            PreviewController prevcontr = new PreviewController(null);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void addPreviewAbleTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable prev = new IDMarker(1);
            Vector3D v = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.addPreviewable(prev, v);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void addPreviewAbleTest_nullArgument02()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable prev = null;
            Vector3D v = new Vector3D(Double.MinValue, Double.MinValue, Double.MinValue);
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.addPreviewable(prev, v);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void addPreviewAbleTest_nullArgument03()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable prev = null;
            Vector3D v = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.addPreviewable(prev, v);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void addPreviewAbleTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void addSourceTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            AbstractSource source = new FileSource(System.IO.Directory.GetCurrentDirectory() + "\\res\\highcharts\\barChartColumn\\data.xml");
            AbstractAugmentation augmentation = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.addSource(source, augmentation);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void addSourceTest_nullArgument02()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            AbstractSource source = null;
            AbstractAugmentation augmentation = new Chart();
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.addSource(source, augmentation);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void addSourceTest_nullArgument03()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            AbstractSource source = null;
            AbstractAugmentation augmentation = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.addSource(source, augmentation);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void addSourceTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void removeSourceTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            AbstractSource source = new FileSource(System.IO.Directory.GetCurrentDirectory() + "\\res\\highcharts\\barChartColumn\\data.xml");
            IPreviewable previewable = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.removeSource(source, previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void removeSourceTest_nullArgument02()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            AbstractSource source = null;
            IPreviewable previewable = new Chart();
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.removeSource(source, previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void removeSourceTest_nullArgument03()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            AbstractSource source = null;
            IPreviewable previewable = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.removeSource(source, previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void removeSourceTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void removePreviewableTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable previewable = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.removePreviewable(previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void removePreviewableTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        public void updatePreviewPanelTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        public void reloadPreviewPanelTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void reloadPreviewableTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            AbstractAugmentation augmentation = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.reloadPreviewable(augmentation);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void reloadPreviewableTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void addPictureBoxTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable prev = new IDMarker(1);
            Vector3D v = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.addPictureBox(prev, v);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void addPictureBoxTest_nullArgument02()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable prev = null;
            Vector3D v = new Vector3D(Double.MinValue, Double.MinValue, Double.MinValue);
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.addPictureBox(prev, v);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void addPictureBoxTest_nullArgument03()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable prev = null;
            Vector3D v = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.addPictureBox(prev, v);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void addPictureBoxTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void findBoxTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable previewable = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.findBox(previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void findBoxTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void setCurrentElementTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable previewable = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.setCurrentElement(previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void setCurrentElementTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void scaleIPreviewableTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable previewable = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.scaleIPreviewable(previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void scaleIPreviewableTest_nullArgument02()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable previewable = new PictureMarker(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);
            //no trackable set

            //act
            prevcontr.scaleIPreviewable(previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void scaleIPreviewableTest01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable previewable = new PictureMarker(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);
            prevcontr.trackable = new IDMarker(1);

            //act
            Bitmap bmp = prevcontr.scaleIPreviewable(previewable);

            //assert
            Assert.IsNotNull(bmp);
            Assert.IsInstanceOfType(bmp, typeof(Bitmap));
        }

        [TestMethod()]
        public void scaleIPreviewableTest02()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void scaleBitmapTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            Bitmap bmp1 = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            Bitmap bmp2 = prevcontr.scaleBitmap(bmp1, 128, 128);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void scaleBitmapTest_nullArgument02()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            Bitmap bmp1 = new Bitmap(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);

            //act
            Bitmap bmp2 = prevcontr.scaleBitmap(bmp1, int.MinValue, 128);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void scaleBitmapTest_nullArgument03()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            Bitmap bmp1 = new Bitmap(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);

            //act
            Bitmap bmp2 = prevcontr.scaleBitmap(bmp1, 128, int.MinValue);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void scaleBitmapTest01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            Bitmap bmp1 = new Bitmap(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);

            //act
            Bitmap bmp2 = prevcontr.scaleBitmap(bmp1, 128, 256);

            //assert
            Assert.IsNotNull(bmp2);
            Assert.IsInstanceOfType(bmp2, typeof(Bitmap));
            Assert.AreNotSame(bmp2, bmp1);
            Assert.IsTrue(bmp2.Size.Width == 128);
            Assert.IsTrue(bmp2.Size.Height == 256);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void scaleBitmapTest02()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            Bitmap bmp1 = new Bitmap(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);

            //act
            Bitmap bmp2 = prevcontr.scaleBitmap(bmp1, 0, 0);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void scaleBitmapTest03()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            Bitmap bmp1 = new Bitmap(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);

            //act
            Bitmap bmp2 = prevcontr.scaleBitmap(bmp1, 0, 1);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void scaleBitmapTest04()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            Bitmap bmp1 = new Bitmap(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);

            //act
            Bitmap bmp2 = prevcontr.scaleBitmap(bmp1, 1, 0);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void scaleBitmapTest05()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            Bitmap bmp1 = new Bitmap(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);

            //act
            Bitmap bmp2 = prevcontr.scaleBitmap(bmp1, 2, 1);

            //assert
            Assert.IsNotNull(bmp2);
            Assert.IsInstanceOfType(bmp2, typeof(Bitmap));
            Assert.AreNotSame(bmp2, bmp1);
            Assert.IsTrue(bmp2.Size.Width == 2);
            Assert.IsTrue(bmp2.Size.Height == 1);
        }

        [TestMethod()]
        public void scaleBitmapTest06()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            Bitmap bmp1 = new Bitmap(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);

            //act
            Bitmap bmp2 = prevcontr.scaleBitmap(bmp1, 1, 2);

            //assert
            Assert.IsNotNull(bmp2);
            Assert.IsInstanceOfType(bmp2, typeof(Bitmap));
            Assert.AreNotSame(bmp2, bmp1);
            Assert.IsTrue(bmp2.Size.Width == 1);
            Assert.IsTrue(bmp2.Size.Height == 2);
        }

        [TestMethod()]
        public void rescalePreviewPanelTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void setCoordinatesTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable prev = new IDMarker(1);
            Vector3D v = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.setCoordinates(prev, v);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void setCoordinatesTest_nullArgument02()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable prev = null;
            Vector3D v = new Vector3D(Double.MinValue, Double.MinValue, Double.MinValue);
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.setCoordinates(prev, v);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void setCoordinatesTest_nullArgument03()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable prev = null;
            Vector3D v = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.setCoordinates(prev, v);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void setCoordinatesTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        public void updateTranslationTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        public void updateElementComboboxTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void rotateAugmentationTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable previewable = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.rotateAugmentation(previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void rotateAugmentationTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void getSizedBitmapTest_nullArgument01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable previewable = null;
            PreviewController prevcontr = new PreviewController(ew);

            //act
            prevcontr.getSizedBitmap(previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void getSizedBitmapTest_nullArgument02()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable previewable = new PictureMarker(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);
            //no trackable set

            //act
            prevcontr.getSizedBitmap(previewable);

            //assert
            //assert is handled by the ExcpectedException
        }

        [TestMethod()]
        public void getSizedBitmapTest01()
        {
            //arrange
            EditorWindow ew = new EditorWindow();
            IPreviewable previewable = new PictureMarker(System.IO.Directory.GetCurrentDirectory() + "\\res\\testFiles\\trackables\\pictureMarker1.png");
            PreviewController prevcontr = new PreviewController(ew);
            prevcontr.trackable = new IDMarker(1);

            //act
            Bitmap bmp = prevcontr.getSizedBitmap(previewable);

            //assert
            Assert.IsNull(bmp);
        }

        [TestMethod()]
        public void getSizedBitmapTest02()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        public void copy_augmentationTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        public void paste_augmentationTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }

        [TestMethod()]
        public void paste_augmentation_centerTest()
        {
            //arrange

            //act

            //assert
            //result has to be tested via UI testing
            Assert.Inconclusive("please test correct functionality via UI test");
        }
    }
}
