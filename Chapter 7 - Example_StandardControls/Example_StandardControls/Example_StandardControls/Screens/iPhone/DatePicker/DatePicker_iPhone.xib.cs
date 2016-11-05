
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Example_StandardControls.Controls;

namespace Example_StandardControls.Screens.iPhone.DatePicker
{
	public partial class DatePicker_iPhone : UIViewController
	{
		ActionSheetDatePicker _actionSheetDatePicker;
		ActionSheetDatePicker _actionSheetTimerPicker;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public DatePicker_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public DatePicker_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public DatePicker_iPhone () : base("DatePicker_iPhone", null)
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
			
			this.Title = "Date Picker";
			
			//---- setup our custom action sheet date picker
			this._actionSheetDatePicker = new ActionSheetDatePicker (this.View);
			this._actionSheetDatePicker.Title = "Choose Date:";
			this._actionSheetDatePicker.DatePicker.ValueChanged += Handle_actionSheetDatePickerDatePickerValueChanged;
			this._actionSheetDatePicker.DatePicker.Mode = UIDatePickerMode.DateAndTime;
			this._actionSheetDatePicker.DatePicker.MinimumDate = DateTime.Today.AddDays (-7);
			this._actionSheetDatePicker.DatePicker.MaximumDate = DateTime.Today.AddDays (7);
			this.btnChooseDate.TouchUpInside += (s, e) => {
				this._actionSheetDatePicker.Show ();
			};
			
			//---- setup our countdown timer 
			this._actionSheetTimerPicker = new ActionSheetDatePicker (this.View);
			this._actionSheetTimerPicker.Title = "Choose Time:";
			this._actionSheetTimerPicker.DatePicker.Mode = UIDatePickerMode.CountDownTimer;
		}

		protected void Handle_actionSheetDatePickerDatePickerValueChanged (object sender, EventArgs e)
		{
			this.lblDate.Text = (sender as UIDatePicker).Date.ToString ();
		}

	}
}

