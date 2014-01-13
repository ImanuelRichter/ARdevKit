using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARdevKit.Controller;

namespace ARdevKit.Controller.EditorController
{
    class SceneElementCategory
    {
        public enum MetaCategory { Source, Augmentation, Trackable };

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Hält die Kategorie der in den SceneElements enthaltenen IPreviewables. </summary>
        ///
        /// <value> The meta category. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public MetaCategory metaCategory { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Hält einen Namen, der die Kategorie beschreibt. Dieser wird in der ComboBox des
        ///     ElementSelectionPanels angezeigt.
        /// </summary>
        ///
        /// <value> The name. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public String name { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Hält die zur Kategorie gehörenden SceneElements. </summary>
        ///
        /// <value> The scene elements. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<SceneElement> sceneElements { get; set; }
    }
}
