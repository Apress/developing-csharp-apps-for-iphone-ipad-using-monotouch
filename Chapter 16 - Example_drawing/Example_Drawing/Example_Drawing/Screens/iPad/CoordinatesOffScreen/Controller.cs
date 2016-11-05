using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace Example_Drawing.Screens.iPad.CoordinatesOffScreen
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
			SizeF bitmapSize = new SizeF (this.View.Frame.Size);
			using (CGBitmapContext context = new CGBitmapContext (IntPtr.Zero
					, (int)bitmapSize.Width, (int)bitmapSize.Height, 8
					, (int)(4 * bitmapSize.Width), CGColorSpace.CreateDeviceRGB ()
					, CGImageAlphaInfo.PremultipliedFirst))
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
								this.ShowTextAtPoint (context, 30, (yPos - (textHeight / 2)), yPos.ToString (), textHeight);
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
								this.ShowCenteredTextAtPoint (context, xPos, 30, xPos.ToString (), textHeight);
							}
						}
						
						//---- increment the position of the next tick
						xPos += 20;
					}
				}
				
				#endregion
				
				//---- draw our "origin" text
				ShowTextAtPoint (context, 20, (20 + (textHeight / 2)), "Origin (0,0)", textHeight);
				
				#region -= points =-
				
				//---- (250,700)
				context.FillEllipseInRect (new RectangleF (250, 700, 6, 6));
				this.ShowCenteredTextAtPoint (context, 250, 715, "(250,700)", textHeight);				

				//---- (500,300)
				context.FillEllipseInRect (new RectangleF (500, 300, 6, 6));
				this.ShowCenteredTextAtPoint (context, 500, 315, "(500,300)", textHeight);
				
				
				#endregion
				
				//---- output the drawing to the view
				this._imageView.Image = UIImage.FromImage (context.ToImage ());
			}
		}
		
		protected void ShowTextAtPoint (CGContext context, float x, float y, string text, int textHeight)
		{
			context.SelectFont ("Helvetica-Bold", textHeight, CGTextEncoding.MacRoman);
			context.SetTextDrawingMode (CGTextDrawingMode.Fill);
			context.ShowTextAtPoint (x, y, text, text.Length);
		}

		private void ShowCenteredTextAtPoint (CGContext context, float centerX, float y, string text, int textHeight)
		{
			context.SelectFont ("Helvetica-Bold", textHeight, CGTextEncoding.MacRoman);
			context.SetTextDrawingMode (CGTextDrawingMode.Invisible);
			context.ShowTextAtPoint (centerX, y, text, text.Length);
			context.SetTextDrawingMode (CGTextDrawingMode.Fill);
			context.ShowTextAtPoint (centerX - (context.TextPosition.X - centerX) / 2, y, text, text.Length);
		}

	}
}

