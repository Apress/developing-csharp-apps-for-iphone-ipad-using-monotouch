using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Example_AppSettings
{
	//========================================================================
	public class MainViewController : UIViewController
	{
		//========================================================================
		#region -= declarations =-
		
		MainViewController_iPhone _mainViewController_iPhone;
		MainViewController_iPad _mainViewController_iPad;

		IMainScreen _mainScreen = null;

		#endregion
		//========================================================================

		//========================================================================
		#region -= constructors =-
		
		//========================================================================
		//
		// Constructor invoked from the NIB loader
		//
		public MainViewController (IntPtr p) : base(p)
		{
		}
		//========================================================================

		//========================================================================
		public MainViewController () : base()
		{
		}
		//========================================================================

		#endregion
		//========================================================================
		
		
		//========================================================================
		public override void ViewDidLoad ()
		{
			//---- all your base
			base.ViewDidLoad ();
			
			//---- load the appropriate view, based on the device type
			this.LoadViewForDevice ();

			//---- wire up our save button
			this._mainScreen.BtnSaveSettings.TouchUpInside += BtnSaveSettingsTouchUpInside;
			
			//---- populate page from settings
			this.PopulateSettings ();

		}
		//========================================================================
		
		//========================================================================
		#region -= protected methods =-
		
		//========================================================================
		/// <summary>
		/// Loads either the iPad or iPhone view, based on the current device
		/// </summary>
		protected void LoadViewForDevice()
		{
			//---- load the appropriate view based on the device
			switch (((AppDelegate)UIApplication.SharedApplication.Delegate).CurrentDevice)
			{
				case DeviceType.iPad:
					this._mainViewController_iPad = new MainViewController_iPad ();
					this.View.AddSubview (this._mainViewController_iPad.View);
					this._mainScreen = this._mainViewController_iPad as IMainScreen;
					break;
				case DeviceType.iPhone:
					this._mainViewController_iPhone = new MainViewController_iPhone ();
					this.View.AddSubview (this._mainViewController_iPhone.View);
					this._mainScreen = this._mainViewController_iPhone as IMainScreen;
					break;
				default:
					break;
			}
		}
		//========================================================================

		//========================================================================
		/// <summary>
		/// Populates our page from settings
		/// </summary>
		protected void PopulateSettings ()
		{
			//---- can access through typed calls:
			this._mainScreen.TxtUserName.Text = NSUserDefaults.StandardUserDefaults.StringForKey ("username");
			//---- can access the dictionary, but must check for null:
			if (NSUserDefaults.StandardUserDefaults["password"] != null)
			{
				this._mainScreen.TxtPassword.Text = NSUserDefaults.StandardUserDefaults["password"].ToString ();
			}
			this._mainScreen.SwchStaySignedIn.On = NSUserDefaults.StandardUserDefaults.BoolForKey ("staySignedIn");
			this._mainScreen.LblFavoriteColor.Text = NSUserDefaults.StandardUserDefaults.StringForKey ("favoriteColor");
			this._mainScreen.LblCityOfBirth.Text = NSUserDefaults.StandardUserDefaults.StringForKey ("cityOfBirth");
			this._mainScreen.SldrHowBig.Value = NSUserDefaults.StandardUserDefaults.FloatForKey ("howBig");
			this._mainScreen.TxtFavoriteBand.Text = NSUserDefaults.StandardUserDefaults.StringForKey ("favoriteBand");
		}
		//========================================================================
		
		//========================================================================
		/// <summary>
		/// Saves our settings
		/// </summary>
		protected void BtnSaveSettingsTouchUpInside (object sender, EventArgs e)
		{
			NSUserDefaults.StandardUserDefaults.SetString (string.IsNullOrEmpty
				(this._mainScreen.TxtUserName.Text) ? "" : this._mainScreen.TxtUserName.Text, "username");
			NSUserDefaults.StandardUserDefaults.SetString (string.IsNullOrEmpty
				(this._mainScreen.TxtPassword.Text) ? "" : this._mainScreen.TxtPassword.Text, "password");
			NSUserDefaults.StandardUserDefaults.SetBool (this._mainScreen.SwchStaySignedIn.On, "staySignedIn");
			NSUserDefaults.StandardUserDefaults.SetString (string.IsNullOrEmpty
				(this._mainScreen.TxtFavoriteBand.Text) ? "" : this._mainScreen.TxtFavoriteBand.Text, "favoriteBand");
			NSUserDefaults.StandardUserDefaults.SetFloat (this._mainScreen.SldrHowBig.Value, "howBig");
		}
		//========================================================================


		#endregion
		//========================================================================
		
	}
	//========================================================================		
}
