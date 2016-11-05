using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.UIKit;
using Tapku;

namespace Example_Tapku
{
	//========================================================================
	public class CoverflowScreen : UIViewController
	{
		protected TKCoverflowView _coverflow;
		protected CoverFlowDataSource _coverflowDataSource;
		
		//========================================================================
		public CoverflowScreen () : base()
		{
			//---- create a new coverflow view control
			float statusBarHeight = UIApplication.SharedApplication.StatusBarFrame.Height;
			this._coverflow = new TKCoverflowView(new RectangleF(0, statusBarHeight
				, UIScreen.MainScreen.ApplicationFrame.Width, UIScreen.MainScreen.ApplicationFrame.Height - statusBarHeight));
			
			//---- create the datasource for it
			List<UIImage> images = new List<UIImage>();
			images.Add(UIImage.FromBundle("Images/Covers/Cover_DeathCabForCutie_PhotoAlbum_Resized.jpg"));
			images.Add(UIImage.FromBundle("Images/Covers/Cover_Earlimart_HymnAndHer_Resized.jpg"));			           
			images.Add(UIImage.FromBundle("Images/Covers/Cover_IronAndWine_WomanKing_Resized.jpg"));			           
			images.Add(UIImage.FromBundle("Images/Covers/Cover_MumfordAndSons_SighNoMore_Resized.jpg"));			           
			images.Add(UIImage.FromBundle("Images/Covers/Cover_Radiohead_OKComputer_Resized.jpg"));			           
			images.Add(UIImage.FromBundle("Images/Covers/Cover_Spiritualized_LadiesAndGentlemen_Resized.jpg"));			           
			images.Add(UIImage.FromBundle("Images/Covers/Cover_Stars_SetYourselfOnFire_Resized.jpg"));			           
			this._coverflowDataSource = new CoverFlowDataSource(images);
			
			//---- assign the datasource to the cover flow
			this._coverflow.DataSource = this._coverflowDataSource;
			this._coverflow.NumberOfCovers = images.Count;
								
			//---- wire up a handler for when a cover is brought to the front
			this._coverflow.CoverWasBroughtToFront += (object s, CoverflowEventArgs e) => {
				Console.WriteLine("Cover [" + e.Index.ToString() + "], brought to front");			
			};
			
			//---- wire up a double tap handler
			this._coverflow.CoverWasDoubleTapped += (object s, CoverflowEventArgs e) => {
				new UIAlertView("Coverflow", "Cover [" + e.Index.ToString() + "] tapped.", null, "OK", null).Show();
			};
			
			this.View = this._coverflow;
		}
		//========================================================================

		//========================================================================
		/// <summary>
		/// The cover flow is meant to work in landscape mode, so we'll force it here.
		/// </summary>
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return (toInterfaceOrientation == UIInterfaceOrientation.LandscapeLeft 
			        || toInterfaceOrientation == UIInterfaceOrientation.LandscapeRight);
		}
		//========================================================================
	
		//========================================================================		
		/// <summary>
		/// Our data source for the cover flow. It works pretty much just like a 
		/// UITableView's data source
		/// </summary>
		public class CoverFlowDataSource : TKCoverflowViewDataSource
		{
			/// <summary>
			/// A List of images we'll show
			/// </summary>
			protected List<UIImage> _coverImages = null;
			
			
			public CoverFlowDataSource(List<UIImage> images) : base()
			{
				this._coverImages = images;
			}
			
			/// <summary>
			/// GetCover is just like GetCell on a UITableView DataSource.
			/// </summary>
			public override TKCoverflowCoverView GetCover (TKCoverflowView coverflowView, int index)
			{
				//---- try to dequeue a reusable cover
				TKCoverflowCoverView view = coverflowView.DequeueReusableCoverView();
				//---- if we didn't get one, create a new one
				if(view == null)
				{
					view = new TKCoverflowCoverView(new RectangleF(0, 0, 244, 244));
					view.Baseline = 224;
				}
								
				//---- set the image
				view.Image = this._coverImages[index];
				
				//---- return the cover view
				return view;
			}
			
		}
		//========================================================================		
	}
}