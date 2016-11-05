using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Example_3rdPartyLibs
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected UINavigationController _mainNavController;
		protected Example_3rdPartyLibs.Screens.iPhone.Home.HomeNavController _iPhoneHome;
		
		#endregion
		//========================================================================
		
		//========================================================================
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			//---- create our window
			this._window = new UIWindow (UIScreen.MainScreen.Bounds);
			this._window.MakeKeyAndVisible ();
			
			//---- instantiate our main navigatin controller and add it's view to the window
			this._mainNavController = new UINavigationController ();
			
			this._iPhoneHome = new Example_3rdPartyLibs.Screens.iPhone.Home.HomeNavController ();
			this._mainNavController.PushViewController (this._iPhoneHome, false);
			
			this._window.AddSubview (this._mainNavController.View);
			
			
			//----
			return true;
		}
		//========================================================================
				
	}
	//========================================================================
}
