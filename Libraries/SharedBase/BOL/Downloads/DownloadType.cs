/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *  Enterprise Manager is distributed under the GNU General Public License version 3 and  
 *  is also available under alternative licenses negotiated directly with Simon Carter.  
 *  If you obtained Enterprise Manager under the GPL, then the GPL applies to all loadable 
 *  Enterprise Manager modules used on your system as well. The GPL (version 3) is 
 *  available at https://opensource.org/licenses/GPL-3.0
 *
 *  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 *  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 *  See the GNU General Public License for more details.
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2010 - 2018 Simon Carter.  All Rights Reserved.
 *
 *  Product:  Enterprise Manager
 *  
 *  File: DownloadType.cs
 *
 *  Purpose:  
 *
 *  Date        Name                Reason
 *  
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System;

namespace SharedBase.BOL.Downloads
{
	/// <summary>
	/// Wrapper for Firebird table WS_DOWNLOADTYPE
	/// 
	/// Automatically generated by FBSPGen (http://www.sieradelta.com/Products/FBSPGen.aspx)
	/// </summary>
	public class DownloadType
	{
		#region Constructors

		/// <summary>
		/// Standard constructor for DownloadType
		/// </summary>
		/// <param name="iD">Property Description for Field ID</param>
		/// <param name="description">Property Description for Field DESCRIPTION</param>
		public DownloadType (int iD, string description)
		{
			ID = iD;
			Description = description;
		}

		#endregion Constructors

		#region Public Methods

		/// <summary>
		/// Saves the current record
		/// </summary>
		public void Save()
		{
            SharedBase.DAL.FirebirdDB.DownloadTypeUpdate(this);
		}

		/// <summary>
		/// Deletes the current record
		/// </summary>
		public bool Delete()
		{
			return SharedBase.DAL.FirebirdDB.DownloadTypeDelete(this);
		}


		/// <summary>
		/// Reloads the current record
		/// </summary>
		public void Reload()
		{
			throw new NotImplementedException();
		}

		#endregion Public Methods

		#region Overridden Methods

		/// <summary>
		/// Returns the String for the class
		/// </summary>
		public override string ToString()
		{
			return String.Format("WS_DOWNLOADTYPE Record {0}", ID);
		}

		#endregion Overridden Methods

		#region Properties

		/// <summary>
		/// Property Description for Field ID
		/// </summary>
		public int ID { get; internal set; }

		/// <summary>
		/// Property Description for Field DESCRIPTION
		/// </summary>
		public string Description { get; set; }

		#endregion Properties
	}
}