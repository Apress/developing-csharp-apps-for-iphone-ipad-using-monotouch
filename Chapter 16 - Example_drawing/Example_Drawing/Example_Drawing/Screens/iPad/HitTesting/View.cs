using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;
using MonoTouch.Foundation;

namespace Example_Drawing.Screens.iPad.HitTesting
{
public class View : UIView
{
	
	CGPath _myRectangleButtonPath;
	bool _touchStartedInPath;
	
	//========================================================================
	#region -= constructors =-

	//========================================================================
	public View () : base()
	{
	}
	//========================================================================

	#endregion
	//========================================================================

	/// <summary>
	/// rect changes depending on if the whole view is being redrawn, or just a section
	/// </summary>
	public override void Draw (RectangleF rect)
	{
		Console.WriteLine ("Draw() Called");
		base.Draw (rect);
		
		using (CGContext context = UIGraphics.GetCurrentContext ())
		{
			//---- draw a rectangle using a path
			this._myRectangleButtonPath = new CGPath ();
			this._myRectangleButtonPath.AddRect (new RectangleF (new PointF (100, 10), new SizeF (200, 400)));
			context.AddPath (this._myRectangleButtonPath);
			context.DrawPath (CGPathDrawingMode.Stroke);
		}
	}
	
	/// <summary>
	/// Raised when a user begins a touch on the screen. We check to see if the touch 
	/// was within our path. If it was, we set the _touchStartedInPath = true so that 
	/// we can track to see if when the user raised their finger, it was also in the path
	/// </summary>
	public override void TouchesBegan (NSSet touches, UIEvent evt)
	{
		base.TouchesBegan (touches, evt);
		//---- get a reference to the touch
		UITouch touch = touches.AnyObject as UITouch;
		//---- make sure there was one
		if (touch != null)
		{
			//---- check to see if the location of the touch was within our path
			if (this._myRectangleButtonPath.ContainsPoint (touch.LocationInView (this), true))
			{
				this._touchStartedInPath = true;
			}
		}
	}
	
	/// <summary>
	/// Raised when a user raises their finger from the screen. Since we need to check to 
	/// see if the user touch started and ended within the path, we have to track to see
	/// when the finger is raised, if it did.
	/// </summary>
	public override void TouchesEnded (NSSet touches, UIEvent evt)
	{
		base.TouchesEnded (touches, evt);
		
		//---- get a reference to any of the touches
		UITouch touch = touches.AnyObject as UITouch;
		
		//---- if there is a touch
		if (touch != null)
		{
			//---- the point of touch
			PointF pt = touch.LocationInView (this);
			
			//---- if the touch ended in the path AND it started in the path
			if (this._myRectangleButtonPath.ContainsPoint (pt, true) && this._touchStartedInPath)
			{
				Console.WriteLine ("touched at location: " + pt.ToString ());
				UIAlertView alert = new UIAlertView ("Hit!", "You sunk my battleship!", null, "OK", null);
				alert.Show ();
			}
		}
		
		//---- reset
		this._touchStartedInPath = false;
	}
	
	/// <summary>
	/// if for some reason the touch was cancelled, we clear our _touchStartedInPath flag
	/// </summary>
	public override void TouchesCancelled (NSSet touches, UIEvent evt)
	{
		base.TouchesCancelled (touches, evt);
		this._touchStartedInPath = false;
	}

}
}

