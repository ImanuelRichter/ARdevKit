using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using ARdevKit.Controller.ProjectController;
using ARdevKit.Model.Project;
using ARdevKit.Model.Project.File;

namespace EditorTest
{
    [TestClass]
    public class ExportVisitorTest
    {
        Project testProject;

        private void export()
        {
            ExportVisitor exporter = new ExportVisitor();
            testProject.Accept(exporter);

            foreach (AbstractFile file in exporter.Files)
            {
                file.Save();
            }
        }

        [TestMethod]
        [TestCategory("ExportVisitorTest")]
        [ExpectedException(typeof(ArgumentException))]
        public void Export_EmptyProject()
        {
            testProject = new Project();
            export();
        }
    }
}
