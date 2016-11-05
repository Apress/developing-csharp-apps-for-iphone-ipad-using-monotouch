
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_Fonts.Screens.iPad.FontViewer
{
	public partial class FontViewerScreen_iPad : UIViewController
	{
		UIFont _font;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public FontViewerScreen_iPad (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public FontViewerScreen_iPad (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public FontViewerScreen_iPad (UIFont font) : base("FontViewerScreen_iPad", null)
		{
			Initialize ();
			this._font = font;
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			this.Title = this._font.Name;
			
			this.txtMain.Font = this._font;
			
			
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
		
		
	}
}

