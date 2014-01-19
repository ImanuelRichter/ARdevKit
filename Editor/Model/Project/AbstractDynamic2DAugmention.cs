using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project
{
    [Serializable]
    public abstract class AbstractDynamic2DAugmention : Abstract2DAugmention
    {
        /// <summary>
        /// New Variable which is for link a Source with this Augmentation
        /// </summary>
        public AbstractSource source { get; set; }
    }
}
