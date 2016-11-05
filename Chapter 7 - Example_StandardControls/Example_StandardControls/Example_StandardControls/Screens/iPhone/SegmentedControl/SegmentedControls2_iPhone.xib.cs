
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_StandardControls.Screens.iPhone.SegmentedControl
{
	public partial class SegmentedControls2_iPhone : UIViewController
	{
		UISegmentedControl _segControl1;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public SegmentedControls2_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public SegmentedControls2_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public SegmentedControls2_iPhone () : base("SegmentedControls2_iPhone", null)
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
			
			this.Title = "Programmatic Segmented Controls";
			
			this._segControl1 = new UISegmentedControl ();
			this._segControl1.ControlStyle = UISegmentedControlStyle.Bordered;
			this._segControl1.InsertSegment ("One", 0, false);
			this._segControl1.InsertSegment ("Two", 1, false);
			this._segControl1.SetWidth (100f, 1);
			this._segControl1.SelectedSegment = 1;
			this._segControl1.Frame = new System.Drawing.RectangleF (20, 20, 280, 44);
			this.View.AddSubview (this._segControl1);
			
			this._segControl1.ValueChanged += delegate(object sender, EventArgs e) {
				Console.WriteLine ("Item " + (sender as UISegmentedControl).SelectedSegment.ToString () + " selected");
			};
			
		}
		
	}
}

