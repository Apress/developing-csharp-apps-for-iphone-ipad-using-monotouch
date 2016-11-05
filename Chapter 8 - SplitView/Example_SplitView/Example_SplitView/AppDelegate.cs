using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Example_SplitView.Screens;

namespace Example_SplitView
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected Screens.MainSplitView.MainSplitView _splitView;
		
		#endregion
		//========================================================================
		
		//========================================================================
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			//---- create our window
			this._window = new UIWindow (UIScreen.MainScreen.Bounds);
			this._window.MakeKeyAndVisible ();
			
			//---- instantiate our main split view controller
			this._splitView = new Screens.MainSplitView.MainSplitView();
			
			this._window.AddSubview (this._splitView.View);
			
			
			//----
			return true;
		}
		//========================================================================
		
	}
	//========================================================================
}
