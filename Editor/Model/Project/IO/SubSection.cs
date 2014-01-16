using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit.Model.Project.IO
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A sub section is a <see cref="Section"/> which has a parent <see cref="Section"/>
    ///             and can have 0..* <see cref="SubSection"/>s or <see cref="Line"/>s. </summary>
    ///
    /// <remarks>   Imanuel, 15.01.2014. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class SubSection : Section
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the parent section. </summary>
        ///
        /// <value> The parent section. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Section ParentSection { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Imanuel, 15.01.2014. </remarks>
        ///
        /// <param name="tag">  The tag. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public SubSection(Tag tag) : base(tag) { }

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
    }
}
