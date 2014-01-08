using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit
{
    class PreviewController
    {

        /// <summary>   The MetaCategory of the current element, which is important for the current method.
        ///             The MetaCategory is set in the event, which use the method. </summary>
        private MetaCategory currentMetaCategory;
        /// <summary>   The Trackable is the element which is the top element so we need the trackable for save the other elements. </summary>
        private IPreviewable trackable;
        /// <summary>   The PreviewPanel which we need to add Previewables. </summary>
        private PreviewPanel panel;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <param name="p">    The PreviewPanel which we need to add Previewables. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public PreviewController(PreviewPanel p) 
        {
            panel = p;
            trackable = null;
            currentMetaCategory = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     add Trackable is the method for adding the trackable, each PreviewPanel can holding one
        ///     Trackable.
        /// </summary>
        ///
        /// <param name="currentTrackable"> The current Trackable, which should set in the previewPanel. </param>
        /// <param name="v">                The Vector3D to set the Trackable. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void addTrackable(IPreviewable currentTrackable, Vector3D v) 
        {
            throw new NotImplementedException();
            //TODO add Trackable to the Previewpanel.
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     add Source or Augmentation, this method can only be used with the element, which is the
        ///     over element by augmentation the overelement is Trackable. by Source the overelement is
        ///     Augmentation.
        /// </summary>
        ///
        /// <param name="currentElement">   The current element. </param>
        /// <param name="overElement">      The over element. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void addPreviewable(IPreviewable currentElement, IPreviewable overElement) 
        {
            throw new NotImplementedException();
            if(currentMetaCategory == Source) {
                //TODO Source add
            }

            else if(currentMetaCategory == Augmentation) {
                //TODO Augmentation add
            }

            else{
                //TODO Throw WindowException Trackable can't be used here.
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Move the Trackable whith all connected augmentations &amp; sources to the new vector-
        ///     position.
        /// </summary>
        ///
        /// <remarks>   Lizzard, 1/8/2014. </remarks>
        ///
        /// <param name="currentTrackable"> The current Trackable, which should set in the previewPanel. </param>
        /// <param name="v">                The Vector3D to move the Trackable. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void moveTrackable(IPreviewable currentTrackable, Vector3D v) 
        {
            throw new NotImplementedException();
            //TODO move Trackable and all augmentations + sources which are connected to the trackable.
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the previewables, when their look was changed. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void updatePreviewables()
        {
            throw new NotImplementedException();
            //TODO !?
        }
    
    
    }
}
