using ARdevKit.Model.Project;
using ARdevKit.Model.Project.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ARdevKit.View
{
    /// <summary>
    /// An <see cref="EventTypeEditor"/> is an <see cref="UITypeEditor"/> that is used to edit <see cref="AbstractEvent"/>s.
    /// </summary>
    public class EventTypeEditor : UITypeEditor
    {
        /// <summary>
        /// Ruft den Editor-Stil ab, der von der <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" />-Methode verwendet wird.
        /// </summary>
        /// <param name="context">Eine <see cref="T:System.ComponentModel.ITypeDescriptorContext" />-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können.</param>
        /// <returns>
        /// Ein <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" />-Wert, der den von der <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" />-Methode verwendeten Editor-Stil angibt. Wenn <see cref="T:System.Drawing.Design.UITypeEditor" /> diese Methode nicht unterstützt, gibt <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> den Wert <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" /> zurück.
        /// </returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context == null || context.Instance == null)
                return base.GetEditStyle(context);
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
            IWindowsFormsEditorService editorService;

            if (context == null || context.Instance == null || provider == null)
                return value;

            try
            {
                // get the editor service, just like in windows forms
                editorService = (IWindowsFormsEditorService)
                provider.GetService(typeof(IWindowsFormsEditorService));
                AbstractAugmentation a = (AbstractAugmentation)context.Instance;
                AbstractEvent selectedEvent = (AbstractEvent)value;
                TextEditorForm tef = new TextEditorForm(selectedEvent);
                using (tef)
                {
                    if (tef.ShowDialog() == DialogResult.OK)
                        return tef.SelectedEvent;
                    else
                        return selectedEvent;
                }
            }
            finally
            {
                editorService = null;
            }
        }
    }
}
