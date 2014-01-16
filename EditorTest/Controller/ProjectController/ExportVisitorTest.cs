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

            PictureMarker pictureMarker1 = new PictureMarker("metaioman_target.png");
            PictureMarker pictureMarker2 = new PictureMarker("junaioman.png");

            BarGraph barGraph = new BarGraph();
            pictureMarker1.augmentations.Add(barGraph);
            barGraph.Trackable = pictureMarker1;

            project.Trackables.Add(pictureMarker1);
            project.Trackables.Add(pictureMarker2);

            ExportVisitor exporter = new ExportVisitor(projectPath);
            project.Accept(exporter);

            exporter.ArelProjectConfig.Save();
            exporter.TrackingDataFile.Save();
            exporter.ArelConfigFile.Save();
        }
    }
}
