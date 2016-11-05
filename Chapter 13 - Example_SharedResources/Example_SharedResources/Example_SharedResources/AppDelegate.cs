using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Example_SharedResources
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected UINavigationController _mainNavController;
		protected Example_SharedResources.Screens.iPhone.Home.HomeNavController _iPhoneHome;
		protected int _networkActivityCount = 0;
		
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
			
			this._iPhoneHome = new Example_SharedResources.Screens.iPhone.Home.HomeNavController ();
			this._mainNavController.PushViewController (this._iPhoneHome, false);
			
			
			this._window.AddSubview (this._mainNavController.View);
			
			//----
			return true;
		}
		//========================================================================
		
		//========================================================================
		/// <summary>
		/// Keeps a running reference of items that want to turn the network activity on or off 
		/// so it doesn't get turned off by one activity if another is still active
		/// </summary>
		public void SetNetworkActivityIndicator(bool onOrOff)
		{
			//---- increment or decrement our reference count
			if(onOrOff)
			{ this._networkActivityCount++; }
			else { this._networkActivityCount--; }
			
			//---- set it's visibility based on whether or not there is still activity
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = (this._networkActivityCount > 0);
		}
		//========================================================================
		
	}
	//========================================================================
}
