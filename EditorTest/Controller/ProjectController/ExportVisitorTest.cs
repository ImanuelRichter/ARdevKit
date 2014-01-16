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
        public void Export_PictureMarker_WithValidPath_ResultingFile()
        {
            string projectPath = "..\\..\\..\\bin\\Debug\\currentProject";
            Project project = new Project("HelloWorld");

            PictureMarker pictureMarker1 = new PictureMarker("pictureMarker1.png");
            PictureMarker pictureMarker2 = new PictureMarker("pictureMarker2.png");

            BarGraph barGraph2 = new BarGraph();
            barGraph2.AugmentationPath = "..\\..\\..\\bin\\Debug\\currentProject\\nice.jpg";
            barGraph2.IsVisible = false;
            barGraph2.Coordinatesystemid = 1;
            barGraph2.vector = new Vector3D(3, 3, 3);
            pictureMarker1.augmentations.Add(barGraph2);
            barGraph2.Trackable = pictureMarker1;

            BarGraph barGraph3 = new BarGraph();
            barGraph3.AugmentationPath = "..\\..\\..\\bin\\Debug\\currentProject\\barGraph.png";
            barGraph3.IsVisible = false;
            barGraph3.Coordinatesystemid = 2;
            barGraph3.vector = new Vector3D(3, 3, 3);
            pictureMarker2.augmentations.Add(barGraph3);
            barGraph3.Trackable = pictureMarker2;

            project.Sensor = new PictureMarkerSensor();
            project.Trackables.Add(pictureMarker1);
            project.Trackables.Add(pictureMarker2);

            ExportVisitor exporter = new ExportVisitor(projectPath);
            project.Accept(exporter);

            exporter.ArelProjectFile.Save();
            exporter.TrackingDataFile.Save();
            exporter.ArelConfigFile.Save();
            exporter.ArelGlueFile.Save();
        }

        [TestMethod]
        public void Export_IDMarker_WithValidPath_ResultingFile()
        {
            string projectPath = "..\\..\\..\\bin\\Debug\\currentProject";
            Project project = new Project("HelloWorld");

            IDMarker idMarker1 = new IDMarker(1);
            IDMarker idMarker2 = new IDMarker(2);

            BarGraph barGraph = new BarGraph();
            barGraph.AugmentationPath = "..\\..\\..\\bin\\Debug\\currentProject\\nice.jpg";
            barGraph.IsVisible = false;
            barGraph.Coordinatesystemid = 1;
            barGraph.vector = new Vector3D(3, 3, 3);
            idMarker1.augmentations.Add(barGraph);
            barGraph.Trackable = idMarker1;

            BarGraph barGraph1 = new BarGraph();
            barGraph1.AugmentationPath = "..\\..\\..\\bin\\Debug\\currentProject\\barGraph.png";
            barGraph1.IsVisible = false;
            barGraph1.Coordinatesystemid = 2;
            barGraph1.vector = new Vector3D(3, 3, 3);
            idMarker2.augmentations.Add(barGraph1);
            barGraph1.Trackable = idMarker2;

            project.Sensor = new IDMarkerSensor();
            project.Trackables.Add(idMarker1);
            project.Trackables.Add(idMarker2);

            ExportVisitor exporter = new ExportVisitor(projectPath);
            project.Accept(exporter);

            exporter.ArelProjectFile.Save();
            exporter.TrackingDataFile.Save();
            exporter.ArelConfigFile.Save();
            exporter.ArelGlueFile.Save();
        }
    }
}
