using System;
using MonoTouch.UIKit;

namespace Example_AppSettings
{
	public interface IMainScreen
	{
		UITextField TxtUserName
		{
			get;
		}
		UITextField TxtPassword
		{
			get;
		}
		UISwitch SwchStaySignedIn
		{
			get;
		}
		UISlider SldrHowBig
		{
			get;
		}
		UILabel LblFavoriteColor
		{
			get;
		}
		UILabel LblCityOfBirth
		{
			get;
		}
		UITextField TxtFavoriteBand
		{
			get;
		}
		UIButton BtnSaveSettings
		{
			get;
		}
	
	}
}
