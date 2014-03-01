using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using ARdevKit.Model.Project;
using System.Windows.Forms;

namespace ARdevKit.View
{
    /// <summary>
    /// Class which acts as "bridge" for the .net propertyGrid and an custome ControlForm.
    /// </summary>
    public class SliderEditor : UITypeEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SliderEditor"/> class.
        /// </summary>
        public SliderEditor()
        {
        }

        /// <summary>
        /// Bearbeitet den Wert des angegebenen Objekts mit dem von der <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" />-Methode angegebenen Editor-Stil.
        /// </summary>
        /// <param name="context">Eine <see cref="T:System.ComponentModel.ITypeDescriptorContext" />-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können.</param>
        /// <param name="provider">Ein <see cref="T:System.IServiceProvider" />, über den dieser Editor Dienste anfordern kann.</param>
        /// <param name="value">Das zu bearbeitende Objekt.</param>
        /// <returns>
        /// Der neue Wert des Objekts. Wenn sich der Wert des Objekts nicht geändert hat, wird hierbei dasselbe Objekt zurückgegeben, das zuvor übergeben wurde.
        /// </returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (svc != null)
            {
                if (context.Instance is AbstractTrackable)
                {
                    Slider sd = new Slider((double)value);
                    svc.DropDownControl(sd);
                    return (object)sd.SliderValueDouble;
                }
                if (context.Instance is Abstract2DAugmentation)
                {
                    Slider sd = new Slider((int)value, 1000);
                    svc.DropDownControl(sd);
                    return (object)sd.SliderValueInt;
                }
            }
            return value;
        }

        /// <summary>
        /// Ruft den Editor-Stil ab, der von der <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" />-Methode verwendet wird.
        /// </summary>
        /// <param name="context">Eine <see cref="T:System.ComponentModel.ITypeDescriptorContext" />-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können.</param>
        /// <returns>
        /// Ein <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" />-Wert, der den von der <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" />-Methode verwendeten Editor-Stil angibt. Wenn <see cref="T:System.Drawing.Design.UITypeEditor" /> diese Methode nicht unterstützt, gibt <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> den Wert <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" /> zurück.
        /// </returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }

    /// <summary>
    /// Class which acts as "bridge" for the .net propertyGrid and an custome Form.
    /// </summary>
    public class TextEditor : UITypeEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditor"/> class.
        /// </summary>
        public TextEditor()
        {
        }

        /// <summary>
        /// Ruft den Editor-Stil ab, der von der <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" />-Methode verwendet wird.
        /// </summary>
        /// <param name="context">Eine <see cref="T:System.ComponentModel.ITypeDescriptorContext" />-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können.</param>
        /// <returns>
        /// Ein <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" />-Wert, der den von der <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" />-Methode verwendeten Editor-Stil angibt. Wenn <see cref="T:System.Drawing.Design.UITypeEditor" /> diese Methode nicht unterstützt, gibt <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> den Wert <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" /> zurück.
        /// </returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// Bearbeitet den Wert des angegebenen Objekts mit dem von der <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" />-Methode angegebenen Editor-Stil.
        /// </summary>
        /// <param name="context">Eine <see cref="T:System.ComponentModel.ITypeDescriptorContext" />-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können.</param>
        /// <param name="provider">Ein <see cref="T:System.IServiceProvider" />, über den dieser Editor Dienste anfordern kann.</param>
        /// <param name="value">Das zu bearbeitende Objekt.</param>
        /// <returns>
        /// Der neue Wert des Objekts. Wenn sich der Wert des Objekts nicht geändert hat, wird hierbei dasselbe Objekt zurückgegeben, das zuvor übergeben wurde.
        /// </returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (svc != null)
                using (TextEditorForm form = new TextEditorForm())
                {
                    form.Value = (string[])value;
                    svc.ShowDialog(form);
                    return (object)form.Value;
                }
            return value;
        }

    }
}
