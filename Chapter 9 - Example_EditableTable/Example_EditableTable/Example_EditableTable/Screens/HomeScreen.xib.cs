using System;
using MonoTouch.UIKit;
using Example_EditableTable.Code;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace Example_EditableTable.Screens
{
	public partial class HomeScreen : UIViewController
	{
		protected TableSource _tableSource;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public HomeScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public HomeScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public HomeScreen () : base("HomeScreen", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			this.CreateTableItems ();
			this.tblMain.Source = this._tableSource;
			
			//---- toggle the table's editing mode when the edit button is clicked
			this.btnEdit.Clicked += (s, e) =>
			{
				this.tblMain.SetEditing (!this.tblMain.Editing, true);
				
				//---- manually call our methods to let the data source know when the table
				// is going into our out of editing mode
				if (this.tblMain.Editing)
				{ this._tableSource.WillBeginTableEditing (this.tblMain); }
				else
				{ this._tableSource.DidFinishTableEditing (this.tblMain); }
			};
				
		}

		/// <summary>
		/// Creates a set of table items.
		/// </summary>
		protected void CreateTableItems ()
		{
			List<TableItemGroup> tableItems = new List<TableItemGroup> ();
			
			//---- declare vars
			TableItemGroup tGroup;
			
			//---- Section 1
			tGroup = new TableItemGroup() { Name = "Places" };
			tGroup.Items.Add (new TableItem() { ImageName = "Images/Beach.png", Heading = "Fiji", SubHeading = "A nice beach" });
			tGroup.Items.Add (new TableItem() { ImageName = "Images/Shanghai.png", Heading = "Beijing", SubHeading = "AKA Shanghai" });
			tableItems.Add (tGroup);
			
			//---- Section 2
			tGroup = new TableItemGroup() { Name = "Other" };
			tGroup.Items.Add (new TableItem() { ImageName = "Images/Seeds.png", Heading = "Seedlings", SubHeading = "Tiny Plants" });
			tGroup.Items.Add (new TableItem() { ImageName = "Images/Plants.png", Heading = "Plants", SubHeading = "Green plants" });
			tableItems.Add (tGroup);
			
			//---- For custom cells, comment out the first and uncomment the second.
			this._tableSource = new TableSource(tableItems);
		}

	}
}

