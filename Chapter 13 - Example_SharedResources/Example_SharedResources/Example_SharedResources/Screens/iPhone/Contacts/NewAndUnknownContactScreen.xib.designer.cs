// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Example_SharedResources.Screens.iPhone.Contacts {
	
	
	// Base type probably should be MonoTouch.UIKit.UIViewController or subclass
	[MonoTouch.Foundation.Register("NewAndUnknownContactScreen")]
	public partial class NewAndUnknownContactScreen {
		
		private MonoTouch.UIKit.UIView __mt_view;
		
		private MonoTouch.UIKit.UIButton __mt_btnCreateNewContact;
		
		private MonoTouch.UIKit.UIButton __mt_btnPromptForUnknown;
		
		private MonoTouch.UIKit.UITextField __mt_txtFirstName;
		
		private MonoTouch.UIKit.UITextField __mt_txtLastName;
		
		#pragma warning disable 0169
		[MonoTouch.Foundation.Connect("view")]
		private MonoTouch.UIKit.UIView view {
			get {
				this.__mt_view = ((MonoTouch.UIKit.UIView)(this.GetNativeField("view")));
				return this.__mt_view;
			}
			set {
				this.__mt_view = value;
				this.SetNativeField("view", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("btnCreateNewContact")]
		private MonoTouch.UIKit.UIButton btnCreateNewContact {
			get {
				this.__mt_btnCreateNewContact = ((MonoTouch.UIKit.UIButton)(this.GetNativeField("btnCreateNewContact")));
				return this.__mt_btnCreateNewContact;
			}
			set {
				this.__mt_btnCreateNewContact = value;
				this.SetNativeField("btnCreateNewContact", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("btnPromptForUnknown")]
		private MonoTouch.UIKit.UIButton btnPromptForUnknown {
			get {
				this.__mt_btnPromptForUnknown = ((MonoTouch.UIKit.UIButton)(this.GetNativeField("btnPromptForUnknown")));
				return this.__mt_btnPromptForUnknown;
			}
			set {
				this.__mt_btnPromptForUnknown = value;
				this.SetNativeField("btnPromptForUnknown", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("txtFirstName")]
		private MonoTouch.UIKit.UITextField txtFirstName {
			get {
				this.__mt_txtFirstName = ((MonoTouch.UIKit.UITextField)(this.GetNativeField("txtFirstName")));
				return this.__mt_txtFirstName;
			}
			set {
				this.__mt_txtFirstName = value;
				this.SetNativeField("txtFirstName", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("txtLastName")]
		private MonoTouch.UIKit.UITextField txtLastName {
			get {
				this.__mt_txtLastName = ((MonoTouch.UIKit.UITextField)(this.GetNativeField("txtLastName")));
				return this.__mt_txtLastName;
			}
			set {
				this.__mt_txtLastName = value;
				this.SetNativeField("txtLastName", value);
			}
		}
	}
}
