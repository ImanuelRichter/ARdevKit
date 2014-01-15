using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.File
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A line is a <see cref="SubSection"/> which can have a value or not. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Line : SubSection
    {
        /// <summary>   The value. </summary>
        private string value = "";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="tag">  The tag. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Line(Tag tag) : base(tag) { }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="tag">      The tag. </param>
        /// <param name="value">    The value. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Line(Tag tag, string value) : base(tag)
        {
            this.value = value;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes itself the given writer. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="writer">   The writer to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Write(System.IO.StreamWriter writer)
        {
            string tabs = "";
            for (int i = 0; i < Level; i++)
            {
                tabs += "\t";
            }
            writer.WriteLine(tabs + tag + value + tag);
        }
    }
}
