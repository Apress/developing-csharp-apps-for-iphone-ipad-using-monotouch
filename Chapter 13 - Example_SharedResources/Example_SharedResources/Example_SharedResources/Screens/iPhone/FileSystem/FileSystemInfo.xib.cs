
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_SharedResources.Screens.iPhone.FileSystem
{
	public partial class FileSystemInfo : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public FileSystemInfo (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public FileSystemInfo (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public FileSystemInfo () : base("FileSystemInfo", null)
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
			
			this.Title = "File System";
			
			this.lblAppHomeDir.Text = NSBundle.MainBundle.BundlePath;
			//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			
		}
	}
}

