using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Model.Project.File;

namespace ARdevKit.Controller.ProjectController
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An export visitor which exports the project to be readable by the player. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ExportVisitor : AbstractProjectVisitor
    {
        private ProjectConfigFile projectConfig;
        public ProjectConfigFile ProjectConfig
        {
            get { return projectConfig; }
            set { projectConfig = value; }
        }

        /// <summary>   The trackin configuration XML. </summary>
        private TrackingConfigurationFile trackingConfig;
        public TrackingConfigurationFile TrackingConfig
        {
            get { return trackingConfig; }
            set { trackingConfig = value; }
        }
        private Section sensorsSection;
        private int pictureMarkerCounter = 1;
        private int idMarkerCounter = 1;

        public ExportVisitor()
        {
        }

        public override void visit(BarGraph graph)
        {
            throw new NotImplementedException();
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
            sensorsSection.AddSubSection(sensorSubSection);

            // SensorID
            sensorSubSection.AddLine(new Line(new Tag("SensorID"), pictureMarker.PictureMarkerTrackingSensor.SensorID.ToString()));

            // Parameters
            SubSection parametersSubSection = new SubSection(new Tag("Parameters"));
            sensorSubSection.AddSubSection(parametersSubSection);

            parametersSubSection.AddLine(new Line(new Tag("FeatureDescriptorAlignment"), pictureMarker.PictureMarkerTrackingSensor.FeatureDescriptorAlignment.ToString()));
            parametersSubSection.AddLine(new Line(new Tag("MaxObjectsToDetectPerFrame"), pictureMarker.PictureMarkerTrackingSensor.MaxObjectsToDetectPerFrame.ToString()));
            parametersSubSection.AddLine(new Line(new Tag("MaxObjectsToTrackInParallel"), pictureMarker.PictureMarkerTrackingSensor.MaxObjectsToTrackInParallel.ToString()));
            parametersSubSection.AddLine(new Line(new Tag("SimilarityThreshold"), pictureMarker.PictureMarkerTrackingSensor.SimilarityThreshold.ToString()));

            SubSection sensorCOSSubSection = new SubSection(new Tag("SensorCOS"));
            sensorSubSection.AddSubSection(sensorCOSSubSection);

            sensorCOSSubSection.AddLine(new Line(new Tag("SensorCosID"), "PictureMarker" + pictureMarkerCounter++));

            SubSection parameterSubSection = new SubSection(new Tag("Parameters"));
            sensorCOSSubSection.AddSubSection(parameterSubSection);

            parameterSubSection.AddLine(new Line(new Tag("ReferenceImage"), pictureMarker.ImageName));
        }
        public override void visit(IDMarker idMarker)
        {
            // Sensors
            string sensorExtension = "Type=\"" + idMarker.IdMarkerTrackingSensor.SensorType + "\" Subtype=\"" + idMarker.IdMarkerTrackingSensor.SensorSubType + "\"";
            SubSection sensorSubSection = new SubSection(new Tag("Sensor", sensorExtension));
            sensorsSection.AddSubSection(sensorSubSection);

            // SensorID
            sensorSubSection.AddLine(new Line(new Tag("SensorID"), idMarker.IdMarkerTrackingSensor.SensorID.ToString()));

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

            sensorCOSSubSection.AddLine(new Line(new Tag("SensorCosID"), "IDMarker" + idMarkerCounter++));

            // Parameters
            SubSection parameterSubSection = new SubSection(new Tag("Parameters"));
            sensorCOSSubSection.AddSubSection(parameterSubSection);

            // MarkerParameters
            SubSection markerParametersSubSection = new SubSection(new Tag("MarkerParameters"));
            parametersSubSection.AddSubSection(markerParametersSubSection);

            //markerParametersSubSection.AddLine(new Line(new Tag("Size"), idMarker.Size.ToString()));
            markerParametersSubSection.AddLine(new Line(new Tag("Size"), "60"));
            markerParametersSubSection.AddLine(new Line(new Tag("MatrixID"), idMarker.MatrixID.ToString()));
        }

        public override void visit(Project p)
        {
            // Create [projectName].html
            projectConfig = new ProjectConfigFile(new Tag("html"));
            Section headSection = new Section(new Tag("head"));
            projectConfig.AddSection(headSection);

            headSection.AddLine(new Line(new OpenTag("meta", "charset=\"UTF-8\"")));
            headSection.AddLine(new Line(new OpenTag("meta", "name=\"viewport\" content=\"width=device-width, initial-scale=1\"")));
            headSection.AddLine(new Line(new Tag("script", "type=\"text/javascript\" src=\"../arel/arel.js\"")));
            headSection.AddLine(new Line(new Tag("script", "type=\"text/javascript\" src=\"Assets/arelGlue.js\"")));
            headSection.AddLine(new Line(new Tag("title"), p.Name));

            Section body = new Section(new Tag("body"));
            projectConfig.AddSection(body);

            // Prepare TrackinConfiguration.xml
            TrackingConfig = new TrackingConfigurationFile(new Tag("TrackingData"));
            sensorsSection = new Section(new Tag("Sensors"));
            TrackingConfig.AddSection(sensorsSection);
        }
    }
}
