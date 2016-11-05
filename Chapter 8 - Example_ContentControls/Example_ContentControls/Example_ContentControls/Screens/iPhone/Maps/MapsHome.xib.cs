
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_ContentControls.Screens.iPhone.Maps
{
	public partial class MapsHome : UIViewController
	{
		BasicMapScreen _basicMaps = null;
		AnnotatedMapScreen _mapWithAnnotations = null;
		MapWithOverlayScreen _mapWithOverlay = null;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public MapsHome (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public MapsHome (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public MapsHome () : base("MapsHome", null)
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
			
			this.Title = "Maps";
			
			this.btnBasicMap.TouchUpInside += (s, e) => {
				if(this._basicMaps == null)
				{ this._basicMaps = new BasicMapScreen(); }
				this.NavigationController.PushViewController(this._basicMaps, true);	
			};
			
			this.btnMapWithAnnotations.TouchUpInside += (s, e) => {
				if(this._mapWithAnnotations == null)
				{ this._mapWithAnnotations = new AnnotatedMapScreen(); }
				this.NavigationController.PushViewController(this._mapWithAnnotations, true);	
			};
			
			this.btnMapWithOverlay.TouchUpInside += (s, e) => {
				if(this._mapWithOverlay == null)
				{ this._mapWithOverlay = new MapWithOverlayScreen(); }
				this.NavigationController.PushViewController(this._mapWithOverlay, true);	
			};
		}
	}
}

