
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_Touch.Screens.iPhone.Home
{
	public partial class Home_iPhone : UIViewController
	{
		GestureRecognizers.GestureRecognizers_iPhone _gestureScreen;
		GestureRecognizers.CustomCheckmarkGestureRecognizer_iPhone _customGestureScreen;
		SimpleTouch.Touches_iPhone _touchScreen;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public Home_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public Home_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public Home_iPhone () : base("Home_iPhone", null)
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
			
			this.Title = "Working with Touch";
			
			this.btnTouch.TouchUpInside += (s, e) => {
				if(this._touchScreen == null)
				{ this._touchScreen = new Example_Touch.Screens.iPhone.SimpleTouch.Touches_iPhone(); }
				this.NavigationController.PushViewController(this._touchScreen, true);
			};

			this.btnGestureRecognizers.TouchUpInside += (s, e) => {
				if(this._gestureScreen == null)
				{ this._gestureScreen = new Example_Touch.Screens.iPhone.GestureRecognizers.GestureRecognizers_iPhone(); }
				this.NavigationController.PushViewController(this._gestureScreen, true);
			};

			this.btnCustomGestureRecognizer.TouchUpInside += (s, e) => {
				if(this._customGestureScreen == null)
				{ this._customGestureScreen = new Example_Touch.Screens.iPhone.GestureRecognizers.CustomCheckmarkGestureRecognizer_iPhone(); }
				this.NavigationController.PushViewController(this._customGestureScreen, true);
			};

		}
	}
}

