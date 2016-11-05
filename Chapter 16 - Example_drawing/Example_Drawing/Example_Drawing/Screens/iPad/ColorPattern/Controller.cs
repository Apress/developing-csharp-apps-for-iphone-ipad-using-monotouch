using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace Example_Drawing.Screens.iPad.ColorPattern
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
				RectangleF patternRect = new RectangleF (0, 0, 16, 16);
				
				//---- set the color space of our fill to be the patter colorspace
				context.SetFillColorSpace (CGColorSpace.CreatePattern (null));
				
				//---- create a new pattern
				CGPattern pattern = new CGPattern (patternRect
					, CGAffineTransform.MakeRotation (.3f), 16, 16, CGPatternTiling.NoDistortion
					, true, DrawPolkaDotPattern);
				
				//---- set our fill as our pattern, color doesn't matter because the pattern handles it
				context.SetFillPattern (pattern, new float[] { 1 });
				
				//---- fill the entire view with that pattern
				context.FillRect (this._imageView.Frame);
					
				//---- output the drawing to the view
				this._imageView.Image = UIImage.FromImage (context.ToImage ());
			}
		
			
		}
		
		/// <summary>
		/// This is our pattern callback. it's called by coregraphics to create 
		/// the pattern base.
		/// </summary>
		protected void DrawPolkaDotPattern (CGContext context)
		{
			context.SetRGBFillColor (.3f, .3f, .3f, 1);
			context.FillEllipseInRect (new RectangleF (4, 4, 8, 8));
		}

		/// <summary>
		/// This is a slightly more complicated draw pattern, but using it is just 
		/// as easy as the previous one. To use this one, simply change "DrawPolkaDotPattern"
		/// in line 54 above to "DrawStarPattern"
		/// </summary>
		protected void DrawStarPattern (CGContext context)
		{
			//---- declare vars
			float starDiameter = 16;
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
			//---- fill our star as dark gray
			context.SetRGBFillColor (.3f, .3f, .3f, 1);
			context.ClosePath ();
			context.FillPath ();
		}
		
	}
}