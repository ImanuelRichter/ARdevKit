using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Model.Project;
using System.Collections;
using ARdevKit;



    class PreviewController 
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   MetaCategory is need for some things. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public enum MetaCategory { Source, Augmentation, Trackable};

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   The MetaCategory of the current element. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private MetaCategory currentMetaCategory;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   The MetaCategory of the over element. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private MetaCategory overMetaCategory;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   The Trackable which hold the Augmentations and Sources. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private AbstractTrackable trackable;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   The PreviewPanel which we need to add Previewables. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private Panel panel;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   The Dictionary which hold the PictureBoxes and the Keys of Objekts. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private Dictionary<IPreviewable, PictureBox> dic { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   EditorWindow Instanz </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private EditorWindow ew;
 
        
        public PreviewController(EditorWindow ew)
        {
            this.ew = ew;
            this.panel = this.ew.getPreviewPanel();
            this.trackable = null;
            this.dic = null;
            this.currentMetaCategory = new MetaCategory();
            this.overMetaCategory = new MetaCategory();
            
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
                if(currentMetaCategory == MetaCategory.Trackable && trackable == null) {        
                    PictureBox tempBox = new PictureBox();
                    tempBox.Location = new Point(v.x, v.y);
                    tempBox.Image = (Image) currentElement.getPreview();            
                    tempBox.Size = currentElement.getPreview().Size;
                    tempBox.Tag = (AbstractTrackable) currentElement;
                    
                    panel.Controls.Add(tempBox);
                    trackable = (AbstractTrackable) currentElement;
                    dic.Add(currentElement, tempBox);
                    
                
            
                }
                else if(currentMetaCategory == MetaCategory.Augmentation && trackable != null) {
                    
                    trackable.addAugmentation((AbstractAugmentation) currentElement);
                   
                    PictureBox tempBox = new PictureBox();
           
                    tempBox.Image = (Image) currentElement.getPreview();            
                    tempBox.Size = currentElement.getPreview().Size;
                    tempBox.Location = new Point(v.x, v.y);
                    tempBox.Tag = (AbstractAugmentation) currentElement;
                    
                    this.panel.Controls.Add(tempBox);
                    dic.Add(currentElement, tempBox);
                    trackable.addAugmentation((AbstractAugmentation) currentElement);
                }

                else {
                    //TODO ERROR WINDOW NOT ALLOWED.
                }

                currentMetaCategory = new MetaCategory();
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
            if (currentMetaCategory == MetaCategory.Source && overMetaCategory == MetaCategory.Augmentation)
            {
                if(!trackable.isAugmentionFull() && (trackable.findAugmentation((AbstractAugmentation) overElement) != -1)) {
                    AbstractAugmentation[] aug = this.trackable.augmentations;
                    aug[trackable.findAugmentation((AbstractAugmentation) overElement)].source = (AbstractSource) currentElement;
                    
                    PictureBox temp;
                    this.dic.TryGetValue(currentElement, out temp);
                    this.panel.Controls.Remove(temp);
                    
                    temp.Tag = aug[trackable.findAugmentation((AbstractAugmentation) overElement)];
                    this.dic.Remove(currentElement);
                    this.dic.Add(currentElement, temp);
                    this.panel.Controls.Add(temp); 
                }
            }
            else{
                //TODO Throw WindowException Trackable can't be used here.
            }

            currentMetaCategory = new MetaCategory();
            overMetaCategory = new MetaCategory();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Removes the Previewable and the Objekt, what is linked to the Previewable. </summary>
        ///
        /// <param name="p">    The p control. </param>
        /// <param name="prev"> The previous. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Obsolete]
        public void removePreviewable(IPreviewable currentElement) {
            if(currentMetaCategory == MetaCategory.Trackable && trackable == null) {
                this.removeAll();
            }
            else if(currentMetaCategory == MetaCategory.Augmentation && trackable != null) {
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

             temp.Location = new Point(v.x, v.y);

             this.panel.Controls.Add(temp);
        }
    }

