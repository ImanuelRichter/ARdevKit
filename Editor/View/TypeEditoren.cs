using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace ARdevKit.View
{
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
                Slider sd = new Slider((double)value);
                sd.SliderValue = (double)value;
                svc.DropDownControl(sd);
                return (object)sd.SliderValue;
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }
}
