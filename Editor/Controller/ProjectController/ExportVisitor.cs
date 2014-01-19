﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Model.Project.File;
using System.Globalization;

using ARdevKit.Model.Project;

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
        /// <summary>   The project that sould be exported. </summary>
        private Project project;

        /// <summary>   The <see cref="ARELProjectFile"/> for the <see cref="project"/>. </summary>
        private ARELProjectFile arelProjectFile;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the arel project file. </summary>
        ///
        /// <value> The arel project file. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ARELProjectFile ArelProjectFile
        {
            get { return arelProjectFile; }
        }

        /// <summary>   The <see cref="TrackingDataFile"/> for the <see cref="project"/>. </summary>
        private TrackingDataFile trackingDataFile;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the tracking data file. </summary>
        ///
        /// <value> The tracking data file. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public TrackingDataFile TrackingDataFile
        {
            get { return trackingDataFile; }
        }

        /// <summary>   The <see cref="ARELConfigFile"/> for the <see cref="project"/>. </summary>
        private ARELConfigFile arelConfigFile;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the arel configuration file. </summary>
        ///
        /// <value> The arel configuration file. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ARELConfigFile ArelConfigFile
        {
            get { return arelConfigFile; }
        }

        /// <summary>   The <see cref="ARELGlueFile"/> for the <see cref="project"/>. </summary>
        private ARELGlueFile arelGlueFile;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the arel glue file. </summary>
        ///
        /// <value> The arel glue file. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ARELGlueFile ArelGlueFile
        {
            get { return arelGlueFile; }
        }

        private List<BarChartFile> barChartFiles;

        public List<BarChartFile> BarChartFiles
        {
            get { return barChartFiles; }
            set { barChartFiles = value; }
        }

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
        private JavaScriptBlock sceneReadyFunktionBlock;
        /// <summary>   if pattern is found block within the <see cref="arelGlueFile"/>. </summary>
        private JavaScriptBlock ifPatternIsFoundBlock;
        /// <summary>   if pattern is lost block within the <see cref="arelGlueFile"/>. </summary>
        private JavaScriptBlock ifPatternIsLostBlock;
        /// <summary>   Number of images added to the <see cref="arelGlueFile"/>. </summary>
        private int imageCount = 1;
        private int barChartCount = 1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="BarGraph"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="barChart">    The bar graph. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public override void Visit(BarGraph barChart)
        {
            // TrackingData.xml

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
            sensorSourceBlock.AddLine(new XMLLine(new XMLTag("SensorCosID"), barChart.Trackable.SensorCosID));

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

            string augmentionPositionX = barChart.TranslationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionPositionY = barChart.TranslationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionPositionZ = barChart.TranslationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("X"), augmentionPositionX));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Y"), augmentionPositionY));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Z"), augmentionPositionZ));

            // Rotation
            XMLBlock COSOffsetRotationOffset = new XMLBlock(new XMLTag("RotationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetRotationOffset);

            // TODO get vectors
            string augmentionRotationX = barChart.RotationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionRotationY = barChart.RotationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionRotationZ = barChart.RotationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionRotationW = barChart.RotationVector.W.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("X"), augmentionRotationX));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("Y"), augmentionRotationY));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("Z"), augmentionRotationZ));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("W"), augmentionRotationW));
            
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // arel[projectName].html
            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "src=\"Assets/jquery-2.0.3.js\"")));
            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "src=\"Assets/highcharts.js\"")));
            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "src=\"Assets/barChart" + barChartCount + ".js\"")));

            Copy("res\\highcharts\\highcharts.js", Path.Combine(project.ProjectPath, "Assets"));
            Copy("res\\jquery\\jquery-2.0.3.js", Path.Combine(project.ProjectPath, "Assets"));

            // arelGlue.js
            JavaScriptBlock loadContentBlock = new JavaScriptBlock();
            sceneReadyFunktionBlock.AddBlock(loadContentBlock);

            string barChartVariable = "barChart" + barChartCount;

            JavaScriptBlock arelGlueVariablesBlock = new JavaScriptBlock();
            arelGlueVariablesBlock.AddLine(new JavaScriptLine("var " + barChartVariable));
            arelGlueFile.AddBlock(arelGlueVariablesBlock);

            loadContentBlock.AddLine(new JavaScriptLine(barChartVariable + " = arel.Plugin.BarChart" + barChartCount));

            loadContentBlock.AddLine(new JavaScriptLine(barChartVariable + ".create()"));
            loadContentBlock.AddLine(new JavaScriptLine(barChartVariable + ".hide()"));

            JavaScriptBlock barChartIfPatternIsFoundShowBlock = new JavaScriptBlock("if (param[0].getCoordinateSystemID() == " + barChartVariable + ".getCoordinateSystemID())", new BlockMarker("{", "}"));
            ifPatternIsFoundBlock.AddBlock(barChartIfPatternIsFoundShowBlock);
            barChartIfPatternIsFoundShowBlock.AddLine(new JavaScriptLine(barChartVariable + ".show()"));

            ifPatternIsLostBlock.AddLine(new JavaScriptLine(barChartVariable + ".hide()"));

            // Create barChart[i].js
            if (barChartCount == 1)
                barChartFiles = new List<BarChartFile>();

            BarChartFile barChartFile = new BarChartFile(project.ProjectPath, barChartCount);
            JavaScriptBlock barChartFileVariablesBlock = new JavaScriptBlock();

            barChartFileVariablesBlock.AddLine(new JavaScriptLine("var id = \"" + barChartVariable + "\""));
            barChartFileVariablesBlock.AddLine(new JavaScriptLine("var coordinateSystemID = " + barChart.Coordinatesystemid));
            barChartFile.AddBlock(barChartFileVariablesBlock);

            JavaScriptBlock barChartFileDefineBlock = new JavaScriptBlock("arel.Plugin.BarChart" + barChartCount.ToString() + " = ", new BlockMarker("{", "};"));
            barChartFile.AddBlock(barChartFileDefineBlock);

            JavaScriptBlock barChartFileCreateBlock = new JavaScriptBlock("create : function()", new BlockMarker("{", "},"));
            barChartFileDefineBlock.AddBlock(barChartFileCreateBlock);
            barChartFileCreateBlock.AddLine(new JavaScriptLine("var chart = document.createElement(\"div\")"));
            barChartFileCreateBlock.AddLine(new JavaScriptLine("chart.setAttribute(\"id\", id)"));
            barChartFileCreateBlock.AddLine(new JavaScriptLine("chart.style.position = \"" + barChart.Style.Position + "\""));
            if (barChart.Style.Top >= 0)
                barChartFileCreateBlock.AddLine(new JavaScriptLine("chart.style.top = \"" + barChart.Style.Top + "px\""));
            if (barChart.Style.Left >= 0)
                barChartFileCreateBlock.AddLine(new JavaScriptLine("chart.style.left = \"" + barChart.Style.Left + "px\""));
            if (barChart.Style.Bottom >= 0)
                barChartFileCreateBlock.AddLine(new JavaScriptLine("chart.style.bottom = \"" + barChart.Style.Bottom + "px\""));
            if (barChart.Style.Right >= 0)
                barChartFileCreateBlock.AddLine(new JavaScriptLine("chart.style.right = \"" + barChart.Style.Right + "px\""));
            barChartFileCreateBlock.AddLine(new JavaScriptLine("chart.style.width = \"" + barChart.Width + "px\""));
            barChartFileCreateBlock.AddLine(new JavaScriptLine("chart.style.height = \"" + barChart.Height + "px\""));
            barChartFileCreateBlock.AddLine(new JavaScriptLine("document.documentElement.appendChild(chart)"));

            JavaScriptBlock barChartFileHighchartBlock = new JavaScriptBlock("$('#' + id).highcharts", new BlockMarker("({", "});"));
            barChartFileCreateBlock.AddBlock(barChartFileHighchartBlock);

            JavaScriptBlock barChartFileHighchartChartBlock = new JavaScriptBlock("chart: ", new BlockMarker("{", "},"));
            barChartFileHighchartBlock.AddBlock(barChartFileHighchartChartBlock);
            barChartFileHighchartChartBlock.AddLine(new JavaScriptInLine("type: 'column'", false));

            JavaScriptBlock barChartFileHighchartTitleBlock = new JavaScriptBlock("title: ", new BlockMarker("{", "},"));
            barChartFileHighchartBlock.AddBlock(barChartFileHighchartTitleBlock);
            barChartFileHighchartTitleBlock.AddLine(new JavaScriptInLine("text: '" + barChart.Title + "'", false));

            JavaScriptBlock barChartFileHighchartSubTitleBlock = new JavaScriptBlock("subtitle: ", new BlockMarker("{", "},"));
            barChartFileHighchartBlock.AddBlock(barChartFileHighchartSubTitleBlock);
            barChartFileHighchartSubTitleBlock.AddLine(new JavaScriptInLine("text: '" + barChart.Subtitle + "'", false));

            JavaScriptBlock barChartFileHighchartXAxisBlock = new JavaScriptBlock("xAxis: ", new BlockMarker("{", "},"));
            barChartFileHighchartBlock.AddBlock(barChartFileHighchartXAxisBlock);

            JavaScriptBlock barChartFileHighchartXAxisCategoriesBlock = new JavaScriptBlock("categories: ", new BlockMarker("[", "]"));
            barChartFileHighchartXAxisBlock.AddBlock(barChartFileHighchartXAxisCategoriesBlock);

            for (int i = 0; i < barChart.Categories.Length - 1; i++)
            {
                barChartFileHighchartXAxisCategoriesBlock.AddLine(new JavaScriptInLine("'" + barChart.Categories[i] + "'", true));
            }
            barChartFileHighchartXAxisCategoriesBlock.AddLine(new JavaScriptInLine("'" + barChart.Categories[barChart.Categories.Length - 1] + "'", false));

            JavaScriptBlock barChartFileHighchartYAxisBlock = new JavaScriptBlock("yAxis: ", new BlockMarker("{", "},"));
            barChartFileHighchartBlock.AddBlock(barChartFileHighchartYAxisBlock);

            barChartFileHighchartYAxisBlock.AddLine(new JavaScriptInLine("min: " + barChart.MinValue, true));
            JavaScriptBlock barChartFileHighchartYAxisTitleBlock = new JavaScriptBlock("title: ", new BlockMarker("{", "}"));
            barChartFileHighchartYAxisBlock.AddBlock(barChartFileHighchartYAxisTitleBlock);
            barChartFileHighchartYAxisTitleBlock.AddLine(new JavaScriptInLine("text: '" + barChart.YAxisTitle + "'", false));

            JavaScriptBlock barChartFileHighchartTooltipBlock = new JavaScriptBlock("tooltip: ", new BlockMarker("{", "},"));
            barChartFileHighchartBlock.AddBlock(barChartFileHighchartTooltipBlock);
            // TODO make these things editable
            barChartFileHighchartTooltipBlock.AddLine(new JavaScriptInLine("headerFormat: '<span style=\"font-size:10px\">{point.key}</span><table>'", true));
            barChartFileHighchartTooltipBlock.AddLine(new JavaScriptInLine("pointFormat: '<tr><td style=\"color:{series.color};padding:0\">{series.name}: </td>' +\n" +
					"'<td style=\"padding:0\"><b>{point.y:.1f} mm</b></td></tr>'", true));
            barChartFileHighchartTooltipBlock.AddLine(new JavaScriptInLine("footerFormat: '</table>'", true));
            barChartFileHighchartTooltipBlock.AddLine(new JavaScriptInLine("shared: true", true));
            barChartFileHighchartTooltipBlock.AddLine(new JavaScriptInLine("useHTML: true", false));

            JavaScriptBlock barChartFileHighchartPlotOptions = new JavaScriptBlock("plotOptions: ", new BlockMarker("{", "},"));
            barChartFileHighchartBlock.AddBlock(barChartFileHighchartPlotOptions);

            JavaScriptBlock barChartFileHighchartPlotOptionsColumn = new JavaScriptBlock("column: ", new BlockMarker("{", "}"));
            barChartFileHighchartPlotOptions.AddBlock(barChartFileHighchartPlotOptionsColumn);
            barChartFileHighchartPlotOptionsColumn.AddLine(new JavaScriptInLine("pointPadding: " + barChart.PointPadding.ToString(CultureInfo.InvariantCulture), true));
            barChartFileHighchartPlotOptionsColumn.AddLine(new JavaScriptInLine("borderWidth: " + barChart.BorderWidth, false));

            JavaScriptBlock barChartFileHighchartSeriesBlock = new JavaScriptBlock("series: ", new BlockMarker("[", "]"));
            barChartFileHighchartBlock.AddBlock(barChartFileHighchartSeriesBlock);

            int n1 = barChart.Data.Count - 1;
            for (int i = 0; i < n1; i++)
            {
                JavaScriptBlock dataBlock = new JavaScriptBlock("", new BlockMarker("{", "},"));
                barChartFileHighchartSeriesBlock.AddBlock(dataBlock);
                dataBlock.AddLine(new JavaScriptInLine("name: '" + barChart.Names[i] + "'", true));
                string data = "";

                int n2 = barChart.Data[i].Length - 1;
                for (int j = 0; j < n2; j++)
                {
                    data += barChart.Data[i][j].ToString(CultureInfo.InvariantCulture) + ", ";
                }
                data += barChart.Data[i][n2].ToString(CultureInfo.InvariantCulture);
                dataBlock.AddLine(new JavaScriptInLine("data: [" + data + "]", false));
            }
            JavaScriptBlock lastDataBlock = new JavaScriptBlock("", new BlockMarker("{", "}"));
            barChartFileHighchartSeriesBlock.AddBlock(lastDataBlock);
            lastDataBlock.AddLine(new JavaScriptInLine("name: '" + barChart.Names[n1] + "'", true));
            string lastData = "";

            int lastN2 = barChart.Data[n1].Length - 1;
            for (int j = 0; j < lastN2; j++)
            {
                lastData += barChart.Data[n1][j].ToString(CultureInfo.InvariantCulture) + ", ";
            }
            lastData += barChart.Data[n1][lastN2].ToString(CultureInfo.InvariantCulture);
            lastDataBlock.AddLine(new JavaScriptInLine("data: [" + lastData + "]", false));

            JavaScriptBlock barChartShowBlock = new JavaScriptBlock("show : function()", new BlockMarker("{", "},"));
            barChartFileDefineBlock.AddBlock(barChartShowBlock);
            barChartShowBlock.AddLine(new JavaScriptLine("$('#' + id).show()"));

            JavaScriptBlock barChartHideBlock = new JavaScriptBlock("hide : function()", new BlockMarker("{", "},"));
            barChartFileDefineBlock.AddBlock(barChartHideBlock);
            barChartHideBlock.AddLine(new JavaScriptLine("$('#' + id).hide()"));

            JavaScriptBlock barChartGetCoordinateSystemIDBlock = new JavaScriptBlock("getCoordinateSystemID : function()", new BlockMarker("{", "}"));
            barChartFileDefineBlock.AddBlock(barChartGetCoordinateSystemIDBlock);
            barChartGetCoordinateSystemIDBlock.AddLine(new JavaScriptLine("return coordinateSystemID"));

            barChartFiles.Add(barChartFile);
            barChartCount++;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="ImageAugmention"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="image">    The image. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(ImageAugmention image)
        {
            // Copy to projectPath
            Copy(image.ImagePath, Path.Combine(project.ProjectPath, "Assets"));

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
            sensorSourceBlock.AddLine(new XMLLine(new XMLTag("SensorCosID"), image.Trackable.SensorCosID));

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

            string augmentionPositionX = image.TranslationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionPositionY = image.TranslationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionPositionZ = image.TranslationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("X"), augmentionPositionX));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Y"), augmentionPositionY));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Z"), augmentionPositionZ));

            // Rotation
            XMLBlock COSOffsetRotationOffset = new XMLBlock(new XMLTag("RotationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetRotationOffset);

            // TODO get vectors
            string augmentionRotationX = image.RotationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionRotationY = image.RotationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionRotationZ = image.RotationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionRotationW = image.RotationVector.W.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("X"), augmentionRotationX));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("Y"), augmentionRotationY));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("Z"), augmentionRotationZ));
            COSOffsetRotationOffset.AddLine(new XMLLine(new XMLTag("W"), augmentionRotationW));

            // arelGlue.js
            JavaScriptBlock loadContentBlock = new JavaScriptBlock();
            sceneReadyFunktionBlock.AddBlock(loadContentBlock);

            string imageVariable = "image" + imageCount;
            loadContentBlock.AddLine(new JavaScriptLine("var " + imageVariable + " = arel.Object.Model3D.createFromImage(\"" + imageVariable + "\",\"Assets/" + Path.GetFileName(image.ImagePath) + "\")"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setVisibility(" + image.IsVisible.ToString().ToLower() + ")"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setCoordinateSystemID(" + image.Coordinatesystemid + ")"));
            string augmentionScalingX = image.ScalingVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionScalingY = image.ScalingVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentionScalingZ = image.ScalingVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setScale(new arel.Vector3D(" + augmentionScalingX + "," + augmentionScalingY + "," + augmentionScalingZ + "))"));
            loadContentBlock.AddLine(new JavaScriptLine("arel.Scene.addObject(" + imageVariable + ")"));

            ifPatternIsFoundBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + imageVariable + "\").setVisibility(true)"));
            ifPatternIsLostBlock.AddLine(new JavaScriptLine("arel.Scene.getObject(\"" + imageVariable + "\").setVisibility(false)"));

            imageCount++;
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
            throw new NotImplementedException();
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
        /// <summary>   Visits the given <see cref="PictureMarker"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="pictureMarker">    The picture marker. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(PictureMarker pictureMarker)
        {
            string sourcePictureMarkerFile = pictureMarker.ImagePath;
            string destPictureMarkerFile = Path.Combine(project.ProjectPath, Path.GetFileName(sourcePictureMarkerFile));
            if (Directory.Exists(Path.Combine(project.ProjectPath, "Asstes")) && !File.Exists(destPictureMarkerFile))
            {
                File.Copy(sourcePictureMarkerFile, destPictureMarkerFile);
            }

            XMLBlock sensorCOSBlock = new XMLBlock(new XMLTag("SensorCOS"));
            trackingDataFileSensorBlock.AddBlock(sensorCOSBlock);

            sensorCOSBlock.AddLine(new XMLLine(new XMLTag("SensorCosID"), pictureMarker.SensorCosID));

            XMLBlock parameterBlock = new XMLBlock(new XMLTag("Parameters"));
            sensorCOSBlock.AddBlock(parameterBlock);

            parameterBlock.AddLine(new XMLLine(new XMLTag("Size"), pictureMarker.Size.ToString()));

            XMLBlock markerParametersBlock = new XMLBlock(new XMLTag("MarkerParameters"));
            parameterBlock.AddBlock(markerParametersBlock);
            markerParametersBlock.AddLine(new XMLLine(new XMLTag("referenceImage", "qualityThreshold=\"0.70\""), pictureMarker.ImageName));
            string value = pictureMarker.SimilarityThreshold.ToString("F1", CultureInfo.InvariantCulture);
            parameterBlock.AddLine(new XMLLine(new XMLTag("SimilarityThreshold"), value));
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

            sensorCOSBlock.AddLine(new XMLLine(new XMLTag("SensorCosID"), idMarker.SensorCosID));

            // Parameters
            XMLBlock parameterBlock = new XMLBlock(new XMLTag("Parameters"));
            sensorCOSBlock.AddBlock(parameterBlock);

            // MarkerParameters
            XMLBlock markerParametersBlock = new XMLBlock(new XMLTag("MarkerParameters"));
            parameterBlock.AddBlock(markerParametersBlock);

            // Reaktivated when getter is implemented
            markerParametersBlock.AddLine(new XMLLine(new XMLTag("Size"), idMarker.Size.ToString()));
            markerParametersBlock.AddLine(new XMLLine(new XMLTag("MatrixID"), idMarker.MatrixID.ToString()));
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

            // Create [projectName].html
            arelProjectFile = new ARELProjectFile("<!DOCTYPE html>", Path.Combine(project.ProjectPath, "arel" + p.Name + ".html"));

            // head
            arelProjectFileHeadBlock = new XMLBlock(new XMLTag("head"));
            arelProjectFile.AddBlock(arelProjectFileHeadBlock);

                arelProjectFileHeadBlock.AddLine(new XMLLine(new NonTerminatingXMLTag("meta", "charset=\"UTF-8\"")));
                arelProjectFileHeadBlock.AddLine(new XMLLine(new NonTerminatingXMLTag("meta", "name=\"viewport\" content=\"width=device-width, initial-scale=1\"")));
                arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "type=\"text/javascript\" src=\"../arel/arel.js\"")));
                arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "type=\"text/javascript\" src=\"Assets/arelGlue.js\"")));
                arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("title"), p.Name));

            // body
            XMLBlock bodyBlock = new XMLBlock(new XMLTag("body"));
            arelProjectFile.AddBlock(bodyBlock);

            // Prepare TrackinData.xml
            string trackingDataFileName = "TrackingData_" + project.Sensor.Name;
            trackingDataFileName += p.Sensor.SensorSubType != AbstractSensor.SensorSubTypes.None ? p.Sensor.SensorSubType.ToString() : "";
            trackingDataFileName += ".xml";
            trackingDataFile = new TrackingDataFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", project.ProjectPath, trackingDataFileName);

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

            // Create arelConfig.xml
            arelConfigFile = new ARELConfigFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", project.ProjectPath);

                // Results
                XMLBlock resultsBlock = new XMLBlock(new XMLTag("results"));
                arelConfigFile.AddBlock(resultsBlock);

                // Trackingdata
                string trackingdataExtension = "channel=\"0\" poiprefix=\"extpoi-124906-\" url=\"Assets/" + trackingDataFileName +"\" /";
                resultsBlock.AddLine(new XMLLine(new NonTerminatingXMLTag("trackingdata", trackingdataExtension)));
                resultsBlock.AddLine(new XMLLine(new XMLTag("apilevel"), "7"));
                resultsBlock.AddLine(new XMLLine(new XMLTag("arel"), Path.GetFileName(arelProjectFile.FilePath)));

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

        private void Copy(string srcFile, string destDirectory)
        {
            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            string destFile = Path.Combine(destDirectory, Path.GetFileName(srcFile));
            if (!File.Exists(destFile))
            {
                File.Copy(srcFile, Path.Combine(destDirectory, Path.GetFileName(srcFile)));
            }
        }
    }
}
