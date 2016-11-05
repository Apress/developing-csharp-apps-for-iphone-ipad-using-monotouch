using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Example_Fonts
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected UINavigationController _mainNavController;
		protected Example_Fonts.Screens.Universal.FontListing.FontFamiliesTableViewController _home;

		/// <summary>
		/// The current device (iPad or iPhone)
		/// </summary>
		public DeviceType CurrentDevice
		{
			get { return this._currentDevice; }
			set { this._currentDevice = value; }
		}
		protected DeviceType _currentDevice;
		
		#endregion
		//========================================================================
		
		//========================================================================
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			//---- create our window
			this._window = new UIWindow (UIScreen.MainScreen.Bounds);
			this._window.MakeKeyAndVisible ();
			
			//---- are we running an iPhone or an iPad?
			this.DetermineCurrentDevice ();

			//---- instantiate our main navigatin controller and add it's view to the window
			this._mainNavController = new UINavigationController ();
			
			switch (this._currentDevice)
			{
				case DeviceType.iPhone:
				case DeviceType.iPad:
					this._home = new Example_Fonts.Screens.Universal.FontListing.FontFamiliesTableViewController ();
					this._mainNavController.PushViewController (this._home, false);
					break;
			}
			
			
			this._window.AddSubview (this._mainNavController.View);
			
			
			//----
			return true;
		}
		//========================================================================
		
		//========================================================================
		/// <summary>
		/// 
		/// </summary>
		protected void DetermineCurrentDevice ()
		{
			//---- figure out the current device type
			if (UIScreen.MainScreen.Bounds.Height == 1024 || UIScreen.MainScreen.Bounds.Width == 1024)
			{
				this._currentDevice = DeviceType.iPad;
			} else
			{
				this._currentDevice = DeviceType.iPhone;
			}
		}
		//========================================================================
		
	}
	//========================================================================
}
