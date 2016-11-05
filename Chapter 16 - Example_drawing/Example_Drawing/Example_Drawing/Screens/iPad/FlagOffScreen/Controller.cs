using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace Example_Drawing.Screens.iPad.FlagOffScreen
{
	public class Controller : UIViewController
	{
		UIImageView _imageView;
		
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
			
			//---- instantiate a new image view that takes up the whole screen and add it to 
			// the view hierarchy
			RectangleF imageViewFrame = new RectangleF (0, -this.NavigationController.NavigationBar.Frame.Height, this.View.Frame.Width, this.View.Frame.Height);
			this._imageView = new UIImageView (imageViewFrame);
			this.View.AddSubview (this._imageView);
			
			//---- create our offscreen bitmap context
			// size
			SizeF bitmapSize = new SizeF (this._imageView.Frame.Size);
			using (CGBitmapContext context = new CGBitmapContext (IntPtr.Zero
					, (int)bitmapSize.Width, (int)bitmapSize.Height, 8
					, (int)(4 * bitmapSize.Width), CGColorSpace.CreateDeviceRGB ()
					, CGImageAlphaInfo.PremultipliedFirst))
			{
				//---- draw our coordinates for reference
				this.DrawCoordinateSpace (context);
				
				//---- draw our flag
				this.DrawFlag (context);
				
				//---- add a label
				this.DrawCenteredTextAtPoint (context, 384, 700, "Stars and Stripes", 60);
								
				//---- output the drawing to the view
				this._imageView.Image = UIImage.FromImage (context.ToImage ());
			}
		}
		
		protected void DrawFlag (CGContext context)
		{
			//---- declare vars
			int i, j;
			
			//---- general sizes
			float flagWidth = this._imageView.Frame.Width * .8f;
			float flagHeight = (float)(flagWidth / 1.9);
			PointF flagOrigin = new PointF (this._imageView.Frame.Width * .1f, this._imageView.Frame.Height / 3);
			
			//---- stripe
			float stripeHeight = flagHeight / 13;
			float stripeSpacing = stripeHeight * 2;
			RectangleF stripeRect = new RectangleF (0, 0, flagWidth, stripeHeight);
			
			//---- star field
			float starFieldHeight = 7 * stripeHeight;
			float starFieldWidth = flagWidth * (2f / 5f);
			RectangleF starField = new RectangleF (flagOrigin.X, flagOrigin.Y + (6 * stripeHeight), starFieldWidth, starFieldHeight);
			
			//---- stars
			float starDiameter = flagHeight * 0.0616f;
			float starHorizontalCenterSpacing = (starFieldWidth / 6);
			float starHorizontalPadding = (starHorizontalCenterSpacing / 4);
			float starVerticalCenterSpacing = (starFieldHeight / 5);
			float starVerticalPadding = (starVerticalCenterSpacing / 4);
			PointF firstStarOrigin = new PointF (flagOrigin.X + starHorizontalPadding, flagOrigin.Y + flagHeight - starVerticalPadding - (starVerticalCenterSpacing / 2));
			PointF secondRowFirstStarOrigin = new PointF (firstStarOrigin.X + (starHorizontalCenterSpacing / 2), firstStarOrigin.Y - (starVerticalCenterSpacing / 2));
			
			//---- white background + shadow
			context.SaveState ();
			context.SetShadow (new SizeF (15, -15), 7);
			context.SetRGBFillColor (1, 1, 1, 1);
			context.FillRect (new RectangleF (flagOrigin.X, flagOrigin.Y, flagWidth, flagHeight));
			context.RestoreState ();
			
			//---- create a stripe layer
			using (CGLayer stripeLayer = CGLayer.Create (context, stripeRect.Size))
			{
				//---- set red as the fill color
				// this works
				stripeLayer.Context.SetRGBFillColor (1f, 0f, 0f, 1f);
				// but this doesn't ????
				//stripeLayer.Context.SetFillColor (new float[] { 1f, 0f, 0f, 1f });
				//---- fill the stripe
				stripeLayer.Context.FillRect (stripeRect);
				
				//---- loop through the stripes and draw the layer
				context.SaveState ();
				for (i = 0; i < 7; i++)
				{
					Console.WriteLine ("drawing stripe layer");
					// draw the layer
					context.DrawLayer (stripeLayer, flagOrigin);
					// move the origin
					context.TranslateCTM (0, stripeSpacing);
				}
				context.RestoreState ();
			}
			
			//---- draw the star field
			//BUGBUG: apple bug - this only works on on-screen CGContext and CGLayer
			//context.SetFillColor (new float[] { 0f, 0f, 0.329f, 1.0f });
			context.SetRGBFillColor (0f, 0f, 0.329f, 1.0f);
			context.FillRect (starField);
			
			//---- create the star layer
			using (CGLayer starLayer = CGLayer.Create (context, starField.Size))
			{
				//---- draw the stars
				DrawStar (starLayer.Context, starDiameter);
				
				//---- 6-star rows
				// save state so that as we translate (move the origin around, 
				// it goes back to normal when we restore)
				context.SaveState ();
				context.TranslateCTM (firstStarOrigin.X, firstStarOrigin.Y);
				// loop through each row
				for (j = 0; j < 5; j++)
				{
					// each star in the row
					for (i = 0; i < 6; i++)
					{
						// draw the star, then move the origin to the right
						context.DrawLayer (starLayer, new PointF (0f, 0f));
						context.TranslateCTM (starHorizontalCenterSpacing, 0f);
					}
					// move the row down, and then back left
					context.TranslateCTM ((-i * starHorizontalCenterSpacing), -starVerticalCenterSpacing);
				}
				context.RestoreState ();
				
				//---- 5-star rows			
				context.SaveState ();
				context.TranslateCTM (secondRowFirstStarOrigin.X, secondRowFirstStarOrigin.Y);
				// loop through each row
				for (j = 0; j < 4; j++)
				{
					// each star in the row
					for (i = 0; i < 5; i++)
					{
						context.DrawLayer (starLayer, new PointF (0f, 0f));
						context.TranslateCTM (starHorizontalCenterSpacing, 0);
					}
					context.TranslateCTM ((-i * starHorizontalCenterSpacing), -starVerticalCenterSpacing);
				}
				context.RestoreState ();
			}
		}
		
		/// <summary>
		/// Draws the specified text starting at x,y of the specified height to the context.
		/// </summary>
		protected void DrawTextAtPoint (CGContext context, float x, float y, string text, int textHeight)
		{
			//---- configure font
			context.SelectFont ("Helvetica-Bold", textHeight, CGTextEncoding.MacRoman);
			//---- set it to fill in our text, don't just outline
			context.SetTextDrawingMode (CGTextDrawingMode.Fill);
			//---- call showTextAtPoint
			context.ShowTextAtPoint (x, y, text, text.Length);
		}
		
		/// <summary>
		/// 
		/// </summary>
		protected void DrawCenteredTextAtPoint (CGContext context, float centerX, float y, string text, int textHeight)
		{
			context.SelectFont ("Helvetica-Bold", textHeight, CGTextEncoding.MacRoman);
			context.SetTextDrawingMode (CGTextDrawingMode.Invisible);
			context.ShowTextAtPoint (centerX, y, text, text.Length);
			context.SetTextDrawingMode (CGTextDrawingMode.Fill);
			context.ShowTextAtPoint (centerX - (context.TextPosition.X - centerX) / 2, y, text, text.Length);
		}
		
		/// <summary>
		/// Draws a star at the bottom left of the context of the specified diameter
		/// </summary>
		protected void DrawStar (CGContext context, float starDiameter)
		{
			//---- declare vars
			// 144º
			float theta = 2 * (float)Math.PI * (2f / 5f);
			float radius = starDiameter / 2;

			//---- move up and over
			context.TranslateCTM (starDiameter / 2, starDiameter / 2);
			
			context.MoveTo (0, radius);
			for (int i = 1; i < 5; i++)
			{
				context.AddLineToPoint (radius * (float)Math.Sin (i * theta), radius * (float)Math.Cos (i * theta));
			}
			context.SetRGBFillColor (1, 1, 1, 1);
			context.ClosePath ();
			context.FillPath ();
		}

		
		/// <summary>
		/// Draws our coordinate grid
		/// </summary>
		protected void DrawCoordinateSpace (CGBitmapContext context)
		{
			//---- declare vars
			int remainder;
			int textHeight = 20;
			
			#region -= vertical ticks =-
			
			//---- create our vertical tick lines
			using (CGLayer verticalTickLayer = CGLayer.Create (context, new SizeF (20, 3)))
			{
				//---- draw a single tick
				verticalTickLayer.Context.FillRect (new RectangleF (0, 1, 20, 2));
				
				//---- draw a vertical tick every 20 pixels
				float yPos = 20;
				int numberOfVerticalTicks = ((context.Height / 20) - 1);
				for (int i = 0; i < numberOfVerticalTicks; i++)
				{
					//---- draw the layer
					context.DrawLayer (verticalTickLayer, new PointF (0, yPos));
					
					//---- starting at 40, draw the coordinate text nearly to the top
					if (yPos > 40 && i < (numberOfVerticalTicks - 2))
					{
						//---- draw it every 80 points
						Math.DivRem ((int)yPos, (int)80, out remainder);
						if (remainder == 0)
						{
							this.DrawTextAtPoint (context, 30, (yPos - (textHeight / 2)), yPos.ToString (), textHeight);
						}
					}
					
					//---- increment the position of the next tick
					yPos += 20;
				}
			}
			
			#endregion
			
			#region -= horizontal ticks =-
			
			//---- create our horizontal tick lines
			using (CGLayer horizontalTickLayer = CGLayer.Create (context, new SizeF (3, 20)))
			{
				horizontalTickLayer.Context.FillRect (new RectangleF (1, 0, 2, 20));
				
				//---- draw a horizontal tick every 20 pixels
				float xPos = 20;
				int numberOfHorizontalTicks = ((context.Width / 20) - 1);
				for (int i = 0; i < numberOfHorizontalTicks; i++)
				{
					context.DrawLayer (horizontalTickLayer, new PointF (xPos, 0));
					
					//---- starting at 100, draw the coordinate text nearly to the top
					if (xPos > 100 && i < (numberOfHorizontalTicks - 1))
					{
						//---- draw it every 80 points
						Math.DivRem ((int)xPos, (int)80, out remainder);
						if (remainder == 0)
						{
							this.DrawCenteredTextAtPoint (context, xPos, 30, xPos.ToString (), textHeight);
						}
					}
					
					//---- increment the position of the next tick
					xPos += 20;
				}
			}
			
			#endregion
			
			//---- draw our "origin" text
			DrawTextAtPoint (context, 20, (20 + (textHeight / 2)), "Origin (0,0)", textHeight);
			
		}

	}
}

