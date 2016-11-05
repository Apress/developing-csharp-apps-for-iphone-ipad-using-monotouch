using System;
using System.Collections.Generic;

namespace Apress.Code
{
	//========================================================================
	/// <summary>
	/// A group that contains table items
	/// </summary>
	public class NavItemGroup
	{
		public string Name
		{
			get;
			set;
		}

		public string Footer
		{
			get;
			set;
		}

		public List<NavItem> Items
		{
			get { return this._items; }
			set { this._items = value; }
		}
		protected List<NavItem> _items = new List<NavItem> ();

		public NavItemGroup ()
		{
		}

		public NavItemGroup (string name)
		{
			this.Name = name;
		}

	}
	//========================================================================
}

