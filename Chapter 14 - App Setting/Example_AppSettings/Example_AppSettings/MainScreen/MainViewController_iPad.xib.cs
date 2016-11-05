
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_AppSettings
{
	public partial class MainViewController_iPad : UIViewController, IMainScreen
	{

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

		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public MainViewController_iPad (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public MainViewController_iPad (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public MainViewController_iPad () : base("MainViewController_iPad", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		
		
	}
}
