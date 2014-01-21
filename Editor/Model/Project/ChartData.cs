using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ARdevKit.Model.Project
{
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public abstract class ChartData
    {
        /// <summary>   The name of corresponding to the dataset. </summary>
        protected string name;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [CategoryAttribute("General")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>   The data. </summary>
        protected double[] dataSet;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the data. </summary>
        ///
        /// <value> The data. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public double[] DataSet
        {
            get { return dataSet; }
            set { dataSet = value; }
        }
    }
}
