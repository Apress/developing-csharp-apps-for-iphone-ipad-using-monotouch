
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_HelloWorld_MultipleScreens
{
	public partial class MainScreen : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public MainScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public MainScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public MainScreen ()// : base("MainScreen", null)
		{
			Initialize ();
			//---- this forces the view to be loaded synchronously
			NSBundle.MainBundle.LoadNib("MainScreen", this, null);
			this.ViewDidLoad();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Console.WriteLine("ViewDidLoad called");
		
			//---- wire up our button events
			this.btnHelloUniverse.TouchUpInside += (s, e) => {
				Console.WriteLine("HelloUniverse button clicked");
				this.navMain.PushViewController (new HelloUniverseScreen (), true); };
			this.btnHelloWorld.TouchUpInside += (s, e) => {
				this.navMain.PushViewController (new HelloWorldScreen (), true); };
		}

		public override UINavigationController NavigationController
		{
			get { return this.navMain; }
		}
		
	}
}
