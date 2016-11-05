
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_StandardControls.Screens.iPad.ActionSheets
{
	public partial class ActionSheets_iPad : UIViewController
	{
		UIActionSheet _actionSheet;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public ActionSheets_iPad (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public ActionSheets_iPad (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public ActionSheets_iPad () : base("ActionSheets_iPad", null)
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
			
			this.Title = "Action Sheets";
			
			this.btnSimpleActionSheet.TouchUpInside += HandleBtnSimpleActionSheetTouchUpInside;
			this.btnActionSheetWithOtherButtons.TouchUpInside += HandleBtnActionSheetWithOtherButtonsTouchUpInside;
		}

		protected void HandleBtnSimpleActionSheetTouchUpInside (object sender, EventArgs e)
		{
			//---- create an action sheet using the qualified constructor
			this._actionSheet = new UIActionSheet ("simple action sheet", null, "cancel", "delete", null);
			this._actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) { Console.WriteLine ("Button " + b.ButtonIndex.ToString () + " clicked"); };
			this._actionSheet.ShowInView (this.View);
		}

		protected void HandleBtnActionSheetWithOtherButtonsTouchUpInside (object sender, EventArgs e)
		{
			this._actionSheet = new UIActionSheet ("action sheet with other buttons");
			this._actionSheet.AddButton ("delete");
			this._actionSheet.AddButton ("a different option!");
			this._actionSheet.AddButton ("another option");
			this._actionSheet.DestructiveButtonIndex = 0;
			this._actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) { Console.WriteLine ("Button " + b.ButtonIndex.ToString () + " clicked"); };
			this._actionSheet.ShowInView (this.View);
		}
	}
}

