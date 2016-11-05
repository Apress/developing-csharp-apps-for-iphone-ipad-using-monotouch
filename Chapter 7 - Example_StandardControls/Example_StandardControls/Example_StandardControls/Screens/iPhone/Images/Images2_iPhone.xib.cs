
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Example_StandardControls.Screens.iPhone.Images
{
	public partial class Images2_iPhone : UIViewController
	{
		UIImageView _imageView1;
		UIImageView _imgSpinningCircle;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public Images2_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public Images2_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public Images2_iPhone () : base("Images2_iPhone", null)
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
			
			this.Title = "Images";
			
			//---- a simple image
			this._imageView1 = new UIImageView (UIImage.FromBundle ("Images/Icons/Apress-50x50.png"));
			this._imageView1.Frame = new RectangleF (20, 20, this._imageView1.Image.CGImage.Width, this._imageView1.Image.CGImage.Height);
			this.View.AddSubview (this._imageView1);
			
			//---- an animating image
			this._imgSpinningCircle = new UIImageView();
			this._imgSpinningCircle.AnimationImages = new UIImage[] {
				UIImage.FromBundle ("Images/Spinning Circle_1.png")
				, UIImage.FromBundle ("Images/Spinning Circle_2.png")
				, UIImage.FromBundle ("Images/Spinning Circle_3.png")
				, UIImage.FromBundle ("Images/Spinning Circle_4.png")
			};
			this._imgSpinningCircle.AnimationRepeatCount = 0;
			this._imgSpinningCircle.AnimationDuration = .5;
			this._imgSpinningCircle.Frame = new RectangleF(150, 20, 100, 100);
			this.View.AddSubview(this._imgSpinningCircle);
			this._imgSpinningCircle.StartAnimating ();
		}
		
	}
}

