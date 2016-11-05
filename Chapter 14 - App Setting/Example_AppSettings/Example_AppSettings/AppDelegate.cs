using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Example_AppSettings
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected MainViewController _mainViewController;

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
		/// <summary>
		/// 
		/// </summary>
		public AppDelegate () : base()
		{
			//---- set any user default values (they're not actually set until the settings app is run for the first time)
			UserDefaultsHelper.LoadDefaultSettings ();
			
			//---- initialize our user settings, which loads any saved settings from the file (if they exist)
			NSUserDefaults.StandardUserDefaults.Init ();
		}
		//========================================================================
		
		//========================================================================
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			//---- create our window
			this._window = new UIWindow (UIScreen.MainScreen.Bounds);
			this._window.MakeKeyAndVisible ();
			
			//---- are we running an iPhone or an iPad?
			this.DetermineCurrentDevice ();

			//---- instantiate and load our main screen onto the window
			this._mainViewController = new MainViewController ();
			this._window.AddSubview (this._mainViewController.View);
			
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
