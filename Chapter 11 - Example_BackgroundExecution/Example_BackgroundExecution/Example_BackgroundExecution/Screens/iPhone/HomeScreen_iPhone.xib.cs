
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading;

namespace Example_BackgroundExecution.Screens.iPhone
{
	public partial class HomeScreen_iPhone : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public HomeScreen_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public HomeScreen_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public HomeScreen_iPhone () : base("HomeScreen_iPhone", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			this.btnStartLongRunningTask.TouchUpInside += (s, e) => {
				ThreadStart ts = new ThreadStart(() => { this.DoSomething(); });
				ts.Invoke();
			};
		}
		
		public void DoSomething()
		{
			//---- register our background task
			int taskID = UIApplication.SharedApplication.BeginBackgroundTask(() => { this.BackgroundTaskExpiring(); });

			Console.WriteLine("Starting background task " + taskID.ToString());
			
			//---- sleep for five seconds
			Thread.Sleep(5000);			
			
			Console.WriteLine("Background task " + taskID.ToString() + " completed.");
			
			//---- mark our background task as complete
			UIApplication.SharedApplication.EndBackgroundTask(taskID);
		}
		
		public void BackgroundTaskExpiring()
		{
			Console.WriteLine("Running out of time to complete you background task!");
		}
		
	}
}

