using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace Example_Drawing.Screens.iPad.Images
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
				//----
				
				//---- declare vars
				UIImage apressImage = UIImage.FromFile ("Images/Apress-512x512.png");
				PointF imageOrigin = new PointF ((this._imageView.Frame.Width / 2) - (apressImage.CGImage.Width / 2), (this._imageView.Frame.Height / 2) - (apressImage.CGImage.Height / 2)); 
				RectangleF imageRect = new RectangleF (imageOrigin.X, imageOrigin.Y, apressImage.CGImage.Width, apressImage.CGImage.Height);
				
				//---- draw the image
				context.DrawImage (imageRect, apressImage.CGImage);
				
				
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

