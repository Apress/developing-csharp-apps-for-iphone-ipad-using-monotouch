using System;
using MonoTouch.UIKit;
using Example_Fonts.Code;
using System.Collections.Generic;

namespace Example_Fonts.Screens.Universal.FontListing
{
	public class FontFamiliesTableViewController : UITableViewController
	{
		//---- declare vars
		List<NavItemGroup> _navItems = new List<NavItemGroup>();
		NavItemTableSource _tableSource;
		
		public FontFamiliesTableViewController () : base(UITableViewStyle.Grouped)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			this.Title = "Fonts";
			
			//---- declare vars
			NavItemGroup navGroup;
			string fontName;
			UIFont font;
			NavItem navItem;
			Type controller;
			
			for (int i = 0; i < UIFont.FamilyNames.Length; i++)
			{
				//---- create a nav group
				navGroup = new NavItemGroup (UIFont.FamilyNames[i]);
				this._navItems.Add (navGroup);
				
				//---- loop through each font name in the family
				for (int j = 0; j < UIFont.FontNamesForFamilyName (UIFont.FamilyNames[i]).Length; j++)
				{
					//---- add an item of that font
					fontName = UIFont.FontNamesForFamilyName (UIFont.FamilyNames[i])[j];
					font = UIFont.FromName (fontName, UIFont.SystemFontSize);
					if((UIApplication.SharedApplication.Delegate as AppDelegate).CurrentDevice == DeviceType.iPad)
					{ controller = typeof(Screens.iPad.FontViewer.FontViewerScreen_iPad); }
					else { controller = typeof(Screens.iPhone.FontViewer.FontViewerScreen_iPhone); }
					navItem = new NavItem (fontName, "", controller, new object[] { font });
					navItem.Font = font;
					navGroup.Items.Add (navItem);
				}
			}
			
			//---- create a table source from our nav items
			this._tableSource = new NavItemTableSource (this.NavigationController, this._navItems);
			
			//---- set the source on the table to our data source
			base.TableView.Source = this._tableSource;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}

