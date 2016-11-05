using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace Example_EditableTable.Code
{
	//========================================================================
	/// <summary>
	/// A group that contains table items
	/// </summary>
	public class TableItemGroup
	{
		public string Name { get; set; }
	
		public string Footer { get; set; }
	
		public List<TableItem> Items
		{
			get { return this._items; }
			set { this._items = value; }
		}
		protected List<TableItem> _items = new List<TableItem> ();
	}
	//========================================================================
}

