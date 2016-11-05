using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Example_Tapku
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected CoverflowScreen _coverflowScreen;
		
		
		#endregion
		//========================================================================
		
		//========================================================================
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;
			
			//---- create our window
			this._window = new UIWindow (UIScreen.MainScreen.Bounds);
			this._window.MakeKeyAndVisible ();
						
			//---- add the cover flow to the window
			this._coverflowScreen = new CoverflowScreen();
			this._window.AddSubview (this._coverflowScreen.View);
			
			
			//----
			return true;
		}
		//========================================================================
				
	}
	//========================================================================
}
