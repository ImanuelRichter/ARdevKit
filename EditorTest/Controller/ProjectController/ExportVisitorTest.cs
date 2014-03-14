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
        
        private Project testProject;
        private bool arelNameHtml = false, 
            arelJs = false, 
            arelConfigXml = false, 
            arelGlueJs = false, 
            anchorJpg = false, 
            trackingDataXml = false;
        private ExportVisitor exportVisitor;

        private void export()
        {
            exportVisitor = new ExportVisitor();
            testProject.Accept(exportVisitor);

            foreach (AbstractFile file in exportVisitor.Files)
            {
                file.Save();
            }
        }
        private void checkStandardFiles()
        {
            arelNameHtml = File.Exists(testProject.ProjectPath + "\\arel" + (testProject.Name == "" ? "Test" : testProject.Name) + ".html");
            arelJs = File.Exists(testProject.ProjectPath + "\\arel.js");
            arelConfigXml = File.Exists(testProject.ProjectPath + "\\arelConfig.xml");
            arelGlueJs = File.Exists(testProject.ProjectPath + "\\Assets\\arelGlue.js");
            anchorJpg = File.Exists(testProject.ProjectPath + "\\Assets\\anchor.png");
            trackingDataXml = File.Exists(testProject.ProjectPath + "\\Assets\\trackingData_" + (testProject.Sensor is MarkerSensor ? "Marker" : "Markerless") + ".xml");
            if (!arelNameHtml)
                Assert.IsTrue(false, "arel" + testProject.Name == "" ? "Test" : testProject.Name + ".html ist nicht vorhanden");
            if (!arelJs)
                Assert.IsTrue(false, "arel.js ist nicht vorhanden");
            if (!arelConfigXml)
                Assert.IsTrue(false, "arelConfig.xml ist nicht vorhanden");
            if (!arelGlueJs)
                Assert.IsTrue(false, "arelGlue.js ist nicht vorhanden");
            if (!anchorJpg)
                Assert.IsTrue(false, "anchor.jpg ist nicht vorhanden");
            if (!trackingDataXml)
                Assert.IsTrue(false, "trackingData_" + (testProject.Sensor is MarkerSensor ? "Marker" : "MarkerlessFast") + ".xml ist nicht vorhanden");
        }

        private void checkAugmentations()
        {
            foreach (var trackable in testProject.Trackables)
            {
                foreach (var augmentation in trackable.Augmentations)
                {
                    if (augmentation is Chart)
                    {
                        if (!File.Exists(testProject.ProjectPath + "\\Assets\\" + augmentation.ID + "\\chart.js"))
                        {
                            Assert.IsTrue(false, "\\Assets\\" + augmentation.ID + "\\chart.js existiert nicht");
                        }
                        if (!File.Exists(testProject.ProjectPath + "\\Assets\\" + augmentation.ID + "\\options.js"))
                        {
                            Assert.IsTrue(false, "\\Assets\\" + augmentation.ID + "\\options.js existiert nicht");
                        }
                        var source = ((Chart)augmentation).Source;
                        if (source != null)
                        {
                            if (source.Query != null || source.Query != "")
                            {
                                if (!File.Exists(testProject.ProjectPath + source.Query))
                                {
                                    Assert.IsTrue(false, source.Query + " existiert nicht");
                                }
                            }
                            if (source is FileSource)
                            {
                                if (!File.Exists(testProject.ProjectPath + ((FileSource)source).Data))
                                {
                                    Assert.IsTrue(false, ((FileSource)source).Data + " existiert nicht");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (augmentation is Abstract2DAugmentation)
                        {
                            if (!File.Exists(testProject.ProjectPath + ((Abstract2DAugmentation)augmentation).ResFilePath))
                            {
                                Assert.IsTrue(false, "\\Assets\\" + ((Abstract2DAugmentation)augmentation).ResFilePath + " existiert nicht");
                            }
                            if (augmentation.CustomUserEventReference != null)
                            {
                                if (!File.Exists(testProject.ProjectPath + "\\Assets\\" + augmentation.ID + "_Event.js"))
                                {
                                    Assert.IsTrue(false, "\\Assets\\" + augmentation.ID + "_Event.js exisitert nicht");
                                }
                            }
                        }
                    }
                }
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            exportVisitor = new ExportVisitor();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if(testProject.ProjectPath != null && Directory.Exists(testProject.ProjectPath))
            Directory.Delete(testProject.ProjectPath, true);
            testProject = null;
            exportVisitor = null;
        }

        [TestMethod]
        [TestCategory("ExportVisitorTest")]
        [ExpectedException(typeof(ArgumentException))]
        public void Export_AllEmptyProject()
        {
            testProject = new Project();
            export();
        }

        [TestMethod]
        [TestCategory("ExportVisitorTest")]
        [ExpectedException(typeof(ArgumentException))]
        public void Export_PathEmptyProject()
        {
            testProject = new Project("Name");
            export();
        }

        [TestMethod]
        [TestCategory("ExportVisitorTest")]
        public void Export_NoNameProject_OneIDMarker()
        {
            testProject = new Project("", ".\\res\\TestFiles\\TestProjects\\NoName");
            testProject.Trackables.Add(new IDMarker(1));
            testProject.Sensor = new MarkerSensor();
            export();
            checkStandardFiles();
            Assert.IsTrue(true);   
        }

        [TestMethod]
        [TestCategory("ExportVisitorTest")]
        public void Export_Project_FullIDMarker()
        {
            testProject = SaveLoadController.loadProject(".\\res\\TestFiles\\TestProjects\\FullIDMarker\\FullIDMarker.ardev");
            testProject.ProjectPath = ".\\res\\TestFiles\\TestProjects\\Test";
            export();
            checkStandardFiles();
            checkAugmentations();
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("ExportVisitorTest")]
        public void Export_Project_FullImageTrackable()
        {
            testProject = SaveLoadController.loadProject(".\\res\\TestFiles\\TestProjects\\FullImageTrackable\\FullImageTrackable.ardev");
            testProject.ProjectPath = ".\\res\\TestFiles\\TestProjects\\Test";
            export();
            checkStandardFiles();
            checkAugmentations();
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("ExportVisitorTest")]
        public void Export_Project_FullPictureMarker()
        {
            testProject = SaveLoadController.loadProject(".\\res\\TestFiles\\TestProjects\\FullIDMarker\\FullIDMarker.ardev");
            testProject.ProjectPath = ".\\res\\TestFiles\\TestProjects\\Test";
            export();
            checkStandardFiles();
            checkAugmentations();
            Assert.IsTrue(true);
        }
    }
}
