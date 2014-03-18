using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.IO;
using System.Drawing.Design;
using System.Windows.Forms;
using ARdevKit.Model.Project.File;

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// Describes a <see cref="Chart"/> which is programmed in JavaScript and display a set of 
    /// data which can either be from a file or a database.
    /// It inherits from <see cref="AbstractDynamic2DAugmentation"/>.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class Chart : AbstractDynamic2DAugmentation
    {
        /// <summary>
        /// True if the chart should be recalculated after tracking is lost.
        /// </summary>
        private bool forceRecalculation;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Chart" /> should be recalculated after tracking is lost.
        /// </summary>
        /// <value>
        ///   <c>true</c> if recalculate; otherwise, <c>false</c>.
        /// </value>
        [CategoryAttribute("General")]
        public bool ForceRecalculation { get; set; }

        /// <summary>
        /// The positioning of the Chart
        /// </summary>
        private ChartPositioning positioning;
        /// <summary>
        /// Gets or sets the positioning of the Chart
        /// </summary>
        /// <value>
        /// The positioning.
        /// </value>
        [CategoryAttribute("Position"), ReadOnly(true)]
        public ChartPositioning Positioning
        {
            get { return positioning; }
            set { positioning = value; }
        }

        /// <summary>   Full pathname of the options file. </summary>
        protected string optionsFilePath;

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        [CategoryAttribute("General"), EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Options
        {
            get { return optionsFilePath; }
            set { optionsFilePath = value; }
        }

        /// <summary>
        /// gets or sets the Vector
        /// </summary>
        [Browsable(false)]
        public new Vector3D Rotation
        {
            get { return base.Rotation; }
            set { base.Rotation = value; }
        }

        [Browsable(false)]
        public new string ResFilePath
        {
            get { return base.ResFilePath; }
            set { base.ResFilePath = value; }
        }

        /// <summary>   Default constructor. </summary>
        public Chart()
        {
            Positioning = new ChartPositioning(ChartPositioning.PositioningModes.RELATIVE);
            optionsFilePath = null;
            Width = 200;
            Height = 200;
            Scaling = new Vector3D(0, 0, 0);
        }

        /// <summary>   Gets or sets the scaling. </summary>
        ///
        /// <value> The scaling. </value>
        [Browsable(false)]
        public new Vector3D Scaling
        {
            get { return base.Scaling; }
            set { base.Scaling = value; }
        }


        /// <summary>
        /// An overwriting method, to accept a <see cref="AbstractProjectVisitor" />
        /// which must be implemented according to the visitor design pattern.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        ///     which is performed on this <see cref="Chart"/></param>
        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            base.Accept(visitor);
            visitor.Visit(this);
            if (Source != null)
                Source.Accept(visitor);
        }


        /// <summary>
        /// returns a <see cref="Bitmap" /> in order to be displayed
        /// on the PreviewPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns>
        /// a representative Bitmap
        /// </returns>
        public override Bitmap getPreview(string projectPath)
        {
            return Properties.Resources.highcharts_normal_;
        }


        /// <summary>
        /// returns a <see cref="Bitmap" /> in order to be displayed
        /// on the ElementSelectionPanel, implements <see cref="IPreviewable" />
        /// </summary>
        /// <returns>
        /// a representative iconized Bitmap
        /// </returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.highcharts_normal_;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Clean up (remove created/copied files and directories). </summary>
        ///
        /// <remarks>   Imanuel, 31.01.2014. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void CleanUp()
        {
            string dir = Path.GetDirectoryName(optionsFilePath);
            if (Directory.Exists(dir) && System.IO.File.Exists(Path.Combine(dir, "chart.js")))
                Directory.Delete(dir, true);
        }

        /// <summary>
        /// This method is called by the previewController when a new instance of the element is added to the Scene. It sets "must-have" properties.
        /// </summary>
        /// <param name="ew">The ew.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public override bool initElement(EditorWindow ew)
        {
            bool result = base.initElement(ew);
            if (Options == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\res\\highcharts";
                openFileDialog.Filter = "js (*.js)|*.js";
                openFileDialog.Title = "Wählen sie eine Options Datei";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string newPath = Path.Combine(Environment.CurrentDirectory, "tmp", id);
                    Helper.Copy(openFileDialog.FileName, newPath, "options.js");
                    Options = Path.Combine(newPath, "options.js");
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string newPath = Path.Combine(Environment.CurrentDirectory, "tmp", id);
                Helper.Copy(Options, newPath, "options.js");
                Options = Path.Combine(newPath, "options.js");
            }
            return result;
        }
    }
}
