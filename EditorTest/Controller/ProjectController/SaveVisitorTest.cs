using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ARdevKit.Controller.ProjectController;
using ARdevKit.Model.Project;
using System.IO;

namespace EditorTest.Controller.ProjectController
{
    [TestClass]
    public class SaveVisitorTest
    {


        PictureMarker mm = new PictureMarker("metaioman_target.png");
        PictureMarker jm = new PictureMarker("junaioman_target.png");
        [TestMethod]
        public void SaveProject()
        {
            Project p = new Project("Hello World");
            p.Trackables.Add(mm);
            p.Trackables.Add(jm);

            SaveVisitor visitor = new SaveVisitor("..\\..\\..\\bin\\Debug");
            p.Accept(visitor);
        }
        [TestMethod]
        public void loadProject()
        {
            SaveVisitor visitor = new SaveVisitor("..\\..\\..\\bin\\Debug");
            Project p = visitor.load("..\\..\\..\\bin\\Debug\\Hello_World.bin");

            Assert.AreEqual<PictureMarker>(mm, (PictureMarker) p.Trackables[0]);
            Assert.AreEqual<PictureMarker>(jm, (PictureMarker) p.Trackables[1]);
        }
    }
}
