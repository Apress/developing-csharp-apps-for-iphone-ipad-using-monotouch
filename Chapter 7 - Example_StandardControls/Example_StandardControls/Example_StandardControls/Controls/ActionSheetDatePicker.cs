using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Example_StandardControls.Controls
{
	/// <summary>
	/// A class to show a date picker on an action sheet. To use, create a new ActionSheetDatePicker,
	/// set the Title, modify any settings on the DatePicker property, and call Show(). It will 
	/// automatically dismiss when the user clicks "Done," or you can call Hide() to dismiss it 
	/// manually.
	/// </summary>
	[MonoTouch.Foundation.Register("SlideOnDatePicker")]
	public class ActionSheetDatePicker
	{
		#region -= declarations =-
		
		UIActionSheet _actionSheet;
		UIButton _doneButton = UIButton.FromType (UIButtonType.RoundedRect);
		UIView _owner;
		UILabel _titleLabel = new UILabel ();
		
		#endregion
		
		#region -= properties =-
		
		/// <summary>
		/// Set any datepicker properties here
		/// </summary>
		public UIDatePicker DatePicker
		{
			get { return this._datePicker; }
			set { this._datePicker = value; }
		}
		UIDatePicker _datePicker = new UIDatePicker(RectangleF.Empty);
		
		/// <summary>
		/// The title that shows up for the date picker
		/// </summary>
		public string Title
		{
			get { return this._titleLabel.Text; }
			set { this._titleLabel.Text = value; }
		}
		
		#endregion
		
		#region -= constructor =-
		
		/// <summary>
		/// 
		/// </summary>
		public ActionSheetDatePicker (UIView owner)
		{
			//---- save our uiview owner
			this._owner = owner;
	
			//---- configure the title label
			this._titleLabel.BackgroundColor = UIColor.Clear;
			this._titleLabel.TextColor = UIColor.LightTextColor;
			this._titleLabel.Font = UIFont.BoldSystemFontOfSize (18);
			
			//---- configure the done button
			this._doneButton.SetTitle ("done", UIControlState.Normal);
			this._doneButton.TouchUpInside += (s, e) => { this._actionSheet.DismissWithClickedButtonIndex (0, true); };
			
			//---- create + configure the action sheet
			this._actionSheet = new UIActionSheet () { Style = UIActionSheetStyle.BlackTranslucent };
			this._actionSheet.Clicked += (s, e) => { Console.WriteLine ("Clicked on item {0}", e.ButtonIndex); };
	
			//---- add our controls to the action sheet
			this._actionSheet.AddSubview (this._datePicker);
			this._actionSheet.AddSubview (this._titleLabel);
			this._actionSheet.AddSubview (this._doneButton);
		}
		
		#endregion
		
		#region -= public methods =-
			
		/// <summary>
		/// Shows the action sheet picker from the view that was set as the owner.
		/// </summary>
		public void Show ()
		{
			//---- declare vars
			float titleBarHeight = 40;
			SizeF doneButtonSize = new SizeF (71, 30);
			SizeF actionSheetSize = new SizeF (this._owner.Frame.Width, this._datePicker.Frame.Height + titleBarHeight);
			RectangleF actionSheetFrame = new RectangleF (0, this._owner.Frame.Height - actionSheetSize.Height
				, actionSheetSize.Width, actionSheetSize.Height);
			
			//---- show the action sheet and add the controls to it
			this._actionSheet.ShowInView (this._owner);
			
			//---- resize the action sheet to fit our other stuff
			this._actionSheet.Frame = actionSheetFrame;
			
			//---- move our picker to be at the bottom of the actionsheet (view coords are relative to the action sheet)
			this._datePicker.Frame = new RectangleF 
				(this._datePicker.Frame.X, titleBarHeight, this._datePicker.Frame.Width, this._datePicker.Frame.Height);
			
			//---- move our label to the top of the action sheet
			this._titleLabel.Frame = new RectangleF (10, 4, this._owner.Frame.Width - 100, 35);
			
			//---- move our button
			this._doneButton.Frame = new RectangleF (actionSheetSize.Width - doneButtonSize.Width - 10, 7, doneButtonSize.Width, doneButtonSize.Height);
		}
		
		/// <summary>
		/// Dismisses the action sheet date picker
		/// </summary>
		public void Hide (bool animated)
		{
			this._actionSheet.DismissWithClickedButtonIndex (0, animated);
		}
		
		#endregion		
	}
}

