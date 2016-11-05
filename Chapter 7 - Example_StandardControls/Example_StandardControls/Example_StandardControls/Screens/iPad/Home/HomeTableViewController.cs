using System;
using MonoTouch.UIKit;
using Example_StandardControls.Code;
using System.Collections.Generic;

namespace Example_StandardControls.Screens.iPad.Home
{
	public class HomeNavController : UITableViewController
	{
		//---- declare vars
		List<NavItemGroup> _navItems = new List<NavItemGroup>();
		NavItemTableSource _tableSource;
		
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
			
			
			//---- create the navigation items
			NavItemGroup navGroup = new NavItemGroup ("Form Controls");
			//			this._navItems.Add (navGroup);
			//			navGroup.Items.Add (new NavItem ("Labels", "", typeof(Labels.LabelsScreen_iPhone)));
			//			navGroup.Items.Add (new NavItem ("Text Fields", "", typeof(TextFields.TextFields_iPhone)));
			//			navGroup.Items.Add (new NavItem ("Sliders", "", typeof(Sliders.Sliders_iPhone)));
			//			navGroup.Items.Add (new NavItem ("Buttons", "", typeof(Buttons.ButtonsScreen_iPhone)));
			//			navGroup.Items.Add (new NavItem ("Switches", "", typeof(Switches.Switches_iPhone)));
			//			
			//			navGroup = new NavItemGroup ("Content Controls");
			//			this._navItems.Add (navGroup);
			//			navGroup.Items.Add (new NavItem ("Scroll View", "", typeof(ScrollView.Controller)));
			//			navGroup.Items.Add (new NavItem ("Tap to Zoom Scroll View", "", typeof(TapToZoomScrollView.Controller)));
			//			navGroup.Items.Add (new NavItem ("Pager Control", "", typeof(PagerControl.PagerControl_iPhone)));
			//		
			//			navGroup = new NavItemGroup ("Process Controls");
			//			this._navItems.Add (navGroup);
			//			navGroup.Items.Add (new NavItem ("Activity Spinners", "", typeof(ActivitySpinner.ActivitySpinnerScreen_iPhone)));
			//			navGroup.Items.Add (new NavItem ("Progress Bars", "", typeof(ProgressBars.ProgressBars_iPhone)));
			
			navGroup = new NavItemGroup ("Popups");
			this._navItems.Add (navGroup);
			//navGroup.Items.Add (new NavItem ("Alert Views", "", typeof(AlertViews.AlertViewsScreen_iPhone)));
			navGroup.Items.Add (new NavItem ("Action Sheets", "", typeof(ActionSheets.ActionSheets_iPad)));

			navGroup = new NavItemGroup ("Pickers");
			this._navItems.Add (navGroup);
			navGroup.Items.Add (new NavItem ("Simple Date Picker", "", typeof(DatePicker.DatePickerSimple_iPad)));
			navGroup.Items.Add (new NavItem ("Action Sheet Date Picker", "", typeof(DatePicker.ActionSheetDatePicker_iPad)));


			
			//---- create a table source from our nav items
			this._tableSource = new NavItemTableSource (this.NavigationController, this._navItems);
			
			//---- set the source on the table to our data source
			base.TableView.Source = this._tableSource;
		}
	}
}

