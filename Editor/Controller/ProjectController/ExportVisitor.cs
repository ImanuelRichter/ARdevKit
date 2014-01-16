using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Model.Project.IO;

namespace ARdevKit.Controller.ProjectController
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An export visitor which exports the project to be readable by the player. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ExportVisitor : AbstractProjectVisitor
    {
        private string projectPath;
        public string ProjectPath
        {
            get { return projectPath; }
            set { projectPath = value; }
        }

        private ARELProjectFile arelProjectFile;
        public ARELProjectFile ArelProjectConfig
        {
            get { return arelProjectFile; }
            set { arelProjectFile = value; }
        }

        /// <summary>   The trackin configuration XML. </summary>
        private TrackingDataFile trackingDataFile;
        public TrackingDataFile TrackingDataFile
        {
            get { return trackingDataFile; }
            set { trackingDataFile = value; }
        }

        private ARELConfigFile arelConfigFile;
        public ARELConfigFile ArelConfigFile
        {
            get { return arelConfigFile; }
            set { arelConfigFile = value; }
        }

        private SubSection sensorsSubSection;
        private SubSection connectionsSubSection;
        private int cosCounter = 1;

        public ExportVisitor(string projectPath)
        {
            this.projectPath = projectPath;
        }

        public override void visit(BarGraph graph)
        {
            // COS
            SubSection cosSection = new SubSection(new Tag("COS"));
            connectionsSubSection.AddSubSection(cosSection);

            cosSection.AddLine(new Line(new Tag("Name"), graph.Trackable.Sensor.Name + "COS" + cosCounter++));

            // Fuser
            SubSection fuserSubSection = new SubSection(new Tag("Fuser", "Type=\"" + graph.Trackable.MarkerFuser.FuserType + "\""));
            cosSection.AddSubSection(fuserSubSection);

            // Parameters
            SubSection fuserParametersSubSection = new SubSection(new Tag("Parameters"));
            fuserSubSection.AddSubSection(fuserParametersSubSection);

            fuserParametersSubSection.AddLine(new Line(new Tag("KeepPoseForNumberOfFrames"), graph.Trackable.MarkerFuser.KeepPoseForNumberOfFrames.ToString()));
            fuserParametersSubSection.AddLine(new Line(new Tag("GravityAssistance"), graph.Trackable.MarkerFuser.GravityAssistance.ToString()));
            fuserParametersSubSection.AddLine(new Line(new Tag("AlphaTranslation"), graph.Trackable.MarkerFuser.AlphaTranslation.ToString()));
            fuserParametersSubSection.AddLine(new Line(new Tag("GammaTranslation"), graph.Trackable.MarkerFuser.GammaTranslation.ToString()));
            fuserParametersSubSection.AddLine(new Line(new Tag("AlphaRotation"), graph.Trackable.MarkerFuser.AlphaRotation.ToString()));
            fuserParametersSubSection.AddLine(new Line(new Tag("GammaRotation"), graph.Trackable.MarkerFuser.GammaRotation.ToString()));
            fuserParametersSubSection.AddLine(new Line(new Tag("ContinueLostTrackingWithOrientationSensor"), graph.Trackable.MarkerFuser.ContinueLostTrackingWithOrientationSensor.ToString()));

            // SensorSource
            SubSection sensorSourceSubSection = new SubSection(new Tag("SensorSource"));
            cosSection.AddSubSection(sensorSourceSubSection);

            sensorSourceSubSection.AddLine(new Line(new Tag("SensorID"), graph.Trackable.Sensor.SensorIDString));
            sensorSourceSubSection.AddLine(new Line(new Tag("SensorCosID"), graph.Trackable.SensorCosID));

            // Hand-Eye-Calibration
            SubSection handEyeCalibrationSubSection = new SubSection(new Tag("HandEyeCalibration"));
            sensorSourceSubSection.AddSubSection(handEyeCalibrationSubSection);

            // Translation
            SubSection hecTranslationOffset = new SubSection(new Tag("TranslationOffset"));
            handEyeCalibrationSubSection.AddSubSection(hecTranslationOffset);

            // TODO get vectors
            hecTranslationOffset.AddLine(new Line(new Tag("X"), "0"));
            hecTranslationOffset.AddLine(new Line(new Tag("Y"), "0"));
            hecTranslationOffset.AddLine(new Line(new Tag("Z"), "0"));

            // Rotation
            SubSection hecRotationOffset = new SubSection(new Tag("RotationOffset"));
            handEyeCalibrationSubSection.AddSubSection(hecRotationOffset);

            // TODO get vectors
            hecRotationOffset.AddLine(new Line(new Tag("X"), "0"));
            hecRotationOffset.AddLine(new Line(new Tag("Y"), "0"));
            hecRotationOffset.AddLine(new Line(new Tag("Z"), "0"));
            hecRotationOffset.AddLine(new Line(new Tag("W"), "1"));

            // COSOffset
            SubSection COSOffsetSubSection = new SubSection(new Tag("COSOffset"));
            sensorSourceSubSection.AddSubSection(COSOffsetSubSection);

            // Translation
            SubSection COSOffsetTranslationOffset = new SubSection(new Tag("TranslationOffset"));
            COSOffsetSubSection.AddSubSection(COSOffsetTranslationOffset);

            // TODO get vectors
            COSOffsetTranslationOffset.AddLine(new Line(new Tag("X"), "0"));
            COSOffsetTranslationOffset.AddLine(new Line(new Tag("Y"), "0"));
            COSOffsetTranslationOffset.AddLine(new Line(new Tag("Z"), "0"));

            // Rotation
            SubSection COSOffsetRotationOffset = new SubSection(new Tag("RotationOffset"));
            COSOffsetSubSection.AddSubSection(COSOffsetRotationOffset);

            // TODO get vectors
            COSOffsetRotationOffset.AddLine(new Line(new Tag("X"), "0"));
            COSOffsetRotationOffset.AddLine(new Line(new Tag("Y"), "0"));
            COSOffsetRotationOffset.AddLine(new Line(new Tag("Z"), "0"));
            COSOffsetRotationOffset.AddLine(new Line(new Tag("W"), "1"));
        }

        public override void visit(DbSource source)
        {
            throw new NotImplementedException();
        }
        public override void visit(PictureMarker pictureMarker)
        {
            // Sensors
            string sensorExtension = "Type=\"" + pictureMarker.PictureMarkerTrackingSensor.SensorType + "\" Subtype=\"" + pictureMarker.PictureMarkerTrackingSensor.SensorSubType + "\"";
            SubSection sensorSubSection = new SubSection(new Tag("Sensor", sensorExtension));
            sensorsSubSection.AddSubSection(sensorSubSection);

            // SensorID
            sensorSubSection.AddLine(new Line(new Tag("SensorID"), pictureMarker.PictureMarkerTrackingSensor.SensorIDString));

            // Parameters
            SubSection parametersSubSection = new SubSection(new Tag("Parameters"));
            sensorSubSection.AddSubSection(parametersSubSection);

            parametersSubSection.AddLine(new Line(new Tag("FeatureDescriptorAlignment"), pictureMarker.PictureMarkerTrackingSensor.FeatureDescriptorAlignment.ToString()));
            parametersSubSection.AddLine(new Line(new Tag("MaxObjectsToDetectPerFrame"), pictureMarker.PictureMarkerTrackingSensor.MaxObjectsToDetectPerFrame.ToString()));
            parametersSubSection.AddLine(new Line(new Tag("MaxObjectsToTrackInParallel"), pictureMarker.PictureMarkerTrackingSensor.MaxObjectsToTrackInParallel.ToString()));
            parametersSubSection.AddLine(new Line(new Tag("SimilarityThreshold"), pictureMarker.PictureMarkerTrackingSensor.SimilarityThreshold.ToString()));

            SubSection sensorCOSSubSection = new SubSection(new Tag("SensorCOS"));
            sensorSubSection.AddSubSection(sensorCOSSubSection);

            sensorCOSSubSection.AddLine(new Line(new Tag("SensorCosID"), pictureMarker.SensorCosID));

            SubSection parameterSubSection = new SubSection(new Tag("Parameters"));
            sensorCOSSubSection.AddSubSection(parameterSubSection);

            parameterSubSection.AddLine(new Line(new Tag("ReferenceImage"), pictureMarker.ImageName));
        }
        public override void visit(IDMarker idMarker)
        {
            // Sensors
            string sensorExtension = "Type=\"" + idMarker.IdMarkerTrackingSensor.SensorType + "\" Subtype=\"" + idMarker.IdMarkerTrackingSensor.SensorSubType + "\"";
            SubSection sensorSubSection = new SubSection(new Tag("Sensor", sensorExtension));
            sensorsSubSection.AddSubSection(sensorSubSection);

            // SensorID
            sensorSubSection.AddLine(new Line(new Tag("SensorID"), idMarker.IdMarkerTrackingSensor.SensorIDString));

            // Parameters
            SubSection parametersSubSection = new SubSection(new Tag("Parameters"));
            sensorSubSection.AddSubSection(parametersSubSection);

            // MarkerParameters
            SubSection markerTrackingParametersSubSection = new SubSection(new Tag("MarkerTrackingParameters"));
            parametersSubSection.AddSubSection(markerTrackingParametersSubSection);

            markerTrackingParametersSubSection.AddLine(new Line(new Tag("TrackingQuality"), idMarker.IdMarkerTrackingSensor.TrackingQuality.ToString()));
            markerTrackingParametersSubSection.AddLine(new Line(new Tag("ThresholdOffset"), idMarker.IdMarkerTrackingSensor.ThresholdOffset.ToString()));
            markerTrackingParametersSubSection.AddLine(new Line(new Tag("NumberOfSearchIterations"), idMarker.IdMarkerTrackingSensor.NumberOfSearchIterations.ToString()));

            // SensorCOS
            SubSection sensorCOSSubSection = new SubSection(new Tag("SensorCOS"));
            sensorSubSection.AddSubSection(sensorCOSSubSection);

            sensorCOSSubSection.AddLine(new Line(new Tag("SensorCosID"), idMarker.SensorCosID));

            // Parameters
            SubSection parameterSubSection = new SubSection(new Tag("Parameters"));
            sensorCOSSubSection.AddSubSection(parameterSubSection);

            // MarkerParameters
            SubSection markerParametersSubSection = new SubSection(new Tag("MarkerParameters"));
            parametersSubSection.AddSubSection(markerParametersSubSection);

            // Reaktivated when getter is implemented
            //markerParametersSubSection.AddLine(new Line(new Tag("Size"), idMarker.Size.ToString()));
            markerParametersSubSection.AddLine(new Line(new Tag("Size"), "60"));
            markerParametersSubSection.AddLine(new Line(new Tag("MatrixID"), idMarker.MatrixID.ToString()));
        }

        public override void visit(Project p)
        {
            // Create [projectName].html
            arelProjectFile = new ARELProjectFile("<!DOCTYPE html>", Path.Combine(projectPath, "arel" + p.Name + ".html"));
            Section headSection = new Section(new Tag("head"));
            arelProjectFile.AddSection(headSection);

            headSection.AddLine(new Line(new OpenTag("meta", "charset=\"UTF-8\"")));
            headSection.AddLine(new Line(new OpenTag("meta", "name=\"viewport\" content=\"width=device-width, initial-scale=1\"")));
            headSection.AddLine(new Line(new Tag("script", "type=\"text/javascript\" src=\"../arel/arel.js\"")));
            headSection.AddLine(new Line(new Tag("script", "type=\"text/javascript\" src=\"Assets/arelGlue.js\"")));
            headSection.AddLine(new Line(new Tag("title"), p.Name));

            Section bodySection = new Section(new Tag("body"));
            arelProjectFile.AddSection(bodySection);

            // Prepare TrackinData.xml
            trackingDataFile = new TrackingDataFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", ProjectPath);
            Section trackingDataSection = new Section(new Tag("TrackingData"));
            trackingDataFile.AddSection(trackingDataSection);

            sensorsSubSection = new SubSection(new Tag("Sensors"));
            trackingDataSection.AddSubSection(sensorsSubSection);

            connectionsSubSection = new SubSection(new Tag("Connections"));
            trackingDataSection.AddSubSection(connectionsSubSection);

            // Create arelConfig.xml
            arelConfigFile = new ARELConfigFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", projectPath);

            // Results
            Section resultsSection = new Section(new Tag("results"));
            arelConfigFile.AddSection(resultsSection);

            // Trackingdata
            string trackingdataExtension = "channel=\"0\" poiprefix=\"extpoi-124906-\" url=\"Assets/TrackingData.xml\" /";
            resultsSection.AddLine(new Line(new OpenTag("trackingdata", trackingdataExtension)));
            resultsSection.AddLine(new Line(new Tag("apilevel"), "7"));
            resultsSection.AddLine(new Line(new Tag("arel"), Path.GetFileName(arelProjectFile.FilePath)));
            resultsSection.AddLine(new Line(new Tag("object", "id=\"extpoi-0-1\" channel=\"0\"")));
        }
    }
}
