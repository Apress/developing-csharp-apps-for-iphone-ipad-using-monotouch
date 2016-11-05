using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Reflection;

namespace Example_CoreAnimation.Code.NavigationTable
{
	//========================================================================
	/// <summary>
	/// Combined DataSource and Delegate for our UITableView
	/// </summary>
	public class NavItemTableSource : UITableViewSource
	{
		public event EventHandler<RowClickedEventArgs> RowClicked;
		
		protected List<NavItemGroup> _navItems;
		string _cellIdentifier = "NavTableCellView";
		
		public NavItemTableSource (List<NavItemGroup> items)
		{
			this._navItems = items;
		}

		/// <summary>
		/// Called by the TableView to determine how many sections(groups) there are.
		/// </summary>
		public override int NumberOfSections (UITableView tableView)
		{
			return this._navItems.Count;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override int RowsInSection (UITableView tableview, int section)
		{
			return this._navItems[section].Items.Count;
		}

		/// <summary>
		/// Called by the TableView to retrieve the header text for the particular section(group)
		/// </summary>
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return this._navItems[section].Name;
		}

		/// <summary>
		/// Called by the TableView to retrieve the footer text for the particular section(group)
		/// </summary>
		public override string TitleForFooter (UITableView tableView, int section)
		{
			return this._navItems[section].Footer;
		}

		/// <summary>
		/// Called by the TableView to actually build each cell. 
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			//---- declare vars
			NavItem navItem = this._navItems[indexPath.Section].Items[indexPath.Row];
			UIImage navIcon = null;
			
			var cell = tableView.DequeueReusableCell (this._cellIdentifier);
			if (cell == null)
			{
				cell = new UITableViewCell (UITableViewCellStyle.Default, this._cellIdentifier);
				cell.Tag = Environment.TickCount;
			}
			
			//---- set the cell properties
			cell.TextLabel.Text = this._navItems[indexPath.Section].Items[indexPath.Row].Name;
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
						
			//---- return the cell
			return cell;
		}

		//========================================================================
		/// <summary>
		/// Is called when a row is selected
		/// </summary>
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//---- get a reference to the nav item
			NavItem navItem = this._navItems[indexPath.Section].Items[indexPath.Row];
			
			if(this.RowClicked != null)
			{ this.RowClicked(this, new RowClickedEventArgs(navItem)); }
		}
		//========================================================================
		
	}
}

