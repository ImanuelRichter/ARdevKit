using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;

using ARdevKit.Model.Project;
using ARdevKit.Model.Project.File;

namespace ARdevKit.Controller.ProjectController
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An <see cref="ExportVisitor"/>  is an <see cref="AbstractProjectVisitor"/> which
    ///             exports the project to the path defined in <see cref="Project"/> so that it
    ///             is readable by the player. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ExportVisitor : AbstractProjectVisitor
    {
        /// <summary>
        /// The exported <see cref="Project"/>
        /// </summary>
        private Project project;

        /// <summary>   The <see cref="AbstractFile"/>s created by the export visitor. </summary>
        private List<AbstractFile> files = new List<AbstractFile>();

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the <see cref="AbstractFile"/>s created by the export visitor. </summary>
        ///
        /// <value> The files. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<AbstractFile> Files
        {
            get { return files; }
            set { files = value; }
        }

        /// <summary>   The arel glue file. </summary>
        private ARELGlueFile arelGlueFile;

        /// <summary>   The arel project file head block. </summary>
        private XMLBlock arelProjectFileHeadBlock;

        /// <summary>   The sensor block within the <see cref="trackingDataFile"/>. </summary>
        private XMLBlock trackingDataFileSensorBlock;
        /// <summary>   The sensor parameters block within the <see cref="trackingDataFile"/>. </summary>
        private XMLBlock trackingDataFileSensorParametersBlock;
        /// <summary>   The connections block within the <see cref="trackingDataFile"/>. </summary>
        private XMLBlock trackingDataFileConnectionsBlock;
        /// <summary>   The fuser block within the <see cref="trackingDataFile"/>. </summary>
        private XMLBlock trackingDataFileFuserBlock;

        /// <summary>   The counter for the COSes. </summary>
        private int cosCounter = 1;

        /// <summary>   The scene ready funktion block within the <see cref="arelGlueFile"/>. </summary>
        private JavaScriptBlock sceneReadyFunctionBlock;
        /// <summary>   if pattern is found block within the <see cref="arelGlueFile"/>. </summary>
        private JavaScriptBlock ifPatternIsFoundBlock;
        /// <summary>   if pattern is lost block within the <see cref="arelGlueFile"/>. </summary>
        private JavaScriptBlock ifPatternIsLostBlock;

        /// <summary>   The chart file parse block. </summary>
        private JavaScriptBlock chartFileCreateBlock;
        /// <summary>   The chart file parse block. </summary>
        private JavaScriptBlock chartFileQueryBlock;

        /// <summary>   Number of videoes. </summary>
        private int videoCount;
        /// <summary>   Number of images added to the <see cref="arelGlueFile"/>. </summary>
        private int imageCount;
        /// <summary>   Number of bar charts. </summary>
        private int chartCount;
        /// <summary>   Identifier for the coordinate system. </summary>
        private int coordinateSystemID;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ExportVisitor()
        {
            videoCount = 1;
            imageCount = 1;
            chartCount = 1;
            coordinateSystemID = 0;
        }

        /// <summary>
        /// Visits the given <see cref="CustomUserEvent"/>
        /// </summary>
        /// <param name="cue">The customUserEvent</param>
        public override void Visit(CustomUserEvent cue)
        {
            string newPath = Path.Combine(project.ProjectPath, "Events");
            Helper.Copy(cue.FilePath, newPath);
            cue.FilePath = Path.Combine(newPath, Path.GetFileName(cue.FilePath));
            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "type=\"text/javascript\" src=\"Events/" + Path.GetFileName(cue.FilePath) + "\"")));
        }

        /// <summary>
        /// Visits the given <see cref="VideoAugmentation"/>
        /// </summary>
        /// <param name="video">The video</param>
        public override void Visit(VideoAugmentation video)
        {
            // Copy to projectPath
            string newPath = Path.Combine(project.ProjectPath, "Assets");
            Helper.Copy(video.VideoPath, newPath);
            video.VideoPath = Path.Combine(newPath, Path.GetFileName(video.VideoPath));

            // arelGlue.js
            JavaScriptBlock loadContentBlock = new JavaScriptBlock();
            sceneReadyFunctionBlock.AddBlock(loadContentBlock);

            string videoVariable = "video" + videoCount;
            string videoPath = Path.GetFileNameWithoutExtension(video.VideoPath) + Path.GetExtension(video.VideoPath);
            //string videoPath = Path.GetFileNameWithoutExtension(video.VideoPath) + ".alpha" + Path.GetExtension(video.VideoPath);
            loadContentBlock.AddLine(new JavaScriptLine("var " + videoVariable + " = arel.Object.Model3D.createFromMovie(\"" + videoVariable + "\",\"Assets/" + videoPath + "\")"));
            loadContentBlock.AddLine(new JavaScriptLine(videoVariable + ".setVisibility(" + video.IsVisible.ToString().ToLower() + ")"));
            loadContentBlock.AddLine(new JavaScriptLine(videoVariable + ".setCoordinateSystemID(" + coordinateSystemID + ")"));
            string augmentationScalingX = video.Scaling.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationScalingY = video.Scaling.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationScalingZ = video.Scaling.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine(videoVariable + ".setScale(new arel.Vector3D(" + augmentationScalingX + "," + augmentationScalingY + "," + augmentationScalingZ + "))"));
            string augmentationTranslationX = video.Translation.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationTranslationY = video.Translation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationTranslationZ = video.Translation.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine(videoVariable + ".setTranslation(new arel.Vector3D(" + augmentationTranslationX + "," + augmentationTranslationY + "," + augmentationTranslationZ + "))"));
            string augmentationRotationX = video.Rotation.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationY = video.Rotation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationZ = video.Rotation.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine("var " + videoVariable + "Rotation = new arel.Rotation()"));
            loadContentBlock.AddLine(new JavaScriptLine(videoVariable + "Rotation.setFromEulerAngleDegrees(new arel.Vector3D(" + augmentationRotationX + "," + augmentationRotationY + "," + augmentationRotationZ + "))"));
            loadContentBlock.AddLine(new JavaScriptLine(videoVariable + ".setRotation(" + videoVariable + "Rotation)"));
            loadContentBlock.AddLine(new JavaScriptLine("arel.Scene.addObject(" + videoVariable + ")"));

            ifPatternIsFoundBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + videoVariable + "\").setVisibility(true)"));
            ifPatternIsFoundBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + videoVariable + "\").startMovieTexture()"));

            ifPatternIsLostBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + videoVariable + "\").setVisibility(false)"));
            ifPatternIsLostBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + videoVariable + "\").pauseMovieTexture()"));

            videoCount++;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="ImageAugmentation"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="image">    The image. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(ImageAugmentation image)
        {
            // Copy to projectPath
            string newPath = Path.Combine(project.ProjectPath, "Assets");
            Helper.Copy(image.ImagePath, newPath);
            image.ImagePath = Path.Combine(newPath, Path.GetFileName(image.ImagePath));

            // arelGlue.js
            JavaScriptBlock loadContentBlock = new JavaScriptBlock();
            sceneReadyFunctionBlock.AddBlock(loadContentBlock);

            string imageVariable = "image" + imageCount;
            loadContentBlock.AddLine(new JavaScriptLine("var " + imageVariable + " = arel.Object.Model3D.createFromImage(\"" + imageVariable + "\",\"Assets/" + Path.GetFileName(image.ImagePath) + "\")"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setVisibility(" + image.IsVisible.ToString().ToLower() + ")"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setCoordinateSystemID(" + coordinateSystemID + ")"));
            string augmentationScalingX = image.Scaling.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationScalingY = image.Scaling.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationScalingZ = image.Scaling.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setScale(new arel.Vector3D(" + augmentationScalingX + "," + augmentationScalingY + "," + augmentationScalingZ + "))"));
            string augmentationTranslationX = image.Translation.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationTranslationY = image.Translation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationTranslationZ = image.Translation.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setTranslation(new arel.Vector3D(" + augmentationTranslationX + "," + augmentationTranslationY + "," + augmentationTranslationZ + "))"));
            string augmentationRotationX = image.Rotation.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationY = image.Rotation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationZ = image.Rotation.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine("var " + imageVariable + "Rotation = new arel.Rotation()"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + "Rotation.setFromEulerAngleDegrees(new arel.Vector3D(" + augmentationRotationX + "," + augmentationRotationY + "," + augmentationRotationZ + "))"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setRotation(" + imageVariable + "Rotation)"));
            loadContentBlock.AddLine(new JavaScriptLine("arel.Scene.addObject(" + imageVariable + ")"));

            ifPatternIsFoundBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + imageVariable + "\").setVisibility(true)"));
            ifPatternIsLostBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + imageVariable + "\").setVisibility(false)"));

            imageCount++;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="Chart"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="chart">    The bar graph. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(Chart chart)
        {
            chart.ID = chart.ID == null ? "chart" + chartCount : chart.ID;
            string chartID = chart.ID;
            string chartPluginID = "arel.Plugin." + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(chartID);

            // arel[projectName].html
            if (chartCount == 1)
            {
                arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "src=\"Assets/jquery-2.0.3.js\"")));
                arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "src=\"Assets/highcharts.js\"")));
            }

            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "src=\"Assets/" + chartID + "/chart.js\"")));

            Helper.Copy("res\\highcharts\\highcharts.js", Path.Combine(project.ProjectPath, "Assets"));
            Helper.Copy("res\\jquery\\jquery-2.0.3.js", Path.Combine(project.ProjectPath, "Assets"));

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // arelGlue.js
            JavaScriptBlock loadContentBlock = new JavaScriptBlock();
            sceneReadyFunctionBlock.AddBlock(loadContentBlock);

            arelGlueFile.AddBlock(new JavaScriptLine("var " + chartID));

            loadContentBlock.AddLine(new JavaScriptLine(chartID + " = " + chartPluginID));

            loadContentBlock.AddLine(new JavaScriptLine(chartID + ".create()"));
            loadContentBlock.AddLine(new JavaScriptLine(chartID + ".hide()"));

            // onTracking
            JavaScriptBlock chartIfPatternIsFoundShowBlock = new JavaScriptBlock("if (param[0].getCoordinateSystemID() == " + chartID + ".getCoordinateSystemID())", new BlockMarker("{", "}"));
            ifPatternIsFoundBlock.AddBlock(chartIfPatternIsFoundShowBlock);
            chartIfPatternIsFoundShowBlock.AddLine(new JavaScriptLine(chartID + ".show()"));
            if (chart.Positioning.PositioningMode == ChartPositioning.PositioningModes.RELATIVE)
                chartIfPatternIsFoundShowBlock.AddLine(new JavaScriptLine("arel.Scene.getScreenCoordinatesFrom3DPosition(COS" + coordinateSystemID + "Anchor.getTranslation(), " + chartID + ".getCoordinateSystemID(), function(coord){move(" + chartID + ", coord);})"));

            // onTracking lost
            ifPatternIsLostBlock.AddLine(new JavaScriptLine(chartID + ".hide()"));

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Create chart.js
            ChartFile chartFile = new ChartFile(project.ProjectPath, chartID);
            files.Add(chartFile);

            JavaScriptBlock chartFileVariablesBlock = new JavaScriptBlock();

            JavaScriptBlock chartFileDefineBlock = new JavaScriptBlock(chartPluginID + " = ", new BlockMarker("{", "};"));
            chartFile.AddBlock(chartFileDefineBlock);

            // ID
            chartFileDefineBlock.AddLine(new JavaScriptInLine("id : \"" + chartID + "\"", true));
            // CoordinateSystemID
            chartFileDefineBlock.AddLine(new JavaScriptInLine("coordinateSystemID : " + coordinateSystemID, true));
            // The chart
            chartFileDefineBlock.AddLine(new JavaScriptInLine("chart : {}", true));
            // Options
            chartFileDefineBlock.AddLine(new JavaScriptInLine("options : {}", true));
            // Translation
            string translationX = chart.Translation.X.ToString("F1", CultureInfo.InvariantCulture);
            string translationY = chart.Translation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string translationZ = chart.Translation.Z.ToString("F1", CultureInfo.InvariantCulture);
            chartFileDefineBlock.AddBlock(new JavaScriptInLine("translation : new arel.Vector3D(" + translationX + "," + translationY + "," + translationZ + ")", true));
            // ChartDiv
            chartFileDefineBlock.AddBlock(new JavaScriptInLine("div : document.createElement(\"div\")", true));

            // This is for "realtime" preview
            /*
            if (exportForTest)
                chartFileDefineSetOptionsLoadFileBlock.AddLine(new JavaScriptLine("setTimeout(function() { " + chartPluginID + ".setOptions(optionsPath); }, 5000)"));
             */

            // Create
            // Div
            chartFileCreateBlock = new JavaScriptBlock("create : function()", new BlockMarker("{", "},"));
            chartFileDefineBlock.AddBlock(chartFileCreateBlock);
            chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.setAttribute(\"id\", this.id)"));
            switch (chart.Positioning.PositioningMode)
            {
                case (ChartPositioning.PositioningModes.STATIC):
                    chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.position = \"static\""));
                    break;
                case (ChartPositioning.PositioningModes.ABSOLUTE):
                case (ChartPositioning.PositioningModes.RELATIVE):
                    chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.position = \"absolute\""));
                    break;
            }

            if (chart.Positioning.PositioningMode == ChartPositioning.PositioningModes.ABSOLUTE)
            {
                if (chart.Positioning.Top > 0)
                    chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.top = \"" + chart.Positioning.Top + "px\""));
                if (chart.Positioning.Left > 0)
                    chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.left = \"" + chart.Positioning.Left + "px\""));
            }

            chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.width = \"" + chart.Width + "px\""));
            chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.height = \"" + chart.Height + "px\""));
            chartFileCreateBlock.AddLine(new JavaScriptLine("document.documentElement.appendChild(this.div)"));

            // Copy options.js
            string chartFilesDirectory = Path.Combine(project.ProjectPath, "Assets", chartID);
            Helper.Copy(chart.Options, chartFilesDirectory, "options.js");
            chart.Options = Path.Combine(chartFilesDirectory, "options.js");

            // setOptions
            JavaScriptBlock chartFileDefineLoadOptionsBlock = new JavaScriptBlock("$.getScript(\"Assets/" + chartID + "/options.js\", function()", new BlockMarker("{", "})"));
            chartFileCreateBlock.AddBlock(chartFileDefineLoadOptionsBlock);
            chartFileCreateBlock.AddBlock(new JavaScriptInLine(".fail(function() { console.log(\"Failed to load options\")})", false));
            chartFileCreateBlock.AddBlock(new JavaScriptLine(".done(function() { console.log(\"Loaded options successfully\")})"));

            chartFileDefineLoadOptionsBlock.AddLine(new JavaScriptLine(chartPluginID + ".options = init()"));
            if (chart.Source == null)
                chartFileDefineLoadOptionsBlock.AddLine(new JavaScriptLine(chartPluginID + ".chart = $('#' + " + chartPluginID + ".id).highcharts(" + chartPluginID + ".options)"));

            // Show            
            JavaScriptBlock chartShowBlock = new JavaScriptBlock("show : function()", new BlockMarker("{", "},"));
            chartFileDefineBlock.AddBlock(chartShowBlock);
            chartShowBlock.AddLine(new JavaScriptLine("$('#' + this.id).show()"));

            // Hide
            JavaScriptBlock chartHideBlock = new JavaScriptBlock("hide : function()", new BlockMarker("{", "},"));
            chartFileDefineBlock.AddBlock(chartHideBlock);
            chartHideBlock.AddLine(new JavaScriptLine("$('#' + this.id).hide()"));

            // Get coordinateSystemID
            JavaScriptBlock chartGetCoordinateSystemIDBlock = new JavaScriptBlock("getCoordinateSystemID : function()", new BlockMarker("{", "}"));
            chartFileDefineBlock.AddBlock(chartGetCoordinateSystemIDBlock);
            chartGetCoordinateSystemIDBlock.AddLine(new JavaScriptLine("return this.coordinateSystemID"));

            chartCount++;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="DbSource"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <exception cref="NotImplementedException">  Thrown when the requested operation is
        ///                                             unimplemented. </exception>
        ///
        /// <param name="source">   Source for the. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(DbSource source)
        {
            string chartID = source.Augmentation.ID;
            string chartPluginID = "arel.Plugin." + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(chartID);
            string chartFilesDirectory = Path.Combine(project.ProjectPath, "Assets", chartID);

            if (source.Query != null && source.Query != "")
            {
                Helper.Copy(source.Query, chartFilesDirectory, "query.js");
                source.Query = Path.Combine(chartFilesDirectory, "query.js");

                chartFileQueryBlock = new JavaScriptBlock("$.getScript(\"Assets/" + chartID + "/" + Path.GetFileName(source.Query) + "\", function()", new BlockMarker("{", "})"));
                chartFileCreateBlock.AddBlock(chartFileQueryBlock);
                chartFileCreateBlock.AddBlock(new JavaScriptInLine(".fail(function() { console.log(\"Failed to load query\")})", false));
                chartFileCreateBlock.AddBlock(new JavaScriptLine(".done(function() { console.log(\"Loaded query successfully\")})"));
                chartFileQueryBlock.AddLine(new JavaScriptLine("var dataPath = \"" + source.Url + "\""));
                chartFileQueryBlock.AddLine(new JavaScriptLine("query(dataPath, " + chartPluginID + ".id, " + chartPluginID + ".options)"));
            }
            else
                chartFileCreateBlock.AddLine(new JavaScriptLine("alert('No query defined')"));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="FileSource"/>. </summary>
        ///
        /// <remarks>   Imanuel, 23.01.2014. </remarks>
        ///
        /// <exception cref="NotImplementedException">  Thrown when the requested operation is
        ///                                             unimplemented. </exception>
        ///
        /// <param name="source">   Source for the <see cref="AbstractDynamic2DAugmentation"/>. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(FileSource source)
        {
            string chartID = source.Augmentation.ID;
            string chartPluginID = "arel.Plugin." + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(chartID);
            string chartFilesDirectory = Path.Combine(project.ProjectPath, "Assets", chartID);

            if (source.Data != null && source.Data != "")
            {
                Helper.Copy(source.Data, chartFilesDirectory, "data" + Path.GetExtension(source.Data));
                source.Data = Path.Combine(chartFilesDirectory, "data" + Path.GetExtension(source.Data));

                if (source.Query != null && source.Query != "")
                {
                    Helper.Copy(source.Query, chartFilesDirectory, "query.js");
                    source.Query = Path.Combine(chartFilesDirectory, "query.js");

                    chartFileQueryBlock = new JavaScriptBlock("$.getScript(\"Assets/" + chartID + "/" + Path.GetFileName(source.Query) + "\", function()", new BlockMarker("{", "})"));
                    chartFileCreateBlock.AddBlock(chartFileQueryBlock);
                    chartFileCreateBlock.AddBlock(new JavaScriptInLine(".fail(function() { console.log(\"Failed to load query\")})", false));
                    chartFileCreateBlock.AddBlock(new JavaScriptLine(".done(function() { console.log(\"Loaded query successfully\")})"));
                    chartFileQueryBlock.AddLine(new JavaScriptLine("var dataPath = \"Assets/" + chartID + "/data.xml\""));
                    chartFileQueryBlock.AddLine(new JavaScriptLine(chartPluginID + ".chart = query(dataPath, " + chartPluginID + ".id, " + chartPluginID + ".options)"));
                }
                else
                    chartFileCreateBlock.AddLine(new JavaScriptLine("alert('No query defined')"));
            }
            else
                chartFileCreateBlock.AddLine(new JavaScriptLine("alert('No source file defined')"));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="MarkerlessFuser"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="markerlessFuser">  The markerless fuser. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(MarkerlessFuser markerlessFuser)
        {
            // Fuser
            trackingDataFileFuserBlock.Update(new XMLTag("Fuser", "type=\"" + markerlessFuser.FuserType + "\""));

            // Parameters
            XMLBlock fuserParametersBlock = new XMLBlock(new XMLTag("Parameters"));
            trackingDataFileFuserBlock.AddBlock(fuserParametersBlock);

            string value = markerlessFuser.KeepPoseForNumberOfFrames.ToString();
            fuserParametersBlock.AddLine(new XMLLine(new XMLTag("KeepPoseForNumberOfFrames"), value));

            value = markerlessFuser.GravityAssistance.ToString();
            fuserParametersBlock.AddLine(new XMLLine(new XMLTag("GravityAssistance"), value));

            value = markerlessFuser.AlphaTranslation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new XMLTag("AlphaTranslation"), value));

            value = markerlessFuser.GammaTranslation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new XMLTag("GammaTranslation"), value));

            value = markerlessFuser.AlphaRotation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new XMLTag("AlphaRotation"), value));

            value = markerlessFuser.GammaRotation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new XMLTag("GammaRotation"), value));

            value = markerlessFuser.ContinueLostTrackingWithOrientationSensor.ToString().ToLower();
            fuserParametersBlock.AddLine(new XMLLine(new XMLTag("ContinueLostTrackingWithOrientationSensor"), value));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="MarkerlessSensor"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="markerlessSensor"> The markerless sensor. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(MarkerlessSensor markerlessSensor)
        {
            trackingDataFileSensorParametersBlock.AddLine(new XMLLine(new XMLTag("FeatureDescriptorAlignment"), markerlessSensor.FeatureDescriptorAlignment.ToString()));
            trackingDataFileSensorParametersBlock.AddLine(new XMLLine(new XMLTag("MaxObjectsToDetectPerFrame"), markerlessSensor.MaxObjectsToDetectPerFrame.ToString()));
            trackingDataFileSensorParametersBlock.AddLine(new XMLLine(new XMLTag("MaxObjectsToTrackInParallel"), markerlessSensor.MaxObjectsToTrackInParallel.ToString()));
            trackingDataFileSensorParametersBlock.AddLine(new XMLLine(new XMLTag("SimilarityThreshold"), markerlessSensor.SimilarityThreshold.ToString("F1", CultureInfo.InvariantCulture)));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="MarkerFuser"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="markerFuser">  The marker fuser. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(MarkerFuser markerFuser)
        {
            // Fuser
            trackingDataFileFuserBlock.Update(new XMLTag("Fuser", "type=\"" + markerFuser.FuserType + "\""));

            // Parameters
            XMLBlock fuserParametersBlock = new XMLBlock(new XMLTag("Parameters"));
            trackingDataFileFuserBlock.AddBlock(fuserParametersBlock);

            string value = markerFuser.AlphaRotation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new XMLTag("AlphaRotation"), value));

            value = markerFuser.AlphaTranslation.ToString("F1", CultureInfo.InvariantCulture);
            fuserParametersBlock.AddLine(new XMLLine(new XMLTag("AlphaTranslation"), value));

            value = markerFuser.KeepPoseForNumberOfFrames.ToString();
            fuserParametersBlock.AddLine(new XMLLine(new XMLTag("KeepPoseForNumberOfFrames"), value));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="PictureMarkerSensor"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="pictureMarkerSensor">  The picture marker sensor. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(PictureMarkerSensor pictureMarkerSensor)
        {
            // MarkerParameters
            XMLBlock markerTrackingParametersBlock = new XMLBlock(new XMLTag("MarkerTrackingParameters"));
            trackingDataFileSensorParametersBlock.AddBlock(markerTrackingParametersBlock);

            markerTrackingParametersBlock.AddLine(new XMLLine(new XMLTag("TrackingQuality"), pictureMarkerSensor.TrackingQuality.ToString()));
            markerTrackingParametersBlock.AddLine(new XMLLine(new XMLTag("ThresholdOffset"), pictureMarkerSensor.ThresholdOffset.ToString()));
            markerTrackingParametersBlock.AddLine(new XMLLine(new XMLTag("NumberOfSearchIterations"), pictureMarkerSensor.NumberOfSearchIterations.ToString()));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="Image"/>. </summary>
        ///
        /// <remarks>   Imanuel, 26.01.2014. </remarks>
        ///
        /// <param name="image">    The image. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(ImageTrackable image)
        {
            // Copy the file
            string newPath = Path.Combine(project.ProjectPath, "Assets");
            Helper.Copy(image.ImagePath, newPath);
            image.ImagePath = Path.Combine(newPath, Path.GetFileName(image.ImagePath));

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // trackingData.xml

            XMLBlock sensorCOSBlock = new XMLBlock(new XMLTag("SensorCOS"));
            trackingDataFileSensorBlock.AddBlock(sensorCOSBlock);

            image.SensorCosID = IDFactory.CreateNewSensorCosID(image);
            sensorCOSBlock.AddLine(new XMLLine(new XMLTag("SensorCosID"), image.SensorCosID));

            XMLBlock parameterBlock = new XMLBlock(new XMLTag("Parameters"));
            sensorCOSBlock.AddBlock(parameterBlock);
            parameterBlock.AddLine(new XMLLine(new XMLTag("referenceImage"), Path.GetFileName(image.ImagePath)));
            string value = image.SimilarityThreshold.ToString("F1", CultureInfo.InvariantCulture);
            parameterBlock.AddLine(new XMLLine(new XMLTag("SimilarityThreshold"), value));

            // Connections 

            // COS
            XMLBlock cosBlock = new XMLBlock(new XMLTag("COS"));
            trackingDataFileConnectionsBlock.AddBlock(cosBlock);

            // Name
            cosBlock.AddLine(new XMLLine(new XMLTag("Name"), project.Sensor.Name + "COS" + cosCounter++));

            // Fuser
            trackingDataFileFuserBlock = new XMLBlock(new XMLTag("Fuser"));
            cosBlock.AddBlock(trackingDataFileFuserBlock);

            // SensorSource
            XMLBlock sensorSourceBlock = new XMLBlock(new XMLTag("SensorSource"));
            cosBlock.AddBlock(sensorSourceBlock);

            // SensorID
            sensorSourceBlock.AddLine(new XMLLine(new XMLTag("SensorID"), project.Sensor.SensorIDString));
            // SensorCosID
            sensorSourceBlock.AddLine(new XMLLine(new XMLTag("SensorCosID"), image.SensorCosID));

            // Hand-Eye-Calibration
            XMLBlock handEyeCalibrationBlock = new XMLBlock(new XMLTag("HandEyeCalibration"));
            sensorSourceBlock.AddBlock(handEyeCalibrationBlock);

            // Translation
            XMLBlock hecTranslationOffset = new XMLBlock(new XMLTag("TranslationOffset"));
            handEyeCalibrationBlock.AddBlock(hecTranslationOffset);

            // TODO get vectors
            hecTranslationOffset.AddLine(new XMLLine(new XMLTag("X"), "0"));
            hecTranslationOffset.AddLine(new XMLLine(new XMLTag("Y"), "0"));
            hecTranslationOffset.AddLine(new XMLLine(new XMLTag("Z"), "0"));

            // Rotation
            XMLBlock hecRotationOffset = new XMLBlock(new XMLTag("RotationOffset"));
            handEyeCalibrationBlock.AddBlock(hecRotationOffset);

            // TODO get vectors
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("X"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("Y"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("Z"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("W"), "1"));

            // COSOffset
            XMLBlock COSOffsetBlock = new XMLBlock(new XMLTag("COSOffset"));
            sensorSourceBlock.AddBlock(COSOffsetBlock);

            // Translation
            XMLBlock COSOffsetTranslationOffset = new XMLBlock(new XMLTag("TranslationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetTranslationOffset);

            string augmentationPositionX = image.Translation.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationPositionY = image.Translation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationPositionZ = image.Translation.Z.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("X"), augmentationPositionX));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Y"), augmentationPositionY));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Z"), augmentationPositionZ));

            // Rotation
            XMLBlock COSOffsetRotationOffset = new XMLBlock(new XMLTag("RotationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetRotationOffset);

            string augmentationRotationX = image.Rotation.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationY = image.Rotation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationZ = image.Rotation.Z.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationW = image.Rotation.W.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("X"), augmentationRotationX));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("Y"), augmentationRotationY));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("Z"), augmentationRotationZ));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("W"), augmentationRotationW));

            coordinateSystemID++;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // arelGlue.js

            // Set anchor
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            g.Flush();
            string anchorPath = Path.Combine(project.ProjectPath, "Assets", "anchor.png");
            if (!File.Exists(anchorPath))
                bmp.Save(anchorPath, System.Drawing.Imaging.ImageFormat.Png);

            // Add global variable for the anchor
            arelGlueFile.AddBlock(new JavaScriptLine("var COS" + coordinateSystemID + "Anchor"));

            // Add the anchor to the scene
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("COS" + coordinateSystemID + "Anchor = arel.Object.Model3D.createFromImage(\"COS" + coordinateSystemID + "Anchor" + "\",\"Assets/anchor.png" + "\")"));
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("COS" + coordinateSystemID + "Anchor.setVisibility(false)"));
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("COS" + coordinateSystemID + "Anchor.setCoordinateSystemID(" + coordinateSystemID + ")"));
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("arel.Scene.addObject(COS" + coordinateSystemID + "Anchor)"));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="PictureMarker"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="pictureMarker">    The picture marker. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(PictureMarker pictureMarker)
        {
            // Copy the file
            string newPath = Path.Combine(project.ProjectPath, "Assets");
            Helper.Copy(pictureMarker.PicturePath, newPath);
            pictureMarker.PicturePath = Path.Combine(newPath, Path.GetFileName(pictureMarker.PicturePath));

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // trackingData.xml

            XMLBlock sensorCOSBlock = new XMLBlock(new XMLTag("SensorCOS"));
            trackingDataFileSensorBlock.AddBlock(sensorCOSBlock);

            pictureMarker.SensorCosID = IDFactory.CreateNewSensorCosID(pictureMarker);
            sensorCOSBlock.AddLine(new XMLLine(new XMLTag("SensorCosID"), pictureMarker.SensorCosID));

            XMLBlock parameterBlock = new XMLBlock(new XMLTag("Parameters"));
            sensorCOSBlock.AddBlock(parameterBlock);

            parameterBlock.AddLine(new XMLLine(new XMLTag("Size"), pictureMarker.Size.ToString()));

            XMLBlock markerParametersBlock = new XMLBlock(new XMLTag("MarkerParameters"));
            parameterBlock.AddBlock(markerParametersBlock);
            markerParametersBlock.AddLine(new XMLLine(new XMLTag("referenceImage", "qualityThreshold=\"0.70\""), Path.GetFileName(pictureMarker.PicturePath)));
            string value = pictureMarker.SimilarityThreshold.ToString("F1", CultureInfo.InvariantCulture);
            parameterBlock.AddLine(new XMLLine(new XMLTag("SimilarityThreshold"), value));

            // Connections 

            // COS
            XMLBlock cosBlock = new XMLBlock(new XMLTag("COS"));
            trackingDataFileConnectionsBlock.AddBlock(cosBlock);

            // Name
            cosBlock.AddLine(new XMLLine(new XMLTag("Name"), project.Sensor.Name + "COS" + cosCounter++));

            // Fuser
            trackingDataFileFuserBlock = new XMLBlock(new XMLTag("Fuser"));
            cosBlock.AddBlock(trackingDataFileFuserBlock);

            // SensorSource
            XMLBlock sensorSourceBlock = new XMLBlock(new XMLTag("SensorSource", "trigger=\"1\""));
            cosBlock.AddBlock(sensorSourceBlock);

            // SensorID
            sensorSourceBlock.AddLine(new XMLLine(new XMLTag("SensorID"), project.Sensor.SensorIDString));
            // SensorCosID
            sensorSourceBlock.AddLine(new XMLLine(new XMLTag("SensorCosID"), pictureMarker.SensorCosID));

            // Hand-Eye-Calibration
            XMLBlock handEyeCalibrationBlock = new XMLBlock(new XMLTag("HandEyeCalibration"));
            sensorSourceBlock.AddBlock(handEyeCalibrationBlock);

            // Translation
            XMLBlock hecTranslationOffset = new XMLBlock(new XMLTag("TranslationOffset"));
            handEyeCalibrationBlock.AddBlock(hecTranslationOffset);

            // TODO get vectors
            hecTranslationOffset.AddLine(new XMLLine(new XMLTag("X"), "0"));
            hecTranslationOffset.AddLine(new XMLLine(new XMLTag("Y"), "0"));
            hecTranslationOffset.AddLine(new XMLLine(new XMLTag("Z"), "0"));

            // Rotation
            XMLBlock hecRotationOffset = new XMLBlock(new XMLTag("RotationOffset"));
            handEyeCalibrationBlock.AddBlock(hecRotationOffset);

            // TODO get vectors
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("X"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("Y"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("Z"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("W"), "1"));

            // COSOffset
            XMLBlock COSOffsetBlock = new XMLBlock(new XMLTag("COSOffset"));
            sensorSourceBlock.AddBlock(COSOffsetBlock);

            // Translation
            XMLBlock COSOffsetTranslationOffset = new XMLBlock(new XMLTag("TranslationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetTranslationOffset);

            string augmentationPositionX = pictureMarker.Translation.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationPositionY = pictureMarker.Translation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationPositionZ = pictureMarker.Translation.Z.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("X"), augmentationPositionX));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Y"), augmentationPositionY));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Z"), augmentationPositionZ));

            // Rotation
            XMLBlock COSOffsetRotationOffset = new XMLBlock(new XMLTag("RotationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetRotationOffset);

            string augmentationRotationX = pictureMarker.Rotation.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationY = pictureMarker.Rotation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationZ = pictureMarker.Rotation.Z.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationW = pictureMarker.Rotation.W.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("X"), augmentationRotationX));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("Y"), augmentationRotationY));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("Z"), augmentationRotationZ));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("W"), augmentationRotationW));

            coordinateSystemID++;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // arelGlue.js

            // Set anchor
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            g.Flush();
            string anchorPath = Path.Combine(project.ProjectPath, "Assets", "anchor.png");
            if (!File.Exists(anchorPath))
                bmp.Save(anchorPath, System.Drawing.Imaging.ImageFormat.Png);

            // Add global variable for the anchor
            arelGlueFile.AddBlock(new JavaScriptLine("var COS" + coordinateSystemID + "Anchor"));

            // Add the anchor to the scene
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("COS" + coordinateSystemID + "Anchor = arel.Object.Model3D.createFromImage(\"COS" + coordinateSystemID + "Anchor" + "\",\"Assets/anchor.png" + "\")"));
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("COS" + coordinateSystemID + "Anchor.setVisibility(false)"));
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("COS" + coordinateSystemID + "Anchor.setCoordinateSystemID(" + coordinateSystemID + ")"));
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("arel.Scene.addObject(COS" + coordinateSystemID + "Anchor)"));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="MarkerSensor"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="idMarkerSensor">   The identifier marker sensor. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(MarkerSensor idMarkerSensor)
        {
            // MarkerParameters
            XMLBlock markerTrackingParametersBlock = new XMLBlock(new XMLTag("MarkerTrackingParameters"));
            trackingDataFileSensorParametersBlock.AddBlock(markerTrackingParametersBlock);

            markerTrackingParametersBlock.AddLine(new XMLLine(new XMLTag("TrackingQuality"), idMarkerSensor.TrackingQuality.ToString()));
            markerTrackingParametersBlock.AddLine(new XMLLine(new XMLTag("ThresholdOffset"), idMarkerSensor.ThresholdOffset.ToString()));
            markerTrackingParametersBlock.AddLine(new XMLLine(new XMLTag("NumberOfSearchIterations"), idMarkerSensor.NumberOfSearchIterations.ToString()));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="IDMarker"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="idMarker"> The identifier marker. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(IDMarker idMarker)
        {
            // SensorCOS
            XMLBlock sensorCOSBlock = new XMLBlock(new XMLTag("SensorCOS"));
            trackingDataFileSensorBlock.AddBlock(sensorCOSBlock);

            idMarker.SensorCosID = IDFactory.CreateNewSensorCosID(idMarker);
            sensorCOSBlock.AddLine(new XMLLine(new XMLTag("SensorCosID"), idMarker.SensorCosID));

            // Parameters
            XMLBlock parameterBlock = new XMLBlock(new XMLTag("Parameters"));
            sensorCOSBlock.AddBlock(parameterBlock);

            // MarkerParameters
            XMLBlock markerParametersBlock = new XMLBlock(new XMLTag("MarkerParameters"));
            parameterBlock.AddBlock(markerParametersBlock);

            markerParametersBlock.AddLine(new XMLLine(new XMLTag("Size"), idMarker.Size.ToString()));
            markerParametersBlock.AddLine(new XMLLine(new XMLTag("MatrixID"), idMarker.MatrixID.ToString()));

            // Connections 

            // COS
            XMLBlock cosBlock = new XMLBlock(new XMLTag("COS"));
            trackingDataFileConnectionsBlock.AddBlock(cosBlock);

            // Name
            cosBlock.AddLine(new XMLLine(new XMLTag("Name"), project.Sensor.Name + "COS" + cosCounter++));

            // Fuser
            trackingDataFileFuserBlock = new XMLBlock(new XMLTag("Fuser"));
            cosBlock.AddBlock(trackingDataFileFuserBlock);

            // SensorSource
            XMLBlock sensorSourceBlock = new XMLBlock(new XMLTag("SensorSource", "trigger=\"1\""));
            cosBlock.AddBlock(sensorSourceBlock);

            // SensorID
            sensorSourceBlock.AddLine(new XMLLine(new XMLTag("SensorID"), project.Sensor.SensorIDString));
            // SensorCosID
            sensorSourceBlock.AddLine(new XMLLine(new XMLTag("SensorCosID"), idMarker.SensorCosID));

            // Hand-Eye-Calibration
            XMLBlock handEyeCalibrationBlock = new XMLBlock(new XMLTag("HandEyeCalibration"));
            sensorSourceBlock.AddBlock(handEyeCalibrationBlock);

            // Translation
            XMLBlock hecTranslationOffset = new XMLBlock(new XMLTag("TranslationOffset"));
            handEyeCalibrationBlock.AddBlock(hecTranslationOffset);

            // TODO get vectors
            hecTranslationOffset.AddLine(new XMLLine(new XMLTag("X"), "0"));
            hecTranslationOffset.AddLine(new XMLLine(new XMLTag("Y"), "0"));
            hecTranslationOffset.AddLine(new XMLLine(new XMLTag("Z"), "0"));

            // Rotation
            XMLBlock hecRotationOffset = new XMLBlock(new XMLTag("RotationOffset"));
            handEyeCalibrationBlock.AddBlock(hecRotationOffset);

            // TODO get vectors
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("X"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("Y"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("Z"), "0"));
            hecRotationOffset.AddLine(new XMLLine(new XMLTag("W"), "1"));

            // COSOffset
            XMLBlock COSOffsetBlock = new XMLBlock(new XMLTag("COSOffset"));
            sensorSourceBlock.AddBlock(COSOffsetBlock);

            // Translation
            XMLBlock COSOffsetTranslationOffset = new XMLBlock(new XMLTag("TranslationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetTranslationOffset);

            string trackablePositionX = idMarker.Translation.X.ToString("F1", CultureInfo.InvariantCulture);
            string trackablePositionY = idMarker.Translation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string trackablePositionZ = idMarker.Translation.Z.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("X"), trackablePositionX));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Y"), trackablePositionY));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Z"), trackablePositionZ));

            // Rotation
            XMLBlock COSOffsetRotationOffset = new XMLBlock(new XMLTag("RotationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetRotationOffset);

            string trackableRotationX = idMarker.Rotation.X.ToString("F1", CultureInfo.InvariantCulture);
            string trackableRotationY = idMarker.Rotation.Y.ToString("F1", CultureInfo.InvariantCulture);
            string trackableRotationZ = idMarker.Rotation.Z.ToString("F1", CultureInfo.InvariantCulture);
            string trackableRotationW = idMarker.Rotation.W.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("X"), trackableRotationX));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("Y"), trackableRotationY));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("Z"), trackableRotationZ));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("W"), trackableRotationW));

            coordinateSystemID++;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // arelGlue.js

            // Set anchor
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            g.Flush();
            string anchorPath = Path.Combine(project.ProjectPath, "Assets", "anchor.png");
            if (!File.Exists(anchorPath))
                bmp.Save(anchorPath, System.Drawing.Imaging.ImageFormat.Png);

            // Add global variable for the anchor
            arelGlueFile.AddBlock(new JavaScriptLine("var COS" + coordinateSystemID + "Anchor"));

            // Add the anchor to the scene
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("COS" + coordinateSystemID + "Anchor = arel.Object.Model3D.createFromImage(\"COS" + coordinateSystemID + "Anchor" + "\",\"Assets/anchor.png" + "\")"));
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("COS" + coordinateSystemID + "Anchor.setVisibility(false)"));
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("COS" + coordinateSystemID + "Anchor.setCoordinateSystemID(" + coordinateSystemID + ")"));
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("arel.Scene.addObject(COS" + coordinateSystemID + "Anchor)"));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="Project"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="p">    The project. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(Project p)
        {
            project = p;

            // Clean up
            if (Directory.Exists(project.ProjectPath))
            {
                bool cleanedUp = false;
                DialogResult abortRetryIgnore = DialogResult.Cancel;
                do
                    try
                    {
                        foreach (string path in Directory.GetFiles(project.ProjectPath))
                            if (!Path.GetExtension(path).Equals(".ardev"))
                                File.Delete(path);
                        string assetsPath = Path.Combine(project.ProjectPath, "Assets");
                        if (Directory.Exists(assetsPath))
                        {
                            foreach (string path in Directory.GetFiles(assetsPath))
                                File.Delete(path);
                        }
                        cleanedUp = true;
                    }
                    catch (Exception e)
                    {
                        abortRetryIgnore = MessageBox.Show(e.Message, "Error!", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                        if (abortRetryIgnore == DialogResult.Abort)
                            throw new OperationCanceledException();
                    }
                while (!cleanedUp && abortRetryIgnore == DialogResult.Retry);
            }

            // Copy arel file
            Helper.Copy(Path.Combine("res", "arel", "arel.js"), project.ProjectPath);

            // Create [projectName].html
            ARELProjectFile arelProjectFile = new ARELProjectFile("<!DOCTYPE html>", Path.Combine(project.ProjectPath, "arel" + (p.Name != "" ? p.Name : "Test") + ".html"));
            files.Add(arelProjectFile);

            // head
            arelProjectFileHeadBlock = new XMLBlock(new XMLTag("head"));
            arelProjectFile.AddBlock(arelProjectFileHeadBlock);

            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("title"), p.Name != "" ? p.Name : "Test"));

            arelProjectFileHeadBlock.AddLine(new XMLLine(new NonTerminatingXMLTag("meta", "charset=\"UTF-8\"")));
            arelProjectFileHeadBlock.AddLine(new XMLLine(new NonTerminatingXMLTag("meta", "name=\"viewport\" content=\"width=device-width, initial-scale=1\"")));

            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("title"), p.Name != null ? p.Name : "Test"));

            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "type=\"text/javascript\" src=\"arel.js\"")));
            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "type=\"text/javascript\" src=\"Assets/arelGlue.js\"")));

            // body
            XMLBlock bodyBlock = new XMLBlock(new XMLTag("body"));
            arelProjectFile.AddBlock(bodyBlock);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Prepare TrackinData.xml
            string trackingDataFileName = "TrackingData_" + p.Sensor.Name;
            trackingDataFileName += p.Sensor.SensorSubType != AbstractSensor.SensorSubTypes.None ? p.Sensor.SensorSubType.ToString() : "";
            trackingDataFileName += ".xml";
            TrackingDataFile trackingDataFile;
            trackingDataFile = new TrackingDataFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", project.ProjectPath, trackingDataFileName);
            files.Add(trackingDataFile);

            // TrackingData
            XMLBlock trackingDataBlock = new XMLBlock(new XMLTag("TrackingData"));
            trackingDataFile.AddBlock(trackingDataBlock);

            // Sensors
            XMLBlock sensorsBlock = new XMLBlock(new XMLTag("Sensors"));
            trackingDataBlock.AddBlock(sensorsBlock);

            // Sensors
            string sensorExtension = "Type=\"" + p.Sensor.SensorType + "\"";
            sensorExtension += p.Sensor.SensorSubType != AbstractSensor.SensorSubTypes.None ? " Subtype=\"" + p.Sensor.SensorSubType + "\"" : "";
            trackingDataFileSensorBlock = new XMLBlock(new XMLTag("Sensor", sensorExtension));
            sensorsBlock.AddBlock(trackingDataFileSensorBlock);

            // SensorID
            trackingDataFileSensorBlock.AddLine(new XMLLine(new XMLTag("SensorID"), p.Sensor.SensorIDString));

            // Parameters
            trackingDataFileSensorParametersBlock = new XMLBlock(new XMLTag("Parameters"));
            trackingDataFileSensorBlock.AddBlock(trackingDataFileSensorParametersBlock);

            // Connections
            trackingDataFileConnectionsBlock = new XMLBlock(new XMLTag("Connections"));
            trackingDataBlock.AddBlock(trackingDataFileConnectionsBlock);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Create arelConfig.xml
            ARELConfigFile arelConfigFile = new ARELConfigFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", project.ProjectPath);
            files.Add(arelConfigFile);

            // Results
            XMLBlock resultsBlock = new XMLBlock(new XMLTag("results"));
            arelConfigFile.AddBlock(resultsBlock);

            // Trackingdata
            string trackingdataExtension = "channel=\"0\" poiprefix=\"extpoi-124906-\" url=\"Assets/" + trackingDataFileName + "\" /";
            resultsBlock.AddLine(new XMLLine(new NonTerminatingXMLTag("trackingdata", trackingdataExtension)));
            resultsBlock.AddLine(new XMLLine(new XMLTag("apilevel"), "7"));
            resultsBlock.AddLine(new XMLLine(new XMLTag("arel"), Path.GetFileName(arelProjectFile.FilePath)));

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Create arelGlue.js
            arelGlueFile = new ARELGlueFile(project.ProjectPath);
            files.Add(arelGlueFile);

            JavaScriptBlock sceneReadyBlock = new JavaScriptBlock("arel.sceneReady", new BlockMarker("(", ");"));
            arelGlueFile.AddBlock(sceneReadyBlock);
            sceneReadyFunctionBlock = new JavaScriptBlock("function()", new BlockMarker("{", "}"));
            sceneReadyBlock.AddBlock(sceneReadyFunctionBlock);

            // Console log ready
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("console.log(\"Scene ready\")"));

            // Set a listener to tracking to get information about when the image is tracked
            JavaScriptBlock eventListenerBlock = new JavaScriptBlock("arel.Events.setListener", new BlockMarker("(", ");"));
            sceneReadyFunctionBlock.AddBlock(eventListenerBlock);
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
            ifPatternIsFoundBlock.AddLine(new JavaScriptLine("console.log(\"Tracked coordinateSystemID: \" + param[0].getCoordinateSystemID())"));

            // Pattern lost
            ifPatternIsLostBlock = new JavaScriptBlock("else if(type && type == arel.Events.Scene.ONTRACKING && param[0].getState() == arel.Tracking.STATE_NOTTRACKING)", new BlockMarker("{", "}"));
            ifTrackingInformationAvailiableBlock.AddBlock(ifPatternIsLostBlock);
            ifPatternIsLostBlock.AddLine(new JavaScriptLine("console.log(\"Tracking lost\")"));

            // Move
            JavaScriptBlock arelGlueMoveBlock = new JavaScriptBlock("function move(object, coord)", new BlockMarker("{", "};"));
            arelGlueFile.AddBlock(arelGlueMoveBlock);
            arelGlueMoveBlock.AddLine(new JavaScriptLine("var left = (coord.getX() - parseInt(object.div.style.width) / 2) + object.translation.getX()"));
            arelGlueMoveBlock.AddLine(new JavaScriptLine("var top = (coord.getY() - parseInt(object.div.style.height) / 2) - object.translation.getY()"));
            arelGlueMoveBlock.AddLine(new JavaScriptLine("object.div.style.left = left + 'px'"));
            arelGlueMoveBlock.AddLine(new JavaScriptLine("object.div.style.top = top + 'px'"));
            arelGlueMoveBlock.AddLine(new JavaScriptLine("console.log(\"Moved \" + object.id + \" to \" + left + \", \" + top)"));
        }
    }
}
