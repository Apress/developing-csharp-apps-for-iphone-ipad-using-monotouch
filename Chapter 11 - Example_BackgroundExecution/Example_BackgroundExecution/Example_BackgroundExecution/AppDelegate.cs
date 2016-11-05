using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Threading;

namespace Example_BackgroundExecution
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected UINavigationController _mainNavController;
		protected Example_BackgroundExecution.Screens.iPhone.HomeScreen_iPhone _iPhoneHome;
		
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
			
			this._iPhoneHome = new Example_BackgroundExecution.Screens.iPhone.HomeScreen_iPhone ();
			this._mainNavController.PushViewController (this._iPhoneHome, false);
			break;
				
			
			this._window.AddSubview (this._mainNavController.View);

			//---- how to check if multi-tasking is supported
			if(UIDevice.CurrentDevice.IsMultitaskingSupported)
			{
				// code here to change your app's behavior
			}
			
			//----
			return true;
		}
		//========================================================================
		
		
		public override void WillEnterForeground (UIApplication application)
		{
			Console.WriteLine("App will enter foreground");	
		}

		/// <summary>
		/// Runs when the activation transitions from running in the background to
		/// being the foreground application.
		/// </summary>
		public override void OnActivated (UIApplication application)
		{
			Console.WriteLine("App is becoming active");	
		}
		
		/// <summary>
		/// 
		/// </summary>
		public override void OnResignActivation (UIApplication application)
		{
			Console.WriteLine("App moving to inactive state.");	
		}
		
		public override void DidEnterBackground (UIApplication application)
		{
			Console.WriteLine("App entering background state.");
			
			//---- if you're creating a VOIP application, this is how you set the keep alive
			//UIApplication.SharedApplication.SetKeepAliveTimout(600, () => { /* keep alive handler code*/ });
			
			//---- register a long running task, and then start it on a new thread so that this method can return
			int taskID = UIApplication.SharedApplication.BeginBackgroundTask(() => {});
			Thread task = new Thread(new ThreadStart(()=>{ FinishLongRunningTask(taskID);}));
			task.Start();
		}
		
		protected void FinishLongRunningTask(int taskID)
		{
			Console.WriteLine("Starting task " + taskID.ToString());

			Console.WriteLine("Background time remaining: " + UIApplication.SharedApplication.BackgroundTimeRemaining.ToString());

			//---- sleep for 5 seconds to simulate a long running task
			Thread.Sleep(5000);
			
			Console.WriteLine("Task " + taskID.ToString() + " finished");
			
			Console.WriteLine("Background time remaining: " + UIApplication.SharedApplication.BackgroundTimeRemaining.ToString());
			
			//---- call our end task
			UIApplication.SharedApplication.EndBackgroundTask(taskID);
		}
		
		/// <summary>
		/// [not guaranteed that this will run]
		/// </summary>
		public override void WillTerminate (UIApplication application)
		{
			Console.WriteLine("App is terminating.");	
		}
		
	}
	//========================================================================
}
