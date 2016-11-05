
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace Example_CoreAnimation.Screens.iPad.LayerAnimation
{
	public partial class LayerAnimationScreen : UIViewController, IDetailView
	{
		protected CGPath _animationPath = new CGPath();
		protected UIImageView _backgroundImage = null;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public LayerAnimationScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public LayerAnimationScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public LayerAnimationScreen () : base("LayerAnimationScreen", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//---- add our background image view that we'll show our path on
			this._backgroundImage = new UIImageView(this.View.Frame);
			this.View.AddSubview(this._backgroundImage);
			
			//---- create our path
			this.CreatePath();
			
			this.btnAnimate.TouchUpInside += (s, e) => {
				
				//---- create a keyframe animation
				CAKeyFrameAnimation keyFrameAnimation = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath("position");
				keyFrameAnimation.Path = this._animationPath;
				keyFrameAnimation.Duration = 3;
				
				keyFrameAnimation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);
				
				this.imgToAnimate.Layer.AddAnimation(keyFrameAnimation, "MoveImage");
				this.imgToAnimate.Layer.Position = new PointF(700, 900);
				
				// later, if we want to stop/remove the animation, we can call RemoveAnimation and pass the name:
				//this.imgToAnimate.Layer.RemoveAnimation("MoveImage");
			};
		}
		
		/// <summary>
		/// Creates the path that we'll use to animate on. Once the path is created, it calls
		/// DrawPathAsBackground to draw the path on the screen.
		/// </summary>
		protected void CreatePath()
		{
			//---- define our path
			PointF curve1StartPoint = new PointF(56, 104);
			PointF curve1ControlPoint1 = new PointF(50, 250);
			PointF curve1ControlPoint2 = new PointF(220, 450);
			PointF curve1EndPoint = new PointF(384, 450);
			PointF curve2ControlPoint1 = new PointF(500, 450);
			PointF curve2ControlPoint2 = new PointF(700, 650);
			PointF curve2EndPoint = new PointF(700, 900);
			this._animationPath.MoveToPoint(curve1StartPoint.X, curve1StartPoint.Y);
			this._animationPath.AddCurveToPoint(curve1ControlPoint1.X, curve1ControlPoint1.Y, curve1ControlPoint2.X, curve1ControlPoint2.Y, curve1EndPoint.X, curve1EndPoint.Y);
			this._animationPath.AddCurveToPoint(curve2ControlPoint1.X, curve2ControlPoint1.Y, curve2ControlPoint2.X, curve2ControlPoint2.Y, curve2EndPoint.X, curve2EndPoint.Y);
			
			//----
			this.DrawPathAsBackground();
		}
		
		/// <summary>
		/// Draws our animation path on the background image, just to show it
		/// </summary>
		protected void DrawPathAsBackground()
		{
			//---- create our offscreen bitmap context
			// size
			SizeF bitmapSize = new SizeF (this.View.Frame.Size);
			using (CGBitmapContext context = new CGBitmapContext (IntPtr.Zero
					, (int)bitmapSize.Width, (int)bitmapSize.Height, 8
					, (int)(4 * bitmapSize.Width), CGColorSpace.CreateDeviceRGB ()
					, CGImageAlphaInfo.PremultipliedFirst))
			{
				//---- convert to View space
				CGAffineTransform affineTransform = CGAffineTransform.MakeIdentity ();
				//---- invert the y axis
				affineTransform.Scale (1, -1);
				//---- move the y axis up
				affineTransform.Translate (0, this.View.Frame.Height);
				context.ConcatCTM (affineTransform);

				//---- actually draw the path
				context.AddPath(this._animationPath);
				context.SetStrokeColorWithColor(UIColor.LightGray.CGColor);
				context.SetLineWidth(3);
				context.StrokePath();
				
				//---- set what we've drawn as the backgound image
				this._backgroundImage.Image = UIImage.FromImage(context.ToImage());
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public void AddContentsButton (UIBarButtonItem button)
		{
			button.Title = "Contents";
			this.tlbrMain.SetItems(new UIBarButtonItem[] { button }, false );
		}
		
		public void RemoveContentsButton ()
		{
			this.tlbrMain.SetItems(new UIBarButtonItem[0], false);
		}

	}
}

