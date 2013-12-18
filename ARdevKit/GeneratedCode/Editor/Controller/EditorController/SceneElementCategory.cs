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
    /// <summary>   A scene element category. </summary>
    ///
    /// <remarks>   Geht, 18.12.2013. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

	public class SceneElementCategory
	{
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the scene elements. </summary>
        ///
        /// <value> The scene elements. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

		private List<SceneElement> sceneElements
		{
			get;
			set;
		}

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

		private string name
		{
			get;
			set;
		}

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the category the meta belongs to. </summary>
        ///
        /// <value> The meta category. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

		private ARdevKit_UML::Editor::Controller::EditorController::MetaCategory metaCategory
		{
			get;
			set;
		}

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds an element. </summary>
        ///
        /// <remarks>   Geht, 18.12.2013. </remarks>
        ///
        /// <exception cref="NotImplementedException">  Thrown when the requested operation is
        ///                                             unimplemented. </exception>
        ///
        /// <param name="element">  The element. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

		public virtual void addElement(ARdevKit_UML::Editor::Controller::EditorController::SceneElement element)
		{
			throw new System.NotImplementedException();
		}

	}
}

