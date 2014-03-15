using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Windows.Forms;

using ARdevKit;
using ARdevKit.Model.Project;
using ARdevKit.Controller.TestController;

namespace EditorTest.Controller
{
    [TestClass]
    public class TestControllerTest
    {
        private string filePath = System.IO.Path.GetFullPath(@"\res\testFiles\testProjects\everyAugmentation");
        private EditorWindow ew;

        [TestInitialize]
        public void initTest()
        {
            ew = new EditorWindow();
            MessageBox.Show(new Form() { TopMost = true }, "Öffne " + filePath + " im OpenDialog, sobald du hier auf OK klickst.", "Testschritt!");
            ew.loadProject();
        }

        [TestMethod]
        public void testNullEditorWindow()
        {
            TestController.StartPlayer(null, ew.project, TestController.IMAGE, 300, 300, false);
        }

        [TestMethod]
        public void testNullProject()
        {
            TestController.StartPlayer(ew, null, TestController.IMAGE, 300, 300, false);
        }

        [TestMethod]
        public void testOutOfRangeMode()
        {
            TestController.StartPlayer(ew, ew.project, TestController.VIDEO + 5, 300, 300, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testZeroWidth()
        {
            TestController.StartPlayer(ew, ew.project, TestController.IMAGE, 0, 300, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testZeroHeight()
        {
            TestController.StartPlayer(ew, ew.project, TestController.IMAGE, 300, 0, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testZeroHeightWidth()
        {
            TestController.StartPlayer(ew, ew.project, TestController.IMAGE, 0, 0, false);
        }
    }
}
