using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Apress
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected UINavigationController _navController;
		protected Screens.NavTable.HomeNavController _navTable;
		
		
		#endregion
		//========================================================================
		
		//========================================================================
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;
			
			//---- create our window
			this._window = new UIWindow (UIScreen.MainScreen.Bounds);
			this._window.MakeKeyAndVisible ();
			
			this._navController = new UINavigationController();
			this._navTable = new Screens.NavTable.HomeNavController();
			this._navController.PushViewController(this._navTable, false);
			
			//---- add the nav controller to the window
			this._window.AddSubview (this._navController.View);
			
			
			//----
			return true;
		}
		//========================================================================
				
	}
	//========================================================================
}
