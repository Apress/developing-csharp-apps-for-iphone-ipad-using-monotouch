using System;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;

namespace Example_SplitView.Screens.MainSplitView
{
	public class MainSplitView : UISplitViewController
	{
		#region -= declarations =-
		
		protected Screens.MasterView.MasterTableView _masterView;
		protected Screens.DetailView.DetailViewScreen _detailView;
		
		#endregion
		
		public MainSplitView () : base()
		{
			//---- create our master and detail views
			this._masterView = new Screens.MasterView.MasterTableView ();
			this._detailView = new Screens.DetailView.DetailViewScreen ();

			//---- create an array of controllers from them and then assign it to the 
			// controllers property
			this.ViewControllers = new UIViewController[] { this._masterView, this._detailView };
			
			//---- in this example, i expose an event on the master view called RowClicked, and i listen 
			// for it in here, and then call a method on the detail view to update. this class thereby 
			// becomes the defacto controller for the screen (both views).
			this._masterView.RowClicked += (object sender, MasterView.MasterTableView.RowClickedEventArgs e) => {
				this._detailView.Text = e.Item;
			};
			
			//---- when the master view controller is hid (portrait mode), we add a button to 
			// the detail view that when clicked will show the master view in a popover controller
			this.WillHideViewController += (object sender, UISplitViewHideEventArgs e) => {
				this._detailView.AddContentsButton(e.BarButtonItem);
				//---- set the popover controller
				//this._detailView.PopoverController = e.Pc;
			};

			//---- when the master view controller is shown (landscape mode), remove the button
			// since the controller is shown.
			this.WillShowViewController += (object sender, UISplitViewShowEventArgs e) => {
				this._detailView.RemoveContentsButton();
				//this._detailView.PopoverController = null;
			};
		}
	}
}

