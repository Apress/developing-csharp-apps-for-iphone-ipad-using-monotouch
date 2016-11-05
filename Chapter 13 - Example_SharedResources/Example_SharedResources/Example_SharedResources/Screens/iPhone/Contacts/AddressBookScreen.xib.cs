
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AddressBook;
using MonoTouch.AddressBookUI;
using System.Drawing;

namespace Example_SharedResources.Screens.iPhone.Contacts
{
	public partial class AddressBookScreen : UIViewController
	{
		/// <summary>
		/// Our address book picker control
		/// </summary>
		protected ABPeoplePickerNavigationController _addressBookPicker;
		/// <summary>
		/// The table data source that will bind the phone numbers
		/// </summary>
		protected PhoneNumberTableSource _tableDataSource;
		/// <summary>
		/// The ID of our person
		/// </summary>
		protected int _contactID;
		/// <summary>
		/// Used to resize the scroll view to allow for keyboard
		/// </summary>
		RectangleF _contentViewSize = RectangleF.Empty;
		
		//================================================================================
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public AddressBookScreen (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public AddressBookScreen (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AddressBookScreen () : base("AddressBookScreen", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
			this._contentViewSize = this.View.Frame;
			this.scrlMain.ContentSize = this._contentViewSize.Size;
		}
		
		#endregion
		//================================================================================
		
		//================================================================================
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			this.Title = "Address Book";
			
			//---- add a button to the nav bar that will select a contact to edit
			UIButton btnSelectContact = UIButton.FromType(UIButtonType.RoundedRect);
			btnSelectContact.SetTitle("Select Contact", UIControlState.Normal);
			this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Action,  this.SelectContact), false);
			
			//---- wire up keyboard hiding
			this.txtPhoneLabel.ShouldReturn += (t) => { t.ResignFirstResponder(); return true; };
			this.txtPhoneNumber.ShouldReturn += (t) => { t.ResignFirstResponder(); return true; };
			this.txtFirstName.ShouldReturn += (t) => { t.ResignFirstResponder(); return true; };
			this.txtLastName.ShouldReturn += (t) => { t.ResignFirstResponder(); return true; };
			
			//---- wire up event handlers
			this.btnSaveChanges.TouchUpInside += BtnSaveChangesTouchUpInside;
			this.btnAddPhoneNumber.TouchUpInside += BtnAddPhoneNumberTouchUpInside;

			#region -= Sample code showing how to loop through all the records =-
			//==== This block of code writes out each person contact in the address book and 
			// each phone number for that person to the console, just to illustrate the code 
			// neccessary to access each item
			
			//---- instantiate a reference to the address book
			using(ABAddressBook addressBook = new ABAddressBook())
			{
				//---- if you want to subscribe to changes
				addressBook.ExternalChange += (object sender, ExternalChangeEventArgs e) => {
					// code to deal with changes
				};
				
				//---- for each record
				foreach(ABRecord item in addressBook)
				{
					Console.WriteLine(item.Type.ToString() + " " + item.Id);
					//---- there are two possible record types, person and group
					if(item.Type == ABRecordType.Person)
					{
						//---- since we've already tested it to be a person, just create a shortcut to that
						// type
						ABPerson person = item as ABPerson;
						Console.WriteLine(person.FirstName + " " + person.LastName);
						
						//---- get the phone numbers
						ABMultiValue<string> phones = person.GetPhones();
						foreach(ABMultiValueEntry<string> val in phones)
						{
							Console.Write(val.Label + ": " + val.Value);
						}
					}
				}
				
				//---- save changes (if you were to have made any)
				//addressBook.Save();
				//---- or cancel them
				//addressBook.Revert();
			}
			//====
			#endregion
			
			#region -= keyboard stuff =-
			
			//---- wire up our keyboard events
			NSNotificationCenter.DefaultCenter.AddObserver (
				UIKeyboard.WillShowNotification, delegate(NSNotification n) { this.KeyboardOpenedOrClosed (n, "Open"); }); 
			NSNotificationCenter.DefaultCenter.AddObserver (
				UIKeyboard.WillHideNotification, delegate(NSNotification n) { this.KeyboardOpenedOrClosed (n, "Close"); });
			
			#endregion
		}
		//================================================================================
		
