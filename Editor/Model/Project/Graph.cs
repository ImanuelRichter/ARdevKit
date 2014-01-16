using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    public abstract class Graph : AbstractDynamic2DAugmention 
    {
        private int maxValue;
        private int minValue;
        private int scaling;
    }
}
