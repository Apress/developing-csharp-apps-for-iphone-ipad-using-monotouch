
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;
using MonoTouch.MapKit;

namespace Example_ContentControls.Screens.iPhone.Maps
{
	public partial class AnnotatedMapScreen : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public AnnotatedMapScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public AnnotatedMapScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AnnotatedMapScreen () : base("AnnotatedMapScreen", null)
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
			
			this.Title = "Highland Park, Los Angeles";
			
			//---- create our location and zoom for los angeles
			CLLocationCoordinate2D coords = new CLLocationCoordinate2D(34.120, -118.188);
			MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(20), MilesToLongitudeDegrees(20, coords.Latitude));

			//---- set the coords and zoom on the map
			this.mapMain.Region = new MKCoordinateRegion(coords, span);
			
			//---- set our delegate. we don't actually need a delegate if we want to just drop a pin
			// on there, but if we want to specify anything custom, we do
			this.mapMain.Delegate = new MapDelegate();
			
			//---- add a basic annotation
			this.mapMain.AddAnnotation(new BasicMapAnnotation(new CLLocationCoordinate2D(34.120, -118.188), "Los Angeles", "City of Demons"));
			
			//---- this will cause an error (unrecognized selector set to instance)
			//this.mapMain.AddAnnotationObject(
			//	new BasicMapAnnotationProto() { Coordinate = new CLLocationCoordinate2D(34.120, -118.188), Title = "Los Angeles", Subtitle = "City of Demons" }
			//);
		}
		
		/// <summary>
		/// The map delegate is much like the table delegate.
		/// </summary>
		protected class MapDelegate : MKMapViewDelegate
		{
			protected string _annotationIdentifier = "BasicAnnotation";
			
			/// <summary>
			/// This is very much like the GetCell method on the table delegate
			/// </summary>
			public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, NSObject annotation)
			{
				//---- try and dequeue the annotation view
				MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(this._annotationIdentifier);
				
				//---- if we couldn't dequeue one, create a new one
				if (annotationView == null)
				{ annotationView = new MKPinAnnotationView(annotation, this._annotationIdentifier); }
		      else //---- if we did dequeue one for reuse, assign the annotation to it
		      { annotationView.Annotation = annotation; }
		     
				//---- configure our annotation view properties
				annotationView.CanShowCallout = true;
				(annotationView as MKPinAnnotationView).AnimatesDrop = true;
				(annotationView as MKPinAnnotationView).PinColor = MKPinAnnotationColor.Green;
				annotationView.Selected = true;
				
				//---- you can add an accessory view, in this case, we'll add a button on the right, and an image on the left
				UIButton detailButton = UIButton.FromType(UIButtonType.DetailDisclosure);
				detailButton.TouchUpInside += (s, e) => { new UIAlertView("Annotation Clicked", "You clicked on " +
					(annotation as MKAnnotation).Coordinate.Latitude.ToString() + ", " +
					(annotation as MKAnnotation).Coordinate.Longitude.ToString() , null, "OK", null).Show(); };
				annotationView.RightCalloutAccessoryView = detailButton;
				annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromBundle("Images/Apress-29x29.png"));
				//annotationView.Image = UIImage.FromBundle("Images/Apress-29x29.png");
				
				return annotationView;
			}
			
			/// <summary>
			/// as an optimization, you should override this method to add or remove annotations as the 
			/// map zooms in or out.
			/// </summary>
			public override void RegionChanged (MKMapView mapView, bool animated) {}
		}
		
		/// <summary>
		/// This errors with unrecognized selector error.
		/// </summary>
		[MonoTouch.Foundation.Register("MKAnnotation")]
		protected class BasicMapAnnotationProto : NSObject
		{
			[Export("coordinate")]
			public CLLocationCoordinate2D Coordinate { get; set; }
			public string Title { get; set; }
			public string Subtitle { get; set; }
		}
		
		/// <summary>
		/// MonoTouch doesn't provide even a basic map annotation base class, so this can
		/// serve as one.
		/// </summary>
		protected class BasicMapAnnotation : MKAnnotation
		{
			/// <summary>
			/// The location of the annotation
			/// </summary>
			public override CLLocationCoordinate2D Coordinate { get; set; }
			
			/// <summary>
			/// The title text
			/// </summary>
			public override string Title
			{ get { return this._title; } }
			protected string _title;
			
			/// <summary>
			/// The subtitle text
			/// </summary>
			public override string Subtitle
			{ get { return _subtitle; } }
			protected string _subtitle;
			
			/// <summary>
			/// 
			/// <summary>
			public BasicMapAnnotation (CLLocationCoordinate2D coordinate, string title, string subTitle)
				: base()
			{
				this.Coordinate = coordinate;
				this._title = title;
				this._subtitle = subTitle;
			}
		}
		
		/// <summary>
		/// Converts miles to latitude degrees
		/// </summary>
		public double MilesToLatitudeDegrees(double miles)
		{
			double earthRadius = 3960.0;
			double radiansToDegrees = 180.0/Math.PI;
			return (miles/earthRadius) * radiansToDegrees;
		}

		/// <summary>
		/// Converts miles to longitudinal degrees at a specified latitude
		/// </summary>
		public double MilesToLongitudeDegrees(double miles, double atLatitude)
		{
			double earthRadius = 3960.0;
			double degreesToRadians = Math.PI/180.0;
			double radiansToDegrees = 180.0/Math.PI;

			//---- derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreesToRadians);
    		return (miles / radiusAtLatitude) * radiansToDegrees;
		}

	}
}

