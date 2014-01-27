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

            ImageAugmentation image1 = new ImageAugmentation();
            image1.ImagePath = Path.Combine(testProject.ProjectPath, "Assets", "frame.png");
            image1.IsVisible = false;
            image1.Translation = new Vector3D(0, 0, 0);
            image1.Rotation = new Vector3Di(0, 0, 0, 1);
            image1.Scaling = new Vector3D(0, 0, 0);
            idMarker1.Augmentations.Add(image1);
            image1.Trackable = idMarker1;

            testProject.Sensor = new MarkerSensor();
            testProject.Trackables.Add(idMarker1);
        }

        private void SetUptProject_pictureMarker_barChart_noSource()
        {
            string projectPath = "currentProject";
            testProject = new Project("HelloPictureMarker", projectPath);

            PictureMarker pictureMarker1 = new PictureMarker("res\\testFiles\\trackables\\pictureMarker1.png");

            Chart barChart1 = new Chart();
            barChart1.IsVisible = false;
            barChart1.Translation = new Vector3D(100, -100, 0);
            barChart1.Width = 200;
            barChart1.Height = 200;

            pictureMarker1.Augmentations.Add(barChart1);
            barChart1.Trackable = pictureMarker1;

            testProject.Sensor = new MarkerSensor();
            testProject.Trackables.Add(pictureMarker1);
        }

        private void SetUptProject_pictureMarker_barChart_fileSource()
        {
            string projectPath = "currentProject";
            testProject = new Project("HelloPictureMarker", projectPath);

            PictureMarker pictureMarker1 = new PictureMarker("res\\testFiles\\trackables\\pictureMarker1.png");

            Chart barChart1 = new Chart();
            barChart1.IsVisible = false;
            barChart1.Translation = new Vector3D(100, -100, 0);
            barChart1.Width = 200;
            barChart1.Height = 200;

            barChart1.Source = new FileSource("res\\highcharts\\barChartColumn\\data.xml");
            barChart1.Source.Query = "res\\highcharts\\barChartColumn\\xmlQuery.js";
            barChart1.Source.Augmentation = barChart1;
            pictureMarker1.Augmentations.Add(barChart1);
            barChart1.Trackable = pictureMarker1;

            testProject.Sensor = new MarkerSensor();
            testProject.Trackables.Add(pictureMarker1);
        }

        private void SetUptProject_pictureMarker_barChart_liveSource()
        {
            string projectPath = "currentProject";
            testProject = new Project("HelloPictureMarker", projectPath);

            PictureMarker pictureMarker1 = new PictureMarker("res\\testFiles\\trackables\\pictureMarker1.png");

            Chart barChart1 = new Chart();
            barChart1.IsVisible = false;
            barChart1.Translation = new Vector3D(100, -100, 0);
            barChart1.Width = 200;
            barChart1.Height = 200;

            barChart1.Options = File.OpenText("res\\highcharts\\barChartColumn\\liveOptions.json").ReadToEnd();
            barChart1.Source = new LiveSource("http://localhost/highcharts/server.php?callback=?");
            barChart1.Source.Query = "res\\highcharts\\barChartColumn\\liveQuery.js";
            barChart1.Source.Augmentation = barChart1;
            pictureMarker1.Augmentations.Add(barChart1);
            barChart1.Trackable = pictureMarker1;

            testProject.Sensor = new MarkerSensor();
            testProject.Trackables.Add(pictureMarker1);
        }

        private void SetUptProjectWithImageTrackableAndImageAugmentation()
        {
            string projectPath = "currentProject";
            testProject = new Project("HelloImageTrackable", projectPath);

            ImageTrackable imageTrackable = new ImageTrackable("res\\testFiles\\trackables\\metaioman_target.png");

            ImageAugmentation image1 = new ImageAugmentation();
            image1.ImagePath = Path.Combine(testProject.ProjectPath, "Assets", "frame.png");
            image1.IsVisible = false;
            imageTrackable.Augmentations.Add(image1);
            image1.Trackable = imageTrackable;

            testProject.Sensor = new MarkerlessSensor();
            testProject.Trackables.Add(imageTrackable);
        }

        private void export()
        {
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
            export();
        }

        [TestMethod]
        public void Export_PictureMarker_barChart_fileSource()
        {
            SetUptProject_pictureMarker_barChart_fileSource();
            export();
        }

        [TestMethod]
        public void Export_PictureMarker_barChart_liveSource()
        {
            SetUptProject_pictureMarker_barChart_liveSource();
            export();
        }

        [TestMethod]
        public void Export_PictureMarker_WithValidPath_ResultingFile()
        {
            SetUptProject_pictureMarker_barChart_noSource();
            export();
        }

        [TestMethod]
        public void Export_ImageTrackable_WithValidPath_ResultingFile()
        {
            SetUptProjectWithImageTrackableAndImageAugmentation();
            export();
        }
    }
}
