using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace Example_Drawing.Screens.iPad.Transformations
{
	public class Controller : UIViewController
	{
		UIImageView _imageView;
		
		UIButton _btnUp;
		UIButton _btnRight;
		UIButton _btnDown;
		UIButton _btnLeft;
		UIButton _btnReset;
		UIButton _btnRotateLeft;
		UIButton _btnRotateRight;
		UIButton _btnScaleUp;
		UIButton _btnScaleDown;
		
		float _currentScale, _initialScale = 1.0f;
		PointF _currentLocation, _initialLocation = new PointF(380, 500);
		float _currentRotation , _initialRotation = 0;
		float _movementIncrement = 20;
		float _rotationIncrement = (float)(Math.PI * 2 / 16);
		float _scaleIncrement = 1.5f;


		
		//========================================================================
		#region -= constructors =-

		//========================================================================
		public Controller () : base()
		{
			this._currentScale = this._initialScale;
			this._currentLocation = this._initialLocation;
			this._currentRotation = this._initialRotation;
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
			
			//---- add all of our buttons
			this.InitializeButtons ();
			
			this.DrawScreen ();
		}
		
		protected void DrawScreen ()
		{
			
			//---- create our offscreen bitmap context
			// size
			SizeF bitmapSize = new SizeF (this._imageView.Frame.Size);
			using (CGBitmapContext context = new CGBitmapContext (IntPtr.Zero, (int)bitmapSize.Width, (int)bitmapSize.Height, 8, (int)(4 * bitmapSize.Width), CGColorSpace.CreateDeviceRGB (), CGImageAlphaInfo.PremultipliedFirst))
			{
				//---- save the state of the context while we change the CTM
				context.SaveState ();
				
				//---- draw our circle
				context.SetRGBFillColor (1, 0, 0, 1);
				context.TranslateCTM (this._currentLocation.X, this._currentLocation.Y);
				context.RotateCTM (this._currentRotation);
				context.ScaleCTM (this._currentScale, this._currentScale);
				context.FillRect (new RectangleF (-10, -10, 20, 20));
				
				//---- restore our transformations
				context.RestoreState ();

				//---- draw our coordinates for reference
				this.DrawCoordinateSpace (context);
				
				//---- output the drawing to the view
				this._imageView.Image = UIImage.FromImage (context.ToImage ());
			}
		}
		
		protected void InitializeButtons ()
		{
			this.InitButton (ref this._btnUp, new PointF (600, 20), 50, @"/\");
			this.View.AddSubview (this._btnUp);
			this.InitButton (ref this._btnRight, new PointF (660, 60), 50, ">");
			this.View.AddSubview (this._btnRight);
			this.InitButton (ref this._btnDown, new PointF (600, 100), 50, @"\/");
			this.View.AddSubview (this._btnDown);
			this.InitButton (ref this._btnLeft, new PointF (540, 60), 50, @"<");
			this.View.AddSubview (this._btnLeft);
			this.InitButton (ref this._btnReset, new PointF (600, 60), 50, @"X");
			this.View.AddSubview (this._btnReset);
			this.InitButton (ref this._btnRotateLeft, new PointF (540, 140), 75, "<@");
			this.View.AddSubview (this._btnRotateLeft);
			this.InitButton (ref this._btnRotateRight, new PointF (635, 140), 75, "@>");
			this.View.AddSubview (this._btnRotateRight);
			this.InitButton (ref this._btnScaleUp, new PointF (540, 180), 75, "+");
			this.View.AddSubview (this._btnScaleUp);
			this.InitButton (ref this._btnScaleDown, new PointF (635, 180), 75, "-");
			this.View.AddSubview (this._btnScaleDown);
			
			this._btnReset.TouchUpInside += delegate {
				this._currentScale = this._initialScale;
				this._currentLocation = this._initialLocation;
				this._currentRotation = this._initialRotation;
				this.DrawScreen();
			};
			
			this._btnUp.TouchUpInside += delegate {
				this._currentLocation.Y += this._movementIncrement;
				this.DrawScreen ();
			};
			this._btnDown.TouchUpInside += delegate {
				this._currentLocation.Y -= this._movementIncrement;
				this.DrawScreen ();
			};
			this._btnLeft.TouchUpInside += delegate {
				this._currentLocation.X -= this._movementIncrement;
				this.DrawScreen ();
			};
			this._btnRight.TouchUpInside += delegate {
				this._currentLocation.X += this._movementIncrement;
				this.DrawScreen ();
			};
			this._btnScaleUp.TouchUpInside += delegate {
				this._currentScale = this._currentScale * this._scaleIncrement;
				this.DrawScreen ();
			};
			this._btnScaleDown.TouchUpInside += delegate {
				this._currentScale = this._currentScale / this._scaleIncrement;
				this.DrawScreen ();
			};
			this._btnRotateLeft.TouchUpInside += delegate {
				this._currentRotation += this._rotationIncrement;
				this.DrawScreen ();
			};
			this._btnRotateRight.TouchUpInside += delegate {
				this._currentRotation -= this._rotationIncrement;
				this.DrawScreen ();
			};
			
		}
		
		protected void InitButton (ref UIButton button, PointF location, float width, string text)
		{
			button = UIButton.FromType (UIButtonType.RoundedRect);
			button.SetTitle (text, UIControlState.Normal);
			
			
			button.Frame = new RectangleF (location, new SizeF (width, 33));
			
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

