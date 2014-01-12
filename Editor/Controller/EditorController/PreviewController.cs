using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Model.Project;
using System.Collections;
{
    class PreviewController
    {

        /// <summary>   The MetaCategory of the current element </summary>
        private MetaCategory currentMetaCategory;
        /// <summary>   The MetaCategory of the over element. </summary>
        private MetaCategory overMetaCategory;
        private AbstractTrackable trackable;
        /// <summary>   The PreviewPanel which we need to add Previewables. </summary>
        private Panel panel;
        private Dictionary<IPreviewable, PictureBox> dic;
        
        public PreviewController()
        {
            panel = this.getPreviewPanel();
            trackable = null;
            dic = null;
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
        public void addPreviewable(IPreviewable currentElement, Vector3D v) 
        {
                if(currentMetaCategory == Trackable && trackable == null) {        
                    PictureBox tempBox = new PictureBox;
                    tempBox.Location = new Point(v.getX(), v.getY());
                    tempBox.Image = (Image) currentElement.getPreview();            
                    tempBox.Size = currentElement.getPreview().Size;
                    tempBox.Tag = (AbstractTrackable) currentElement;
                    
                    panel.Controls.Add(tempBox);
                    trackable = (AbstractTrackable) currentElement;
                    dic.Add(currentElement, tempBox);
                    
                
            
                }
                else if(currentMetaCategory == Augmentation && trackable != null) {
                    
                    trackable.addAugmentation((AbstractAugmentation) currentElement);
                   
                    PictureBox tempBox = new PictureBox;
           
                    tempBox.Image = (Image) currentElement.getPreview();            
                    tempBox.Size = currentElement.getPreview().Size;
                    tempBox.Location = new Point(v.getX(), v.getY());
                    tempBox.Tag = (AbstractAugmentation) currentElement;
                    
                    this.panel.Controls.Add(tempBox);
                    dic.Add(currentElement, tempBox);
                    trackable.addAugmentation((AbstractAugmentation) currentElement);
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
        public void addSource(IPreviewable currentElement, IPreviewable overElement) 
        {
            if(currentMetaCategory == Source && overMetaCategory == Augmentation) {
                if(!trackable.isFull() && (trackable.find(currentElement) != -1) {
                    AbstractAugmentation aug[] = this.trackable.getAugmentation();
                    aug[trackable.findAugmentation(overElement)].setSource = (AbstractSource) currentElement;
                    
                    PictureBox temp;
                    this.dic.TryGetValue(currentElement, out temp);
                    this.panel.Controls.Remove(temp);
                    
                    temp.Tag = aug[trackable.findAugmentation(overElement)];
                    this.dic.Remove(currentElement);
                    this.dic.Add(currentElement, temp);
                    this.panel.Controls.Add(temp); 
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
        public void removePreviewable(IPreviewable currentElement) {
            if(currentMetaCategory == Trackable && trackable == null) {
                this.removeAll();
            }
            else if(currentMetaCategory == Augmentation && trackable != null) {
                this.trackable.removeAugmentation((AbstractAugmentation) currentElement);
                
                PictureBox temp;
                this.dic.TryGetValue(currentElement, out temp);
                
                this.panel.Controls.Remove(temp);
                this.dic.Remove(currentElement);
            }
            else{
                //TODO Messagebox
            }
        }

        [Obsolete]
        private void removeAll(){
            this.panel.Controls.Clear();
            this.dic.Clear();
            this.trackable = null;
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

        public void moveTrackable(IPreviewable currentElement, Vector3D v)
        {
             PictureBox temp;
             this.dic.TryGetValue(currentElement, out temp);

             this.panel.Controls.Remove(temp);

             temp.Location.X = v.getX();
             temp.Location.Y = v.getY();

             this.panel.Controls.Add(temp);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the previewables, when their look was changed. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void updatePreviewables()
        {
                //TODO
        }
    }
}
