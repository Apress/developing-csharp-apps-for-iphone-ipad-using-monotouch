using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Example_TableAndCellStyles.Code.CustomCells
{
	public class CustomCellController2 : UIViewController
	{
		UILabel _lblHeading = new UILabel(new RectangleF(11, 0, 195, 46));
		UILabel _lblSubHeading = new UILabel(new RectangleF(20, 45, 186, 30));
		UIImageView _imgMain = new UIImageView(new RectangleF(214, 5, 100, 75));
		
		public UITableViewCell Cell
		{
			get { return this._cell; }
		}
		UITableViewCell _cell = new UITableViewCell(UITableViewCellStyle.Default, "CustomCell2");
		
		public string Heading
		{
			get { return this._lblHeading.Text; }
			set { this._lblHeading.Text = value; }
		}
		public string SubHeading
		{
			get { return this._lblSubHeading.Text; }
			set { this._lblSubHeading.Text = value; }
		}
		
		public UIImage Image
		{
			get { return this._imgMain.Image; }
			set { this._imgMain.Image = value; }
		}

		public CustomCellController2 () : base()
		{	
			base.View.AddSubview(this._cell);
			this._cell.AddSubview(this._lblHeading);
			this._cell.AddSubview(this._lblSubHeading);
			this._cell.AddSubview(this._imgMain);
			
			this._imgMain.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
			this._lblHeading.TextColor = UIColor.Brown;
			this._lblHeading.Font = UIFont.SystemFontOfSize(32);
			this._lblSubHeading.TextColor = UIColor.DarkGray;
			this._lblSubHeading.Font = UIFont.SystemFontOfSize(13);
			
		}
	}
}

