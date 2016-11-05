using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using Example_StandardControls.Controls;

namespace Example_StandardControls.Screens.iPhone.PagerControl
{
	public class Controller_3 : UIViewController
	{
		UILabel _lblMain;


		//========================================================================
		#region -= constructors =-

		//========================================================================
		public Controller_3 () : base()
		{
		}
		//========================================================================

		#endregion
		//========================================================================

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//---- set the background color of the view to white
			this.View.BackgroundColor = UIColor.FromRGB (.5f, .5f, 1);
			
			this._lblMain = new UILabel (new RectangleF (20, 200, 280, 33));
			this._lblMain.Text = "Controller 3";
			this._lblMain.BackgroundColor = UIColor.Clear;
			this.View.AddSubview (this._lblMain);
		}
		
	}
}
