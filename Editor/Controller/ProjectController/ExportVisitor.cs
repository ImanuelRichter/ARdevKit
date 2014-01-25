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
        /// <summary>   true if exporting for test. </summary>
        private bool exportForTest = false;

        private Project project;
        /// <summary>   Full pathname of the project file. </summary>
        private string projectPath;

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
        private JavaScriptBlock chartFileParseBlock;
        /// <summary>   The chart file parse categories block. </summary>
        private JavaScriptBlock chartFileParseContentBlock;

        /// <summary>   Number of images added to the <see cref="arelGlueFile"/>. </summary>
        private int imageCount = 1;
        /// <summary>   Number of bar charts. </summary>
        private int chartCount = 1;
        /// <summary>   Identifier for the coordinate system. </summary>
        private int coordinateSystemID = 0;

        public ExportVisitor(bool exportForTest)
        {
            this.exportForTest = exportForTest;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Visits the given <see cref="BarChart"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="chart">    The bar graph. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(BarChart chart)
        {
            chart.ID = chart.ID == null ? "chart" + chartCount : chart.ID;
            string chartID = chart.ID;
            string chartObjectString = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(chartID);

            // arel[projectName].html
            if (chartCount == 1)
            {
                arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "src=\"Assets/jquery-2.0.3.js\"")));
                arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "src=\"Assets/highcharts.js\"")));
            }

            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "src=\"Assets/" + chartID + "/chart.js\"")));

            Copy("res\\highcharts\\highcharts.js", Path.Combine(projectPath, "Assets"));
            Copy("res\\jquery\\jquery-2.0.3.js", Path.Combine(projectPath, "Assets"));

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // arelGlue.js
            JavaScriptBlock loadContentBlock = new JavaScriptBlock();
            sceneReadyFunctionBlock.AddBlock(loadContentBlock);

            arelGlueFile.AddBlock(new JavaScriptLine("var " + chartID));

            loadContentBlock.AddLine(new JavaScriptLine(chartID + " = arel.Plugin." + chartObjectString));

            loadContentBlock.AddLine(new JavaScriptLine(chartID + ".create()"));
            loadContentBlock.AddLine(new JavaScriptLine(chartID + ".hide()"));

            // onTracking
            JavaScriptBlock chartIfPatternIsFoundShowBlock = new JavaScriptBlock("if (param[0].getCoordinateSystemID() == " + chartID + ".getCoordinateSystemID())", new BlockMarker("{", "}"));
            ifPatternIsFoundBlock.AddBlock(chartIfPatternIsFoundShowBlock);
            chartIfPatternIsFoundShowBlock.AddLine(new JavaScriptLine(chartID + ".show()"));
            if (chart.PositionRelativeToTrackable)
                chartIfPatternIsFoundShowBlock.AddLine(new JavaScriptLine("arel.Scene.getScreenCoordinatesFrom3DPosition(COS" + coordinateSystemID + "Anchor.getTranslation(), " + chartID + ".getCoordinateSystemID(), function(coord){move(" + chartID + ", coord);})"));

            // onTracking lost
            ifPatternIsLostBlock.AddLine(new JavaScriptLine(chartID + ".hide()"));

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Create chart.js
            ChartFile chartFile = new ChartFile(projectPath, chartID);
            files.Add(chartFile);

            JavaScriptBlock chartFileVariablesBlock = new JavaScriptBlock();

            JavaScriptBlock chartFileDefineBlock = new JavaScriptBlock("arel.Plugin." + chartObjectString + " = ", new BlockMarker("{", "};"));
            chartFile.AddBlock(chartFileDefineBlock);

            // ID
            chartFileDefineBlock.AddLine(new JavaScriptInLine("id : \"" + chartID + "\"", true));
            // CoordinateSystemID
            chartFileDefineBlock.AddLine(new JavaScriptInLine("coordinateSystemID : " + coordinateSystemID, true));
            // Options
            chartFileDefineBlock.AddLine(new JavaScriptInLine("options : {}", true));
            // Translation
            string translationX = chart.TranslationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string translationY = chart.TranslationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string translationZ = chart.TranslationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            chartFileDefineBlock.AddBlock(new JavaScriptInLine("translation : new arel.Vector3D(" + translationX + "," + translationY + "," + translationZ + ")", true));
            // ChartDiv
            chartFileDefineBlock.AddBlock(new JavaScriptInLine("div : document.createElement(\"div\")", true));

            // setOptions
            JavaScriptBlock chartFileDefineSetOptionsBlock = new JavaScriptBlock("setOptions : function(optionsPath)", new BlockMarker("{", "},"));
            chartFileDefineBlock.AddBlock(chartFileDefineSetOptionsBlock);
            chartFileDefineSetOptionsBlock.AddLine(new JavaScriptLine("$.getJSON(optionsPath, function(data) { arel.Plugin." + chartObjectString + ".options = data; })"));

            if (chart.Source == null)
            {
                // Data (data.xml)
                ChartDataFile chartDataFile = new ChartDataFile(projectPath, chartID);
                files.Add(chartDataFile);
                XMLBlock chartDataFileBlock = new XMLBlock(new XMLTag(chartID));
                chartDataFile.AddBlock(chartDataFileBlock);

                XMLBlock chartDataFileCategoriesBlock = new XMLBlock(new XMLTag("categories"));
                chartDataFileBlock.AddBlock(chartDataFileCategoriesBlock);

                for (int i = 0; i < chart.Categories.Length; i++)
                {
                    chartDataFileCategoriesBlock.AddLine(new XMLLine(new XMLTag("item"), chart.Categories[i]));
                }

                // Series
                int n = chart.Data.Count;
                for (int i = 0; i < n; i++)
                {
                    XMLBlock seriesBlock = new XMLBlock(new XMLTag("series"));
                    chartDataFileBlock.AddBlock(seriesBlock);
                    seriesBlock.AddLine(new XMLLine(new XMLTag("name"), chart.Data[i].Name));

                    XMLBlock SeriesColorBlock = new XMLBlock(new XMLTag("color"));
                    seriesBlock.AddBlock(SeriesColorBlock);

                    XMLBlock SeriesColorLinearGradientBlock = new XMLBlock(new XMLTag("linearGradient"));
                    SeriesColorBlock.AddBlock(SeriesColorLinearGradientBlock);
                    SeriesColorLinearGradientBlock.AddLine(new XMLLine(new XMLTag("x1"), "0"));
                    SeriesColorLinearGradientBlock.AddLine(new XMLLine(new XMLTag("x2"), "0"));
                    SeriesColorLinearGradientBlock.AddLine(new XMLLine(new XMLTag("y1"), "0"));
                    SeriesColorLinearGradientBlock.AddLine(new XMLLine(new XMLTag("y2"), "1"));

                    XMLBlock SeriesColorStopsBlock = new XMLBlock(new XMLTag("stops"));
                    SeriesColorBlock.AddBlock(SeriesColorStopsBlock);
                    SeriesColorStopsBlock.AddLine(new XMLLine(new XMLTag("zero"), ColorTranslator.ToHtml(chart.Data[i].MaxValueColor)));
                    SeriesColorStopsBlock.AddLine(new XMLLine(new XMLTag("one"), ColorTranslator.ToHtml(chart.Data[i].MinValueColor)));

                    XMLBlock dataBlock = new XMLBlock(new XMLTag("data"));
                    seriesBlock.AddBlock(dataBlock);
                    string points = "";
                    int m = chart.Data[i].DataSet.Length;
                    for (int j = 0; j < m - 1; j++)
                        points += chart.Data[i].DataSet[j].ToString(CultureInfo.InvariantCulture) + ",";
                    points += chart.Data[i].DataSet[m - 1].ToString(CultureInfo.InvariantCulture);
                    dataBlock.AddLine(new XMLLine(new XMLTag("points"), points));
                }
            }

            // Options options.json
            ChartOptionsFile chartOptionsFile = new ChartOptionsFile(projectPath, chartID);
            files.Add(chartOptionsFile);

            if (chart.UseOptions)
                chartOptionsFile.AddBlock(new JavaScriptInLine(chart.Options, false));
            else
            {
                JavaScriptBlock chartFileHighchartsOptionsBlock = new JavaScriptBlock("options : ", new BlockMarker("{", "},"));
                chartFileDefineBlock.AddBlock(chartFileHighchartsOptionsBlock);
                JavaScriptBlock chartFileHighchartChartBlock = new JavaScriptBlock("chart: ", new BlockMarker("{", "},"));
                chartFileHighchartsOptionsBlock.AddBlock(chartFileHighchartChartBlock);
                chartFileHighchartChartBlock.AddLine(new JavaScriptInLine("type: 'column'", false));

                if (chart.Title != "")
                {
                    JavaScriptBlock chartFileHighchartTitleBlock = new JavaScriptBlock("title: ", new BlockMarker("{", "},"));
                    chartFileHighchartsOptionsBlock.AddBlock(chartFileHighchartTitleBlock);
                    chartFileHighchartTitleBlock.AddLine(new JavaScriptInLine("text: '" + chart.Title + "'", false));
                }

                if (chart.Subtitle != "")
                {
                    JavaScriptBlock chartFileHighchartSubTitleBlock = new JavaScriptBlock("subtitle: ", new BlockMarker("{", "},"));
                    chartFileHighchartsOptionsBlock.AddBlock(chartFileHighchartSubTitleBlock);
                    chartFileHighchartSubTitleBlock.AddLine(new JavaScriptInLine("text: '" + chart.Subtitle + "'", false));
                }

                JavaScriptBlock chartFileHighchartXAxisBlock = new JavaScriptBlock("xAxis: ", new BlockMarker("{", "},"));
                chartFileHighchartsOptionsBlock.AddBlock(chartFileHighchartXAxisBlock);

                if (chart.XAxisTitle != "")
                {
                    JavaScriptBlock chartFileHighchartXAxisTitleBlock = new JavaScriptBlock("title: ", new BlockMarker("{", "},"));
                    chartFileHighchartXAxisBlock.AddBlock(chartFileHighchartXAxisTitleBlock);
                    chartFileHighchartXAxisTitleBlock.AddLine(new JavaScriptInLine("text: '" + chart.XAxisTitle + "'", false));
                }

                JavaScriptBlock chartFileHighchartXAxisCategoriesBlock = new JavaScriptBlock("categories: ", new BlockMarker("[", "]"));
                chartFileHighchartXAxisBlock.AddBlock(chartFileHighchartXAxisCategoriesBlock);

                JavaScriptBlock chartFileHighchartYAxisBlock = new JavaScriptBlock("yAxis: ", new BlockMarker("{", "},"));
                chartFileHighchartsOptionsBlock.AddBlock(chartFileHighchartYAxisBlock);

                chartFileHighchartYAxisBlock.AddLine(new JavaScriptInLine("min: " + chart.MinValue, true));
                if (chart.YAxisTitle != "")
                {
                    JavaScriptBlock chartFileHighchartYAxisTitleBlock = new JavaScriptBlock("title: ", new BlockMarker("{", "}"));
                    chartFileHighchartYAxisBlock.AddBlock(chartFileHighchartYAxisTitleBlock);
                    chartFileHighchartYAxisTitleBlock.AddLine(new JavaScriptInLine("text: '" + chart.YAxisTitle + "'", false));
                }

                // TODO tooltip
                JavaScriptBlock chartFileHighchartTooltipBlock = new JavaScriptBlock("tooltip: ", new BlockMarker("{", "},"));
                chartFileHighchartsOptionsBlock.AddBlock(chartFileHighchartTooltipBlock);

                chartFileHighchartTooltipBlock.AddLine(new JavaScriptInLine("headerFormat: '<span style=\"font-size:10px\">{point.key}</span><table>'", true));
                chartFileHighchartTooltipBlock.AddLine(new JavaScriptInLine("pointFormat: '<tr><td style=\"color:{series.color};padding:0\">{series.name}: </td>' +\n" +
                        "'<td style=\"padding:0\"><b>{point.y:.1f} mm</b></td></tr>'", true));
                chartFileHighchartTooltipBlock.AddLine(new JavaScriptInLine("footerFormat: '</table>'", true));
                chartFileHighchartTooltipBlock.AddLine(new JavaScriptInLine("shared: true", true));
                chartFileHighchartTooltipBlock.AddLine(new JavaScriptInLine("useHTML: true", false));

                JavaScriptBlock chartFileHighchartPlotOptions = new JavaScriptBlock("plotOptions: ", new BlockMarker("{", "},"));
                chartFileHighchartsOptionsBlock.AddBlock(chartFileHighchartPlotOptions);

                JavaScriptBlock chartFileHighchartPlotOptionsColumn = new JavaScriptBlock("column: ", new BlockMarker("{", "}"));
                chartFileHighchartPlotOptions.AddBlock(chartFileHighchartPlotOptionsColumn);
                chartFileHighchartPlotOptionsColumn.AddLine(new JavaScriptInLine("pointPadding: " + chart.PointPadding.ToString(CultureInfo.InvariantCulture), true));
                chartFileHighchartPlotOptionsColumn.AddLine(new JavaScriptInLine("borderWidth: " + chart.BorderWidth, false));

                JavaScriptBlock chartFileHighchartSeriesBlock = new JavaScriptBlock("series: ", new BlockMarker("[", "]"));
                chartFileHighchartsOptionsBlock.AddBlock(chartFileHighchartSeriesBlock);
            }

            // Create
            // Div
            chartFileCreateBlock = new JavaScriptBlock("create : function()", new BlockMarker("{", "},"));
            chartFileDefineBlock.AddBlock(chartFileCreateBlock);
            chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.setAttribute(\"id\", this.id)"));
            chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.position = \"" + chart.Style.Position + "\""));

            if (!chart.PositionRelativeToTrackable)
            {
                if (chart.Style.Top > 0)
                    chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.top = \"" + chart.Style.Top + "px\""));
                if (chart.Style.Left > 0)
                    chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.left = \"" + chart.Style.Left + "px\""));
                if (chart.Style.Bottom > 0)
                    chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.bottom = \"" + chart.Style.Bottom + "px\""));
                if (chart.Style.Right > 0)
                    chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.right = \"" + chart.Style.Right + "px\""));
            }

            chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.width = \"" + chart.Width + "px\""));
            chartFileCreateBlock.AddLine(new JavaScriptLine("this.div.style.height = \"" + chart.Height + "px\""));
            chartFileCreateBlock.AddLine(new JavaScriptLine("document.documentElement.appendChild(this.div)"));
            chartFileCreateBlock.AddLine(new JavaScriptLine("this.setOptions('Assets/" + chartID + "/options.json')"));

            // Parse xml
            chartFileParseBlock = new JavaScriptBlock();
            chartFileCreateBlock.AddBlock(chartFileParseBlock);
            chartFileParseContentBlock = new JavaScriptBlock();
            chartFileParseBlock.AddBlock(chartFileParseContentBlock);
            if (chart.Source == null)
            {
                chartFileParseBlock.Update("$.get('Assets/" + chartID + "/data.xml', function(xml)", new BlockMarker("{", "});"));
                chartFileParseBlock.AddLine(new JavaScriptLine("var $xml = $(xml)"));

                // Add categories
                JavaScriptBlock chartFileParseCategoriesBlock = chartFileParseContentBlock;
                chartFileParseCategoriesBlock.Update("$xml.find('categories item').each(function(i, category)", new BlockMarker("{", "});"));
                chartFileParseBlock.AddBlock(chartFileParseCategoriesBlock);
                chartFileParseCategoriesBlock.AddLine(new JavaScriptLine("arel.Plugin." + chartObjectString + ".options.xAxis.categories.push($(category).text())"));

                // Add series
                JavaScriptBlock chartFileParseSeriesBlock = new JavaScriptBlock("$xml.find('series').each(function(i, series)", new BlockMarker("{", "});"));
                chartFileParseBlock.AddBlock(chartFileParseSeriesBlock);

                // Series options
                JavaScriptBlock chartFileParseSeriesOptionsBlock = new JavaScriptBlock("var seriesOptions =", new BlockMarker("{", "};"));
                chartFileParseSeriesBlock.AddBlock(chartFileParseSeriesOptionsBlock);

                // Name
                chartFileParseSeriesOptionsBlock.AddLine(new JavaScriptInLine("name: $(series).find('name').text()", true));

                // Color
                JavaScriptBlock chartFileParseSeriesOptionsColorBlock = new JavaScriptBlock("color : ", new BlockMarker("{", "},"));
                chartFileParseSeriesOptionsBlock.AddBlock(chartFileParseSeriesOptionsColorBlock);
                chartFileParseSeriesOptionsColorBlock.AddBlock(new JavaScriptInLine("linearGradient: { x1: $(series).find('x1').text(), x2: $(series).find('x2').text(), y1: $(series).find('y1').text(), y1: $(series).find('y2').text() }", true));
                JavaScriptBlock chartFileParseSeriesOptionsColorStopsBlock = new JavaScriptBlock("stops: ", new BlockMarker("[", "]"));
                chartFileParseSeriesOptionsColorBlock.AddBlock(chartFileParseSeriesOptionsColorStopsBlock);
                chartFileParseSeriesOptionsColorStopsBlock.AddLine(new JavaScriptInLine("[0, $(series).find('stops zero').text()]", true));
                chartFileParseSeriesOptionsColorStopsBlock.AddLine(new JavaScriptInLine("[1, $(series).find('stops one').text()]", false));

                // Data
                chartFileParseSeriesOptionsBlock.AddBlock(new JavaScriptInLine("data: []", false));

                // Series data
                chartFileParseSeriesBlock.AddBlock(new JavaScriptLine("var points = $(series).find('points').text().split(',')"));
                JavaScriptBlock chartFileParseSeriesDataBlock = new JavaScriptBlock("$.each(points, function(i, point)", new BlockMarker("{", "});"));
                chartFileParseSeriesBlock.AddBlock(chartFileParseSeriesDataBlock);
                chartFileParseSeriesDataBlock.AddLine(new JavaScriptLine("seriesOptions.data.push(parseInt(point))"));

                // Add it to options
                chartFileParseSeriesBlock.AddBlock(new JavaScriptLine("arel.Plugin." + chartObjectString + ".options.series.push(seriesOptions)"));
            }

            // Create the chart
            chartFileParseBlock.AddBlock(new JavaScriptLine("var chart = $('#' + arel.Plugin." + chartObjectString + ".id).highcharts(arel.Plugin." + chartObjectString + ".options)"));

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
        /// <summary>   Visits the given <see cref="ImageAugmentation"/>. </summary>
        ///
        /// <remarks>   Imanuel, 17.01.2014. </remarks>
        ///
        /// <param name="image">    The image. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Visit(ImageAugmentation image)
        {
            // Copy to projectPath
            Copy(image.ImagePath, Path.Combine(projectPath, "Assets"));

            // arelGlue.js
            JavaScriptBlock loadContentBlock = new JavaScriptBlock();
            sceneReadyFunctionBlock.AddBlock(loadContentBlock);

            string imageVariable = "image" + imageCount;
            loadContentBlock.AddLine(new JavaScriptLine("var " + imageVariable + " = arel.Object.Model3D.createFromImage(\"" + imageVariable + "\",\"Assets/" + Path.GetFileName(image.ImagePath) + "\")"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setVisibility(" + image.IsVisible.ToString().ToLower() + ")"));
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setCoordinateSystemID(" + coordinateSystemID + ")"));
            string augmentationTranslationX = image.TranslationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationTranslationY = image.TranslationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationTranslationZ = image.TranslationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setTranslation(new arel.Vector3D(" + augmentationTranslationX + "," + augmentationTranslationY + "," + augmentationTranslationZ + "))"));
            string augmentationRotationX = image.RotationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationY = image.RotationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationZ = image.RotationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationW = image.RotationVector.W.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setRotation(new arel.Rotation(" + augmentationRotationX + "," + augmentationRotationY + "," + augmentationRotationZ + "," + augmentationRotationW + "))"));
            string augmentationScalingX = image.ScalingVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationScalingY = image.ScalingVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationScalingZ = image.ScalingVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            loadContentBlock.AddLine(new JavaScriptLine(imageVariable + ".setScale(new arel.Vector3D(" + augmentationScalingX + "," + augmentationScalingY + "," + augmentationScalingZ + "))"));
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
            string sourceFileName = Path.GetFileName(source.SourceFilePath);
            string chartID = source.Augmentation.ID;
            if (source.SourceFilePath != null && source.SourceFilePath != "")
            {
                Copy(source.SourceFilePath, Path.Combine(projectPath, "Assets", chartID));
                string newSourceFilePath = Path.Combine(projectPath, "Assets", chartID, sourceFileName);
                source.SourceFilePath = newSourceFilePath;

                chartFileParseBlock.Update("$.get('Assets/" + chartID + "/" + sourceFileName + "', function(xml)", new BlockMarker("{", "});"));
            }
            else
                chartFileParseBlock.AddLine(new JavaScriptLine("alert('no source file defined')"));
            if (source.QueryFilePath != null && source.QueryFilePath != "")
            {
                string[] lines = File.OpenText(source.QueryFilePath).ReadToEnd().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                string newQueryFilePath = Path.Combine(projectPath, "Assets", chartID, "query.js");
                StreamWriter queryWriter = new StreamWriter(newQueryFilePath);
                foreach (string line in lines)
                {
                    queryWriter.WriteLine(line);
                    chartFileParseBlock.AddLine(new JavaScriptInLine(line, false));
                }
                queryWriter.Close();
                source.QueryFilePath = newQueryFilePath;
            }
            else
                chartFileParseBlock.AddLine(new JavaScriptLine("alert('no query defined')"));
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
            // Copy the file
            Copy(pictureMarker.ImagePath, Path.Combine(projectPath, "Assets"));

            string sourcePictureMarkerFile = pictureMarker.ImagePath;
            string destPictureMarkerFile;
            destPictureMarkerFile = Path.Combine(projectPath, Path.GetFileName(sourcePictureMarkerFile));
            if (Directory.Exists(Path.Combine(projectPath, "Asstes")) && !File.Exists(destPictureMarkerFile))
                File.Copy(sourcePictureMarkerFile, destPictureMarkerFile);

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
            markerParametersBlock.AddLine(new XMLLine(new XMLTag("referenceImage", "qualityThreshold=\"0.70\""), Path.GetFileName(pictureMarker.ImagePath)));
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

            string augmentationPositionX = pictureMarker.TranslationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationPositionY = pictureMarker.TranslationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationPositionZ = pictureMarker.TranslationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("X"), augmentationPositionX));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Y"), augmentationPositionY));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Z"), augmentationPositionZ));

            // Rotation
            XMLBlock COSOffsetRotationOffset = new XMLBlock(new XMLTag("RotationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetRotationOffset);

            string augmentationRotationX = pictureMarker.RotationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationY = pictureMarker.RotationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationZ = pictureMarker.RotationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            string augmentationRotationW = pictureMarker.RotationVector.W.ToString("F1", CultureInfo.InvariantCulture);
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
            string anchorPath = Path.Combine(projectPath, "Assets", "anchor.png");
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

            string trackablePositionX = idMarker.TranslationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string trackablePositionY = idMarker.TranslationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string trackablePositionZ = idMarker.TranslationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("X"), trackablePositionX));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Y"), trackablePositionY));
            COSOffsetTranslationOffset.AddLine(new XMLLine(new XMLTag("Z"), trackablePositionZ));

            // Rotation
            XMLBlock COSOffsetRotationOffset = new XMLBlock(new XMLTag("RotationOffset"));
            COSOffsetBlock.AddBlock(COSOffsetRotationOffset);

            string trackableRotationX = idMarker.RotationVector.X.ToString("F1", CultureInfo.InvariantCulture);
            string trackableRotationY = idMarker.RotationVector.Y.ToString("F1", CultureInfo.InvariantCulture);
            string trackableRotationZ = idMarker.RotationVector.Z.ToString("F1", CultureInfo.InvariantCulture);
            string trackableRotationW = idMarker.RotationVector.W.ToString("F1", CultureInfo.InvariantCulture);
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
            string anchorPath = Path.Combine(projectPath, "Assets", "anchor.png");
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
            if (exportForTest)
                projectPath = Path.Combine(Application.StartupPath, "currentProject");
            else
                projectPath = p.ProjectPath;

            // Create [projectName].html
            ARELProjectFile arelProjectFile = new ARELProjectFile("<!DOCTYPE html>", Path.Combine(projectPath, "arel" + (p.Name != "" ? p.Name : "Test") + ".html"));
            files.Add(arelProjectFile);

            // head
            arelProjectFileHeadBlock = new XMLBlock(new XMLTag("head"));
            arelProjectFile.AddBlock(arelProjectFileHeadBlock);

            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("title"), p.Name != "" ? p.Name : "Test"));

            arelProjectFileHeadBlock.AddLine(new XMLLine(new NonTerminatingXMLTag("meta", "charset=\"UTF-8\"")));
            arelProjectFileHeadBlock.AddLine(new XMLLine(new NonTerminatingXMLTag("meta", "name=\"viewport\" content=\"width=device-width, initial-scale=1\"")));

            arelProjectFileHeadBlock.AddLine(new XMLLine(new XMLTag("script", "type=\"text/javascript\" src=\"../arel/arel.js\"")));
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
            trackingDataFile = new TrackingDataFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", projectPath, trackingDataFileName);
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
            ARELConfigFile arelConfigFile = new ARELConfigFile("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", projectPath);
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
            arelGlueFile = new ARELGlueFile(projectPath);
            files.Add(arelGlueFile);

            JavaScriptBlock sceneReadyBlock = new JavaScriptBlock("arel.sceneReady", new BlockMarker("(", ");"));
            arelGlueFile.AddBlock(sceneReadyBlock);
            sceneReadyFunctionBlock = new JavaScriptBlock("function()", new BlockMarker("{", "}"));
            sceneReadyBlock.AddBlock(sceneReadyFunctionBlock);

            // Console log ready
            sceneReadyFunctionBlock.AddLine(new JavaScriptLine("console.log(\"sceneReady\")"));

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
            ifPatternIsFoundBlock.AddLine(new JavaScriptLine("console.log(\"Tracking is active\")"));

            // Pattern lost
            ifPatternIsLostBlock = new JavaScriptBlock("else if(type && type == arel.Events.Scene.ONTRACKING && param[0].getState() == arel.Tracking.STATE_NOTTRACKING)", new BlockMarker("{", "}"));
            ifTrackingInformationAvailiableBlock.AddBlock(ifPatternIsLostBlock);
            ifPatternIsLostBlock.AddLine(new JavaScriptLine("console.log(\"Tracking lost\")"));

            // Move
            JavaScriptBlock arelGlueMoveBlock = new JavaScriptBlock("function move(object, coord)", new BlockMarker("{", "};"));
            arelGlueFile.AddBlock(arelGlueMoveBlock);
            arelGlueMoveBlock.AddLine(new JavaScriptLine("var left = (coord.getX() - parseInt(object.div.style.width) / 2) + object.translation.getX()"));
            arelGlueMoveBlock.AddLine(new JavaScriptLine("var top = (coord.getY() - parseInt(object.div.style.height) / 2) + object.translation.getY()"));
            arelGlueMoveBlock.AddLine(new JavaScriptLine("object.div.style.left = left + 'px'"));
            arelGlueMoveBlock.AddLine(new JavaScriptLine("object.div.style.top = top + 'px'"));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Copies a passed file to the passed directory. </summary>
        ///
        /// <remarks>   Imanuel, 19.01.2014. </remarks>
        ///
        /// <param name="srcFile">          Source file. </param>
        /// <param name="destDirectory">    Pathname of the destination directory. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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
