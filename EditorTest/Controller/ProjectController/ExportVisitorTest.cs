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
            Project p = new Project("Hello World");
            p.Trackables.Add(new PictureMarker("metaioman_target.png"));
            p.Trackables.Add(new PictureMarker("junaioman_target.png"));

            ExportVisitor exporter = new ExportVisitor();
            p.Accept(exporter);

            exporter.ProjectConfig.Write(Path.Combine(projectPath, p.Name));
            exporter.TrackingConfiguration.Write(projectPath);
        }
    }
}
