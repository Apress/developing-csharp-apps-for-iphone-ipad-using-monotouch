using System;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;

namespace Example_Touch.Code
{
	public class GestureRecognizer<T> : NSObject where T : UIGestureRecognizer, new()
	{
		/// <summary>
		/// Raised when the gesture state is updated. 
		/// </summary>
		public event Action<T> GestureUpdated;
		
		/// <summary>
		/// The gesture recognizer.
		/// </summary>
		public T Recognizer
		{ get { return this._recognizer;} }
		protected T _recognizer = new T();
		
		/// <summary>
		/// Creates a new generic gesture recognizer.
		/// </summary>
		public GestureRecognizer()
		{
			this._recognizer.AddTarget(this, new Selector("RaiseGestureUpdatedEvent"));
		}        
		
		/// <summary>
		/// Do not call this directly. This is called by the iOS when the 
		/// gesture is recognized.
		/// </summary>
		[Export("RaiseGestureUpdatedEvent")]
		public void RaiseGestureUpdatedEvent(UIGestureRecognizer recognizer)
		{
			if(this.GestureUpdated != null)
			{ this.GestureUpdated(recognizer as T); }
		}
	}
}

