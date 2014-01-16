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

public class PreviewController
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   MetaCategory is need for some things. </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum MetaCategory {Augmentation, Trackable };

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   The MetaCategory of the current element. </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public MetaCategory currentMetaCategory { get; set; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   The Trackable which hold the Augmentations and Sources. </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public AbstractTrackable trackable { get; set; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   The PreviewPanel which we need to add Previewables. </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private Panel panel;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   EditorWindow Instanz </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private EditorWindow ew;

    /// <summary>   The Index which Trackable out of Project we musst use </summary>
    public int index;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Constructor. </summary>
    ///
    /// <param name="ew">   EditorWindow Instanz. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////


    public PreviewController(EditorWindow ew)
    {
        this.ew = ew;
        this.panel = this.ew.Pnl_editor_preview;
        this.currentMetaCategory = new MetaCategory();
        this.index = 0;
        this.trackable = null;
        this.ew.project.Trackables.Add(trackable);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   (This method is obsolete) adds a preview able. </summary>
    ///
    /// <exception cref="NotImplementedException"> Thrown when the requested operation is
    /// unimplemented. </exception>
    ///
    /// <param name="p">    The Panel to process. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Obsolete("addPreviewable(IPreviewable p) : eache IPreviewable needs a Vector where the new" 
                + "Previewable should sit in the panel you should use addPreviewable(IPreviewable"
                + "currentElement, Vector3d v) for Augmentations & Trackables", true)]
    public void addPreviewAble(IPreviewable p)
    { throw new NotImplementedException(); }


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     add Trackable is the method for adding the trackable, each PreviewPanel can holding one
    ///     Trackable.
    /// </summary>
    /// 
    /// <summary>   currentMetaCategory musst set to Trackable/Augmentation</summary>
    ///
    /// <param name="currentTrackable"> The current Trackable, which should set in the previewPanel. </param>
    /// <param name="v">                The Vector3D to set the Trackable. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public void addPreviewable(IPreviewable currentElement, Vector3D v)
    {
        if (currentMetaCategory == MetaCategory.Trackable && trackable == null)
        {                                                                           
            PictureBox tempBox = new PictureBox();                                  
            tempBox.Location = new Point(v.X, v.Y);
            tempBox.Image = (Image)currentElement.getPreview();
            tempBox.Size = new Size(currentElement.getPreview().Height / 4, currentElement.getPreview().Width / 4);
            tempBox.SizeMode = PictureBoxSizeMode.StretchImage;

            //set the vector to the trackable
            ((AbstractTrackable)currentElement).vector = v;                                         

            //set references
            this.ew.project.Trackables[index] = (AbstractTrackable)currentElement;
            tempBox.Tag = this.ew.project.Trackables[index];
            this.trackable = (AbstractTrackable)currentElement;
        }

        else if (currentMetaCategory == MetaCategory.Augmentation && trackable != null)
        {

            PictureBox tempBox = new PictureBox();                          
            tempBox.Image = (Image)currentElement.getPreview();
            tempBox.Size = new Size(currentElement.getPreview().Height / 4, currentElement.getPreview().Width / 4); 
            tempBox.SizeMode = PictureBoxSizeMode.StretchImage;
            tempBox.Location = new Point(v.X, v.Y);

            //set the vector and the trackable in augmentation
            ((AbstractAugmentation)currentElement).vector = v;                                      
            ((AbstractAugmentation)currentElement).trackable = this.trackable;

            //set references 
            trackable.augmentations.Add((AbstractAugmentation)currentElement);                      
            tempBox.Tag = this.trackable.findAugmentation((AbstractAugmentation)currentElement);    
            
            this.panel.Controls.Add(tempBox);
            
            //set the new box to the front
            this.findBox(currentElement).BringToFront();
        }

        else
        {
            MessageBox.Show("More than one Trackable & three Augmentations are not allowed!");
        }
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     add Source or Augmentation, this method can only be used with the element, which is the
    ///     over element by augmentation the overelement is Trackable. by Source the overelement is
    ///     Augmentation.
    /// </summary>
    /// 
    /// <summary>   currentMetaCategory musst set to Augmentation</summary>
    ///
    /// <param name="currentElement">   The current element. </param>
    /// <param name="overElement">      The over element. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void addSource(AbstractSource source, IPreviewable currentElement)
    {
        if (currentMetaCategory == MetaCategory.Augmentation)
        {
            if (this.trackable != null && trackable.existAugmentation((AbstractAugmentation)currentElement))
            {
                //set reference to the augmentions in Source
                source.augmentions.Add((AbstractDynamic2DAugmentation)currentElement);
                
                //add references in Augmentation, Picturebox + project.sources List.
                ((AbstractAugmentation)this.findBox((AbstractAugmentation)currentElement).Tag).source = source;
                this.ew.project.Sources.Add(((AbstractAugmentation)this.findBox((AbstractAugmentation)currentElement).Tag).source);
            }
        }
        else
        {
            MessageBox.Show("ERROR in addSource()");
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Removes the choosen Source out of the Augmentation and also out of the sourcesList in
    ///     Project.
    /// </summary>
    ///
    /// <summary>   currentMetaCategory musst set to Augmentation </summary>
    /// 
    /// <remarks>   Lizzard, 1/15/2014. </remarks>
    ///
    /// <param name="source">           Source for the. </param>
    /// <param name="currentElement">   The current element. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void removeSource(AbstractSource source, IPreviewable currentElement) {
        if (currentMetaCategory == MetaCategory.Augmentation)
        {
            if (this.ew.project.findSource(source).augmentions.Count > 1)
            {
                ((AbstractAugmentation)currentElement).source = null;
                this.ew.project.findSource(source).augmentions.Remove((AbstractDynamic2DAugmentation)currentElement);
            }
            else if (this.ew.project.findSource(source).augmentions.Count == 1)
            {
                ((AbstractAugmentation)currentElement).source = null;
                this.ew.project.Sources.Remove(source);
            }       
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Removes the Previewable and the Objekt, what is linked to the Previewable. </summary>
    /// 
    /// <summary>   currentMetaCategory musst set to Trackable/Augmentation</summary>
    /// 
    /// <remarks>   Lizzard, 1/16/2014. </remarks>
    ///
    /// <param name="currentElement">   The current element. </param>
    ///
    /// ### <param name="p">    The p control. </param>
    /// ### <param name="prev"> The previous. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void removePreviewable(IPreviewable currentElement)
    {
        if (currentMetaCategory == MetaCategory.Trackable && trackable != null)
        {
            this.removeAll();
        }
        else if (currentMetaCategory == MetaCategory.Augmentation && trackable != null)
        {
            this.trackable.augmentations.Remove((AbstractAugmentation)currentElement);

            this.panel.Controls.Remove(this.findBox((AbstractAugmentation)currentElement));
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>  Removes all Elements from the PreviewPanel and clear all lists and dictionarys </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    private void removeAll()
    {
        this.panel.Controls.Clear();
        this.trackable = null;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Move the Trackable whith all connected augmentations &amp; sources to the new vector-
    ///     position.
    /// </summary>
    ///
    /// <summary>   currentMetaCategory musst set to Trackable/Augmentation</summary>
    ///
    /// <param name="currentTrackable"> The current Trackable, which should set in the previewPanel. </param>
    /// <param name="v">                The Vector3D to move the Trackable. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void moveElement(IPreviewable currentElement, Vector3D v)
    {
        if (currentMetaCategory == MetaCategory.Trackable && trackable != null)
        {
            ((AbstractTrackable)this.findBox(currentElement).Tag).vector = v;
            this.findBox(currentElement).Location = new Point(v.X, v.Y);
        }
        else if (currentMetaCategory == MetaCategory.Augmentation && trackable != null)
        {
            ((AbstractAugmentation)this.findBox(currentElement).Tag).vector = v;
            this.findBox(currentElement).Location = new Point(v.X, v.Y);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>    (This method is obsolete) updates the preview panel. </summary>
    ///
    /// <exception cref="NotImplementedException"> Thrown when the requested operation is
    /// unimplemented. </exception>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void updatePreviewPanel()
    {
            this.panel.Controls.Clear();
            this.ew.project.Trackables.Add(trackable);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   This Reload funktion is here to load a other Trackable out of the Project. </summary>
    ///
    /// <remarks>   Lizzard, 1/15/2014. </remarks>
    ///
    /// <param name="index">    The Index which Trackable out of Project we musst use. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void reloadPreviewPanel(int index)
    {
        //if it's the same Scene do nothing
        if (index == this.index)
        {
            MessageBox.Show("You've choosen the same Scene");
        }
        //if it's a scene which exists reload scene
        else if (index < this.ew.project.Trackables.Count)
        {
            MessageBox.Show("Scene No. " + (index + 1) + " will be load");
            
            this.index = index;
            this.trackable = this.ew.project.Trackables[index];
            this.panel.Controls.Clear();
            if (trackable != null)
            {
                this.addAllToPanel(this.ew.project.Trackables[index]);
            }
        }
        //if the scene is new create a new empty scene
        else if (index >= this.ew.project.Trackables.Count)
        {
            MessageBox.Show("You've choosen a new Scene! gl & hf");
            this.index = index;
            this.trackable = null;
            this.panel.Controls.Clear();
            this.ew.project.Trackables.Add(trackable);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Add all existent Objects of the trackable in the Panel, this funktion is exists for change
    ///     the trackable.
    /// </summary>
    ///
    /// <remarks>   Lizzard, 1/15/2014. </remarks>
    ///
    /// <param name="trackable">    The Trackable which hold the Augmentations and Sources. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    private void addAllToPanel(AbstractTrackable trackable)
    {
        PictureBox tempBox;
        if (trackable.augmentations.Count > 0)
        {
            foreach( AbstractAugmentation aug in trackable.augmentations)
            {              
                    tempBox = new PictureBox();
                    tempBox.Tag = aug;
                    tempBox.Location = new Point(aug.vector.x, aug.vector.y);
                    tempBox.Image = (Image)aug.getPreview();
                    tempBox.Size = new Size(aug.getPreview().Height / 4, aug.getPreview().Width / 4);
                    tempBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.panel.Controls.Add(tempBox);
            }
        }

        tempBox = new PictureBox();
        tempBox.Tag = trackable;
        tempBox.Location = new Point(trackable.vector.X, trackable.vector.Y);
        tempBox.Image = (Image)trackable.getPreview();
        tempBox.Size = new Size(trackable.getPreview().Height / 4, trackable.getPreview().Width / 4);
        tempBox.SizeMode = PictureBoxSizeMode.StretchImage;
        this.panel.Controls.Add(tempBox);

        
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Searchs in the Panel for the important PictureBox and gives this box back. </summary>
    ///
    /// <param name="prev"> The previous. </param>
    ///
    /// <returns>   The found box. </returns>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    private PictureBox findBox(IPreviewable prev)
    {
        if (currentMetaCategory == MetaCategory.Trackable)
        {
            foreach (Control comp in panel.Controls)
            {
                if (comp.Tag == (AbstractTrackable)prev)
                {
                    return (PictureBox)comp;
                }
            }
        }
        else if (currentMetaCategory == MetaCategory.Augmentation)
        {
            foreach (Control comp in panel.Controls)
            {
                if (comp.Tag == (AbstractAugmentation)prev)
                {
                    return (PictureBox)comp;
                }
            }
        }
        return null;
    }

}

