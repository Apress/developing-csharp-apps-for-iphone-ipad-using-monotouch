using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_Touch.Screens.iPhone.SimpleTouch
{
	public partial class Touches_iPhone : UIViewController
	{
		protected bool _touchStartedInside;
		protected bool _imageHighlighted = false;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public Touches_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public Touches_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public Touches_iPhone () : base("Touches_iPhone", null)
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
			
			this.Title = "Touches";
			
			this.imgDragMe.Image = UIImage.FromBundle("Images/DragMe.png");
			this.imgTouchMe.Image = UIImage.FromBundle("Images/TouchMe.png");
			this.imgTapMe.Image = UIImage.FromBundle("Images/DoubleTapMe.png");
		}
		
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			
			//---- we can get the number of fingers from the touch count, but Multitouch must be enabled
			this.lblNumberOfFingers.Text = "Number of fingers: " + touches.Count.ToString();
					
			//---- get the touch
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				Console.WriteLine("screen touched");
		
				//==== IMAGE TOUCH
				if (this.imgTouchMe.Frame.Contains (touch.LocationInView (this.View)))
				{ this.lblTouchStatus.Text = "TouchesBegan"; }
		
				//==== IMAGE DOUBLE TAP
				if(touch.TapCount == 2 && this.imgTapMe.Frame.Contains(touch.LocationInView(this.View)))
				{
					if(this._imageHighlighted)
					{ this.imgTapMe.Image = UIImage.FromBundle("Images/DoubleTapMe.png"); }
					else { this.imgTapMe.Image = UIImage.FromBundle("Images/DoubleTapMe_Highlighted.png"); }
					this._imageHighlighted = !this._imageHighlighted;
				}
		
				//==== IMAGE DRAG
				//---- check to see if the touch started in the dragme image
				if (this.imgDragMe.Frame.Contains(touch.LocationInView (this.View)))
				{ this._touchStartedInside = true; }
			}
		}
		
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);
			//---- get the touch
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				//==== IMAGE TOUCH
				if (this.imgTouchMe.Frame.Contains (touch.LocationInView (this.View)))
				{ this.lblTouchStatus.Text = "TouchesMoved"; }
		
				//==== IMAGE DRAG
				//---- check to see if the touch started in the dragme image
				if (this._touchStartedInside/* && this.imgDragMe.Frame.Contains (touch.LocationInView (this.View))*/)
				{
					//---- move the shape
					float offsetX = touch.PreviousLocationInView(this.View).X - touch.LocationInView(this.View).X;
					float offsetY = touch.PreviousLocationInView(this.View).Y - touch.LocationInView(this.View).Y;
					this.imgDragMe.Frame = new System.Drawing.RectangleF(new PointF(this.imgDragMe.Frame.X - offsetX, this.imgDragMe.Frame.Y - offsetY), this.imgDragMe.Frame.Size);
				
				}
			}
		}
		
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			//---- get the touch
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				//==== IMAGE TOUCH
				if (this.imgTouchMe.Frame.Contains (touch.LocationInView (this.View)))
				{ this.lblTouchStatus.Text = "TouchesEnded"; }
			}
			//---- reset our tracking flags
			this._touchStartedInside = false;
		}
		
		public override void TouchesCancelled (NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled (touches, evt);
			//---- reset our tracking flags
			this._touchStartedInside = false;
		}
	}
}

