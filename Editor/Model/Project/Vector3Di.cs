using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    public class Vector3Di : Vector3D
    {
        private int w;
        public int W
        {
            get { return w; }
            set { w = value; }
        }

        public Vector3Di(int x, int y, int z, int w) : base(x, y, z)
        {
            this.w = w;
        }
    }
}
