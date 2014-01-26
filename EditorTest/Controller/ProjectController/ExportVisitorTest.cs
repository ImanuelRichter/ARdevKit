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
            string projectPath = "currentProject";
            testProject = new Project("HelloIDMarker", projectPath);

            IDMarker idMarker1 = new IDMarker(1);
            IDMarker idMarker2 = new IDMarker(2);

            ImageAugmentation image1 = new ImageAugmentation();
            image1.ImagePath = Path.Combine(testProject.ProjectPath, "Assets", "frame.png");
            image1.IsVisible = false;
            image1.TranslationVector = new Vector3D(0, 0, 0);
            image1.RotationVector = new Vector3Di(0, 0, 0, 1);
            image1.ScalingVector = new Vector3D(0, 0, 0);
            idMarker1.Augmentations.Add(image1);
            image1.Trackable = idMarker1;

            ImageAugmentation image2 = new ImageAugmentation();
            image2.ImagePath = Path.Combine(testProject.ProjectPath, "Assets", "frame.png");
            image2.IsVisible = false;
            image2.TranslationVector = new Vector3D(0, 0, 0);
            image2.RotationVector = new Vector3Di(0, 0, 0, 1);
            image2.ScalingVector = new Vector3D(3, 3, 3);
            idMarker2.Augmentations.Add(image2);
            image2.Trackable = idMarker2;

            testProject.Sensor = new MarkerSensor();
            testProject.Trackables.Add(idMarker1);
            testProject.Trackables.Add(idMarker2);
        }

        private void SetUptProjectWithPictureMarkerAndBarChart()
        {
            string projectPath = "currentProject";
            testProject = new Project("HelloPictureMarker", projectPath);

            //PictureMarker pictureMarker1 = new PictureMarker("res\\testFiles\\marker\\pictureMarker1.png");
            ImageTrackable imageTrackable = new ImageTrackable("res\\testFiles\\marker\\metaioman_target.png");

            /*
            BarChart barChart1 = new BarChart();
            barChart1.IsVisible = false;
            barChart1.TranslationVector = new Vector3D(0, 0, 0);
            barChart1.RotationVector = new Vector3Di(0, 0, 0, 1);
            barChart1.ScalingVector = new Vector3D(0, 0, 0);
            barChart1.Style = new ChartStyle();
            barChart1.PositionRelativeToTrackable = true;
            barChart1.Style.Top = 100;
            barChart1.Style.Left = 100;
            barChart1.Width = 200;
            barChart1.Height = 200;

            barChart1.Title = "Feuchtigkeitsstand";
            barChart1.Subtitle = "sensorbasiert";
            barChart1.XAxisTitle = "Wochentag";
            barChart1.Categories = new string[] { "Mo", "Di", "Mi" };
            barChart1.MinValue = 0;
            barChart1.YAxisTitle = "Feuchtigkeit in %";
            barChart1.PointPadding = 0.2;
            barChart1.BorderWidth = 0;
            barChart1.Data = new List<BarChartData>();
            barChart1.Data.Add(new BarChartData("Rose", new double[] { 72.5, 50.3, 33.1 }));
            barChart1.Source = new FileSource("res\\highcharts\\barChartColumn\\data.xml");
            barChart1.Source.QueryFilePath = "res\\highcharts\\barChartColumn\\XMLQuery.js";
            barChart1.Source.Augmentation = barChart1;
            pictureMarker1.Augmentations.Add(barChart1);
            barChart1.Trackable = pictureMarker1;
            */
            ImageAugmentation image1 = new ImageAugmentation();
            image1.ImagePath = Path.Combine(testProject.ProjectPath, "Assets", "frame.png");
            image1.IsVisible = false;
            image1.TranslationVector = new Vector3D(0, 0, 0);
            image1.RotationVector = new Vector3Di(0, 0, 0, 1);
            image1.ScalingVector = new Vector3D(0, 0, 0);
            imageTrackable.Augmentations.Add(image1);
            image1.Trackable = imageTrackable;

            testProject.Sensor = new MarkerlessSensor();
            //testProject.Trackables.Add(pictureMarker1);
            testProject.Trackables.Add(imageTrackable);
        }

        [TestMethod]
        public void Export_PictureMarker_WithValidPath_ResultingFile()
        {
            SetUptProjectWithPictureMarkerAndBarChart();

            ExportVisitor exporter = new ExportVisitor(false);
            testProject.Accept(exporter);

            foreach (AbstractFile file in exporter.Files)
            {
                file.Save();
            }
        }

        [TestMethod]
        public void Export_IDMarker_WithValidPath_ResultingFile()
        {
            SetUptProjectWithIDMarkerAndImage();

            ExportVisitor exporter = new ExportVisitor(false);
            testProject.Accept(exporter);

            foreach (AbstractFile file in exporter.Files)
            {
                file.Save();
            }
        }
    }
}
