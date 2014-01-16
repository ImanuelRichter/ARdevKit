using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public abstract class AbstractMarker : AbstractTrackable
    {
        protected string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        protected int size;
        public int Size
        {
            get { return size; }
            set { size = value; }
        }
    }
}
