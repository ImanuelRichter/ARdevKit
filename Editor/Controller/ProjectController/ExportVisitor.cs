using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Model.Project.IO;
using System.Globalization;

using ARdevKit.Model.Project;

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

        private XMLBlock sensorBlock;
        private XMLBlock sensorParametersBlock;
        private XMLBlock connectionsBlock;
        private XMLBlock fuserBlock;

        private int cosCounter = 1;

        private JavaScriptBlock sceneReadyFunktionBlock;
        private JavaScriptBlock ifPatternIsFoundBlock;
        private JavaScriptBlock ifPatternIsLostBlock;
        private int imageCount = 1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given graph. </summary>
        ///
        /// <remarks>   Imanuel, 16.01.2014. </remarks>
        ///
        /// <param name="graph">    The graph. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(BarGraph graph)
        {
            // Connections 

            // COS
            XMLBlock cosBlock = new XMLBlock(new TerminatingTag("COS"));
            connectionsBlock.AddBlock(cosBlock);

            // Name
            cosBlock.AddLine(new XMLLine(new TerminatingTag("Name"), project.Sensor.Name + "COS" + cosCounter++));

            // Fuser
            fuserBlock = new XMLBlock(new TerminatingTag("Dummy"));
            cosBlock.AddBlock(fuserBlock);

            // SensorSource
            XMLBlock sensorSourceBlock = new XMLBlock(new TerminatingTag("SensorSource", "trigger=\"1\""));
            cosBlock.AddBlock(sensorSourceBlock);

            // SensorID
            sensorSourceBlock.AddLine(new XMLLine(new TerminatingTag("SensorID"), project.Sensor.SensorIDString));
            // SensorCosID
            sensorSourceBlock.AddLine(new XMLLine(new TerminatingTag("SensorCosID"), graph.Trackable.SensorCosID));

            // Hand-Eye-Calibration
            XMLBlock handEyeCalibrationBlock = new XMLBlock(new TerminatingTag("HandEyeCalibration"));
            sensorSourceBlock.AddBlock(handEyeCalibrationBlock);

            // Translation
            XMLBlock hecTranslationOffset = new XMLBlock(new TerminatingTag("TranslationOffset"));
            handEyeCalibrationBlock.AddBlock(hecTranslationOffset);

            // TODO get vectors
            hecTranslationOffset.AddLine(new XMLLine(new TerminatingTag("X"), "0"));
            hecTranslationOffset.AddLine(new XMLLine(new TerminatingTag("Y"), "0"));
            hecTranslationOffset.AddLine(new XMLLine(new TerminatingTag("Z"), "0"));

            // Rotation
            XMLBlock hecRotationOffset = new XMLBlock(new TerminatingTag("RotationOffset"));
            handEyeCalibrationBlock.AddBlock(hecRotationOffset);

            // TODO get vectors
            hecRotationOffset.AddLine(new XMLLine(new TerminatingTag("X"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new TerminatingTag("Y"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new TerminatingTag("Z"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new TerminatingTag("W"), "1"));

            // COSOffset
            XMLBlock COSOffsetBlock = new XMLBlock(new TerminatingTag("COSOffset"));
            sensorSourceBlock.AddBlock(COSOffsetBlock);

            // Translation
            XMLBlock COSOffsetTranslationOffset = new XMLBlock(new TerminatingTag("TranslationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetTranslationOffset);
            
            string augmentionPositionX = graph.TranslationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionPositionY = graph.TranslationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionPositionZ = graph.TranslationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetTranslationOffset.AddLine(new XMLLine(new TerminatingTag("X"), augmentionPositionX));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new TerminatingTag("Y"), augmentionPositionY));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new TerminatingTag("Z"), augmentionPositionZ));

            // Rotation
            XMLBlock COSOffsetRotationOffset = new XMLBlock(new TerminatingTag("RotationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetRotationOffset);

            // TODO get vectors
            string augmentionRotationX = graph.RotationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionRotationY = graph.RotationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionRotationZ = graph.RotationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionRotationW = graph.RotationVector.W.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetRotationOffset.AddLine(new XMLLine(new TerminatingTag("X"), augmentionRotationX));
            COSOffsetRotationOffset.AddLine(new XMLLine(new TerminatingTag("Y"), augmentionRotationY));
            COSOffsetRotationOffset.AddLine(new XMLLine(new TerminatingTag("Z"), augmentionRotationZ));
            COSOffsetRotationOffset.AddLine(new XMLLine(new TerminatingTag("W"), augmentionRotationW));

            // arelGlue.js
            JavaScriptBlock loadContentBlock = new JavaScriptBlock();
            sceneReadyFunktionBlock.AddBlock(loadContentBlock);

            string imageVariable = "image" + imageCount;
            loadContentBlock.AddLine(new JavaScriptLine("var " + imageVariable + " = arel.Object.Model3D.createFromImage(\"" + imageVariable + "\",\"Assets/" + Path.GetFileName(graph.AugmentionPath) + "\")"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setVisibility(" + graph.IsVisible.ToString().ToLower() + ")"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setCoordinateSystemID(" + graph.Coordinatesystemid + ")"));
            string augmentionScalingX = graph.ScalingVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionScalingY = graph.ScalingVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionScalingZ = graph.ScalingVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setScale(new arel.Vector3D(" + augmentionScalingX + "," + augmentionScalingY + "," + augmentionScalingZ + "))"));
            loadContentBlock.AddLine(new JavaScriptLine("arel.Scene.addObject(" + imageVariable + ")"));

            ifPatternIsFoundBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + imageVariable + "\").setVisibility(true)"));
            ifPatternIsLostBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + imageVariable + "\").setVisibility(false)"));

            imageCount++;
        }

        public override void Visit(DbSource source)
        {
            throw new NotImplementedException();
        }

        public override void Visit(PictureMarker pictureMarker)
        {
            XMLBlock sensorCOSBlock = new XMLBlock(new TerminatingTag("SensorCOS"));
            sensorBlock.AddBlock(sensorCOSBlock);

            sensorCOSBlock.AddLine(new XMLLine(new TerminatingTag("SensorCosID"), pictureMarker.SensorCosID));

            XMLBlock parameterBlock = new XMLBlock(new TerminatingTag("Parameters"));
            sensorCOSBlock.AddBlock(parameterBlock);

            parameterBlock.AddLine(new XMLLine(new TerminatingTag("Size"), pictureMarker.Size.ToString()));

            XMLBlock markerParametersBlock = new XMLBlock(new TerminatingTag("MarkerParameters"));
            parameterBlock.AddBlock(markerParametersBlock);
            markerParametersBlock.AddLine(new XMLLine(new TerminatingTag("referenceImage", "qualityThreshold=\"0.70\""), pictureMarker.ImageName));
            string value = pictureMarker.SimilarityThreshhold.ToString("F1", CultureInfo.InvariantCulture);
            parameterBlock.AddLine(new XMLLine(new TerminatingTag("SimilarityThreshold"), value));
        }

        public override void Visit(MarkerFuser markerFuser)
        {
            // Fuser
            fuserBlock.Update(new TerminatingTag("Fuser", "type=\"" + markerFuser.FuserType + "\""));

            // Parameters
            XMLBlock fuserParametersBlock = new XMLBlock(new TerminatingTag("Parameters"));
            fuserBlock.AddBlock(fuserParametersBlock);

            string value = markerFuser.AlphaRotation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new TerminatingTag("AlphaRotation"), value));

            value = markerFuser.AlphaTranslation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new TerminatingTag("AlphaTranslation"), value));

            value = markerFuser.KeepPoseForNumberOfFrames.ToString();
            fuserParametersBlock.AddLine(new XMLLine(new TerminatingTag("KeepPoseForNumberOfFrames"), value));
        }

        public override void Visit(MarkerlessFuser markerlessFuser)
        {
            // Fuser
            fuserBlock.Update(new TerminatingTag("Fuser", "type=\"" + markerlessFuser.FuserType + "\""));

            // Parameters
            XMLBlock fuserParametersBlock = new XMLBlock(new TerminatingTag("Parameters"));
            fuserBlock.AddBlock(fuserParametersBlock);

            string value = markerlessFuser.KeepPoseForNumberOfFrames.ToString();
            fuserParametersBlock.AddLine(new XMLLine(new TerminatingTag("KeepPoseForNumberOfFrames"), value));

            value = markerlessFuser.GravityAssistance.ToString();
            fuserParametersBlock.AddLine(new XMLLine(new TerminatingTag("GravityAssistance"), value));

            value = markerlessFuser.AlphaTranslation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new TerminatingTag("AlphaTranslation"), value));

            value = markerlessFuser.GammaTranslation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new TerminatingTag("GammaTranslation"), value));

            value = markerlessFuser.AlphaRotation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new TerminatingTag("AlphaRotation"), value));

            value = markerlessFuser.GammaRotation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new TerminatingTag("GammaRotation"), value));

            value = markerlessFuser.ContinueLostTrackingWithOrientationSensor.ToString().ToLower();
            fuserParametersBlock.AddLine(new XMLLine(new TerminatingTag("ContinueLostTrackingWithOrientationSensor"), value));
        }

        public override void Visit(IDMarker idMarker)
        {
            // SensorCOS
            XMLBlock sensorCOSBlock = new XMLBlock(new TerminatingTag("SensorCOS"));
            sensorBlock.AddBlock(sensorCOSBlock);

            sensorCOSBlock.AddLine(new XMLLine(new TerminatingTag("SensorCosID"), idMarker.SensorCosID));

            // Parameters
            XMLBlock parameterBlock = new XMLBlock(new TerminatingTag("Parameters"));
            sensorCOSBlock.AddBlock(parameterBlock);

            // MarkerParameters
            XMLBlock markerParametersBlock = new XMLBlock(new TerminatingTag("MarkerParameters"));
            parameterBlock.AddBlock(markerParametersBlock);

            // Reaktivated when getter is implemented
            markerParametersBlock.AddLine(new XMLLine(new TerminatingTag("Size"), idMarker.Size.ToString()));
            markerParametersBlock.AddLine(new XMLLine(new TerminatingTag("MatrixID"), idMarker.MatrixID.ToString()));
        }

        public override void Visit(PictureMarkerSensor pictureMarkerSensor)
        {
            // MarkerParameters
            XMLBlock markerTrackingParametersBlock = new XMLBlock(new TerminatingTag("MarkerTrackingParameters"));
            sensorParametersBlock.AddBlock(markerTrackingParametersBlock);

            markerTrackingParametersBlock.AddLine(new XMLLine(new TerminatingTag("TrackingQuality"), pictureMarkerSensor.TrackingQuality.ToString()));
            markerTrackingParametersBlock.AddLine(new XMLLine(new TerminatingTag("ThresholdOffset"), pictureMarkerSensor.ThresholdOffset.ToString()));
            markerTrackingParametersBlock.AddLine(new XMLLine(new TerminatingTag("NumberOfSearchIterations"), pictureMarkerSensor.NumberOfSearchIterations.ToString()));
        }

        public override void Visit(MarkerSensor idMarkerSensor)
        {
            // MarkerParameters
            XMLBlock markerTrackingParametersBlock = new XMLBlock(new TerminatingTag("MarkerTrackingParameters"));
            sensorParametersBlock.AddBlock(markerTrackingParametersBlock);

            markerTrackingParametersBlock.AddLine(new XMLLine(new TerminatingTag("TrackingQuality"), idMarkerSensor.TrackingQuality.ToString()));
            markerTrackingParametersBlock.AddLine(new XMLLine(new TerminatingTag("ThresholdOffset"), idMarkerSensor.ThresholdOffset.ToString()));
            markerTrackingParametersBlock.AddLine(new XMLLine(new TerminatingTag("NumberOfSearchIterations"), idMarkerSensor.NumberOfSearchIterations.ToString()));
        }

        public override void Visit(MarkerlessSensor markerlessSensor)
        {
            sensorParametersBlock.AddLine(new XMLLine(new TerminatingTag("FeatureDescriptorAlignment"), markerlessSensor.FeatureDescriptorAlignment.ToString()));
            sensorParametersBlock.AddLine(new XMLLine(new TerminatingTag("MaxObjectsToDetectPerFrame"), markerlessSensor.MaxObjectsToDetectPerFrame.ToString()));
            sensorParametersBlock.AddLine(new XMLLine(new TerminatingTag("MaxObjectsToTrackInParallel"), markerlessSensor.MaxObjectsToTrackInParallel.ToString()));
            sensorParametersBlock.AddLine(new XMLLine(new TerminatingTag("SimilarityThreshold"), markerlessSensor.SimilarityThreshold.ToString("F1", CultureInfo.InvariantCulture)));
        }

        public override void Visit(Project p)
        {
            project = p;

            // Create [projectName].html
            arelProjectFile = new ARELProjectFile("<!DOCTYPE html>", Path.Combine(project.ProjectPath, "arel" + p.Name + ".html"));

            // head
            XMLBlock headBlock = new XMLBlock(new TerminatingTag("head"));
            arelProjectFile.AddBlock(headBlock);

                headBlock.AddLine(new XMLLine(new NonTerminatingTag("meta", "charset=\"UTF-8\"")));
                headBlock.AddLine(new XMLLine(new NonTerminatingTag("meta", "name=\"viewport\" content=\"width=device-width, initial-scale=1\"")));
                headBlock.AddLine(new XMLLine(new TerminatingTag("script", "type=\"text/javascript\" src=\"../arel/arel.js\"")));
                headBlock.AddLine(new XMLLine(new TerminatingTag("script", "type=\"text/javascript\" src=\"Assets/arelGlue.js\"")));
                headBlock.AddLine(new XMLLine(new TerminatingTag("title"), p.Name));

            // body
            XMLBlock bodyBlock = new XMLBlock(new TerminatingTag("body"));
            arelProjectFile.AddBlock(bodyBlock);

            // Prepare TrackinData.xml
            string trackingDataFileName = "TrackingData_" + project.Sensor.Name;
            trackingDataFileName += p.Sensor.SensorSubType != AbstractSensor.SensorSubTypes.None ? p.Sensor.SensorSubType.ToString() : "" + ".xml";
            trackingDataFile = new TrackingDataFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", project.ProjectPath, trackingDataFileName);

            // TrackingData
            XMLBlock trackingDataBlock = new XMLBlock(new TerminatingTag("TrackingData"));
            trackingDataFile.AddBlock(trackingDataBlock);

                // Sensors
                XMLBlock sensorsBlock = new XMLBlock(new TerminatingTag("Sensors"));
                trackingDataBlock.AddBlock(sensorsBlock);

                // Sensors
                string sensorExtension = "Type=\"" + p.Sensor.SensorType + "\"";
                sensorExtension += p.Sensor.SensorSubType != AbstractSensor.SensorSubTypes.None ? " Subtype=\"" + p.Sensor.SensorSubType + "\"" : "";
                sensorBlock = new XMLBlock(new TerminatingTag("Sensor", sensorExtension));
                sensorsBlock.AddBlock(sensorBlock);

                // SensorID
                sensorBlock.AddLine(new XMLLine(new TerminatingTag("SensorID"), p.Sensor.SensorIDString));

                // Parameters
                sensorParametersBlock = new XMLBlock(new TerminatingTag("Parameters"));
                sensorBlock.AddBlock(sensorParametersBlock);

                // Connections
                connectionsBlock = new XMLBlock(new TerminatingTag("Connections"));
                trackingDataBlock.AddBlock(connectionsBlock);

            // Create arelConfig.xml
            arelConfigFile = new ARELConfigFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", project.ProjectPath);

                // Results
                XMLBlock resultsBlock = new XMLBlock(new TerminatingTag("results"));
                arelConfigFile.AddBlock(resultsBlock);

                // Trackingdata
                string trackingdataExtension = "channel=\"0\" poiprefix=\"extpoi-124906-\" url=\"Assets/" + trackingDataFileName +"\" /";
                resultsBlock.AddLine(new XMLLine(new NonTerminatingTag("trackingdata", trackingdataExtension)));
                resultsBlock.AddLine(new XMLLine(new TerminatingTag("apilevel"), "7"));
                resultsBlock.AddLine(new XMLLine(new TerminatingTag("arel"), Path.GetFileName(arelProjectFile.FilePath)));

            // Create arelGlue.js
            arelGlueFile = new ARELGlueFile(project.ProjectPath);
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
