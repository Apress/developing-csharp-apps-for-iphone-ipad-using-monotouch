using System;
using MonoTouch.UIKit;

namespace Example_ContentControls.Screens.iPhone.Tabs
{
	public class TabBarController : UITabBarController
	{
		//---- screens
		UINavigationController _browsersTabNavController;
		Browsers.BrowsersHome _browsersHome;
		Search.SearchScreen _searchScreen;
		UINavigationController _mapsTabNavController;
		Maps.MapsHome _mapsHome;
		UINavigationController _customizeNavBarNavController;
		CustomizingNavBar.CustomizingNavBarScreen _customizingNavBarScreen;
		
		/// <summary>
		/// 
		/// </summary>
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		
			//---- browsers tab
			// in this case, we create a navigation controller and then add our screen 
			// to that
			this._browsersTabNavController = new UINavigationController();
			this._browsersTabNavController.TabBarItem = new UITabBarItem();
			this._browsersTabNavController.TabBarItem.Title = "Browsers";
			this._browsersHome = new Browsers.BrowsersHome();
			this._browsersTabNavController.PushViewController(this._browsersHome, false);
			
			//---- maps tab
			this._mapsTabNavController = new UINavigationController();
			this._mapsTabNavController.TabBarItem = new UITabBarItem();
			this._mapsTabNavController.TabBarItem.Title = "Maps";
			this._mapsHome = new Maps.MapsHome();
			this._mapsTabNavController.PushViewController(this._mapsHome, false);
			
			//---- search
			this._searchScreen = new Search.SearchScreen();
			this._searchScreen.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 1);
		
			//---- custom nav bar
			this._customizeNavBarNavController = new UINavigationController();
			this._customizeNavBarNavController.TabBarItem = new UITabBarItem();
			this._customizeNavBarNavController.TabBarItem.Title = "Nav";
			this._customizingNavBarScreen = new CustomizingNavBar.CustomizingNavBarScreen();
			this._customizeNavBarNavController.PushViewController(this._customizingNavBarScreen, false);
			
			//---- set a badge, just for fun
			this._customizeNavBarNavController.TabBarItem.BadgeValue = "3";
			
			
			//---- create our array of controllers
			var viewControllers = new UIViewController[] {
				this._browsersTabNavController,
				this._mapsTabNavController,
				this._searchScreen,
				this._customizeNavBarNavController,
				new ExtraScreens.CustomizableTabScreen() { Number = "1" },
				new ExtraScreens.CustomizableTabScreen() { Number = "2" },
				new ExtraScreens.CustomizableTabScreen() { Number = "3" },
				new ExtraScreens.CustomizableTabScreen() { Number = "4" },
				new ExtraScreens.CustomizableTabScreen() { Number = "5" }
			};
			
			//---- create an array of customizable controllers from just the 
			// ones we want to customize. 
			var customizableControllers = new UIViewController[] {
				viewControllers[2],
				viewControllers[3],
				viewControllers[4],
				viewControllers[5],
				viewControllers[6]
			};
			
			//---- attach the view controllers
			this.ViewControllers = viewControllers;
			
			//---- tell the tab bar which controllers are allowed to customize. if we 
			// don't set this, it assumes all controllers are customizable.
			this.CustomizableViewControllers = customizableControllers;
			
			//---- set our selected item
			this.SelectedViewController = this._browsersTabNavController;
			
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

	}
}

