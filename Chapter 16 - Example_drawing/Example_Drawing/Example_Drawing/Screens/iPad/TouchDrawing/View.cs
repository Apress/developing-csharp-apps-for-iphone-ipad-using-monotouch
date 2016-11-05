using System;
using MonoTouch.CoreGraphics;
using System.Drawing;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;

namespace Example_Drawing.Screens.iPad.TouchDrawing
{
	public class View : UIView
	{
		List<Spot> _touchSpots = new List<Spot> ();
		SizeF _spotSize = new SizeF(15,15);
		
		//========================================================================
		#region -= constructors =-
	
		//========================================================================
		public View () : base()
		{
		}
		//========================================================================
	
		#endregion
		//========================================================================
	
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
				//---- turn on anti-aliasing
				context.SetAllowsAntialiasing (true);
				
				//---- loop through each spot and draw it
				foreach (Spot spot in this._touchSpots)
				{
					context.SetRGBFillColor (spot.Red, spot.Green, spot.Blue, spot.Alpha);
					context.FillEllipseInRect (new RectangleF (spot.Point, this._spotSize));
				}
			}
		}
		//========================================================================
				
		//========================================================================
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			
			//---- get the touch
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null) { this.AddSpot (touch); }
		}
		//========================================================================
		
		//========================================================================
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);
			
			//---- get the touch
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null) { this.AddSpot (touch); }
		}
		//========================================================================
	
		//========================================================================
		protected void AddSpot (UITouch touch)
		{
			//---- create a random color spot at the point of touch, then add it to the others
			Spot spot = Spot.CreateNewRandomColor (touch.LocationInView (this));
			this._touchSpots.Add (spot);
			//---- tell the OS to redraw
			this.SetNeedsDisplay ();
		}
		//========================================================================
		
		//========================================================================
		protected class Spot
		{
			public PointF Point { get; set; }
			public float Red { get; set; }
			public float Green { get; set; }
			public float Blue { get; set; }
			public float Alpha { get; set; }
			
			public static Spot CreateNewRandomColor(PointF point)
			{
				Random rdm = new Random (Environment.TickCount);
				Spot spot = new View.Spot ()
				{ 
					Red = (float)rdm.NextDouble ()
					, Green = (float)rdm.NextDouble ()
					, Blue = (float)rdm.NextDouble ()
					, Alpha = 1
				};
				spot.Point = point;
				return spot;
			}
		}
		//========================================================================
	}
}

