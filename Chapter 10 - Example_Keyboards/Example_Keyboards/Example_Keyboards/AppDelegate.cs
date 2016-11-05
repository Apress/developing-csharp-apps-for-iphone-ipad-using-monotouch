using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Example_Keyboards
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected Example_Keyboards.Screens.iPhone.HomeScreen _iPhoneHome;
		//protected Example_Keyboards.Screens.iPad.HomeScreen _iPadHome;

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

			switch (this._currentDevice)
			{
				case DeviceType.iPhone:
					this._iPhoneHome = new Example_Keyboards.Screens.iPhone.HomeScreen();
					this._iPhoneHome.View.Frame = new System.Drawing.RectangleF(0, UIApplication.SharedApplication.StatusBarFrame.Height, UIScreen.MainScreen.ApplicationFrame.Width, UIScreen.MainScreen.ApplicationFrame.Height);
					this._window.AddSubview (this._iPhoneHome.View);
					break;
				
				case DeviceType.iPad:
					//this._iPadHome = new Example_StandardControls.Screens.iPad.Home.HomeNavController ();
					//this._mainNavController.PushViewController (this._iPadHome, false);
					break;
			}			
			
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
