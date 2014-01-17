using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ARdevKit.Controller.ProjectController;
using ARdevKit.Model.Project;

namespace EditorTest
{
    [TestClass]
    public class ExportVisitorTest
    {
        Project testProject;

        private void SetUptProjectWithIDMarker()
        {
            string projectPath = "..\\..\\..\\bin\\Debug\\currentProject";
            testProject = new Project("HelloIDMarker", projectPath);

            IDMarker idMarker1 = new IDMarker(1);
            IDMarker idMarker2 = new IDMarker(2);

            BarGraph barGraph1 = new BarGraph();
            barGraph1.AugmentionPath = Path.Combine(testProject.ProjectPath, "frame.png");
            barGraph1.IsVisible = false;
            barGraph1.Coordinatesystemid = 1;
            barGraph1.TranslationVector = new Vector3D(0, 0, 0);
            barGraph1.RotationVector = new Vector3Di(0, 0, 0, 1);
            barGraph1.ScalingVector = new Vector3D(0, 0, 0);
            idMarker1.Augmentions.Add(barGraph1);
            barGraph1.Trackable = idMarker1;

            BarGraph barGraph2 = new BarGraph();
            barGraph2.AugmentionPath = Path.Combine(testProject.ProjectPath, "frame.png");
            barGraph2.IsVisible = false;
            barGraph2.Coordinatesystemid = 2;
            barGraph2.TranslationVector = new Vector3D(0, 0, 0);
            barGraph2.RotationVector = new Vector3Di(0, 0, 0, 1);
            barGraph2.ScalingVector = new Vector3D(3, 3, 3);
            idMarker2.Augmentions.Add(barGraph2);
            barGraph2.Trackable = idMarker2;

            testProject.Sensor = new MarkerSensor();
            testProject.Trackables.Add(idMarker1);
            testProject.Trackables.Add(idMarker2);
        }

        private void SetUptProjectWithPictureMarker()
        {
            string projectPath = "..\\..\\..\\bin\\Debug\\currentProject";
            testProject = new Project("HelloPictureMarker", projectPath);

            PictureMarker pictureMarker1 = new PictureMarker("pictureMarker1.png");
            PictureMarker pictureMarker2 = new PictureMarker("pictureMarker2.png");

            BarGraph barGraph1 = new BarGraph();
            barGraph1.AugmentionPath = Path.Combine(testProject.ProjectPath, "pi.jpg");
            barGraph1.IsVisible = false;
            barGraph1.Coordinatesystemid = 1;
            barGraph1.TranslationVector = new Vector3D(2, 3, 0);
            barGraph1.RotationVector = new Vector3Di(0, 0, 0, 1);
            barGraph1.ScalingVector = new Vector3D(0, 0, 0);
            pictureMarker1.Augmentions.Add(barGraph1);
            barGraph1.Trackable = pictureMarker1;

            BarGraph barGraph2 = new BarGraph();
            barGraph2.AugmentionPath = Path.Combine(testProject.ProjectPath, "top.png");
            barGraph2.IsVisible = false;
            barGraph2.Coordinatesystemid = 2;
            barGraph2.TranslationVector = new Vector3D(2, 3, 0);
            barGraph2.RotationVector = new Vector3Di(0, 0, 0, 1);
            barGraph2.ScalingVector = new Vector3D(3, 3, 3);
            pictureMarker2.Augmentions.Add(barGraph2);
            barGraph2.Trackable = pictureMarker2;

            testProject.Sensor = new PictureMarkerSensor();
            testProject.Trackables.Add(pictureMarker1);
            testProject.Trackables.Add(pictureMarker2);
        }

        [TestMethod]
        public void Export_PictureMarker_WithValidPath_ResultingFile()
        {
            SetUptProjectWithPictureMarker();

            ExportVisitor exporter = new ExportVisitor();
            testProject.Accept(exporter);

            exporter.ArelProjectFile.Save();
            exporter.TrackingDataFile.Save();
            exporter.ArelConfigFile.Save();
            exporter.ArelGlueFile.Save();
        }

        [TestMethod]
        public void Export_IDMarker_WithValidPath_ResultingFile()
        {
            SetUptProjectWithIDMarker();

            ExportVisitor exporter = new ExportVisitor();
            testProject.Accept(exporter);

            exporter.ArelProjectFile.Save();
            exporter.TrackingDataFile.Save();
            exporter.ArelConfigFile.Save();
            exporter.ArelGlueFile.Save();
        }
    }
}
