
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_Drawing.Screens.iPad.Home
{
	public partial class HomeScreen : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public HomeScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public HomeScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public HomeScreen () : base("HomeScreen", null)
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
			
			this.btnDrawRectVsPath.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new DrawRectVsPath.Controller (), true);
			};
			this.btnDrawUsingCGBitmapContext.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new DrawOffScreenUsingCGBitmapContext.Controller (), true);
			};
			this.btnDrawUsingLayers.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new Layers.Controller (), true);
			};
			this.btnOnScreenCoords.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new CoordinatesOnScreen.Controller (), true);
			};
			this.btnOffScreenCoords.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new CoordinatesOffScreen.Controller (), true); 
			};
			this.btnOnScreenUncorrectedText.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new OnScreenUncorrectedTextRotation.Controller (), true);
			};
			this.btnImage.TouchUpInside += delegate { 
				this.NavigationController.PushViewController (new Images.Controller (), true);
			};
			this.btnOffScreenFlag.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new FlagOffScreen.Controller (), true);
			};
			this.btnOnScreenFlag.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new FlagOnScreen.Controller (), true);
			};
			this.btnPatterns.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new ColorPattern.Controller (), true);
			};
			this.btnStencilPattern.TouchUpInside += delegate { 
				this.NavigationController.PushViewController (new StencilPattern.Controller (), true); 
			};
			this.btnShadows.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new Shadows.Controller (), true);
			};
			this.btnHitTesting.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new HitTesting.Controller (), true);
			};
			this.btnTouchDrawing.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new TouchDrawing.Controller (), true);
			};
			this.btnTransformations.TouchUpInside += delegate {
				this.NavigationController.PushViewController (new Transformations.Controller (), true);
			};
			
		}
		
		
	}
}

