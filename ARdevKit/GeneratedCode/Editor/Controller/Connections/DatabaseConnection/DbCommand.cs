﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Wenn der Code neu generiert wird, gehen alle Änderungen an dieser Datei verloren
// </auto-generated>
//------------------------------------------------------------------------------
namespace Editor.Controller.Connections.DatabaseConnection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Represents an DB statement or stored procedure to execute against a data source.
    /// </summary>
    ///
    /// <remarks>   Geht, 18.12.2013. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

	public class DbCommand
	{
		public virtual {
			get;
			set;
		}

		/// <summary>
		/// Creates a new instance of a DbParameter object.
		/// </summary>
		public virtual ARdevKit_UML::Editor::Controller::Connections::DatabaseConnection::DbParameter CreateDbParameter()
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Executes the command text against the connection.
		/// </summary>
		public virtual ARdevKit_UML::Editor::Controller::Connections::DatabaseConnection::DbDataReader ExecuteDbDataReader()
		{
			throw new System.NotImplementedException();
		}

	}
}

