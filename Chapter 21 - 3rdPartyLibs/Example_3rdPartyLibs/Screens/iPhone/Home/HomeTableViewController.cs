using System;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using Example_3rdPartyLibs.Screens;
using System.Collections.Generic;

namespace Example_3rdPartyLibs.Screens.iPhone.Home
{
	public class HomeNavController : UITableViewController
	{
		//---- declare vars
		protected List<NavItemGroup> _navItems = new List<NavItemGroup>();
		protected NavItemTableSource _tableSource;
		
		/// <summary>
		/// A basic MT.D data set
		/// </summary>
		protected RootElement _mtDialogBasicMenu;
		/// <summary>
		/// We'll use this to create an MT.RootElement that automatically binds to this object.
		/// </summary>
		protected AccountInfo _accountInfo;
		
		protected RootElement _linqBuiltElementTree;
		
		public HomeNavController () : base(UITableViewStyle.Grouped)
		{
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			//---- hide the nav bar when this controller appears
			this.NavigationController.SetNavigationBarHidden (true, true);
		}
		
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			//---- show the nav bar when other controllers appear
			this.NavigationController.SetNavigationBarHidden (false, true);
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavItemGroup navGroup;
			
			this.CreateMTDItems();
			
			//---- create the navigation items
			navGroup = new NavItemGroup ("Three20");
			this._navItems.Add (navGroup);
			navGroup.Items.Add (new NavItem ("Photo Viewer", "", typeof(Three20.PhotoViewerScreen)));

			navGroup = new NavItemGroup ("MonoTouch.Dialog");
			this._navItems.Add (navGroup);
			navGroup.Items.Add (new NavItem ("Basic List", new DialogViewController(this._mtDialogBasicMenu, true)));
			navGroup.Items.Add (new NavItem ("Basic List, Lazy Loaded", typeof(DialogViewController), new object[] { this._mtDialogBasicMenu, true }));
			BindingContext bc = new BindingContext(this, this._accountInfo, "Account Information");
			navGroup.Items.Add (new NavItem ("Screen From a Bound Object", typeof(DialogViewController), new object[] { bc.Root, true }));
			navGroup.Items.Add (new NavItem ("Element tree from LINQ", typeof(DialogViewController), new object[] { this._linqBuiltElementTree, true }));
			
			
			//---- create a table source from our nav items
			this._tableSource = new NavItemTableSource (this.NavigationController, this._navItems);
			
			//---- set the source on the table to our data source
			base.TableView.Source = this._tableSource;
		}
		
		protected void CreateMTDItems()
		{
			
			//---- create an MT.D page content via a RootElement
			this._mtDialogBasicMenu = new RootElement ("Demos"){
				
				new Section ("Element API", "optional footer text"){
					new BooleanElement ("Airplane Mode", false),
					new StringElement("Foo", "FOOper")
				},
				new Section ("Another Section!"){
					new ImageElement(UIImage.FromBundle("Images/Icons/Apress-50x50.png")),
					new EntryElement("Login", "please enter e-mail", ""),
					new EntryElement("Pass", "", "", true),
					new FloatElement(UIImage.FromBundle("Images/Icons/Apress-50x50.png"), UIImage.FromBundle("Images/Icons/Apress-50x50.png"), 40),
					new BadgeElement(UIImage.FromBundle("Images/Icons/Apress-50x50.png"), "badge element!")
				}
			};

			//---- create a new account info object that we'll use with MT.D
			this._accountInfo = new AccountInfo();
			
			//---- create an element tree via LINQ
			this._linqBuiltElementTree = new RootElement ("LINQ root") {
				from x in new string [] { "one", "two", "three" }
					select new Section (x) {
						from y in "Hello:World".Split (':')
							select (Element) new StringElement (y)
					}
			};

		}

		public class AccountInfo
		{
			[Section]
			public bool AirplaneMode;
			
			[Section ("Data Entry", "Your credentials")]
			
			[Entry ("Enter your login name")]
			public string Login;
			
			[Caption ("Password"), Password ("Enter your password")]
			public string passwd;
		}
			
	}
}

