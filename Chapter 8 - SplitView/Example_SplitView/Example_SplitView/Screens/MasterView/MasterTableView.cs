using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Example_SplitView.Screens.MasterView
{
	public class MasterTableView : UITableViewController
	{
		TableSource _tableSource;
		
		public event EventHandler<RowClickedEventArgs> RowClicked;
		
		public MasterTableView ()
		{
		}
		
		public MasterTableView(IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//---- setup our data source
			List<string> items = new List<string>();
			for(int i = 1; i <= 10; i++)
			{ items.Add(i.ToString()); }
			this._tableSource = new TableSource(items, this);

			//---- add the data source to the table
			this.TableView.Source = this._tableSource;
			
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

		public class RowClickedEventArgs : EventArgs
		{
			public string Item { get; set; }
			
			public RowClickedEventArgs(string item) : base()
			{ this.Item = item; }
		}
		
		protected class TableSource : UITableViewSource
		{
			public List<string> Items = new List<string>();
			protected string _cellIdentifier = "basicCell";
			protected MasterTableView _parentController;
			
			public TableSource(List<string> items, MasterTableView parentController)
			{
				this.Items = items;
				this._parentController = parentController;
			}
			
			public override int NumberOfSections (UITableView tableView)
			{ return 1; }
			
			public override int RowsInSection (UITableView tableview, int section)
			{
				return this.Items.Count;
			}
			
			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				//---- declare vars 
				UITableViewCell cell = tableView.DequeueReusableCell (this._cellIdentifier);
				//---- if there are no cells to reuse, create a new one 
				if (cell == null)
				{
					cell = new UITableViewCell (UITableViewCellStyle.Default, this._cellIdentifier);
				}
				//---- set the item text 
				cell.TextLabel.Text = this.Items[indexPath.Row];
				
				return cell;
			}
			
			public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				if(this._parentController.RowClicked != null)
				{ this._parentController.RowClicked(this, new RowClickedEventArgs(this.Items[indexPath.Row])); }
			}
		}
	}
}

