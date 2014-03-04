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
            image1.ResFilePath = Path.Combine(testProject.ProjectPath, "Assets", "frame.png");
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
            barChart1.Source = new DbSource("http://localhost/highcharts/server.php?callback=?");
            barChart1.Source.Query = "res\\highcharts\\barChartColumn\\liveQuery.js";
            barChart1.Source.Augmentation = barChart1;
            pictureMarker1.Augmentations.Add(barChart1);
            barChart1.Trackable = pictureMarker1;

            testProject.Sensor = new MarkerSensor();
            testProject.Trackables.Add(pictureMarker1);
        }

        private void SetUptProject_imageTrackable_imageAugmentation()
        {
            string projectPath = "currentProject";
            testProject = new Project("HelloImageTrackable", projectPath);

            ImageTrackable imageTrackable = new ImageTrackable("res\\testFiles\\trackables\\metaioman_target.png");

            ImageAugmentation image1 = new ImageAugmentation();
            image1.ResFilePath = Path.Combine(testProject.ProjectPath, "Assets", "frame.png");
            image1.IsVisible = false;
            imageTrackable.Augmentations.Add(image1);
            image1.Trackable = imageTrackable;

            testProject.Sensor = new MarkerlessSensor();
            testProject.Trackables.Add(imageTrackable);
        }

        private void SetUptProject_imageTrackable_videoAugmentation()
        {
            testProject = new Project("HelloVideo");

            ImageTrackable imageTrackable = new ImageTrackable("res\\testFiles\\trackables\\metaioman_target.png");

            VideoAugmentation video = new VideoAugmentation();
            video.ResFilePath = "res\\testFiles\\augmentations\\video.alpha.3g2";
            video.IsVisible = false;
            video.Rotation = new Vector3D(0, 0, -90);
            video.Scaling = new Vector3D(2, 2, 1);
            imageTrackable.Augmentations.Add(video);
            video.Trackable = imageTrackable;

            testProject.Sensor = new MarkerlessSensor();
            testProject.Trackables.Add(imageTrackable);
        }

        private void export(bool exportToTestFolder)
        {
            ExportVisitor exporter = new ExportVisitor();
            testProject.Accept(exporter);

            foreach (AbstractFile file in exporter.Files)
            {
                file.Save();
            }
        }

        [TestMethod]
        public void Export_idMarker_validPath_resultingFiles()
        {
            SetUptProjectWithIDMarkerAndImage();
            export(false);
        }

        [TestMethod]
        public void Export_pictureMarker_barChart_fileSource()
        {
            SetUptProject_pictureMarker_barChart_fileSource();
            export(true);
        }

        [TestMethod]
        public void Export_pictureMarker_barChart_liveSource()
        {
            SetUptProject_pictureMarker_barChart_liveSource();
            export(true);
        }

        [TestMethod]
        public void Export_pictureMarker_validPath_resultingFile()
        {
            SetUptProject_pictureMarker_barChart_noSource();
            export(false);
        }

        [TestMethod]
        public void Export_imageTrackable_validPath_resultingFile()
        {
            SetUptProject_imageTrackable_imageAugmentation();
            export(false);
        }

        [TestMethod]
        public void Export_imageTrackable_videoAugmentation()
        {
            SetUptProject_imageTrackable_videoAugmentation();
            export(true);
        }
    }
}
