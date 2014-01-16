using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.IO
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A section has a <see cref="Tag"/> and a parent <see cref="AbstractFile"/>.
    ///             It can have 0..* <see cref="SubSection"/>s or <see cref="Line"/>s. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Section
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the level. </summary>
        ///
        /// <value> The level. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Level { get; set; }

        /// <summary>   The tag. </summary>
        protected Tag tag;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the parent file. </summary>
        ///
        /// <value> The parent file. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public AbstractFile ParentFile { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the sub sections. </summary>
        ///
        /// <value> The sub sections. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected List<SubSection> SubSections { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the lines. </summary>
        ///
        /// <value> The lines. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected List<Line> Lines { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="tag">  The tag. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Section(Tag tag)
        {
            this.tag = tag;
            Level = 0;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a sub section. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="css">  The CSS. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddSubSection(SubSection css)
        {
            SubSections = SubSections == null ? new List<SubSection>() : SubSections;
            css.ParentSection = this;
            css.Level = Level + 1;
            SubSections.Add(css);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a line. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="cln">  The cln. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddLine(Line cln)
        {
            Lines = Lines == null ? new List<Line>() : Lines;
            cln.ParentSection = this;
            cln.Level = Level + 1;
            Lines.Add(cln);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes with the given writer. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="writer">   The writer to write. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public virtual void Write(System.IO.StreamWriter writer)
        {
            string tabs = "";
            for (int i = 0; i < Level; i++)
            {
                tabs += "\t";
            }
            writer.WriteLine(tabs + tag);
            if (Lines != null)
            {
                foreach (Line cln in Lines)
                {
                    cln.Write(writer);
                }
            }
            if (SubSections != null)
            {
                foreach (SubSection css in SubSections)
                {
                    css.Write(writer);
                }
            }
            writer.WriteLine(tabs + tag);
        }
    }
}
