
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_Fonts.Screens.iPhone.FontViewer
{
	public partial class FontViewerScreen_iPhone : UIViewController
	{
		UIFont _font;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public FontViewerScreen_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public FontViewerScreen_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public FontViewerScreen_iPhone (UIFont font) : base("FontViewerScreen_iPhone", null)
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

