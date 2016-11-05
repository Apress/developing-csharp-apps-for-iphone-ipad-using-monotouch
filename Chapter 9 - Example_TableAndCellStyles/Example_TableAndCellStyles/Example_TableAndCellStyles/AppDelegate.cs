using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Example_TableAndCellStyles
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected UINavigationController _navController;
		protected Example_TableAndCellStyles.Screens.iPhone.Home.HomeNavController _iPhoneHome;
		
		#endregion
		//========================================================================
		
		//========================================================================
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			//---- create our window
			this._window = new UIWindow (UIScreen.MainScreen.Bounds);
			this._window.MakeKeyAndVisible ();
			
			//---- create our navigation controller
			this._navController = new UINavigationController();
			
			//---- create the home screen and add it to the nav controller
			this._iPhoneHome = new Example_TableAndCellStyles.Screens.iPhone.Home.HomeNavController();
			this._navController.PushViewController(this._iPhoneHome, false);
			
			this._window.AddSubview (this._navController.View);
			
			//----
			return true;
		}
		//========================================================================
		
	}
	//========================================================================
}
