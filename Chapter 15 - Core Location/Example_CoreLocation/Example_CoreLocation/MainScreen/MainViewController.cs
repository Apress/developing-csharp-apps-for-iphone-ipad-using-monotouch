using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreLocation;
using Example_CoreLocation.MainScreen;

namespace Example_CoreLocation
{
	//========================================================================
	public class MainViewController : UIViewController
	{
		//========================================================================
		#region -= declarations =-
		
		MainViewController_iPhone _mainViewController_iPhone;
		MainViewController_iPad _mainViewController_iPad;

		IMainScreen _mainScreen = null;

		//---- location stuff
		CLLocationManager _iPhoneLocationManager = null;
		// uncomment this if you want to use the delegate pattern
		//LocationDelegate _locationDelegate = null;

		
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

			//---- initialize our location manager and callback handler
			this._iPhoneLocationManager = new CLLocationManager ();
			
			//---- uncomment this if you want to use the delegate pattern:
			//this._locationDelegate = new LocationDelegate (this._mainScreen);
			//this._iPhoneLocationManager.Delegate = this._locationDelegate;
			
			//---- you can set the update threshold and accuracy if you want:
			//this._iPhoneLocationManager.DistanceFilter = 10; // move ten meters before updating
			//this._iPhoneLocationManager.HeadingFilter = 3; // move 3 degrees before updating
			
			//---- you can also set the desired accuracy:
			this._iPhoneLocationManager.DesiredAccuracy = 1000; // 1000 meters/1 kilometer
			// you can also use presets, which simply evalute to a double value:
			//this._iPhoneLocationManager.DesiredAccuracy = CLLocation.AccuracyNearestTenMeters;
			
			//---- handle the updated location method and update the UI
			this._iPhoneLocationManager.UpdatedLocation += (object sender, CLLocationUpdatedEventArgs e) => {
				this._mainScreen.LblAltitude.Text = e.NewLocation.Altitude.ToString () + "meters";
				this._mainScreen.LblLongitude.Text = e.NewLocation.Coordinate.Longitude.ToString () + "º";
				this._mainScreen.LblLatitude.Text = e.NewLocation.Coordinate.Latitude.ToString () + "º";
				this._mainScreen.LblCourse.Text = e.NewLocation.Course.ToString () + "º";
				this._mainScreen.LblSpeed.Text = e.NewLocation.Speed.ToString () + "meters/s";
				
				//---- get the distance from here to paris
				this._mainScreen.LblDistanceToParis.Text = (e.NewLocation.DistanceFrom(new CLLocation(48.857, 2.351)) / 1000).ToString() + "km";
			};
			
			//---- handle the updated heading method and update the UI
			this._iPhoneLocationManager.UpdatedHeading += (object sender, CLHeadingUpdatedEventArgs e) => {
				this._mainScreen.LblMagneticHeading.Text = e.NewHeading.MagneticHeading.ToString () + "º";
				this._mainScreen.LblTrueHeading.Text = e.NewHeading.TrueHeading.ToString () + "º";
			};
			
			//---- start updating our location, et. al.
			if (CLLocationManager.LocationServicesEnabled)
			{ this._iPhoneLocationManager.StartUpdatingLocation (); }
			if (CLLocationManager.HeadingAvailable)
			{ this._iPhoneLocationManager.StartUpdatingHeading (); }

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


		#endregion
		//========================================================================

		
		//========================================================================
		/// <summary>
		/// If you don't want to use events, you could define a delegate to do the 
		/// updates as well, as shown here.
		/// </summary>
		public class LocationDelegate : CLLocationManagerDelegate
		{
			IMainScreen _ms;

			//========================================================================
			public LocationDelegate (IMainScreen mainScreen) : base()
			{
				this._ms = mainScreen;
			}
			//========================================================================
			
			//========================================================================
			public override void UpdatedLocation (CLLocationManager manager, CLLocation newLocation, CLLocation oldLocation)
			{
				this._ms.LblAltitude.Text = newLocation.Altitude.ToString () + "meters";
				this._ms.LblLongitude.Text = newLocation.Coordinate.Longitude.ToString () + "º";
				this._ms.LblLatitude.Text = newLocation.Coordinate.Latitude.ToString () + "º";
				this._ms.LblCourse.Text = newLocation.Course.ToString () + "º";
				this._ms.LblSpeed.Text = newLocation.Speed.ToString () + "meters/s";
				
				//---- get the distance from here to paris
				this._ms.LblDistanceToParis.Text = (newLocation.DistanceFrom(new CLLocation(48.857, 2.351)) / 1000).ToString() + "km";
			}
			//========================================================================

			//========================================================================
			public override void UpdatedHeading (CLLocationManager manager, CLHeading newHeading)
			{
				this._ms.LblMagneticHeading.Text = newHeading.MagneticHeading.ToString () + "º";
				this._ms.LblTrueHeading.Text = newHeading.TrueHeading.ToString () + "º";
			}
			//========================================================================
		}
		//========================================================================

	}
	//========================================================================		
}
