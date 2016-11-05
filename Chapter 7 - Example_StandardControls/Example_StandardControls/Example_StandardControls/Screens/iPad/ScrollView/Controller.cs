using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace Example_StandardControls.Screens.iPad.ScrollView
{
	public class Controller : UIViewController
	{
		UIScrollView _scrollView;
		

		//========================================================================
		#region -= constructors =-

		//========================================================================
		public Controller () : base()
		{
		}
		//========================================================================

		#endregion
		//========================================================================

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//---- set the background color of the view to white
			this.View.BackgroundColor = UIColor.White;
			
			this._scrollView = new UIScrollView (this.View.Frame);
			this.View.AddSubview (this._scrollView);
			
			
		}
		
	}
}
