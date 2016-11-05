
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Example_StandardControls.Screens.iPhone.Labels
{
	public partial class LabelsScreen_iPhone : UIViewController
	{
		UILabel _customLabel;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public LabelsScreen_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public LabelsScreen_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public LabelsScreen_iPhone () : base("LabelsScreen_iPhone", null)
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
			
			this._customLabel = new UILabel (new RectangleF (20, 300, 280, 40));
			this._customLabel.Text = "A label created programatically";
			this._customLabel.TextColor = UIColor.Blue;
			this._customLabel.Font = UIFont.FromName ("Helvetica-Bold", 20);
			this._customLabel.AdjustsFontSizeToFitWidth = true;
			this._customLabel.MinimumFontSize = 12;
			this._customLabel.LineBreakMode = UILineBreakMode.TailTruncation;
			this._customLabel.Lines = 1;
			
			this.View.AddSubview (this._customLabel);
			
		}
		
		
	}
}

