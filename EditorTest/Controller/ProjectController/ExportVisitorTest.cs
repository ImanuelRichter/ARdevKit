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

        private void SetUptProjectWithIDMarkerAndImage()
        {
            string projectPath = "..\\..\\..\\bin\\Debug\\currentProject";
            testProject = new Project("HelloIDMarker", projectPath);

            IDMarker idMarker1 = new IDMarker(1);
            IDMarker idMarker2 = new IDMarker(2);

            ImageAugmention image1 = new ImageAugmention();
            image1.ImagePath = Path.Combine(testProject.ProjectPath, "Assets", "frame.png");
            image1.IsVisible = false;
            image1.Coordinatesystemid = 1;
            image1.TranslationVector = new Vector3D(0, 0, 0);
            image1.RotationVector = new Vector3Di(0, 0, 0, 1);
            image1.ScalingVector = new Vector3D(0, 0, 0);
            idMarker1.Augmentions.Add(image1);
            image1.Trackable = idMarker1;

            ImageAugmention image2 = new ImageAugmention();
            image2.ImagePath = Path.Combine(testProject.ProjectPath, "Assets", "frame.png");
            image2.IsVisible = false;
            image2.Coordinatesystemid = 2;
            image2.TranslationVector = new Vector3D(0, 0, 0);
            image2.RotationVector = new Vector3Di(0, 0, 0, 1);
            image2.ScalingVector = new Vector3D(3, 3, 3);
            idMarker2.Augmentions.Add(image2);
            image2.Trackable = idMarker2;

            testProject.Sensor = new MarkerSensor();
            testProject.Trackables.Add(idMarker1);
            testProject.Trackables.Add(idMarker2);
        }

        private void SetUptProjectWithPictureMarkerAndBarChart()
        {
            string projectPath = "..\\..\\..\\bin\\Debug\\currentProject";
            testProject = new Project("HelloPictureMarker", projectPath);

            PictureMarker pictureMarker1 = new PictureMarker("pictureMarker1.png");
            PictureMarker pictureMarker2 = new PictureMarker("pictureMarker2.png");
            
            BarChart barChart1 = new BarChart();
            barChart1.Coordinatesystemid = 1;
            barChart1.Width = 500;
            barChart1.Height = 350;

            barChart1.Title = "Feuchtigkeitsstand";
            barChart1.Subtitle = "sensorbasiert";
            barChart1.Categories = new string[] {"Mo", "Di", "Mi"};
            barChart1.MinValue = 0;
            barChart1.YAxisTitle = "Feuchtigkeit in %";
            barChart1.PointPadding = 0.2;
            barChart1.BorderWidth = 0;
            barChart1.Names = new List<string>();
            barChart1.Names.Add("Rose");
            barChart1.Data = new List<double[]>();
            barChart1.Data.Add(new double[] { 49.2, 50.3, 33.1 });
            pictureMarker1.Augmentions.Add(barChart1);
            barChart1.Trackable = pictureMarker1;
            
            ImageAugmention image1 = new ImageAugmention();
            image1.ImagePath = Path.Combine(testProject.ProjectPath, "Assets", "frame.png");
            image1.IsVisible = false;
            image1.Coordinatesystemid = 1;
            image1.TranslationVector = new Vector3D(0, 0, 0);
            image1.RotationVector = new Vector3Di(0, 0, 0, 1);
            image1.ScalingVector = new Vector3D(0, 0, 0);
            pictureMarker2.Augmentions.Add(image1);
            image1.Trackable = pictureMarker2;

            testProject.Sensor = new PictureMarkerSensor();
            testProject.Trackables.Add(pictureMarker1);
            testProject.Trackables.Add(pictureMarker2);
        }

        [TestMethod]
        public void Export_PictureMarker_WithValidPath_ResultingFile()
        {
            SetUptProjectWithPictureMarkerAndBarChart();

            ExportVisitor exporter = new ExportVisitor();
            testProject.Accept(exporter);

            exporter.ArelProjectFile.Save();
            exporter.TrackingDataFile.Save();
            exporter.ArelConfigFile.Save();
            exporter.ArelGlueFile.Save();
            
            foreach (BarChartFile barChartFile in exporter.BarChartFiles)
            {
                barChartFile.Save();
            }
            
            Assert.IsTrue(File.Exists("..\\..\\..\\bin\\Debug\\currentProject\\Assets\\imageToCopy.png"));
        }

        [TestMethod]
        public void Export_IDMarker_WithValidPath_ResultingFile()
        {
            SetUptProjectWithIDMarkerAndImage();

            ExportVisitor exporter = new ExportVisitor();
            testProject.Accept(exporter);

            exporter.ArelProjectFile.Save();
            exporter.TrackingDataFile.Save();
            exporter.ArelConfigFile.Save();
            exporter.ArelGlueFile.Save();
        }
    }
}
