using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.IO;

namespace Example_EditableTable.Code
{
	//========================================================================
	/// <summary>
	/// Combined DataSource and Delegate for our UITableView
	/// </summary>
	public class TableSource : UITableViewSource
	{
		//---- declare vars
		protected List<TableItemGroup> _tableItems;
		protected string _cellIdentifier = "TableCell";

		public TableSource (List<TableItemGroup> items)
		{
			this._tableItems = items;
		}
		
		//========================================================================
		#region -= data binding/display methods =-
		
		/// <summary>
		/// Called by the TableView to determine how many sections(groups) there are.
		/// </summary>
		public override int NumberOfSections (UITableView tableView)
		{
			return this._tableItems.Count;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override int RowsInSection (UITableView tableview, int section)
		{
			return this._tableItems[section].Items.Count;
		}

		/// <summary>
		/// Called by the TableView to retrieve the header text for the particular section(group)
		/// </summary>
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return this._tableItems[section].Name;
		}

		/// <summary>
		/// Called by the TableView to retrieve the footer text for the particular section(group)
		/// </summary>
		public override string TitleForFooter (UITableView tableView, int section)
		{
			return this._tableItems[section].Footer;
		}
		
		#endregion
		//========================================================================	
				
		//========================================================================
		#region -= user interaction methods =-
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			new UIAlertView("Row Selected"
				, this._tableItems[indexPath.Section].Items[indexPath.Row].Heading, null, "OK", null).Show();
		}
		
		public override void RowDeselected (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine("Row " + indexPath.Row.ToString() + " deselected");	
		}
		
		public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine("Accessory for Section, " + indexPath.Section.ToString() + " and Row, " + indexPath.Row.ToString() + " tapped");
		}
			
		#endregion
		//========================================================================	

		//========================================================================
		#region -= editing methods =-
		
		/// <summary>
		/// Called by the table view to determine whether or not the row is editable
		/// </summary>
		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		/// <summary>
		/// Called by the table view to determine whether or not the row is moveable
		/// </summary>
		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}
		
		/// <summary>
		/// Called by the table view to determine whether the editing control should be an insert
		/// or a delete.
		/// </summary>
		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0 && indexPath.Row == 1)
			{
				return UITableViewCellEditingStyle.Insert;
			} else
			{
				return UITableViewCellEditingStyle.Delete;
			}
		}
		
		/// <summary>
		/// Should be called CommitEditingAction or something, is called when a user initiates a specific editing event
		/// </summary>
		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			switch (editingStyle)
			{
				case UITableViewCellEditingStyle.Delete:
					//---- remove the item from the underlying data source
					this._tableItems[indexPath.Section].Items.RemoveAt (indexPath.Row);
					//---- delete the row from the table
					tableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
					break;
				
				case UITableViewCellEditingStyle.Insert:
					//---- create a new item and add it to our underlying data
					this._tableItems[indexPath.Section].Items.Insert (indexPath.Row, new TableItem ());
					//---- insert a new row in the table
					tableView.InsertRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
					break;
				
				case UITableViewCellEditingStyle.None:
					Console.WriteLine ("CommitEditingStyle:None called");
					break;
			}
		}
		
		/// <summary>
		/// called by the table view when a row is moved.
		/// </summary>
		public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
		{
			//---- get a reference to the item
			var item = this._tableItems[sourceIndexPath.Section].Items[sourceIndexPath.Row];
			int deleteAt = sourceIndexPath.Row;
			
			//---- if we're moving within the same section, and we're inserting it before
			if ((sourceIndexPath.Section == destinationIndexPath.Section) && (destinationIndexPath.Row < sourceIndexPath.Row))
			{
				//---- add one to where we delete, because we're increasing the index by inserting
				deleteAt = sourceIndexPath.Row + 1;
			}
			
			//---- copy the item to the new location
			this._tableItems[destinationIndexPath.Section].Items.Insert (destinationIndexPath.Row, item);
			
			//---- remove from the old
			this._tableItems[sourceIndexPath.Section].Items.RemoveAt (deleteAt);
			
		}
		
		/// <summary>
		/// Called manually when the table goes into edit mode
		/// </summary>
		public void WillBeginTableEditing (UITableView tableView)
		{
			//---- start animations
			tableView.BeginUpdates ();
			
			//---- insert a new row in the table
			tableView.InsertRows (new NSIndexPath[] { NSIndexPath.FromRowSection (1, 1) }, UITableViewRowAnimation.Fade);
			//---- create a new item and add it to our underlying data
			this._tableItems[1].Items.Insert (1, new TableItem ());
			
			//---- end animations
			tableView.EndUpdates ();
		}
		
		/// <summary>
		/// Called manually when the table leaves edit mode
		/// </summary>
		public void DidFinishTableEditing (UITableView tableView)
		{
			//---- start animations
			tableView.BeginUpdates ();
			//---- remove our row from the underlying data
			this._tableItems[1].Items.RemoveAt (1);
			//---- remove the row from the table
			tableView.DeleteRows (new NSIndexPath[] { NSIndexPath.FromRowSection (1, 1) }, UITableViewRowAnimation.Fade);
			//---- finish animations
			tableView.EndUpdates ();
		}

		
		
		#endregion
		//========================================================================	
		
		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular section and row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			//---- declare vars
			UITableViewCell cell = tableView.DequeueReusableCell (this._cellIdentifier);
			TableItem item = this._tableItems[indexPath.Section].Items[indexPath.Row];
			
			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{ cell = new UITableViewCell (item.CellStyle, this._cellIdentifier); }
			
			//---- set the item text
			cell.TextLabel.Text = this._tableItems[indexPath.Section].Items[indexPath.Row].Heading;
			
			//---- if it's a cell style that supports a subheading, set it
			if(item.CellStyle == UITableViewCellStyle.Subtitle 
			   || item.CellStyle == UITableViewCellStyle.Value1
			   || item.CellStyle == UITableViewCellStyle.Value2)
			{ cell.DetailTextLabel.Text = item.SubHeading; }
			
			//---- if the item has a valid image, and it's not the contact style (doesn't support images)
			if(!string.IsNullOrEmpty(item.ImageName) && item.CellStyle != UITableViewCellStyle.Value2)
			{
				if(File.Exists(item.ImageName))
				{ cell.ImageView.Image = UIImage.FromBundle(item.ImageName); }
			}
			
			//---- set the accessory
			cell.Accessory = item.CellAccessory;
			
			return cell;
		}
		
	}
	//========================================================================
}

