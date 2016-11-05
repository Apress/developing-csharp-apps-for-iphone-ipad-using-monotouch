
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_SharedResources.Screens.iPhone.Photos
{
	public partial class CustomCameraViewScreen : UIViewController
	{
		protected UIImagePickerController _imagePicker;
		protected PickerDelegate _pickerDelegate;	
		

		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public CustomCameraViewScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public CustomCameraViewScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public CustomCameraViewScreen () : base("CustomCameraViewScreen", null)
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
			
			this.Title = "Custom Camera View";
			
			this.btnTakePhoto.TouchUpInside += (object sender, EventArgs e) => {
				//---- create a new picker controller
				this._imagePicker = new UIImagePickerController();
				
				//---- set our source to the camera
				this._imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
								
				//---- set				
				this._imagePicker.MediaTypes = new string[] { "public.image" };
				
				//---- show the camera controls
				this._imagePicker.ShowsCameraControls = true;
			
				//UILabel overlay = new UILabel (new System.Drawing.RectangleF (20, 100, 200, 30));
				//overlay.Text = "This is an overlay";
				
				//this._imagePicker.CameraOverlayView = overlay;
				
				
				//BUGBUG: MT/Apple Bug? - this won't display the overlay, but if i add a label, it'll display
				this._imagePicker.CameraOverlayView = new CameraOverlayView(this.view.Frame);
				
				//---- attach the delegate
				this._pickerDelegate = new PickerDelegate();
				this._imagePicker.Delegate = this._pickerDelegate;
				
				//---- show the picker
				this.NavigationController.PresentModalViewController(this._imagePicker, true);

			};
		}

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
				
//				//---- get common info (shared between images and video)
//				NSUrl referenceURL = info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
//				if(referenceURL != null)
//				{
//					//----
//					Console.WriteLine(referenceURL.ToString());
//				}
				
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
					
//					//---- get the edited image
//					UIImage editedImage = info[UIImagePickerController.EditedImage] as UIImage;
//					if(editedImage != null)
//					{
//						//---- do something with the image
//						Console.WriteLine("got the edited image");
//						
//						
//					}
					
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
				
				
				//---- dismiss the picker
				picker.DismissModalViewControllerAnimated(true);
			}			
		}
		//================================================================================

	}
}

