using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using Example_StandardControls.Controls;

namespace Example_StandardControls.Screens.iPhone.TapToZoomScrollView
{
	public class Controller : UIViewController
	{
		TapZoomScrollView _scrollView;
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
			
			//---- create our scroll view
			this._scrollView = new TapZoomScrollView (
				new RectangleF (0, 0, this.View.Frame.Width, this.View.Frame.Height - this.NavigationController.NavigationBar.Frame.Height));
			this.View.AddSubview (this._scrollView);
			
			//---- create our image view
			this._imageView = new UIImageView (UIImage.FromFile ("Images/Apress-512x512.png"));
			this._scrollView.ContentSize = this._imageView.Image.Size;
			this._scrollView.MaximumZoomScale = 3f;
			this._scrollView.MinimumZoomScale = .1f;
			this._scrollView.AddSubview (this._imageView);
			
			//---- when the scroll view wants to zoom, it asks for the view to zoom, so 
			// in this case, we tell it that we want it to zoom the image view
			this._scrollView.ViewForZoomingInScrollView += delegate(UIScrollView scrollView) { return this._imageView; };
		}
		
	}
}
