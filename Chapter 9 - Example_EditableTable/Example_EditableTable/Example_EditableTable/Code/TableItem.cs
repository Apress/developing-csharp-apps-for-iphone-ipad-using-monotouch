using System;
using MonoTouch.UIKit;

namespace Example_EditableTable.Code
{
	//========================================================================
	/// <summary>
	/// Represents our item in the table
	/// </summary>
	public class TableItem
	{
		public string Heading { get; set; }
		
		public string SubHeading { get; set; }
		
		public string ImageName { get; set; }
		
		public UITableViewCellStyle CellStyle
		{
			get { return this._cellStyle; }
			set { this._cellStyle = value; }
		}
		protected UITableViewCellStyle _cellStyle = UITableViewCellStyle.Default;
		
		public UITableViewCellAccessory CellAccessory
		{
			get { return this._cellAccessory; }
			set { this._cellAccessory = value; }
		}
		protected UITableViewCellAccessory _cellAccessory = UITableViewCellAccessory.None;

		public TableItem () { }
		
		public TableItem (string heading)
		{ this.Heading = heading; }
	}
	//========================================================================
}

