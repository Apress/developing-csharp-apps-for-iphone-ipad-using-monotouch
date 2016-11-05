using System;
using MonoTouch.UIKit;

namespace Example_Drawing.Screens.iPad.CoordinatesOnScreen
{
	public class Controller : UIViewController
	{

		//========================================================================
		#region -= constructors =-

		//========================================================================
		public Controller () : base()
		{
		}
		//========================================================================

		#endregion
		//========================================================================
		
		public override void LoadView ()
		{
			this.View = new View ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//---- set the background color of the view to white
			this.View.BackgroundColor = UIColor.White;			
		}		
	}
}

