using System;
using MonoTouch.CoreGraphics;
using System.Drawing;
using MonoTouch.UIKit;

namespace Example_Drawing.Screens.iPad.Layers
{
	public class View : UIView
	{
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
		/// <param name="rect">
		/// A <see cref="RectangleF"/>
		/// </param>
		public override void Draw (RectangleF rect)
		{
			Console.WriteLine ("Draw() Called");
			base.Draw (rect);
			
			using (CGContext context = UIGraphics.GetCurrentContext ())
			{
				CGAffineTransform affineTransform = context.GetCTM ();
				//affineTransform.Scale (1, -1);
				affineTransform.Translate (1, -1);
				context.ConcatCTM (affineTransform);
				
				//---- fill the background with white
				// set fill color
				UIColor.White.SetFill ();
				//context.SetRGBFillColor (1, 1, 1, 1f);
				// paint
				context.FillRect (rect);
				
				PointF[] myStarPoints = { new PointF (5f, 5f)
					, new PointF (10f, 15f), new PointF (10f, 15f)
					, new PointF (15f, 5f), new PointF (15f, 5f)
					, new PointF (12f, 5f), new PointF (15f, 5f)
					, new PointF (2.5f, 11f), new PointF (2.5f, 11f)
					, new PointF (16.5f, 11f),
				new PointF (16.5f, 11f)
					, new PointF (5f, 5f) };
				
				//---- create the layer
				using (CGLayer starLayer = CGLayer.Create (context, rect.Size))
				{
					//---- set fill to blue
					starLayer.Context.SetRGBFillColor (0f, 0f, 1f, 1f);
					starLayer.Context.AddLines (myStarPoints);
					starLayer.Context.FillPath ();

					//---- draw the layer onto our screen
					float starYPos = 5;
					float starXPos = 5;
					
					for (int row = 0; row < 50; row++)
					{
						//---- reset the x position for each row
						starXPos = 5;
						//----
						for (int col = 0; col < 30; col++)
						{
							context.DrawLayer (starLayer, new PointF (starXPos, starYPos));
							starXPos += 20;
						}
						starYPos += 20;
					}

				
				}
				
			}
		}
	}
}

