// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Example_EditableTable.Screens {
	
	
	// Base type probably should be MonoTouch.UIKit.UIViewController or subclass
	[MonoTouch.Foundation.Register("HomeScreen")]
	public partial class HomeScreen {
		
		private MonoTouch.UIKit.UIView __mt_view;
		
		private MonoTouch.UIKit.UIBarButtonItem __mt_btnEdit;
		
		private MonoTouch.UIKit.UITableView __mt_tblMain;
		
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
		
		[MonoTouch.Foundation.Connect("btnEdit")]
		private MonoTouch.UIKit.UIBarButtonItem btnEdit {
			get {
				this.__mt_btnEdit = ((MonoTouch.UIKit.UIBarButtonItem)(this.GetNativeField("btnEdit")));
				return this.__mt_btnEdit;
			}
			set {
				this.__mt_btnEdit = value;
				this.SetNativeField("btnEdit", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("tblMain")]
		private MonoTouch.UIKit.UITableView tblMain {
			get {
				this.__mt_tblMain = ((MonoTouch.UIKit.UITableView)(this.GetNativeField("tblMain")));
				return this.__mt_tblMain;
			}
			set {
				this.__mt_tblMain = value;
				this.SetNativeField("tblMain", value);
			}
		}
	}
}
