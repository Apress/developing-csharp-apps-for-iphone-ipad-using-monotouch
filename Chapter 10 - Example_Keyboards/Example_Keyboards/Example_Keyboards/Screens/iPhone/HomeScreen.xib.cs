
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Example_Keyboards.Screens.iPhone
{
	public partial class HomeScreen : UIViewController
	{
		/// <summary>
		/// Track the content size so we can reset to this
		/// </summary>
		RectangleF _contentViewSize = RectangleF.Empty;
		
		#region Constructors
	
		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code
	
		public HomeScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}
	
		[Export("initWithCoder:")]
		public HomeScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}
	
		public HomeScreen () : base("HomeScreen", null)
		{
			Initialize ();
		}
	
		void Initialize ()
		{
			this._contentViewSize = this.View.Frame;
			this.scrlMain.ContentSize = this._contentViewSize.Size;
		}
		
		#endregion
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//---- to hide the keyboard, handle the should return event/property and call resign first responder
			this.txtDefault.ShouldReturn += (textField) => { textField.ResignFirstResponder(); return true; };
			this.txtEmailAddress.ShouldReturn += (textField) => { textField.ResignFirstResponder(); return true; };	
			this.txtNamePhonePad.ShouldReturn += (textField) => { textField.ResignFirstResponder(); return true; };
			this.txtNumberPad.ShouldReturn += (textField) => { textField.ResignFirstResponder(); return true; };
			this.txtNumbersAndPunctuation.ShouldReturn += (textField) => { textField.ResignFirstResponder(); return true; };
			this.txtURL.ShouldReturn += (textField) => { textField.ResignFirstResponder(); return true; };
			
			//---- wire up our keyboard events			
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillShowNotification, delegate(NSNotification n) { this.KeyboardOpenedOrClosed (n, "Open"); });
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, delegate(NSNotification n) { this.KeyboardOpenedOrClosed (n, "Close"); });
			
		}
		
		/// <summary>
		/// resizes the view when the keyboard comes up or goes away, allows our scroll view to work
		/// </summary>
		protected void KeyboardOpenedOrClosed (NSNotification n, string openOrClose)
		{
			//---- if it's opening
			if (openOrClose == "Open")
			{
				Console.WriteLine ("Keyboard opening");
				
				//---- declare vars
				RectangleF kbdFrame = UIKeyboard.BoundsFromNotification (n);
				double animationDuration = UIKeyboard.AnimationDurationFromNotification (n);
				
				//---- get the current view's bounds
				RectangleF newFrame = this.View.Bounds;
				//---- shrink the frame's height
				newFrame.Height -= kbdFrame.Height;

				//---- apply the size change
				UIView.BeginAnimations ("ResizeForKeyboard");
				UIView.SetAnimationDuration (animationDuration);
				this.scrlMain.Frame = newFrame;
				UIView.CommitAnimations ();
			}
			else //---- if it's closing, resize
			{
				//---- declare vars
				double animationDuration = UIKeyboard.AnimationDurationFromNotification (n);
				
				Console.WriteLine("resetting scroll view frame to: " + this._contentViewSize.ToString());
				
				//---- reset the content size back to what it was before
				UIView.BeginAnimations ("ResizeForKeyboard");
				UIView.SetAnimationDuration (animationDuration);
				this.scrlMain.Frame = this._contentViewSize;
				UIView.CommitAnimations ();
			}
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);
			
			Console.WriteLine("Rotated, new view size: " + this.View.Bounds.ToString());
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	
	}
}

