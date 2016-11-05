using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Example_AppSettings
{
	public partial class MainViewController_iPhone : UIViewController, IMainScreen
	{
		//========================================================================
		#region -= declarations =-

		/// <summary>
		/// Whether or not the keyboard has actually been closed, we track this because when moving
		/// from field to field, the keyboard open event gets fired, even though the keyboard doesn't
		/// actually close and re-open.
		/// </summary>
		protected bool _keyboardClosed = true;
		
		public UITextField TxtUserName
		{
			get { return this.txtUsername; }
		}
		public UITextField TxtPassword
		{
			get { return this.txtPassword; }
		}
		public UISwitch SwchStaySignedIn
		{
			get { return this.swchStaySignedIn; }
		}
		public UISlider SldrHowBig
		{
			get { return this.sldrHowBig; }
		}
		public UILabel LblFavoriteColor
		{
			get { return this.lblFavoriteColor; }
		}
		public UILabel LblCityOfBirth
		{
			get { return this.lblCityOfBirth; }
		}
		public UITextField TxtFavoriteBand
		{
			get { return this.txtFavoriteBand; }
		}
		public UIButton BtnSaveSettings
		{
			get { return this.btnSaveSettings; }
		}

		
		#endregion
		//========================================================================
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public MainViewController_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public MainViewController_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public MainViewController_iPhone () : base("MainViewController_iPhone", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		//========================================================================
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//---- wire up keyboard dismissal
			this.txtFavoriteBand.ShouldReturn += DismissKeyboard;
			this.txtPassword.ShouldReturn += DismissKeyboard;
			this.txtFavoriteBand.ShouldReturn += DismissKeyboard;
	
			//---- set the content size of the scroll view
			//this.scrlMain.ContentSize = new SizeF (this.View.Frame.Width, this.View.Frame.Height - 44);
			this.scrlMain.ContentSize = new SizeF (this.View.Frame.Width, this.View.Frame.Height - 20);
	
			//---- wire up our keyboard events			
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillShowNotification, delegate(NSNotification n) { this.KeyboardOpenedOrClosed (n, "Open"); });
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, delegate(NSNotification n) { this.KeyboardOpenedOrClosed (n, "Close"); });
			
			//---- set our keyboard closed flag
			this._keyboardClosed = true;

		}
		//========================================================================
		
		//========================================================================
		/// <summary>
		/// Dismisses the keyboard by calling ResignFirstResponder
		/// </summary>
		protected bool DismissKeyboard (UITextField textBox)
		{
			textBox.ResignFirstResponder ();
			return true;
		}
		//========================================================================

		//========================================================================
		/// <summary>
		/// resizes the view when the keyboard comes up or goes away, allows our scroll view to work
		/// </summary>
		protected void KeyboardOpenedOrClosed (NSNotification n, string openOrClose)
		{
			//---- if it's opening
			if (openOrClose == "Open")
			{
				Console.WriteLine ("Keyboard opening");
						
				//---- if it's actually been closed, resize
				if (this._keyboardClosed)
				{
					//---- declare vars
					RectangleF kbdFrame = UIKeyboard.BoundsFromNotification (n);
					double animationDuration = UIKeyboard.AnimationDurationFromNotification (n);
					RectangleF mainViewFrame = this.View.Frame;
					
					//---- resize our frame depending on whether the keyboard pops in or out
					mainViewFrame.Height -= kbdFrame.Height;
					
					//---- apply the size change
					UIView.BeginAnimations ("ResizeForKeyboard");
					UIView.SetAnimationDuration (animationDuration);
					this.View.Frame = mainViewFrame;
					UIView.CommitAnimations ();
					
					//---- track our keyboard variable
					this._keyboardClosed = false;
				}
			}
			else //---- if it's closing, resize
			{
				//---- declare vars
				RectangleF kbdFrame = UIKeyboard.BoundsFromNotification (n);
				double animationDuration = UIKeyboard.AnimationDurationFromNotification (n);
				RectangleF mainViewFrame = this.View.Frame;
				mainViewFrame.Height += kbdFrame.Height;
				
				//---- apply the size change
				UIView.BeginAnimations ("ResizeForKeyboard");
				UIView.SetAnimationDuration (animationDuration);
				this.View.Frame = mainViewFrame;
				UIView.CommitAnimations ();

				//---- track our keyboard variable
				this._keyboardClosed = true;
			}
		}
		//========================================================================
		
		
	}
}
