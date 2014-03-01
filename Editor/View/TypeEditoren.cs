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
        public SliderEditor()
        {
        }

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
        public TextEditor()
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

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
