using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ARdevKit.Model.Project
{
    abstract class Graph : AbstractDynamic2DAugmentation 
    {
        private int maxValue;
        private int minValue;
        private int scaling;
    }
}
