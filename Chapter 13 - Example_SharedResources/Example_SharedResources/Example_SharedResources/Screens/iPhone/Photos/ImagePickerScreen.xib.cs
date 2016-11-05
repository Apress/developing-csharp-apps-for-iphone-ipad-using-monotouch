
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace Example_SharedResources.Screens.iPhone.Photos
{
	public partial class ImagePickerScreen : UIViewController
	{
		protected UIImagePickerController _imagePicker;
		protected FeaturesTableDataSource _tableSource;
		protected PickerDelegate _pickerDelegate;	
		
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public ImagePickerScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public ImagePickerScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public ImagePickerScreen () : base("ImagePickerScreen", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		//================================================================================
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			this.Title = "Image Picker";
			
			//---- populate the features table
			this.PopulateFeaturesTable();
			
			this.btnChoosePhoto.TouchUpInside += (s, e) => {
				//---- create a new picker controller
				this._imagePicker = new UIImagePickerController();
				
				//---- set our source to the photo library
				this._imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
								
				//---- set				
				this._imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
				
				//---- attach the delegate
				this._pickerDelegate = new ImagePickerScreen.PickerDelegate();
				this._imagePicker.Delegate = this._pickerDelegate;
				
				//---- show the picker
				this.NavigationController.PresentModalViewController(this._imagePicker, true);
			};
			
			this.btnTakePhoto.TouchUpInside += (s, e) => {
				//---- create a new picker controller
				this._imagePicker = new UIImagePickerController();
				
				//---- set our source to the camera
				this._imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
								
				//---- set				
				this._imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.Camera);
				
				//---- show the camera controls
				this._imagePicker.ShowsCameraControls = true;

				//---- attach the delegate
				this._pickerDelegate = new ImagePickerScreen.PickerDelegate();
				this._imagePicker.Delegate = this._pickerDelegate;
				
				//---- show the picker
				this.NavigationController.PresentModalViewController(this._imagePicker, true);
				
			};

					
			this.btnPhotoRoll.TouchUpInside += (s, e) => {
				//---- create a new picker controller
				this._imagePicker = new UIImagePickerController();
				
				//---- set our source to the camera
				this._imagePicker.SourceType = UIImagePickerControllerSourceType.SavedPhotosAlbum;
								
				//---- set				
				this._imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.SavedPhotosAlbum);

				//---- attach the delegate
				this._pickerDelegate = new ImagePickerScreen.PickerDelegate();
				this._imagePicker.Delegate = this._pickerDelegate;

				//---- show the picker
				this.NavigationController.PresentModalViewController(this._imagePicker, true);
				
			};
		}
		//================================================================================
		
		//================================================================================
		/// <summary>
		/// Fills the table with a list of available features
		/// </summary>
		protected void PopulateFeaturesTable()
		{
			//---- declare vars
			List<FeatureGroup> features = new List<FeatureGroup>();
			FeatureGroup featGroup;
			string[] mediaTypes;
			
			//---- Sources
			featGroup = new FeatureGroup() { Name = "Sources" };
			featGroup.Features.Add(new Feature() { Name = "Camera", IsAvailable = UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera) });
			featGroup.Features.Add(new Feature() { Name = "Photo Library", IsAvailable = UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary) });
			featGroup.Features.Add(new Feature() { Name = "Saved Photos Album", IsAvailable = UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.SavedPhotosAlbum) });			
			features.Add(featGroup);
			
			//---- Camera and Flash
			featGroup = new FeatureGroup() { Name = "Camera and Flash" };			
			featGroup.Features.Add(new Feature() { Name = "Front Camera", IsAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front) });			
			featGroup.Features.Add(new Feature() { Name = "Front Flash", IsAvailable = UIImagePickerController.IsFlashAvailableForCameraDevice(UIImagePickerControllerCameraDevice.Front) });			
			featGroup.Features.Add(new Feature() { Name = "Rear Camera", IsAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear) });			
			featGroup.Features.Add(new Feature() { Name = "Rear Flash", IsAvailable = UIImagePickerController.IsFlashAvailableForCameraDevice(UIImagePickerControllerCameraDevice.Rear) });
			features.Add(featGroup);
			
			//---- Camera Media Types
			featGroup = new FeatureGroup() { Name = "Camera Media Types" };
			mediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.Camera);
			foreach(var mediaType in mediaTypes)
			{ featGroup.Features.Add(new Feature() { Name = mediaType, IsAvailable = true }); }
			features.Add(featGroup);
			
			//---- Photo Library Media Types
			featGroup = new FeatureGroup() { Name = "Photo Library Media Types" };
			mediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
			foreach(var mediaType in mediaTypes)
			{ featGroup.Features.Add(new Feature() { Name = mediaType, IsAvailable = true }); }
			features.Add(featGroup);
			
			//---- Saved Photos Album Media Types
			featGroup = new FeatureGroup() { Name = "Saved Photos Album Media Types" };
			mediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.SavedPhotosAlbum);
			foreach(var mediaType in mediaTypes)
			{ featGroup.Features.Add(new Feature() { Name = mediaType, IsAvailable = true }); }
			features.Add(featGroup);
			
			//---- bind to the table
			this._tableSource = new ImagePickerScreen.FeaturesTableDataSource(features);
			this.tblFeatures.Source = this._tableSource;
		}
		//================================================================================
		
		//================================================================================
		/// <summary>
		/// Our custom picker delegate. The events haven't been exposed so we have to use a 
		/// delegate.
		/// </summary>
		protected class PickerDelegate : UIImagePickerControllerDelegate
		{
			public override void Canceled (UIImagePickerController picker)
			{
				Console.WriteLine("picker cancelled");
				picker.DismissModalViewControllerAnimated(true);
			}
						
			public override void FinishedPickingMedia (UIImagePickerController picker, NSDictionary info)
			{
				//---- determine what was selected, video or image
				bool isImage = false;
				switch(info[UIImagePickerController.MediaType].ToString())
				{
					case "public.image":
						Console.WriteLine("Image selected");
						isImage = true;
						break;
						Console.WriteLine("Video selected");
					case "public.video":
						break;
				}
				
				//MT BUGBUG:				
				Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");
				
				//---- get common info (shared between images and video)
				//NSUrl referenceURL = info[UIImagePickerController.ReferenceUrl] as NSUrl;
				NSUrl referenceURL = info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
				if(referenceURL != null)
				{
					//----
					Console.WriteLine(referenceURL.ToString());
				}
				
				//---- if it was an image, get the other image info
				if(isImage)
				{
//					//---- get the original image
//					UIImage originalImage = info[UIImagePickerController.OriginalImage] as UIImage;
//					if(originalImage != null)
//					{
//						//---- do something with the image
//						Console.WriteLine("got the original image");
//					}
					
					//---- get the edited image
					UIImage editedImage = info[UIImagePickerController.EditedImage] as UIImage;
					if(editedImage != null)
					{
						//---- do something with the image
						Console.WriteLine("got the edited image");
						
						
					}
					
//					//---- get the cropping, if any
//					try
//					{
//						RectangleF cropRectangle = (RectangleF)info[UIImagePickerController.CropRect];
//						if(cropRectangle != null)
//						{
//							//---- do something with the crop rectangle
//							Console.WriteLine("Got the crop rectangle");
//						}
//					} finally {}
					
//					//----- get the image metadata
//					NSDictionary imageMetadata = info[UIImagePickerController.MediaMetadata] as NSDictionary;
//					if(imageMetadata != null)
//					{
//						//---- do something with the metadata
//						Console.WriteLine("got image metadata");
//					}
					
					
				}
				//---- if it's a video
				else
				{
					//---- get video url
					NSUrl mediaURL = info[UIImagePickerController.MediaURL] as NSUrl;
					if(mediaURL != null)
					{
						//----
						Console.WriteLine(mediaURL.ToString());
					}
					
				}
				
				
				//---- dismiss the picker
				picker.DismissModalViewControllerAnimated(true);
			}			
		}
		//================================================================================
		
		//================================================================================
		#region -= table stuff =-
		
		/// <summary>
		/// Group that holds features available
		/// </summary>
		protected class FeatureGroup
		{
			public string Name { get; set; }
			public List<Feature> Features
			{ get { return this._features; } set { this._features = value; } }
			protected List<Feature> _features = new List<Feature>();
		}
		
		/// <summary>
		/// A feature, such as whether or not the front camera is available
		/// </summary>
		protected class Feature
		{
			public string Name { get; set; }
			public bool IsAvailable { get; set; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		protected class FeaturesTableDataSource : UITableViewSource
		{
			protected List<FeatureGroup> _features { get; set; }
			
			public FeaturesTableDataSource(List<FeatureGroup> features)
			{ this._features = features; }
			
			public override int NumberOfSections (UITableView tableView) { return this._features.Count; }
			
			public override int RowsInSection (UITableView tableview, int section) { return this._features[section].Features.Count; }
			
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell("FeatureCell");
				if(cell == null)
				{ cell = new UITableViewCell(UITableViewCellStyle.Value1, "FeatureCell"); }
				
				cell.TextLabel.Text = this._features[indexPath.Section].Features[indexPath.Row].Name;
				cell.DetailTextLabel.Text = this._features[indexPath.Section].Features[indexPath.Row].IsAvailable.ToString();
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
				return cell;
			}
			
			public override string TitleForHeader (UITableView tableView, int section)
			{ return this._features[section].Name; }
			
			public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{ return 35; }
		}
		
		#endregion
		//================================================================================
	}
}

