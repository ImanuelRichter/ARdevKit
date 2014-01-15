using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ARdevKit.Controller.ProjectController;
using ARdevKit.Model.Project;

namespace EditorTest.Controller.ProjectController
{
    [TestClass]
    public class ExportVisitorTest
    {
        [TestMethod]
        public void VisitProjectTest()
        {
            Project p = new Project();
            p.Name = "Hello World";

            ExportVisitor exporter = new ExportVisitor("");
            exporter.visit(p);
        }
    }
}
