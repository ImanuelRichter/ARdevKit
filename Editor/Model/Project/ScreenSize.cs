using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    class ScreenSize
    {

        private int height;

        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                OnSizeChanged(this, EventArgs.Empty);
            }
        }

        private int width;

        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                OnSizeChanged(this, EventArgs.Empty);
            }
        }

        private EventHandler sizeChanged;

        [Browsable(false)]
        public EventHandler SizeChanged
        {
            get { return sizeChanged; }
            set { sizeChanged = value; }
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            EventHandler eh = sizeChanged;
            if (eh != null)
                eh(sender, e);
        }
    }
}
