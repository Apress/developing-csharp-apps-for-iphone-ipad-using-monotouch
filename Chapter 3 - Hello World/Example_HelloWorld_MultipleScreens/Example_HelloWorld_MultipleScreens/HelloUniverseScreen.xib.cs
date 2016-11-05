
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_HelloWorld_MultipleScreens
{
	public partial class HelloUniverseScreen : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public HelloUniverseScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public HelloUniverseScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public HelloUniverseScreen () : base("HelloUniverseScreen", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		
		
	}
}