		//================================================================================
		/// <summary>
		/// 
		/// </summary>
		protected void BtnAddPhoneNumberTouchUpInside (object sender, EventArgs e)
		{			
			//---- get a reference to the contact
			using(ABAddressBook addressBook = new ABAddressBook())
			{
				ABPerson contact = addressBook.GetPerson(this._contactID);
				
				//---- get the phones and copy them to a mutable set of multivalues (so we can edit)
				ABMutableMultiValue<string> phones = contact.GetPhones().ToMutableMultiValue();
				
				//---- add the phone number to the phones via the multivalue.Add method
				phones.Add(new NSString(this.txtPhoneLabel.Text), new NSString(this.txtPhoneNumber.Text));
				
				//---- attach the phones back to the contact
				contact.SetPhones(phones);
				
				//---- save the address book changes
				addressBook.Save();
				
				//---- show an alert, letting the user know the number addition was successful
				new UIAlertView("Alert", "Phone Number Added", null, "OK", null).Show();
				
				//---- update the page
				this.PopulatePage(contact);
				
				//---- we have to call reload to refresh the table because the action didn't originate
				// from the table.
				this.tblPhoneNumbers.ReloadData();
			}
		}
		//================================================================================
		
		//================================================================================
		/// <summary>
		/// Called when a phone number is swiped for deletion. Illustrates how to delete a multivalue property
		/// </summary>
		protected void DeletePhoneNumber(int phoneNumberID)
		{
			using(ABAddressBook addressBook = new ABAddressBook())
			{
				ABPerson contact = addressBook.GetPerson(this._contactID);
				
				//---- get the phones and copy them to a mutable set of multivalues (so we can edit)
				ABMutableMultiValue<string> phones = contact.GetPhones().ToMutableMultiValue();
				
				//---- loop backwards and delete the phone number
				for(int i = phones.Count - 1; i >= 0 ; i--)
				{
					if(phones[i].Identifier == phoneNumberID)
					{ phones.RemoveAt(i); }
				}
				
				//---- attach the phones back to the contact
				contact.SetPhones(phones);
				
				//---- save the changes
				addressBook.Save();
				
				//---- show an alert, letting the user know the number deletion was successful
				new UIAlertView("Alert", "Phone Number Deleted", null, "OK", null).Show();
				
				//---- repopulate the page
				this.PopulatePage(contact);
			}
		}
		//================================================================================
		
		protected void BtnSaveChangesTouchUpInside (object sender, EventArgs e)
		{
			
		}
		
		//================================================================================
		protected void PopulatePage(ABPerson contact)
		{
			//---- save the ID of our person
			this._contactID = contact.Id;
			
			//---- set the data on the page
			this.txtFirstName.Text = contact.FirstName;
			this.txtLastName.Text = contact.LastName;
			this._tableDataSource = new AddressBookScreen.PhoneNumberTableSource(contact.GetPhones());
			this.tblPhoneNumbers.Source = this._tableDataSource;
			
			//---- wire up our delete clicked handler
			this._tableDataSource.DeleteClicked += 
			(object sender, PhoneNumberTableSource.PhoneNumberClickedEventArgs e) => { this.DeletePhoneNumber(e.PhoneNumberID); };
		}
		//================================================================================
		
		//================================================================================
		/// <summary>
		/// Opens up a contact picker and then populates the screen, based on the contact chosen
		/// </summary>
		protected void SelectContact(object s, EventArgs e)
		{
			//---- create the picker control
			this._addressBookPicker = new ABPeoplePickerNavigationController();
			
			this.NavigationController.PresentModalViewController(this._addressBookPicker, true);
			
			//---- wire up the cancelled event to dismiss the picker
			this._addressBookPicker.Cancelled += (sender, eventArgs) => { this.NavigationController.DismissModalViewControllerAnimated(true); };
			
			//---- when a contact is chosen, populate the page and then dismiss the picker
			this._addressBookPicker.SelectPerson += (object sender, ABPeoplePickerSelectPersonEventArgs args) => {
				this.PopulatePage(args.Person);				
				this.NavigationController.DismissModalViewControllerAnimated(true);			
			};
		}
		//================================================================================
		
		//================================================================================
		#region -= table binding stuff (not important to understanding the address API) =-
		
		//================================================================================
		/// <summary>
		/// A simple table view source to bind our phone numbers to the table
		/// </summary>
		protected class PhoneNumberTableSource : UITableViewSource
		{	
			public event EventHandler<PhoneNumberClickedEventArgs> DeleteClicked;
			
			protected ABMultiValue<string> _phoneNumbers { get; set; }
			
			public PhoneNumberTableSource(ABMultiValue<string> phoneNumbers)
			{ this._phoneNumbers = phoneNumbers; }
			
			public override int NumberOfSections (UITableView tableView) { return 1; }
			
