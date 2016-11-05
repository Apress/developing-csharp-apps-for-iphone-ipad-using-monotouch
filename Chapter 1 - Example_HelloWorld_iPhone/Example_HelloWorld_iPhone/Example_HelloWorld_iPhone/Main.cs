using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_HelloWorld_iPhone
{
	public class Application
	{
		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}
	}

	// The name AppDelegate is referenced in the MainWindow.xib file.
	public partial class AppDelegate : UIApplicationDelegate
	{
		//---- number of times we've clicked
		protected int _numberOfClicks;
	
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// If you have defined a view, add it here:
			// window.AddSubview (navigationController.View);
			
			window.MakeKeyAndVisible ();
	
			//---- wire up our event handler
			this.btnClickMe.TouchUpInside += BtnClickMeTouchUpInside;
			
			return true;
		}
		
		partial void actnButtonClick (UIButton sender)
		{
			//---- show which button was clicked
			this.lblResult.Text = sender.CurrentTitle + " Clicked";
		}
	
		protected void BtnClickMeTouchUpInside (object sender, EventArgs e)
		{
			//---- increment our counter
			this._numberOfClicks++;
			//---- update our label
			this.lblResult.Text = "Hello World, [" + this._numberOfClicks.ToString () + "] times";
		}
		
		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
		}
	}
}

