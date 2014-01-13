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
        public MetaCategory currentMetaCategory { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   The MetaCategory of the over element. </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public MetaCategory overMetaCategory { get; set; }

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="ew">   EditorWindow Instanz. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public PreviewController(EditorWindow ew)
        {
            this.ew = ew;
            this.panel = this.ew.Pnl_editor_preview;
            this.trackable = null;
            this.dic = null;
            this.currentMetaCategory = new MetaCategory();
            this.overMetaCategory = new MetaCategory();
            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (This method is obsolete) adds a preview able. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        /// unimplemented. </exception>
        ///
        /// <param name="p">    The Panel to process. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Obsolete("addPreviewable(IPreviewable p) : eache IPreviewable needs a Vector where the new Previewable should sit in the panel"
            + "you should use addPreviewable(IPreviewable currentElement, Vector3d v) for Augmentations & Trackables", true)]
        public /*File*/void addPreviewAble(IPreviewable p)
             { throw new NotImplementedException(); }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     add Trackable is the method for adding the trackable, each PreviewPanel can holding one
        ///     Trackable.
        /// </summary>
        ///
        /// <param name="currentTrackable"> The current Trackable, which should set in the previewPanel. </param>
        /// <param name="v">                The Vector3D to set the Trackable. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void addPreviewable(IPreviewable currentElement, Vector3D v) 
        {
                if(currentMetaCategory == MetaCategory.Trackable && trackable == null) {        
                    PictureBox tempBox = new PictureBox();
                    tempBox.Location = new Point(v.x, v.y);
                    tempBox.Image = (Image) currentElement.getPreview();            
                    tempBox.Size = currentElement.getPreview().Size;
                    ((AbstractTrackable)currentElement).vector = v;
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
                    ((AbstractAugmentation)currentElement).vector = v;
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

       public void addSource(AbstractSource source, IPreviewable overElement)
       {
           if (currentMetaCategory == MetaCategory.Source && overMetaCategory == MetaCategory.Augmentation)
           {
               if (!trackable.isAugmentionFull() && (trackable.findAugmentation((AbstractAugmentation)overElement) != -1))
               {
                   //fügt neue Source in Trackable ein
                   AbstractAugmentation[] aug = this.trackable.augmentations;
                   aug[trackable.findAugmentation((AbstractAugmentation)overElement)].source = source;
                   this.trackable.augmentations = aug;

                   //search the linked PictureBox out of Dictionary, replace the Tag with the new Augmentation and replace the Picturebox in Panel
                   PictureBox temp;
                   this.dic.TryGetValue(overElement, out temp);
                   this.panel.Controls.Remove(temp);

                   temp.Tag = aug[trackable.findAugmentation((AbstractAugmentation)overElement)];
                   this.dic.Remove(overElement);
                   this.dic.Add((IPreviewable)aug[trackable.findAugmentation((AbstractAugmentation)overElement)], temp);
                   this.panel.Controls.Add(temp);

                   this.dic.TryGetValue((IPreviewable)trackable, out temp);
                   this.panel.Controls.Remove(temp);
                   temp.Tag = trackable;
                   this.dic.Remove(trackable);
                   this.dic.Add((IPreviewable)trackable, temp);

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>  Removes all Elements from the PreviewPanel and clear all lists and dictionarys </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void removeAll() {
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

        public void moveElement(IPreviewable currentElement, Vector3D v)
        {
                 PictureBox temp;
                 this.dic.TryGetValue(currentElement, out temp);
                 this.panel.Controls.Remove(temp);
                 temp.Location = new Point(v.x, v.y);
                 
                 if(currentMetaCategory == MetaCategory.Trackable) {
                         ((AbstractTrackable)temp.Tag).vector = v;
                 }
                 else if (currentMetaCategory == MetaCategory.Augmentation && trackable != null){
                         ((AbstractAugmentation)temp.Tag).vector = v;
                 }
                 
                 this.panel.Controls.Add(temp);
                 this.currentMetaCategory = new MetaCategory();
        }

       ////////////////////////////////////////////////////////////////////////////////////////////////////
       /// <summary>
       ///  Reloads all Variables which we need for the other funktions. this Method is here to open a
       ///  saved Panel.
       /// </summary>
       ///
       /// <remarks>    Lizzard, 1/13/2014. </remarks>
       ///
       /// <param name="p"> The Panel to process. </param>
       ////////////////////////////////////////////////////////////////////////////////////////////////////

       public void reloadPanel(Panel p) {
           this.trackable = null;
           this.dic = new Dictionary<IPreviewable,PictureBox>();

           foreach (Control comp in p.Controls)
           {
               this.dic.Add((IPreviewable)((PictureBox)comp.Tag), (PictureBox)comp);
                if(comp.Tag.GetType() == trackable.GetType()) {
                    trackable = (AbstractTrackable)comp.Tag;
                }
           }
      }

       ////////////////////////////////////////////////////////////////////////////////////////////////////
       /// <summary>    (This method is obsolete) updates the preview panel. </summary>
       ///
       /// <exception cref="NotImplementedException"> Thrown when the requested operation is
       /// unimplemented. </exception>
       ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Obsolete("updatePreviewPanel() : don't need is funktion", true)]
       public void updatePreviewPanel()
       {
           throw new NotImplementedException(); 
       }
 }

