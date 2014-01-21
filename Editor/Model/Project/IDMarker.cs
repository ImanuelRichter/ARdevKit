using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.ComponentModel;

namespace ARdevKit.Model.Project
{

    /// <summary>
    /// <see cref="IDMarker"/> is a <see cref="AbstractMarker"/>
    /// adding an matrixID.
    /// </summary>
    [Serializable]
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class IDMarker : AbstractMarker
    {
        /// <summary>
        /// The matrix identifier, describes the Markers, which
        /// are deployed by the metaio SDK.
        /// They reach from 1 to 255.
        /// </summary>
        private int matrixID;
        /// <summary>
        /// Gets or sets the matrix identifier.
        /// </summary>
        /// <value>
        /// The matrix identifier.
        /// </value>
        [CategoryAttribute("General")]
        public int MatrixID
        {
            get { return matrixID; }
            set { matrixID = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IDMarker"/> class.
        /// </summary>
        /// <param name="matrixID">The matrix identifier.</param>
        public IDMarker(int matrixID) : base("IDMarker", 60)
        {
            this.matrixID = matrixID;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns a new MarkerFuser. </summary>
        ///
        /// <remarks>   Imanuel, 20.01.2014. </remarks>
        ///
        /// <exception cref="NotImplementedException">  Thrown when the requested operation is
        ///                                             unimplemented. </exception>
        ///
        /// <returns>   The fuser. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override MarkerFuser getFuser()
        {
            return new MarkerFuser();
        }


        /// <summary>
        /// An method, to accept a <see cref="AbstractProjectVisitor" />
        /// and let the visitor visit the associated fuser.
        /// </summary>
        /// <param name="visitor">the visitor which encapsulates the action
        /// which is performed on this element</param>
        public override void Accept(Controller.ProjectController.AbstractProjectVisitor visitor)
        {
            visitor.Visit(this);
            foreach (AbstractAugmentation augmentation in Augmentations)
            {
                augmentation.Accept(visitor);
            }
        }

        /// <summary>
        /// Gets the preview.
        /// </summary>
        /// <returns>
        /// a representative Bitmap
        /// </returns>
        public override Bitmap getPreview()
        {
            return Properties.Resources.ARRMarker_normal_;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <returns>
        /// a representative iconized Bitmap
        /// </returns>
        public override Bitmap getIcon()
        {
            return Properties.Resources.ARRMarker_small_;
        }
    }
}
