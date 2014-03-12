using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ARdevKit;

namespace EditorTest.Model.Project
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void initializeTest()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project("name");
            Assert.IsTrue(project.Name == "name");
            project = new ARdevKit.Model.Project.Project("name", @"C:\");
            Assert.IsTrue(project.Name == "name" && project.ProjectPath == @"C:\");
            project.Name = "x";
            project.ProjectPath = @"D:\";
            project.Screensize = new ARdevKit.Model.Project.ScreenSize();
            project.Sensor = new ARdevKit.Model.Project.MarkerSensor();
            Assert.IsTrue(project.Name == "x" && project.ProjectPath == @"D:\");
        }

        [TestMethod]
        public void hasTrackableTest1()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            project.Trackables.Add(new ARdevKit.Model.Project.ImageTrackable());
            Assert.IsTrue(project.hasTrackable());
        }

        [TestMethod]
        public void hasTrackableTest2()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            project.Trackables.Add(null);
            Assert.IsFalse(project.hasTrackable());
        }

        [TestMethod]
        public void hasTrackableTest3()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            project.Trackables.Add(null);
            project.Trackables.Add(null);
            project.Trackables.Add(null);
            project.Trackables.Add(null);
            project.Trackables.Add(null);
            project.Trackables.Add(new ARdevKit.Model.Project.ImageTrackable());
            Assert.IsTrue(project.hasTrackable());
        }

        [TestMethod]
        public void nextIDTest1()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            
            project.Trackables.Add(new ARdevKit.Model.Project.IDMarker(1));
            project.Trackables.Add(new ARdevKit.Model.Project.IDMarker(3));
            project.Trackables.Add(new ARdevKit.Model.Project.IDMarker(15));
            project.Trackables.Add(new ARdevKit.Model.Project.IDMarker(12));
            Assert.IsTrue(project.nextID() == 16);
        }

        [TestMethod]
        public void nextIDTest2()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();

            Assert.IsTrue(project.nextID() == 1);
        }

        [TestMethod]
        public void existTrackableTest1()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            ARdevKit.Model.Project.IDMarker temp = new ARdevKit.Model.Project.IDMarker(1);
            project.Trackables.Add(temp);
            Assert.IsTrue(project.existTrackable(temp));
        }

        [TestMethod]
        public void existTrackableTest2()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            ARdevKit.Model.Project.IDMarker temp = new ARdevKit.Model.Project.IDMarker(1);
            Assert.IsFalse(project.existTrackable(temp));
        }

        [TestMethod]
        public void existTrackableTest3()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            ARdevKit.Model.Project.ImageTrackable temp = new ARdevKit.Model.Project.ImageTrackable();
            project.Trackables.Add(new ARdevKit.Model.Project.IDMarker(1));
            Assert.IsFalse(project.existTrackable(temp));
        }

        [TestMethod]
        public void existTrackableTest4()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            ARdevKit.Model.Project.ImageTrackable temp = new ARdevKit.Model.Project.ImageTrackable();
            project.Trackables.Add(temp);
            Assert.IsTrue(project.existTrackable(temp));
        }

        [TestMethod]
        public void existTrackableTest5()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            ARdevKit.Model.Project.PictureMarker temp = new ARdevKit.Model.Project.PictureMarker();
            project.Trackables.Add(new ARdevKit.Model.Project.IDMarker(1));
            Assert.IsFalse(project.existTrackable(temp));
        }

        [TestMethod]
        public void existTrackableTest6()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            ARdevKit.Model.Project.PictureMarker temp = new ARdevKit.Model.Project.PictureMarker();
            project.Trackables.Add(temp);
            Assert.IsTrue(project.existTrackable(temp));
        }

        [TestMethod]
        public void existTrackableIDTest1()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            ARdevKit.Model.Project.IDMarker temp = new ARdevKit.Model.Project.IDMarker(1);
            project.Trackables.Add(temp);
            Assert.IsFalse(project.existTrackable(1));
        }

        [TestMethod]
        public void existTrackableIDTest2()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            Assert.IsFalse(project.existTrackable(1));
        }

        [TestMethod]
        public void existTrackableIDTest3()
        {
            ARdevKit.Model.Project.Project project = new ARdevKit.Model.Project.Project();
            ARdevKit.Model.Project.IDMarker temp = new ARdevKit.Model.Project.IDMarker(1);
            project.Trackables.Add(temp);
            project.Trackables.Add(temp);
            Assert.IsTrue(project.existTrackable(1));
        }

        [TestMethod]
        public void ChecksumTest()
        {

        }
    }
}
