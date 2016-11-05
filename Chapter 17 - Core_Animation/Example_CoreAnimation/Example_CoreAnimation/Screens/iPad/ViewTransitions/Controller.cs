using System;
using MonoTouch.CoreFoundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Example_CoreAnimation.Screens.iPad.ViewTransitions
{
	public class Controller : UIViewController, IDetailView
	{
		protected UIToolbar _toolbar;
		protected TransitionViewController _controller1;
		protected BackTransitionViewController _controller2;
		
		public Controller ()
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this._toolbar = new UIToolbar(new RectangleF(0, 0, this.View.Frame.Width, 44));
			this.View.Add(this._toolbar);
			
			RectangleF mainFrame = new RectangleF(0, this._toolbar.Frame.Height, this.View.Frame.Width, this.View.Frame.Height - this._toolbar.Frame.Height);
			
			this._controller1 = new TransitionViewController();
			this._controller1.View.Frame = mainFrame;
			this._controller2 = new BackTransitionViewController();
			this._controller2.View.Frame = mainFrame;
			
			//this.View.AddSubview(this._controller2.View);
			this.View.AddSubview(this._controller1.View);
			
			//this._controller2.View.Hidden = true;
			
			this._controller1.TransitionClicked += (s, e) => {
				UIView.Animate(1, 0, this._controller1.SelectedTransition, () => {
					this._controller1.View.RemoveFromSuperview();
					this.View.AddSubview(this._controller2.View);
					//this._controller1.View.Hidden = false;
					//this._controller2.View.Hidden = true;
				}, null);
				UIView.BeginAnimations("ViewChange");
//				UIView.SetAnimationTransition(UIViewAnimationTransition.FlipFromLeft, this.View, true);
//				{
//					this._controller1.View.RemoveFromSuperview();
//					this.View.AddSubview(this._controller2.View);
//				}
//				UIView.CommitAnimations();

			};
			
			this._controller2.BackClicked += (s, e) => {
				UIView.Animate(.75, 0, this._controller1.SelectedTransition, () => {
					this._controller2.View.RemoveFromSuperview();
					this.View.AddSubview(this._controller1.View);
				}, null);
			};
		}
		
		/// <summary>
		/// 
		/// </summary>
		public void AddContentsButton (UIBarButtonItem button)
		{
			button.Title = "Contents";
			this._toolbar.SetItems(new UIBarButtonItem[] { button }, false );
		}
		
		public void RemoveContentsButton ()
		{
			this._toolbar.SetItems(new UIBarButtonItem[0], false);
		}

	}
}

