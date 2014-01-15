using System;
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
            Project p = new Project();
            p.Name = "Hello World";

            ExportVisitor exporter = new ExportVisitor("..\\..\\..\\bin\\Debug\\currentProject");
            exporter.visit(p);
        }
    }
}
