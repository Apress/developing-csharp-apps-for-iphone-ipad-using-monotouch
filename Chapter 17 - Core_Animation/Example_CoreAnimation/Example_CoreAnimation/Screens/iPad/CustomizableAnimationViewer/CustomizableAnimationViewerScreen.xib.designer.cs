// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Example_CoreAnimation.Screens.iPad.CustomizableAnimationViewer {
	
	
	// Base type probably should be MonoTouch.UIKit.UIViewController or subclass
	[MonoTouch.Foundation.Register("CustomizableAnimationViewerScreen")]
	public partial class CustomizableAnimationViewerScreen {
		
		private MonoTouch.UIKit.UIView __mt_view;
		
		private MonoTouch.UIKit.UIButton __mt_btnStart;
		
		private MonoTouch.UIKit.UIImageView __mt_imgApress;
		
		private MonoTouch.UIKit.UISegmentedControl __mt_sgmtCurves;
		
		private MonoTouch.UIKit.UISlider __mt_sldrDelay;
		
		private MonoTouch.UIKit.UISlider __mt_sldrDuration;
		
		private MonoTouch.UIKit.UITextField __mt_txtRepeateCount;
		
		private MonoTouch.UIKit.UIToolbar __mt_tlbrMain;
		
		private MonoTouch.UIKit.UISwitch __mt_swtchAutoReverse;
		
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
		
		[MonoTouch.Foundation.Connect("btnStart")]
		private MonoTouch.UIKit.UIButton btnStart {
			get {
				this.__mt_btnStart = ((MonoTouch.UIKit.UIButton)(this.GetNativeField("btnStart")));
				return this.__mt_btnStart;
			}
			set {
				this.__mt_btnStart = value;
				this.SetNativeField("btnStart", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("imgApress")]
		private MonoTouch.UIKit.UIImageView imgApress {
			get {
				this.__mt_imgApress = ((MonoTouch.UIKit.UIImageView)(this.GetNativeField("imgApress")));
				return this.__mt_imgApress;
			}
			set {
				this.__mt_imgApress = value;
				this.SetNativeField("imgApress", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("sgmtCurves")]
		private MonoTouch.UIKit.UISegmentedControl sgmtCurves {
			get {
				this.__mt_sgmtCurves = ((MonoTouch.UIKit.UISegmentedControl)(this.GetNativeField("sgmtCurves")));
				return this.__mt_sgmtCurves;
			}
			set {
				this.__mt_sgmtCurves = value;
				this.SetNativeField("sgmtCurves", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("sldrDelay")]
		private MonoTouch.UIKit.UISlider sldrDelay {
			get {
				this.__mt_sldrDelay = ((MonoTouch.UIKit.UISlider)(this.GetNativeField("sldrDelay")));
				return this.__mt_sldrDelay;
			}
			set {
				this.__mt_sldrDelay = value;
				this.SetNativeField("sldrDelay", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("sldrDuration")]
		private MonoTouch.UIKit.UISlider sldrDuration {
			get {
				this.__mt_sldrDuration = ((MonoTouch.UIKit.UISlider)(this.GetNativeField("sldrDuration")));
				return this.__mt_sldrDuration;
			}
			set {
				this.__mt_sldrDuration = value;
				this.SetNativeField("sldrDuration", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("txtRepeateCount")]
		private MonoTouch.UIKit.UITextField txtRepeateCount {
			get {
				this.__mt_txtRepeateCount = ((MonoTouch.UIKit.UITextField)(this.GetNativeField("txtRepeateCount")));
				return this.__mt_txtRepeateCount;
			}
			set {
				this.__mt_txtRepeateCount = value;
				this.SetNativeField("txtRepeateCount", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("tlbrMain")]
		private MonoTouch.UIKit.UIToolbar tlbrMain {
			get {
				this.__mt_tlbrMain = ((MonoTouch.UIKit.UIToolbar)(this.GetNativeField("tlbrMain")));
				return this.__mt_tlbrMain;
			}
			set {
				this.__mt_tlbrMain = value;
				this.SetNativeField("tlbrMain", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("swtchAutoReverse")]
		private MonoTouch.UIKit.UISwitch swtchAutoReverse {
			get {
				this.__mt_swtchAutoReverse = ((MonoTouch.UIKit.UISwitch)(this.GetNativeField("swtchAutoReverse")));
				return this.__mt_swtchAutoReverse;
			}
			set {
				this.__mt_swtchAutoReverse = value;
				this.SetNativeField("swtchAutoReverse", value);
			}
		}
	}
}