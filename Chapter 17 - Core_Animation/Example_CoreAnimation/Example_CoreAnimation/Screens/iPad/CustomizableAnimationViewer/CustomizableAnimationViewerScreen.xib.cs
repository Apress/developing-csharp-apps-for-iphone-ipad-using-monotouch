
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

namespace Example_CoreAnimation.Screens.iPad.CustomizableAnimationViewer
{
	public partial class CustomizableAnimationViewerScreen : UIViewController, IDetailView
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public CustomizableAnimationViewerScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public CustomizableAnimationViewerScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public CustomizableAnimationViewerScreen () : base("CustomizableAnimationViewerScreen", null)
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
			
			this.btnStart.TouchUpInside += (s, e) => {
				
//				double duration = (double)this.sldrDuration.Value;
//				double delay = (double)this.sldrDelay.Value;
//				UIViewAnimationOptions animationOptions = UIViewAnimationOptions.CurveEaseIn | UIViewAnimationOptions.Repeat;
//				UIView.Animate(duration, delay, animationOptions, () => {
//					//---- move the image one way or the other
//					if(this.imgApress.Frame.Y == 190)
//					{
//						this.imgApress.Frame = new System.Drawing.RectangleF(
//							this.imgApress.Frame.X, this.imgApress.Frame.Y + 400,
//							this.imgApress.Frame.Size.Width, this.imgApress.Frame.Size.Height);
//					}
//					else
//					{
//						this.imgApress.Frame = new System.Drawing.RectangleF(
//							this.imgApress.Frame.X, this.imgApress.Frame.Y - 400,
//							this.imgApress.Frame.Size.Width, this.imgApress.Frame.Size.Height);
//					}
//				}, null);
				
				//---- begin our animation block. the name allows us to refer to it later
				UIView.BeginAnimations("ImageMove");

				UIView.SetAnimationDidStopSelector(new Selector("AnimationStopped"));
				UIView.SetAnimationDelegate(this); //NOTE: you need this for the selector b.s.
				
				//---- animation delay
				UIView.SetAnimationDelay((double)this.sldrDelay.Value);
				
				//---- animation duration
				UIView.SetAnimationDuration((double)this.sldrDuration.Value);
				
				//---- animation curve
				UIViewAnimationCurve curve = UIViewAnimationCurve.EaseInOut;
				switch(this.sgmtCurves.SelectedSegment)
				{
					case 0: curve = UIViewAnimationCurve.EaseInOut; break;
					case 1: curve = UIViewAnimationCurve.EaseIn; break;
					case 2: curve = UIViewAnimationCurve.EaseOut; break;
					case 3: curve = UIViewAnimationCurve.Linear; break;
				}
				UIView.SetAnimationCurve(curve);
				
				//---- repeat count
				UIView.SetAnimationRepeatCount(float.Parse(this.txtRepeateCount.Text));
				
				//---- autorevese when repeating
				UIView.SetAnimationRepeatAutoreverses(this.swtchAutoReverse.On);
				
				//---- move the image one way or the other
				if(this.imgApress.Frame.Y == 190)
				{
					this.imgApress.Frame = new System.Drawing.RectangleF(
						this.imgApress.Frame.X, this.imgApress.Frame.Y + 400,
						this.imgApress.Frame.Size.Width, this.imgApress.Frame.Size.Height);
				}
				else
				{
					this.imgApress.Frame = new System.Drawing.RectangleF(
						this.imgApress.Frame.X, this.imgApress.Frame.Y - 400,
						this.imgApress.Frame.Size.Width, this.imgApress.Frame.Size.Height);
				}
				
				//---- finish our animation block
				UIView.CommitAnimations();
				
				
			};

		}
		
		[Export]
		public void AnimationStopped(string name, NSNumber numFinished, IntPtr context)
		{
			Console.WriteLine("Animation completed");
		}
		
		/// <summary>
		/// 
		/// </summary>
		public void AddContentsButton (UIBarButtonItem button)
		{
			button.Title = "Contents";
			this.tlbrMain.SetItems(new UIBarButtonItem[] { button }, false );
		}
		
		public void RemoveContentsButton ()
		{
			this.tlbrMain.SetItems(new UIBarButtonItem[0], false);
		}
		
		
	}
}

