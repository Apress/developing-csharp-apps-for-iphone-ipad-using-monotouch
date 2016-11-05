
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AddressBookUI;
using MonoTouch.AddressBook;

namespace Example_SharedResources.Screens.iPhone.Contacts
{
	public partial class ContactPickerScreen : UIViewController
	{
		//---- you must declare the Address Book Controllers at the class-level, otherwise they'll get 
		// garbage collected when the method that creates them returns. When the events fire, the handlers
		// will have also been GC'd
		protected ABPeoplePickerNavigationController _addressBookPicker;
		protected ABPersonViewController _addressBookViewPerson;
		
		protected ABPerson _selectedPerson = null;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public ContactPickerScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public ContactPickerScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public ContactPickerScreen () : base("ContactPickerScreen", null)
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
			
			this.Title = "Address Book Controllers";
			
			//---- displays the contact picker controller when the choose contact button is clicked
			this.btnChooseContact.TouchUpInside += (s, e) => {
				//---- create the picker control
				this._addressBookPicker = new ABPeoplePickerNavigationController();
				
				//---- in this case, we can call present modal view controller from the nav controller, 
				// but we could have just as well called this.PresentModalViewController(...)
				this.NavigationController.PresentModalViewController(this._addressBookPicker, true);
				
				//---- when cancel is clicked, dismiss the controller
				this._addressBookPicker.Cancelled += (sender, eventArgs) => { this.NavigationController.DismissModalViewControllerAnimated(true); };
				
				//---- when a contact is chosen, populate the page with details and dismiss the controller
				this._addressBookPicker.SelectPerson += (object sender, ABPeoplePickerSelectPersonEventArgs args) => {
					this._selectedPerson = args.Person;
					this.lblFirstName.Text = this._selectedPerson.FirstName;
					this.lblLastName.Text = this._selectedPerson.LastName;
					this.NavigationController.DismissModalViewControllerAnimated(true);
				};
			};
			
			//---- shows the view/edit contact controller when the button is clicked
			this.btnViewSelectedContact.TouchUpInside += (s, e) => {
				
				//---- if a contact hasn't been selected, show an alert and return out
				if(this._selectedPerson == null)
				{ new UIAlertView("Alert", "You must select a contact first.", null, "OK", null).Show(); return; }
				
				//---- instantiate a new controller
				this._addressBookViewPerson = new ABPersonViewController();
				
				//---- set the contact to display
				this._addressBookViewPerson.DisplayedPerson = this._selectedPerson;
				
				//---- allow editing
				this._addressBookViewPerson.AllowsEditing = true;
				
				//---- push the controller onto the nav stack. the view/edit controller requires a nav
				// controller and handles it's own dismissal
				this.NavigationController.PushViewController(this._addressBookViewPerson, true);
			};
		}
	}
}

