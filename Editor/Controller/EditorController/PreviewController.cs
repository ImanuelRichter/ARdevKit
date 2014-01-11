using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Model.Project;
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
        /// <summary>   The Dictionary to hold the Pictureboxes and the IPreviewable, which belongs to the Picturebox. </summary>
        private Dictionary<PictureBox, IPreviewable> dic;
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
            dic = new Dictionary<PictureBox, IPreviewable>;
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
                if(currentMetaCategory == Trackable && trackable == null) {
                    trackable = (AbstractTrackable)currentTrackable;
                    vector = v;
            
                PictureBox tempBox = new PictureBox;
                tempBox.Location = new Point(v.getX(), v.getY());
                tempBox.Image = (Image) currentTrackable.getPreview();            
                tempBox.Size = currentTrackable.getPreview().Size;
                panel.Controls.Add(tempBox);
            
                dic.Add(tempBox, currentTrackable);
                }
                else {
                    //TODO ERROR WINDOW NOT ALLOWED.
                }

                currentMetaCategory = null;
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
            if(currentMetaCategory == Source && overMetaCategory == Augmentation) {
                AbstractAugmentation[] aug = trackable.getList();
                
                for(int i = 0; i < 3; i++) {
                    if(aug[i] == (AbstractAugmentation) overElement && aug[i].getSource() == null) {
                        aug[i].add((AbstractSource)currentElement);
                        
                        PictureBox tempBox = new PictureBox;
          // location berechnung!              tempBox.Location = new Point(vector.getX() + , vector.getY());
                        tempBox.Image = (Image) currentElement.getPreview();            
                        tempBox.Size = currentElement.getPreview().Size;
                        panel.Controls.Add(tempBox);
                        
                        dic.Add(tempBox, currentElement);
                        break;
                     }
                
                }
            }

            else if(currentMetaCategory == Augmentation && overMetaCategory == Trackable) {
                if((AbstractTrackable)currentElement == trackable) {
                   trackable.addList((AbstractAugmentation)currentElement);

                }
                else {
                    
                }
            }

            else{
                //TODO Throw WindowException Trackable can't be used here.
            }
            currentMetaCategory = null;
            overMetaCategory = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Removes the Previewable and the Objekt, what is linked to the Previewable. </summary>
        ///
        /// <param name="p">    The p control. </param>
        /// <param name="prev"> The previous. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Obsolete]
        public void removePreviewable(PictureBox p)
        {
            if(dic.ContainsKey(p)) {
                dic.Remove(p);
                //TODO REMOVE SUBOBJEKTS
            }
            else {
                throw new NotImplementedException;   
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

        public void moveTrackable(PictureBox p, Vector3D v)
        {
            if(this.isTrackable(p)) {
                vector = v;
                IPreviewable temp = null;
                if(this.dic.TryGetValue(p, out temp)) {
                    dic.Remove(p);
                    p.Location = new Point(v.getX(), v.getY());
                    dic.Add(p, temp);
                }

            }
            else{
                //Throw new Exception other Objekts than Trackables can't move without trackable.
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the previewables, when their look was changed. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void updatePreviewables()
        {
            throw new NotImplementedException();
            //TODO !?
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests if the linked value is the trackable. </summary>
        ///
        /// <param name="p">    The p control. </param>
        ///
        /// <returns>   true if trackable, false if not. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool isTrackable(PictureBox p) {
            if(dic.ContainsKey(p)){
                IPreviewable temp = null;
                this.dic.TryGetValue(p, out temp);
                if((trackable)temp = trackable) {
                    return true;
                }
                else{
                    return false;
                }
            }
            else {
                return false;
            }
        }


    }
}
