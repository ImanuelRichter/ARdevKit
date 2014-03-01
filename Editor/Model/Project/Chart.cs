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

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// Describes a <see cref="Chart"/> with its
    /// Colors and OptimalValues. It is a <see cref="Chart"/>.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class Chart : AbstractDynamic2DAugmentation
    {
        /// <summary>
        /// The style used by HighChart.
        /// </summary>
        protected ChartPositioning positioning;
        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        [CategoryAttribute("Position"), ReadOnly(true)]
        public ChartPositioning Positioning
        {
            get { return positioning; }
            set { positioning = value; }
        }

        /// <summary>   Full pathname of the options file. </summary>
        protected string optionsFilePath;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets <see cref="optionsFilePath"/>. </summary>
        ///
        /// <value> The options. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [CategoryAttribute("General"), EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Options
        {
            get { return optionsFilePath; }
            set
            {
                if (System.IO.File.Exists(value))
                {
                    if (File.Helper.FileExists(@"res\", value))
                    {
                        File.Helper.Copy(value, @"tmp\" + ID + "\\");
                        optionsFilePath = Path.GetFullPath(@"tmp\" + ID + "\\" + Path.GetFileName(value));
                    }
                    else
                        optionsFilePath = value;
                }
            }
        }

        [Browsable(false)]
        public new Vector3D Rotation
        {
            get { return base.Rotation; }
            set { base.Rotation = value; }
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
        public override Bitmap getPreview()
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

        /**
         * <summary>    Makes a deep copy of this object. </summary>
         *
         * <remarks>    Robin, 21.01.2014. </remarks>
         *
         * <returns>    A copy of this object. </returns>
         */

        public override object Clone()
        {
            return base.Clone();
        }

        public override bool initElement(EditorWindow ew)
        {
            bool result = base.initElement(ew);
            if (Options == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Application.StartupPath + "\\res\\highcharts";
                openFileDialog.Filter = "js (*.js)|*.js";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Options = openFileDialog.FileName;
                }
                else
                {
                    return false;
                }
            }
            return result;
        }
    }
}
