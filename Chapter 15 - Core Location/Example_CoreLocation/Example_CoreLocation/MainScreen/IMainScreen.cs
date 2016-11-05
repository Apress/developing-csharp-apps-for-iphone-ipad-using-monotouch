using System;
using MonoTouch.UIKit;

namespace Example_CoreLocation
{
	public interface IMainScreen
	{
		UILabel LblAltitude
		{
			get;
		}
		UILabel LblLatitude
		{
			get;
		}
		UILabel LblLongitude
		{
			get;
		}
		UILabel LblCourse
		{
			get;
		}
		UILabel LblMagneticHeading
		{
			get;
		}
		UILabel LblSpeed
		{
			get;
		}
		UILabel LblTrueHeading
		{
			get;
		}
		UILabel LblDistanceToParis
		{
			get;
		}
	}
}
