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
            Project project = new Project("Hello World");
            PictureMarker pictureMarker1 = new PictureMarker("metaioman_target.png");
            PictureMarker pictureMarker2 = new PictureMarker("junaioman_target.png");

            project.Trackables.Add(pictureMarker1);
            project.Trackables.Add(pictureMarker2);

            ExportVisitor exporter = new ExportVisitor();
            project.Accept(exporter);

            exporter.ProjectConfig.Write(Path.Combine(projectPath, project.Name));
            exporter.TrackingConfig.Write(projectPath);
        }
    }
}
