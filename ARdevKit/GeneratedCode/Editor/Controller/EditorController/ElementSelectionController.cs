﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Wenn der Code neu generiert wird, gehen alle Änderungen an dieser Datei verloren
// </auto-generated>
//------------------------------------------------------------------------------
namespace Editor.Controller.EditorController
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling element selections. </summary>
    ///
    /// <remarks>   Geht, 18.12.2013. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

	public class ElementSelectionController
	{
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the category panels. </summary>
        ///
        /// <value> The category panels. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

		private List<SceneElementCategoryPanel> categoryPanels
		{
			get;
			set;
		}

		public virtual {
			get;
			set;
		}

		public virtual void updateElementSelectionPanel()
		{
			throw new System.NotImplementedException();
		}

		public virtual void createCategoryPanels()
		{
			throw new System.NotImplementedException();
		}

		public virtual void populateComboBox()
		{
			throw new System.NotImplementedException();
		}

	}
}

