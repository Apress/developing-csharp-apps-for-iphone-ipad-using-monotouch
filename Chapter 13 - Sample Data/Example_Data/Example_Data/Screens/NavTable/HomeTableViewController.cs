using System;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.Collections.Generic;
using Apress.Code;

namespace Apress.Screens.NavTable
{
	public class HomeNavController : UITableViewController
	{
		//---- declare vars
		protected List<NavItemGroup> _navItems = new List<NavItemGroup>();
		protected NavItemTableSource _tableSource;
		
		/// <summary>
		/// A basic MT.D data set
		/// </summary>
		protected RootElement _mtDialogBasicMenu;
		/// <summary>
		/// We'll use this to create an MT.RootElement that automatically binds to this object.
		/// </summary>
		//protected AccountInfo _accountInfo;
		
		protected RootElement _linqBuiltElementTree;
		
		public HomeNavController () : base(UITableViewStyle.Grouped)
		{
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			//---- hide the nav bar when this controller appears
			this.NavigationController.SetNavigationBarHidden (true, true);
		}
		
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			//---- show the nav bar when other controllers appear
			this.NavigationController.SetNavigationBarHidden (false, true);
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavItemGroup navGroup;
			
			//---- create the navigation items
			navGroup = new NavItemGroup ("ADO.NET");
			this._navItems.Add (navGroup);
			navGroup.Items.Add (new NavItem ("Basic ADO.NET", "", typeof(ADONET.BasicOperations)));

			navGroup = new NavItemGroup ("SQLite-Net");
			this._navItems.Add (navGroup);
			navGroup.Items.Add (new NavItem ("Basic SQLite-Net", "", typeof(SQLiteNet.BasicOperations)));

			navGroup = new NavItemGroup ("Vici CoolStorage");
			this._navItems.Add (navGroup);
			navGroup.Items.Add (new NavItem ("Basic Vici CoolStorage", "", typeof(ViciCoolStorage.BasicOperations)));

			
			//---- create a table source from our nav items
			this._tableSource = new NavItemTableSource (this.NavigationController, this._navItems);
			
			//---- set the source on the table to our data source
			base.TableView.Source = this._tableSource;
		}
					
	}
}

