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
using ARdevKit.Controller.EditorController;
using ARdevKit.View;
using ARdevKit.Properties;

public class PreviewController
{
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

    public AbstractAugmentation copy { get; set; }

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
        this.ew.Tsm_editor_menu_edit_paste.Click += new System.EventHandler(this.paste_augmentation_center);
        this.ew.Tsm_editor_menu_edit_copie.Click += new System.EventHandler(this.copy_augmentation);
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

            Vector3D center = new Vector3D(0, 0, 0);
            center.Y = panel.Size.Height / 2;
            center.X = panel.Size.Width / 2;
            //ask the user for the picture (if the trackable is a picturemarker)
            if (currentElement.GetType() == typeof(PictureMarker))
            {
                OpenFileDialog openTestImageDialog = new OpenFileDialog();
                openTestImageDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|PPM Files (*.ppm)|*.ppm|PGM Files (*.pgm)|*.pgm";
                if (openTestImageDialog.ShowDialog() == DialogResult.OK)
                {
                    ((PictureMarker)currentElement).ImagePath = openTestImageDialog.FileName;

                    //set the vector to the trackable
                    ((AbstractTrackable)currentElement).vector = center;
                    this.trackable = (AbstractTrackable)currentElement;
                    this.ew.project.Trackables[index] = (AbstractTrackable)currentElement;
                    this.addPictureBox(currentElement, center);
                    if (this.ew.project.isTrackable())
                    {
                        this.ew.ElementSelectionController.setElementEnable(typeof(IDMarker), false);
                        this.ew.project.Sensor = new MarkerSensor();
                    }
                    setCurrentElement(currentElement);
                    ew.PropertyGrid1.SelectedObject = currentElement;
                }
            }
            else
            {
                //set the vector to the trackable
                ((AbstractTrackable)currentElement).vector = center;
                this.trackable = (AbstractTrackable)currentElement;
                this.ew.project.Trackables[index] = (AbstractTrackable)currentElement;
                this.addPictureBox(currentElement, center);
                if (this.ew.project.isTrackable())
                {
                    this.ew.ElementSelectionController.setElementEnable(typeof(PictureMarker), false);
                    this.ew.project.Sensor = new MarkerSensor();
                }
                setCurrentElement(currentElement);
                ew.PropertyGrid1.SelectedObject = currentElement;
            }
            
        }
        else if (currentMetaCategory == MetaCategory.Augmentation && trackable != null && this.ew.project.Trackables[index].Augmentations.Count < 3)
        {
            if (currentElement.GetType() == typeof(ImageAugmentation))
            {
                OpenFileDialog openTestImageDialog = new OpenFileDialog();
                openTestImageDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|PPM Files (*.ppm)|*.ppm|PGM Files (*.pgm)|*.pgm";
                if (openTestImageDialog.ShowDialog() == DialogResult.OK)
                {
                    ((ImageAugmentation)currentElement).ImagePath = openTestImageDialog.FileName;
                    //set the vector and the trackable in <see cref="AbstractAugmentation"/>
                    ((AbstractAugmentation)currentElement).TranslationVector = v;
                    ((AbstractAugmentation)currentElement).Trackable = this.trackable;

                    //set references 
                    AbstractAugmentation augmentation = (AbstractAugmentation)currentElement;
                    trackable.Augmentations.Add(augmentation);

                    this.addPictureBox(currentElement, v);

                    //set the new box to the front
                    this.findBox(currentElement).BringToFront();
                }
            }
            else
            {
                //set the vector and the trackable in <see cref="AbstractAugmentation"/>
                ((AbstractAugmentation)currentElement).TranslationVector = v;
                ((AbstractAugmentation)currentElement).Trackable = this.trackable;

                //set references 
                AbstractAugmentation augmentation = (AbstractAugmentation)currentElement;
                trackable.Augmentations.Add(augmentation);

                this.addPictureBox(currentElement, v);

                //set the new box to the front
                this.findBox(currentElement).BringToFront();
            }
            setCurrentElement(currentElement);
            ew.PropertyGrid1.SelectedObject = currentElement;
        }
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     add Source or augmentation, this method can only be used with the element, which is the
    ///     over element by augmentation the overelement is Trackable. by Source the overelement is
    ///     augmentation.
    /// </summary>
    ///
    /// <summary>   currentMetaCategory musst set to augmentation</summary>
    ///
    /// <param name="currentElement">   The current element. </param>
    /// <param name="overElement">      The over element. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public void addSource(AbstractSource source, AbstractAugmentation currentElement)
    {
        if (currentMetaCategory == MetaCategory.Source && typeof(AbstractDynamic2DAugmentation).IsAssignableFrom(currentElement.GetType()))
        {
            if (this.trackable != null && trackable.existAugmentation((AbstractAugmentation)currentElement)
                && ((AbstractDynamic2DAugmentation)currentElement).source == null)
            {
                //set reference to the augmentations in Source
                source.Augmentations.Add((Abstract2DAugmentation)currentElement);

                //add references in Augmentation, Picturebox + project.sources List.
                ((AbstractDynamic2DAugmentation)currentElement).source = source;
                this.ew.project.Sources.Add(((AbstractDynamic2DAugmentation)this.findBox((AbstractAugmentation)currentElement).Tag).source);

                this.setSourcePreview(currentElement);
            }
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

    public void removeSource(AbstractSource source, IPreviewable currentElement)
    {
        if (currentMetaCategory == MetaCategory.Augmentation)
        {
            if (this.ew.project.findSource(source).Augmentations.Count > 1)
            {
                ((AbstractDynamic2DAugmentation)currentElement).source = null;
                this.ew.project.findSource(source).Augmentations.Remove((Abstract2DAugmentation)currentElement);
            }
            else if (this.ew.project.findSource(source).Augmentations.Count == 1)
            {
                ((AbstractDynamic2DAugmentation)currentElement).source = null;
                this.ew.project.Sources.Remove(source);
            }
            this.findBox(currentElement).Image = currentElement.getPreview();
            this.findBox(currentElement).Refresh();
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
        if (typeof(AbstractTrackable).IsAssignableFrom(currentElement.GetType()) && trackable != null)
        {
            this.removeAll();
            if (!this.ew.project.isTrackable())
            {
                this.ew.ElementSelectionController.setElementEnable(typeof(PictureMarker), true);
                this.ew.ElementSelectionController.setElementEnable(typeof(IDMarker), true);
            }
        }
        else if (typeof(AbstractAugmentation).IsAssignableFrom(currentElement.GetType()) && trackable != null)
        {
            this.trackable.Augmentations.Remove((AbstractAugmentation)currentElement);
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
        this.ew.project.Trackables[index] = null;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Move the Trackable whith all connected Augmentations &amp; sources to the new vector-
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
            ((AbstractAugmentation)this.findBox(currentElement).Tag).TranslationVector = v;
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
        ContextMenu cm = new ContextMenu();
        cm.MenuItems.Add("einfügen", new EventHandler(this.paste_augmentation));
        cm.MenuItems[0].Enabled = false;
        this.panel.ContextMenu = cm;
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
        { }
        //if it's a scene which exists reload scene
        else if (index < this.ew.project.Trackables.Count)
        {

            this.index = index;
            this.trackable = this.ew.project.Trackables[index];
            this.panel.Controls.Clear();
            if (trackable != null)
            {
                this.addAllToPanel(this.ew.project.Trackables[index]);
            }
            if (this.trackable != null && trackable.GetType() == typeof(IDMarker))
            {
                this.ew.ElementSelectionController.setElementEnable(typeof(PictureMarker), false);
            }
            else if (this.trackable != null && trackable.GetType() == typeof(PictureMarker))
            {
                this.ew.ElementSelectionController.setElementEnable(typeof(IDMarker), false);
            }
        }
        //if the scene is new create a new empty scene
        else if (index >= this.ew.project.Trackables.Count)
        {
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
        if (trackable.Augmentations.Count > 0)
        {
            foreach (AbstractAugmentation aug in trackable.Augmentations)
            {
                this.addPictureBox(aug, aug.TranslationVector);
                if (typeof(AbstractDynamic2DAugmentation).IsAssignableFrom(aug.GetType()) && ((AbstractDynamic2DAugmentation)aug).source != null)
                {
                    this.setSourcePreview(aug);
                }
            }
        }
        this.addPictureBox(trackable, trackable.vector);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Adds a PictureBox with for the currentElement to the aktuell Scene. </summary>
    ///
    /// <remarks>   Lizzard, 1/17/2014. </remarks>
    ///
    /// <param name="prev">     The previous. </param>
    /// <param name="vector">   The vector. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    private void addPictureBox(IPreviewable prev, Vector3D vector)
    {
        PictureBox tempBox;
        tempBox = new PictureBox();
        tempBox.Location = new Point(vector.X - prev.getPreview().Width / 8, vector.Y - prev.getPreview().Height / 8);
        tempBox.Image = (Image)prev.getPreview();
        tempBox.Size = new Size(prev.getPreview().Width / 4, prev.getPreview().Height / 4);
        tempBox.SizeMode = PictureBoxSizeMode.StretchImage;
        tempBox.Tag = prev;
        ContextMenu cm = new ContextMenu();

        //adds drag&drop events for augmentations so that sources can be droped on them
        if (typeof(AbstractAugmentation).IsAssignableFrom(prev.GetType()))
        {
            ((Control)tempBox).AllowDrop = true;
            DragEventHandler enterHandler = new DragEventHandler(onAugmentationEnter);
            DragEventHandler dropHandler = new DragEventHandler(onAugmentationDrop);
            tempBox.DragEnter += enterHandler;
            tempBox.DragDrop += dropHandler;
            cm.MenuItems.Add("kopieren", new EventHandler(this.copy_augmentation));
        }
        tempBox.MouseClick += new MouseEventHandler(selectElement);
        cm.MenuItems.Add("löschen", new EventHandler(this.remove_by_click));
        cm.Tag = prev;
        cm.Popup += new EventHandler(this.popupContextMenu);
        tempBox.ContextMenu = cm;
        

        if (tempBox.Tag is AbstractAugmentation)
            tempBox.MouseMove += new MouseEventHandler(controlMouseMove);

        this.panel.Controls.Add(tempBox);

    }

    private void popupContextMenu(object sender, EventArgs e)
    {
            this.setCurrentElement((IPreviewable)((ContextMenu)sender).Tag);
    }

    /**
     * <summary>    Raises the drag event when a source enters a augmentation. </summary>
     *
     * <remarks>    Robin, 19.01.2014. </remarks>
     *
     * <param name="sender">    Source of the event. </param>
     * <param name="e">         Event information to send to registered event handlers. </param>
     */

    public void onAugmentationEnter(object sender, DragEventArgs e)
    {
        if (currentMetaCategory == MetaCategory.Source)
        {
            e.Effect = DragDropEffects.Move;
        }
    }

    /**
     * <summary>    Raises the drag event when a source is droped on an augmentation. </summary>
     *
     * <remarks>    Robin, 19.01.2014. </remarks>
     *
     * <param name="sender">    Source of the event. </param>
     * <param name="e">         Event information to send to registered event handlers. </param>
     */

    public void onAugmentationDrop(object sender, DragEventArgs e)
    {
        if (currentMetaCategory == MetaCategory.Source)
        {
            ElementIcon icon = (ElementIcon)e.Data.GetData(typeof(ElementIcon));
            AbstractAugmentation augmentation = (AbstractAugmentation)((PictureBox)sender).Tag;
            AbstractSource source = (AbstractSource)icon.Element.Prototype.Clone();
            addSource(source, augmentation);
        }
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
        if (typeof(AbstractTrackable).IsAssignableFrom(prev.GetType()))
        {
            foreach (Control comp in panel.Controls)
            {
                if (comp.Tag == (AbstractTrackable)prev)
                {
                    return (PictureBox)comp;
                }
            }
        }
        else if (typeof(AbstractAugmentation).IsAssignableFrom(prev.GetType()))
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

    /// <summary>   Select element (Event). </summary>
    ///
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Mouse event information. </param>

    private void selectElement(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ew.PropertyGrid1.SelectedObject = ((Control)sender).Tag;
            this.setCurrentElement((IPreviewable)((Control)sender).Tag);
        }
    }

    /// <summary>   
    /// Event to move a object of type Control. 
    /// Also updates x/y coord in the Tag of the control.            
    /// </summary>
    ///
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Mouse event information. </param>
    private void controlMouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button == System.Windows.Forms.MouseButtons.Left)
        {
            Control controlToMove = (Control)sender;
            controlToMove.BringToFront();
            controlToMove.Location = new Point(controlToMove.Location.X + e.Location.X - 60,
               controlToMove.Location.Y + e.Location.Y - 60);

            if (((Control)sender).Tag is AbstractAugmentation)
            {
                AbstractAugmentation aa;
                aa = (AbstractAugmentation)((Control)sender).Tag;
                aa.TranslationVector.X = controlToMove.Location.X + e.Location.X;
                aa.TranslationVector.Y = controlToMove.Location.Y + e.Location.Y;
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Event handler. removes the current object. </summary>
    ///
    /// <remarks>   Lizzard, 1/19/2014. </remarks>
    ///
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Event information. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    private void remove_by_click(object sender, EventArgs e)
    {
        IPreviewable temp = (IPreviewable)((ContextMenu)((MenuItem)sender).Parent).Tag;
        this.removePreviewable((IPreviewable)((ContextMenu)((MenuItem)sender).Parent).Tag);
        ew.PropertyGrid1.SelectedObject = null;
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Event handler. Shows Source in PropertyGrid when you want this. </summary>
    ///
    /// <remarks>   Lizzard, 1/19/2014. </remarks>
    ///
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Event information. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    private void show_source_by_click(object sender, EventArgs e)
    {
        ew.PropertyGrid1.SelectedObject = ((AbstractDynamic2DAugmentation)((ContextMenu)((MenuItem)sender).Parent).Tag).source;
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Event handler. Removes the source of the augmentation and the contextmenuentries of this
    ///     augmentation.
    /// </summary>
    ///
    /// <remarks>   Lizzard, 1/19/2014. </remarks>
    ///
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Event information. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    private void remove_source_by_click(object sender, EventArgs e)
    {
        AbstractDynamic2DAugmentation temp = (AbstractDynamic2DAugmentation)((ContextMenu)((MenuItem)sender).Parent).Tag;
        MetaCategory tempMeta = currentMetaCategory;
        this.currentMetaCategory = MetaCategory.Augmentation;
        this.removeSource(temp.source, temp);
        ew.PropertyGrid1.SelectedObject = null;
        this.currentMetaCategory = tempMeta;

        this.findBox(temp).ContextMenu.MenuItems.RemoveAt(2);
        this.findBox(temp).ContextMenu.MenuItems.RemoveAt(2);

    }

    /// <summary>
    /// EventHandler for copy function. copies the currentElement
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    public void copy_augmentation(object sender, EventArgs e)
    {
        if (typeof(AbstractAugmentation).IsAssignableFrom(this.ew.CurrentElement.GetType()))
        {
            this.copy = (AbstractAugmentation)this.ew.CurrentElement.Clone();
            this.panel.ContextMenu.MenuItems[0].Enabled = true;
            this.ew.setPasteButtonEnabled();

        }
    }
    /// <summary>
    /// EventHandler for paste function. paste the object at the current cursor position.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    public void paste_augmentation(object sender, EventArgs e)
    {
            MetaCategory tempMeta = this.currentMetaCategory;
            this.currentMetaCategory = MetaCategory.Augmentation;
            Point p = this.panel.PointToClient(Cursor.Position);
            IPreviewable element = (IPreviewable)this.copy.Clone();
            this.addPreviewable(element, new Vector3D(p.X, p.Y, 0));
            
            if (typeof(AbstractDynamic2DAugmentation).IsAssignableFrom(element.GetType()) && ((AbstractDynamic2DAugmentation)element).source != null)
            {
                this.setSourcePreview(element);
                ((AbstractDynamic2DAugmentation)element).source = (AbstractSource)((AbstractDynamic2DAugmentation)copy).source.Clone();
            }

            currentMetaCategory = tempMeta;
    }

    /// <summary>
    /// EventHandler for paste function. paste the object in the center of panel
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    public void paste_augmentation_center(object sender, EventArgs e)
    {
            MetaCategory tempMeta = this.currentMetaCategory;
            this.currentMetaCategory = MetaCategory.Augmentation;
            this.addPreviewable((IPreviewable)this.copy.Clone(), new Vector3D(this.panel.Width / 2, this.panel.Height / 2, 0));


    }

    /// <summary>
    /// set the current element and mark it on the panel
    /// </summary>
    /// <param name="currentElement">The current element.</param>
    public void setCurrentElement(IPreviewable currentElement)
    {
        this.ew.CurrentElement = currentElement;

        if (typeof(AbstractAugmentation).IsAssignableFrom(currentElement.GetType()))
        {
            this.ew.Tsm_editor_menu_edit_copie.Enabled = true;
        }
        else if (typeof(AbstractTrackable).IsAssignableFrom(currentElement.GetType()))
        {
            this.ew.Tsm_editor_menu_edit_copie.Enabled = false;
        }

        foreach (Control comp in this.panel.Controls)
        {
            if (((PictureBox)comp).BorderStyle == BorderStyle.Fixed3D)
            {
                ((PictureBox)comp).BorderStyle = BorderStyle.None;
            }
        }
        findBox(currentElement).BorderStyle = BorderStyle.Fixed3D;
        findBox(currentElement).BringToFront();
    }

    /// <summary>
    /// set the augmentationPreview to a augmentationPreview with source icon
    /// </summary>
    /// <param name="currentElement">The current element.</param>
    private void setSourcePreview(IPreviewable currentElement)
    {
        PictureBox temp = this.findBox(currentElement);
        Image image1 = currentElement.getPreview();
        Image image2 = new Bitmap(ARdevKit.Properties.Resources.db_small);
        Image newPic = new Bitmap(image1.Width, image1.Height);

        Graphics graphic = Graphics.FromImage(newPic);
        graphic.DrawImage(image1, new Rectangle(0, 0, image1.Width, image1.Height));
        graphic.DrawImage(image2, new Rectangle(0, 0, image2.Width, image2.Height));
        temp.Image = newPic;
        temp.ContextMenu.MenuItems.Add("Source anzeigen", new EventHandler(this.show_source_by_click));
        temp.ContextMenu.MenuItems.Add("Source löschen", new EventHandler(this.remove_source_by_click));
        temp.Refresh();
    }


}

