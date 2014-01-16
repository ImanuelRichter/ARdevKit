using ARdevKit.Model.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Model.Project.IO;
using System.Globalization;

namespace ARdevKit.Controller.ProjectController
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An export visitor which exports the project to be readable by the player. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ExportVisitor : AbstractProjectVisitor
    {
        private Project project;

        private string projectPath;
        public string ProjectPath
        {
            get { return projectPath; }
            set { projectPath = value; }
        }

        private ARELProjectFile arelProjectFile;
        public ARELProjectFile ArelProjectFile
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

        private ARELGlueFile arelGlueFile;
        public ARELGlueFile ArelGlueFile
        {
            get { return arelGlueFile; }
            set { arelGlueFile = value; }
        }

        private HTMLBlock sensorBlock;
        private HTMLBlock sensorParametersBlock;
        private HTMLBlock connectionsBlock;

        private int cosCounter = 1;

        private JavaScriptBlock sceneReadyFunktionBlock;
        private JavaScriptBlock ifPatternIsFoundBlock;
        private JavaScriptBlock ifPatternIsLostBlock;
        private int imageCount = 1;

        public ExportVisitor(string projectPath)
        {
            this.projectPath = projectPath;
        }

        public override void Visit(BarGraph graph)
        {
            // Connections
            
                // COS
                HTMLBlock cosBlock = new HTMLBlock(new Tag("COS"));
                connectionsBlock.AddBlock(cosBlock);

                    // Name
                    cosBlock.AddLine(new HTMLLine(new Tag("Name"), project.Sensor.Name + "COS" + cosCounter++));

                    // Fuser
                    HTMLBlock fuserBlock = new HTMLBlock(new Tag("Fuser", "Type=\"" + graph.Trackable.MarkerFuser.FuserType + "\""));
                    cosBlock.AddBlock(fuserBlock);

                        // Parameters
                        HTMLBlock fuserParametersBlock = new HTMLBlock(new Tag("Parameters"));
                        fuserBlock.AddBlock(fuserParametersBlock);

                        string value = graph.Trackable.MarkerFuser.KeepPoseForNumberOfFrames.ToString();
                        fuserParametersBlock.AddLine(new HTMLLine(new Tag("KeepPoseForNumberOfFrames"), value));

                        value = graph.Trackable.MarkerFuser.GravityAssistance.ToString();
                        fuserParametersBlock.AddLine(new HTMLLine(new Tag("GravityAssistance"), value));

                        value = graph.Trackable.MarkerFuser.AlphaTranslation.ToString("F1", CultureInfo.InvariantCulture);
                        fuserParametersBlock.AddLine(new HTMLLine(new Tag("AlphaTranslation"), value));

                        value = graph.Trackable.MarkerFuser.GammaTranslation.ToString("F1", CultureInfo.InvariantCulture);
                        fuserParametersBlock.AddLine(new HTMLLine(new Tag("GammaTranslation"), value));

                        value = graph.Trackable.MarkerFuser.AlphaRotation.ToString("F1", CultureInfo.InvariantCulture);
                        fuserParametersBlock.AddLine(new HTMLLine(new Tag("AlphaRotation"), value));

                        value = graph.Trackable.MarkerFuser.GammaRotation.ToString("F1", CultureInfo.InvariantCulture);
                        fuserParametersBlock.AddLine(new HTMLLine(new Tag("GammaRotation"), value));

                        value = graph.Trackable.MarkerFuser.ContinueLostTrackingWithOrientationSensor.ToString().ToLower();
                        fuserParametersBlock.AddLine(new HTMLLine(new Tag("ContinueLostTrackingWithOrientationSensor"), value));

                    // SensorSource
                    HTMLBlock sensorSourceBlock = new HTMLBlock(new Tag("SensorSource"));
                    cosBlock.AddBlock(sensorSourceBlock);

                        // SensorID
                        sensorSourceBlock.AddLine(new HTMLLine(new Tag("SensorID"), project.Sensor.SensorIDString));
                        // SensorCosID
                        sensorSourceBlock.AddLine(new HTMLLine(new Tag("SensorCosID"), graph.Trackable.SensorCosID));

                        // Hand-Eye-Calibration
                        HTMLBlock handEyeCalibrationBlock = new HTMLBlock(new Tag("HandEyeCalibration"));
                        sensorSourceBlock.AddBlock(handEyeCalibrationBlock);

                            // Translation
                            HTMLBlock hecTranslationOffset = new HTMLBlock(new Tag("TranslationOffset"));
                            handEyeCalibrationBlock.AddBlock(hecTranslationOffset);

                                // TODO get vectors
                                hecTranslationOffset.AddLine(new HTMLLine(new Tag("X"), "0"));
                                hecTranslationOffset.AddLine(new HTMLLine(new Tag("Y"), "0"));
                                hecTranslationOffset.AddLine(new HTMLLine(new Tag("Z"), "0"));

                            // Rotation
                            HTMLBlock hecRotationOffset = new HTMLBlock(new Tag("RotationOffset"));
                            handEyeCalibrationBlock.AddBlock(hecRotationOffset);

                            // TODO get vectors
                            hecRotationOffset.AddLine(new HTMLLine(new Tag("X"), "0"));
                            hecRotationOffset.AddLine(new HTMLLine(new Tag("Y"), "0"));
                            hecRotationOffset.AddLine(new HTMLLine(new Tag("Z"), "0"));
                            hecRotationOffset.AddLine(new HTMLLine(new Tag("W"), "1"));

                        // COSOffset
                        HTMLBlock COSOffsetBlock = new HTMLBlock(new Tag("COSOffset"));
                        sensorSourceBlock.AddBlock(COSOffsetBlock);

                            // Translation
                            HTMLBlock COSOffsetTranslationOffset = new HTMLBlock(new Tag("TranslationOffset"));
                            COSOffsetBlock.AddBlock(COSOffsetTranslationOffset);

                                // TODO get vectors
                                COSOffsetTranslationOffset.AddLine(new HTMLLine(new Tag("X"), "0"));
                                COSOffsetTranslationOffset.AddLine(new HTMLLine(new Tag("Y"), "0"));
                                COSOffsetTranslationOffset.AddLine(new HTMLLine(new Tag("Z"), "0"));

                            // Rotation
                            HTMLBlock COSOffsetRotationOffset = new HTMLBlock(new Tag("RotationOffset"));
                            COSOffsetBlock.AddBlock(COSOffsetRotationOffset);

                                // TODO get vectors
                                COSOffsetRotationOffset.AddLine(new HTMLLine(new Tag("X"), "0"));
                                COSOffsetRotationOffset.AddLine(new HTMLLine(new Tag("Y"), "0"));
                                COSOffsetRotationOffset.AddLine(new HTMLLine(new Tag("Z"), "0"));
                                COSOffsetRotationOffset.AddLine(new HTMLLine(new Tag("W"), "1"));

            // arelGlue.js
            JavaScriptBlock loadContentBlock = new JavaScriptBlock();
            sceneReadyFunktionBlock.AddBlock(loadContentBlock);

            string imageVariable = "image" + imageCount;
            loadContentBlock.AddLine(new JavaScriptLine("var " + imageVariable + " = arel.Object.Model3D.createFromImage(\"" + imageVariable + "\",\"Assets/" + Path.GetFileName(graph.AugmentationPath) + "\")"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setVisibility(" + graph.IsVisible.ToString().ToLower() + ")"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setCoordinateSystemID(" + graph.Coordinatesystemid + ")"));
            string x = graph.vector.X.ToString("F1", CultureInfo.InvariantCulture);
            string y = graph.vector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string z = graph.vector.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setScale(new arel.Vector3D(" + x + "," + y + "," + z + "))"));
            loadContentBlock.AddLine(new JavaScriptLine("arel.Scene.addObject(" + imageVariable + ")"));

            ifPatternIsFoundBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + imageVariable + "\").setVisibility(true)"));
            ifPatternIsLostBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + imageVariable + "\").setVisibility(false)"));
        }

        public override void Visit(DbSource source)
        {
            throw new NotImplementedException();
        }

        public override void Visit(PictureMarker pictureMarker)
        {
            HTMLBlock sensorCOSBlock = new HTMLBlock(new Tag("SensorCOS"));
            sensorBlock.AddBlock(sensorCOSBlock);

            sensorCOSBlock.AddLine(new HTMLLine(new Tag("SensorCosID"), pictureMarker.SensorCosID));

            HTMLBlock parameterBlock = new HTMLBlock(new Tag("Parameters"));
            sensorCOSBlock.AddBlock(parameterBlock);

            parameterBlock.AddLine(new HTMLLine(new Tag("ReferenceImage"), pictureMarker.ImageName));
        }

        public override void Visit(IDMarker idMarker)
        {
            // SensorCOS
            HTMLBlock sensorCOSBlock = new HTMLBlock(new Tag("SensorCOS"));
            sensorBlock.AddBlock(sensorCOSBlock);

            sensorCOSBlock.AddLine(new HTMLLine(new Tag("SensorCosID"), idMarker.SensorCosID));

            // Parameters
            HTMLBlock parameterBlock = new HTMLBlock(new Tag("Parameters"));
            sensorCOSBlock.AddBlock(parameterBlock);

            // MarkerParameters
            HTMLBlock markerParametersBlock = new HTMLBlock(new Tag("MarkerParameters"));
            parameterBlock.AddBlock(markerParametersBlock);

            // Reaktivated when getter is implemented
            //markerParametersBlock.AddLine(new Line(new Tag("Size"), idMarker.Size.ToString()));
            markerParametersBlock.AddLine(new HTMLLine(new Tag("Size"), "60"));
            markerParametersBlock.AddLine(new HTMLLine(new Tag("MatrixID"), idMarker.MatrixID.ToString()));
        }

        public override void Visit(IDMarkerSensor idMarkerSensor)
        {
            // MarkerParameters
            HTMLBlock markerTrackingParametersBlock = new HTMLBlock(new Tag("MarkerTrackingParameters"));
            sensorParametersBlock.AddBlock(markerTrackingParametersBlock);

            markerTrackingParametersBlock.AddLine(new HTMLLine(new Tag("TrackingQuality"), idMarkerSensor.TrackingQuality.ToString()));
            markerTrackingParametersBlock.AddLine(new HTMLLine(new Tag("ThresholdOffset"), idMarkerSensor.ThresholdOffset.ToString()));
            markerTrackingParametersBlock.AddLine(new HTMLLine(new Tag("NumberOfSearchIterations"), idMarkerSensor.NumberOfSearchIterations.ToString()));
        }

        public override void Visit(PictureMarkerSensor pictureMarkerSensor)
        {
            sensorParametersBlock.AddLine(new HTMLLine(new Tag("FeatureDescriptorAlignment"), pictureMarkerSensor.FeatureDescriptorAlignment.ToString()));
            sensorParametersBlock.AddLine(new HTMLLine(new Tag("MaxObjectsToDetectPerFrame"), pictureMarkerSensor.MaxObjectsToDetectPerFrame.ToString()));
            sensorParametersBlock.AddLine(new HTMLLine(new Tag("MaxObjectsToTrackInParallel"), pictureMarkerSensor.MaxObjectsToTrackInParallel.ToString()));
            sensorParametersBlock.AddLine(new HTMLLine(new Tag("SimilarityThreshold"), pictureMarkerSensor.SimilarityThreshold.ToString("F1", CultureInfo.InvariantCulture)));
        }

        public override void Visit(Project p)
        {
            project = p;

            // Create [projectName].html
            arelProjectFile = new ARELProjectFile("<!DOCTYPE html>", Path.Combine(projectPath, "arel" + p.Name + ".html"));

            // head
            HTMLBlock headBlock = new HTMLBlock(new Tag("head"));
            arelProjectFile.AddBlock(headBlock);

                headBlock.AddLine(new HTMLLine(new OpenTag("meta", "charset=\"UTF-8\"")));
                headBlock.AddLine(new HTMLLine(new OpenTag("meta", "name=\"viewport\" content=\"width=device-width, initial-scale=1\"")));
                headBlock.AddLine(new HTMLLine(new Tag("script", "type=\"text/javascript\" src=\"../arel/arel.js\"")));
                headBlock.AddLine(new HTMLLine(new Tag("script", "type=\"text/javascript\" src=\"Assets/arelGlue.js\"")));
                headBlock.AddLine(new HTMLLine(new Tag("title"), p.Name));

            // body
            HTMLBlock bodyBlock = new HTMLBlock(new Tag("body"));
            arelProjectFile.AddBlock(bodyBlock);

            // Prepare TrackinData.xml
            trackingDataFile = new TrackingDataFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", ProjectPath);

            // TrackingData
            HTMLBlock trackingDataBlock = new HTMLBlock(new Tag("TrackingData"));
            trackingDataFile.AddBlock(trackingDataBlock);

                // Sensors
                HTMLBlock sensorsBlock = new HTMLBlock(new Tag("Sensors"));
                trackingDataBlock.AddBlock(sensorsBlock);

                // Sensors
                string sensorExtension = "Type=\"" + p.Sensor.SensorType + "\" Subtype=\"" + p.Sensor.SensorSubType + "\"";
                sensorBlock = new HTMLBlock(new Tag("Sensor", sensorExtension));
                sensorsBlock.AddBlock(sensorBlock);

                // SensorID
                sensorBlock.AddLine(new HTMLLine(new Tag("SensorID"), p.Sensor.SensorIDString));

                // Parameters
                sensorParametersBlock = new HTMLBlock(new Tag("Parameters"));
                sensorBlock.AddBlock(sensorParametersBlock);

                // Connections
                connectionsBlock = new HTMLBlock(new Tag("Connections"));
                trackingDataBlock.AddBlock(connectionsBlock);

            // Create arelConfig.xml
            arelConfigFile = new ARELConfigFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", projectPath);

                // Results
                HTMLBlock resultsBlock = new HTMLBlock(new Tag("results"));
                arelConfigFile.AddBlock(resultsBlock);

                // Trackingdata
                string trackingdataExtension = "channel=\"0\" poiprefix=\"extpoi-124906-\" url=\"Assets/TrackingData.xml\" /";
                resultsBlock.AddLine(new HTMLLine(new OpenTag("trackingdata", trackingdataExtension)));
                resultsBlock.AddLine(new HTMLLine(new Tag("apilevel"), "7"));
                resultsBlock.AddLine(new HTMLLine(new Tag("arel"), Path.GetFileName(arelProjectFile.FilePath)));

            // Create arelGlue.js
            arelGlueFile = new ARELGlueFile(projectPath);
            JavaScriptBlock sceneReadyBlock = new JavaScriptBlock("arel.sceneReady", new BlockMarker("(", ");"));
            arelGlueFile.AddBlock(sceneReadyBlock);
            sceneReadyFunktionBlock = new JavaScriptBlock("function()", new BlockMarker("{", "}"));
            sceneReadyBlock.AddBlock(sceneReadyFunktionBlock);

                // Console log ready
                sceneReadyFunktionBlock.AddLine(new JavaScriptLine("console.log(\"sceneReady\")"));

                //set a listener to tracking to get information about when the image is tracked
                JavaScriptBlock eventListenerBlock = new JavaScriptBlock("arel.Events.setListener", new BlockMarker("(", ");"));
                sceneReadyFunktionBlock.AddBlock(eventListenerBlock);
                JavaScriptBlock eventListenreFunktionBlock = new JavaScriptBlock("arel.Scene, function(type, param)", new BlockMarker("{", "}"));
                eventListenerBlock.AddBlock(eventListenreFunktionBlock);

                eventListenreFunktionBlock.AddLine(new JavaScriptLine("trackingHandler(type, param)"));

                JavaScriptBlock trackingHandlerBlock = new JavaScriptBlock("function trackingHandler(type, param)", new BlockMarker("{", "};"));
                arelGlueFile.AddBlock(trackingHandlerBlock);

                // Tracking information availiable
                JavaScriptBlock ifTrackingInformationAvailiableBlock = new JavaScriptBlock("if(param[0] !== undefined)", new BlockMarker("{", "}"));
                trackingHandlerBlock.AddBlock(ifTrackingInformationAvailiableBlock);

                // Patternn found
                ifPatternIsFoundBlock = new JavaScriptBlock("if(type && type == arel.Events.Scene.ONTRACKING && param[0].getState() == arel.Tracking.STATE_TRACKING)", new BlockMarker("{", "}"));
                ifTrackingInformationAvailiableBlock.AddBlock(ifPatternIsFoundBlock);
                ifPatternIsFoundBlock.AddLine(new JavaScriptLine("console.log(\"Tracking is active\")"));

                // Pattern lost
                ifPatternIsLostBlock = new JavaScriptBlock("else if(type && type == arel.Events.Scene.ONTRACKING && param[0].getState() == arel.Tracking.STATE_NOTTRACKING)", new BlockMarker("{", "}"));
                ifTrackingInformationAvailiableBlock.AddBlock(ifPatternIsLostBlock);
                ifPatternIsLostBlock.AddLine(new JavaScriptLine("console.log(\"Tracking lost\")"));
        }
    }
}
