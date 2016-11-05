using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Example_Touch.Code;

namespace Example_Touch.Screens.iPhone.GestureRecognizers
{
	public partial class GenericGestureRecognizers_iPhone : UIViewController
	{
		System.Drawing.RectangleF _originalImageFrame = System.Drawing.RectangleF.Empty;
		
		GestureRecognizer<UITapGestureRecognizer> _tapGesture;
		GestureRecognizer<UIPanGestureRecognizer> _dragGesture;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public GenericGestureRecognizers_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public GenericGestureRecognizers_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public GenericGestureRecognizers_iPhone () : base("GenericGestureRecognizers_iPhone", null)
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
			
			this.Title = "Generic Recognizers";
			
			this.imgDragMe.Image = UIImage.FromBundle("Images/DragMe.png");
			this.imgTapMe.Image = UIImage.FromBundle("Images/DoubleTapMe.png");

			this._originalImageFrame = this.imgDragMe.Frame;
			
			this.WireUpTapGestureRecognizer();
			this.WireUpDragGestureRecognizer();
			
		}

		#region -= tap me button =-
		
		protected void WireUpTapGestureRecognizer()
		{
			//---- create the recognizer
			this._tapGesture = new GestureRecognizer<UITapGestureRecognizer>();
			//---- configure it
			this._tapGesture.Recognizer.NumberOfTapsRequired = 2;
			//---- wire up the event handler
			this._tapGesture.GestureUpdated += (UITapGestureRecognizer r) => {
				this.lblGestureStatus.Text = "tap me image tapped @" + r.LocationOfTouch(0, this.imgTapMe).ToString();				
			};
			//---- add the gesture recognizer to the view
			this.imgTapMe.AddGestureRecognizer(this._tapGesture.Recognizer);
		}
					
		#endregion
		
		#region -= drag me button =-
		
		protected void WireUpDragGestureRecognizer()
		{
			//---- create a new tap gesture
			this._dragGesture = new GestureRecognizer<UIPanGestureRecognizer>();
			//---- wire up the event handler (have to use a selector)
			this._dragGesture.GestureUpdated += (UIPanGestureRecognizer r) => {
				//---- if it's just began, cache the location of the image
				if(r.State == UIGestureRecognizerState.Began)
				{ this._originalImageFrame = this.imgDragMe.Frame; }
				
				if(r.State != (UIGestureRecognizerState.Cancelled | UIGestureRecognizerState.Failed 
					| UIGestureRecognizerState.Possible))
				{
					//---- move the shape by adding the offset to the object's frame
					System.Drawing.PointF offset = r.TranslationInView(this.imgDragMe);
					System.Drawing.RectangleF newFrame = this._originalImageFrame;
					newFrame.Offset(offset.X, offset.Y);			
					this.imgDragMe.Frame = newFrame;
				}
			};
			//---- add the gesture recognizer to the view
			this.imgDragMe.AddGestureRecognizer(this._dragGesture.Recognizer);
		}
		
	
		
		#endregion

	}
}

