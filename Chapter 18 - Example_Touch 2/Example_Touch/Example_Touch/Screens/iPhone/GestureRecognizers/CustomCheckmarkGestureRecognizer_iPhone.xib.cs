using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Example_Touch.Code;
using MonoTouch.ObjCRuntime;

namespace Example_Touch.Screens.iPhone.GestureRecognizers
{
	public partial class CustomCheckmarkGestureRecognizer_iPhone : UIViewController
	{
		GestureRecognizer<CheckmarkGestureRecognizer> _checkmarkGesture;
		protected bool _checked = false;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public CustomCheckmarkGestureRecognizer_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public CustomCheckmarkGestureRecognizer_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public CustomCheckmarkGestureRecognizer_iPhone () : base("CustomCheckmarkGestureRecognizer_iPhone", null)
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
			
			this.imgCheckmark.Image = UIImage.FromBundle("Images/CheckBox_Start.png");
			
			this.WireUpCheckmarkGestureRecognizer();
		}
		
		protected void WireUpCheckmarkGestureRecognizer()
		{
			//---- create the recognizer
			this._checkmarkGesture = new GestureRecognizer<CheckmarkGestureRecognizer>();
			//---- wire up the event handler
			this._checkmarkGesture.GestureUpdated += (CheckmarkGestureRecognizer r) => {
				if(r.State == (UIGestureRecognizerState.Recognized | UIGestureRecognizerState.Ended))
				{
					if(this._checked)
					{ this.imgCheckmark.Image = UIImage.FromBundle("Images/CheckBox_Unchecked.png"); }
					else { this.imgCheckmark.Image = UIImage.FromBundle("Images/CheckBox_Checked.png"); }
					this._checked = !this._checked;
				}
			};
			//---- add the gesture recognizer to the view
			this.imgCheckmark.AddGestureRecognizer(this._checkmarkGesture.Recognizer);			
		}
	}
}

