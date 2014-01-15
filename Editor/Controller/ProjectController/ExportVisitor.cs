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
        private TrackingConfigurationFile trackingConfiguration;
        public TrackingConfigurationFile TrackingConfiguration
        {
            get { return trackingConfiguration; }
            set { trackingConfiguration = value; }
        }
        private SubSection sensorSubSection;
        private int pictureMarkerCounter = 1;

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
            SubSection sensorCOSSubSection = new SubSection(new Tag("SensorCOS"));
            sensorSubSection.AddSubSection(sensorCOSSubSection);

            sensorCOSSubSection.AddLine(new Line(new Tag("SensorCosID"), "PictureMarker" + pictureMarkerCounter++));

            SubSection parameterSubSection = new SubSection(new Tag("Parameters"));
            sensorCOSSubSection.AddSubSection(parameterSubSection);

            parameterSubSection.AddLine(new Line(new Tag("ReferenceImage"), pictureMarker.ImageName));
        }
        public override void visit(IDMarker idMarker)
        {
            throw new NotImplementedException();
        }

        public override void visit(Project p)
        {
            // Create [projectName].html
            ProjectConfigFile projectConfigHTML = new ProjectConfigFile(new Tag("html"));
            Section headSection = new Section(new Tag("head"));
            projectConfigHTML.AddSection(headSection);

            headSection.AddLine(new Line(new OpenTag("meta", "charset=\"UTF-8\"")));
            headSection.AddLine(new Line(new OpenTag("meta", "name=\"viewport\" content=\"width=device-width, initial-scale=1\"")));
            headSection.AddLine(new Line(new Tag("script", "type=\"text/javascript\" src=\"../arel/arel.js\"")));
            headSection.AddLine(new Line(new Tag("script", "type=\"text/javascript\" src=\"Assets/arelGlue.js\"")));
            headSection.AddLine(new Line(new Tag("title"), p.Name));

            Section body = new Section(new Tag("body"));
            projectConfigHTML.AddSection(body);

            // Prepare TrackinConfiguration.xml
            TrackingConfiguration = new TrackingConfigurationFile(new Tag("TrackingData"));
            Section sensorsSection = new Section(new Tag("Sensors"));
            TrackingConfiguration.AddSection(sensorsSection);

            // Sensors
            string sensorExtension = "Type=\"" + p.SensorType + "\" Subtype=\"" + p.SensorSubType + "\"";
            sensorSubSection = new SubSection(new Tag("Sensor", sensorExtension));
            sensorsSection.AddSubSection(sensorSubSection);

            // SensorID
            sensorSubSection.AddLine(new Line(new Tag("SensorID"), p.SensorID.ToString()));

            // Parameters
            SubSection parametersSubSection = new SubSection(new Tag("Parameters"));
            sensorSubSection.AddSubSection(parametersSubSection);

            parametersSubSection.AddLine(new Line(new Tag("FeatureDescriptorAlignment"), p.FeatureDescriptorAlignment.ToString()));
            parametersSubSection.AddLine(new Line(new Tag("MaxObjectsToDetectPerFrame"), p.MaxObjectsToDetectPerFrame.ToString()));
            parametersSubSection.AddLine(new Line(new Tag("MaxObjectsToTrackInParallel"), p.MaxObjectsToTrackInParallel.ToString()));
            parametersSubSection.AddLine(new Line(new Tag("SimilarityThreshold"), p.SimilarityThreshold.ToString()));
        }
    }
}
