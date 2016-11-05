using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Example_StandardControls.Controls
{
	[Register("ActivityIndicatorAlertView")]
	public class ActivityIndicatorAlertView : UIAlertView
	{
		/// <summary>
		/// our activity indicator
		/// </summary>
		UIActivityIndicatorView _activityIndicator;
		/// <summary>
		/// the message label in the window
		/// </summary>
		UILabel _lblMessage;
		
		/// <summary>
		/// The message that appears in the alert above the activity indicator
		/// </summary>
		public string Message
		{
			get { return this._message; }
			set { _message = value; }
		}
		protected string _message;
		
		#region -= constructors =-
		
		public ActivityIndicatorAlertView (IntPtr handle) : base(handle) {}
  
		[Export("initWithCoder:")]  
		public ActivityIndicatorAlertView (NSCoder coder) : base(coder) {}
  
		public ActivityIndicatorAlertView () { }
		
		#endregion
  
		/// <summary>
		/// we use this to resize our alert view. doing it at any other time has 
		/// weird effects because of the lifecycle
		/// </summary>
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			//---- resize the control
			this.Frame = new RectangleF (this.Frame.X, this.Frame.Y, this.Frame.Width, 120);
		}

		/// <summary>
		/// this is where we do the meat of creating our alert, which includes adding 
		/// controls, etc.
		/// </summary>
		public override void Draw (RectangleF rect)
		{
			//---- if the control hasn't been setup yet
			if (this._activityIndicator == null)
			{
				//---- if we have a message
				if (!string.IsNullOrEmpty (this._message))
				{
					this._lblMessage = new UILabel (new RectangleF (20, 10, rect.Width - 40, 33));
					this._lblMessage.BackgroundColor = UIColor.Clear;
					this._lblMessage.TextColor = UIColor.LightTextColor;
					this._lblMessage.TextAlignment = UITextAlignment.Center;
					this._lblMessage.Text = this._message;
					this.AddSubview (this._lblMessage);
				}
				
				//---- instantiate a new activity indicator
				this._activityIndicator = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.White);
				this._activityIndicator.Frame = new RectangleF ((rect.Width / 2) - (this._activityIndicator.Frame.Width / 2)
					, 50, this._activityIndicator.Frame.Width, this._activityIndicator.Frame.Height);
				this.AddSubview (this._activityIndicator);
				this._activityIndicator.StartAnimating ();
			}
			base.Draw (rect);		
		}
		
		/// <summary>
		/// dismisses the alert view. makes sure to call it on the main UI 
		/// thread in case it's called from a worker thread.
		/// </summary>
		public void Hide (bool animated)
		{
			this.InvokeOnMainThread (delegate {
				this.DismissWithClickedButtonIndex (0, animated); 
			});
		}
	}
}

