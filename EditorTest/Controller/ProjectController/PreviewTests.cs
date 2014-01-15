using ARdevKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Controller.EditorController;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace EditorTest.Controller.ProjectController
{
    [TestClass]
    public class PreviewTests 
    {
        [TestMethod]
        public void addPreviewAble()
        {
            EditorWindow ew = new EditorWindow();
            PreviewController prev = new PreviewController(ew);

            prev.currentMetaCategory = PreviewController.MetaCategory.Trackable;

        }
    }
}
