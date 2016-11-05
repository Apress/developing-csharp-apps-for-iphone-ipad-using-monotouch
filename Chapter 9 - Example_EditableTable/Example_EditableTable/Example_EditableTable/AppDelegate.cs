using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Example_EditableTable
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected Example_EditableTable.Screens.HomeScreen _iPhoneHome;
		
		#endregion
		//========================================================================
		
		//========================================================================
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			//---- create our window
			this._window = new UIWindow (UIScreen.MainScreen.Bounds);
			this._window.MakeKeyAndVisible ();
						
			//---- create the home screen
			this._iPhoneHome = new Example_EditableTable.Screens.HomeScreen();
			this._iPhoneHome.View.Frame = new System.Drawing.RectangleF(0, UIApplication.SharedApplication.StatusBarFrame.Height, UIScreen.MainScreen.ApplicationFrame.Width, UIScreen.MainScreen.ApplicationFrame.Height);
			
			this._window.AddSubview (this._iPhoneHome.View);
			
			//----
			return true;
		}
		//========================================================================
		
	}
	//========================================================================
}