			public override int RowsInSection (UITableView tableview, int section) { return this._phoneNumbers.Count; }
			
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				EditablePhoneTableCell cell = tableView.DequeueReusableCell("PhoneCell") as EditablePhoneTableCell;
				if(cell == null)
				{ cell = new EditablePhoneTableCell("PhoneCell"); }
				
				cell.PhoneLabel = this._phoneNumbers[indexPath.Row].Label.ToString().Replace("_$!<", "").Replace(">!$_", "");
				cell.PhoneNumber = this._phoneNumbers[indexPath.Row].Value.ToString();
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
				return cell;
			}
			
			public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath) { return true; }
			
			public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
			{ return UITableViewCellEditingStyle.Delete; }
			
			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				if(editingStyle == UITableViewCellEditingStyle.Delete)
				{
					if(this.DeleteClicked != null)
					{ this.DeleteClicked(this, new PhoneNumberClickedEventArgs(this._phoneNumbers[indexPath.Row].Identifier)); }
				}
			}
			
			/// <summary>
			/// We use this so we can pass the id of the phone number that was clicked along with the event
			/// </summary>
			public class PhoneNumberClickedEventArgs : EventArgs
			{
				public int PhoneNumberID { get; set; }
				public PhoneNumberClickedEventArgs(int phoneNumberID) : base()
				{ this.PhoneNumberID = phoneNumberID; }
			}
						                  
		}
		//================================================================================
		
		//================================================================================
		/// <summary>
		/// A simple, two text box cell, that will hold our phone labal, and our phone number.
		/// </summary>
		protected class EditablePhoneTableCell : UITableViewCell
		{	
			//---- label and phone number text boxes
			UITextField _txtLabel = new UITextField(new RectangleF(10, 5, 110, 33));
			UITextField _txtPhoneNumber = new UITextField(new RectangleF(130, 5, 140, 33));
			
			//---- properties
			public string PhoneLabel { get { return this._txtLabel.Text; } set { this._txtLabel.Text = value; } }
			public string PhoneNumber { get { return this._txtPhoneNumber.Text; } set { this._txtPhoneNumber.Text = value; } }
			
			public EditablePhoneTableCell(string reuseIdentifier) : base(UITableViewCellStyle.Default, reuseIdentifier)
			{
				this.AddSubview(this._txtLabel);
				this.AddSubview(this._txtPhoneNumber);
				
				this._txtLabel.ReturnKeyType = UIReturnKeyType.Done;
				this._txtLabel.BorderStyle = UITextBorderStyle.Line;
				this._txtLabel.ShouldReturn += (t) => { t.ResignFirstResponder(); return true; };
				this._txtPhoneNumber.ReturnKeyType= UIReturnKeyType.Done;
				this._txtPhoneNumber.BorderStyle = UITextBorderStyle.Line;
				this._txtPhoneNumber.ShouldReturn += (t) => { t.ResignFirstResponder(); return true; };
			}			
		}
		
		#endregion	                         
		//================================================================================

		//================================================================================
		#region -= keyboard/screen resizing =-
		
		//================================================================================
		/// <summary>
		/// resizes the view when the keyboard comes up or goes away, allows our scroll view to work 
		/// </summary> 
		protected void KeyboardOpenedOrClosed (NSNotification n, string openOrClose) 
		{
			//---- if it's opening 
			if (openOrClose == "Open")
			{
				Console.WriteLine ("Keyboard opening");
				//---- declare vars
				RectangleF kbdFrame = UIKeyboard.BoundsFromNotification (n); 
				double animationDuration = UIKeyboard.AnimationDurationFromNotification (n); 
				RectangleF newFrame = this._contentViewSize;
				//---- resize our frame depending on whether the keyboard pops in or out 
				newFrame.Height -= kbdFrame.Height;
				//---- apply the size change
				UIView.BeginAnimations ("ResizeForKeyboard"); 
				UIView.SetAnimationDuration (animationDuration); 
				this.scrlMain.Frame = newFrame; 
				UIView.CommitAnimations ();
			}
			else //---- if it's closing, resize 
			{
				//---- declare vars 
				double animationDuration = UIKeyboard.AnimationDurationFromNotification (n);
				//---- apply the size change 
				UIView.BeginAnimations ("ResizeForKeyboard"); 
				UIView.SetAnimationDuration (animationDuration); 
				this.scrlMain.Frame = this._contentViewSize; 
				UIView.CommitAnimations ();
			}
		}
		//================================================================================
		
		#endregion
		//================================================================================
	}
}

