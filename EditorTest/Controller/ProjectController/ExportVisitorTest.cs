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
        [TestMethod]
        public void Export_WithValidPath_ResultingFile()
        {
            string projectPath = "..\\..\\..\\bin\\Debug\\currentProject";
            Project project = new Project("HelloWorld");
            project.Sensor = new PictureMarkerSensor();

            PictureMarker pictureMarker1 = new PictureMarker("metaioman_target.png");
            PictureMarker pictureMarker2 = new PictureMarker("pictureMarker1.png");

            BarGraph barGraph = new BarGraph();
            barGraph.AugmentationPath = "..\\..\\..\\bin\\Debug\\currentProject\\barGraph.png";
            barGraph.IsVisible = false;
            barGraph.Coordinatesystemid = 1;
            barGraph.vector = new Vector3D(3, 3, 3);
            pictureMarker1.augmentations.Add(barGraph);
            barGraph.Trackable = pictureMarker1;

            project.Trackables.Add(pictureMarker1);
            project.Trackables.Add(pictureMarker2);

            ExportVisitor exporter = new ExportVisitor(projectPath);
            project.Accept(exporter);

            exporter.ArelProjectFile.Save();
            exporter.TrackingDataFile.Save();
            exporter.ArelConfigFile.Save();
            exporter.ArelGlueFile.Save();
        }
    }
}
