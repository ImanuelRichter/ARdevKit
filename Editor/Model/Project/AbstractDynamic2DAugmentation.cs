using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public abstract class AbstractDynamic2DAugmentation : AbstractAugmentation
    {
        private int height;
        private int width;
    }
}
