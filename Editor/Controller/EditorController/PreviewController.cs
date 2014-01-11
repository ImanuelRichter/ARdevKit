using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARdevKit
{
    class PreviewController
    {

        /// <summary>   The MetaCategory of the current element </summary>
        private MetaCategory currentMetaCategory;
        /// <summary>   The MetaCategory of the over element. </summary>
        private MetaCategory overMetaCategory;
        /// <summary>   The Trackable is the element which is the top element so we need the trackable for save the other elements. </summary>
        private AbstractTrackable trackable;
        /// <summary>   The PreviewPanel which we need to add Previewables. </summary>
        private Panel panel;
        /// <summary>   The vector of the Trackable. </summary>
        private Vector3D vector;


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <param name="p">    The Panel which we need to add Previewables. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public PreviewController()
        {
            panel = this.getPreviewPanel();
            trackable = null;
            currentMetaCategory = null;
            overMetaCategory = null;
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
        
        [Obsolete]
        public void addPreviewable(IPreviewable currentTrackable, Vector3D v) 
        {
            if(currentMetaCategory == Trackable) {
                trackable = currentTrackable;
                vector = v;
            
            PictureBox tempBox = new PictureBox;
            tempBox.Location = new Point(vector.getX(), vector.getY());
            tempBox.Image = (Image) currentTrackable.getPreview();            
            tempBox.Size = currentTrackable.getPreview().Size;
            panel.Add(tempBox);
            }

            FlowLayoutPanel
            else() {
                //TODO ERROR WINDOW NOT ALLOWED.
            }
            
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

         [Obsolete]
        public void addPreviewable(IPreviewable currentElement, IPreviewable overElement) 
        {
            throw new NotImplementedException();
            if(currentMetaCategory == Source && overMetaCategory == Augmentation) {
                if()
            }

            else if(currentMetaCategory == Augmentation && overMetaCategory == Trackable) {
                if((AbstractTrackable)currentElement == trackable) {
             //TODO add remove methode schreiben.
                    trackable.addList((AbstractAugmentation)currentElement);

                }
                else {
                    
                }
            }

            else{
                //TODO Throw WindowException Trackable can't be used here.
            }
        }


        public void removePreviewable(PictureBox p, IPreviewable prev)
        {
            if (currentMetaCategory == Source) {
                panel.Controls.Remove(p);
                // AbstractAugmentation[3] aug = trackable.getArray();
                for (int i = 0; i < 3; i++)
                {
                    if(aug[i].exists(prev)) {
           //CHANGE METHODNAMES
                    aug[i].remove(prev);
                    }
                    tracka
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Move the Trackable whith all connected augmentations &amp; sources to the new vector-
        ///     position.
        /// </summary>
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
