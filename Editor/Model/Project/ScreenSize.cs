using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    /// <summary>
    /// This class models the ScreenSize.
    /// </summary>
    /// <remarks>geht 26.01.2014 20:15</remarks>
    [Serializable]
    public class ScreenSize
    {
        /// <summary>
        /// The width
        /// </summary>
        /// <remarks>geht 26.01.2014 20:16</remarks>
        private int width;

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        /// <remarks>geht 26.01.2014 20:16</remarks>
        [Category("Screengröße"), Browsable(true)]
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                OnSizeChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// The height
        /// </summary>
        /// <remarks>geht 26.01.2014 20:16</remarks>
        private int height;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        /// <remarks>geht 26.01.2014 20:16</remarks>
        [Category("Screengröße"), Browsable(true)]
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                OnSizeChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// event handler for changed size
        /// </summary>
        /// <remarks>geht 26.01.2014 20:16</remarks>
        [field: NonSerializedAttribute]
        private EventHandler sizeChanged;

        /// <summary>
        /// Gets or sets the sizeChanged event handler.
        /// </summary>
        /// <value>
        /// The size changed.
        /// </value>
        /// <remarks>geht 26.01.2014 20:16</remarks>
        [Browsable(false)]
        public EventHandler SizeChanged
        {
            get { return sizeChanged; }
            set { sizeChanged = value; }
        }

        /// <summary>
        /// Called when [size changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>geht 26.01.2014 20:18</remarks>
        private void OnSizeChanged(object sender, EventArgs e)
        {
            EventHandler eh = sizeChanged;
            if (eh != null)
                eh(sender, e);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenSize"/> class.
        /// default constructor.
        /// </summary>
        /// <remarks>geht 26.01.2014 20:18</remarks>
        public ScreenSize() { }
    }
}
