
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Example_StandardControls.Screens.iPhone.PickerView
{
	public partial class PickerWithMultipleComponents_iPhone : UIViewController
	{
		PickerDataModel _pickerDataModel;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public PickerWithMultipleComponents_iPhone (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public PickerWithMultipleComponents_iPhone (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public PickerWithMultipleComponents_iPhone () : base("PickerWithMultipleComponents_iPhone", null)
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
			
			this.Title = "Picker View";
			
			//---- create our simple picker modle
			this._pickerDataModel = new PickerDataModel ();
			
			List<string> items = new List<string> ();
			items.Add ("1");
			items.Add ("2");
			items.Add ("3");
			this._pickerDataModel.Items.Add (0, items);
			
			items = new List<string> ();
			items.Add ("Red");
			items.Add ("Green");
			items.Add ("Blue");
			items.Add ("Alpha");
			this._pickerDataModel.Items.Add (1, items);
			
			//---- set it on our picker class
			this.pkrMain.Model = this._pickerDataModel;
			
			
			//---- wire up the item selected method
			this._pickerDataModel.ValueChanged += (s, e) => {
			//	this.lblSelectedItem.Text = this._pickerDataModel.SelectedItem;
			};
			
			//---- set our initial selection on the label
			//this.lblSelectedItem.Text = this._pickerDataModel.SelectedItem;
		}

		/// <summary>
		/// This is our simple picker model. it uses a list of strings as it's 
		/// data
		/// </summary>
		protected class PickerDataModel : UIPickerViewModel
		{

			public event EventHandler<EventArgs> ValueChanged;

			/// <summary>
			/// The items to show up in the picker
			/// </summary>
			public Dictionary<int, List<string>> Items
			{
				get { return this._items; }
				set { this._items = value; }
			}
			protected Dictionary<int, List<string>> _items = new Dictionary<int, List<string>>();


			/// <summary>
			/// default constructor
			/// </summary>
			public PickerDataModel ()
			{
			}

			/// <summary>
			/// Called by the picker to determine how many rows are in a given spinner item
			/// </summary>
			public override int GetRowsInComponent (UIPickerView picker, int component)
			{
				return this._items[component].Count;
			}

			/// <summary>
			/// called by the picker to get the text for a particular row in a particular 
			/// spinner item
			/// </summary>
			public override string GetTitle (UIPickerView picker, int row, int component)
			{
				return this._items[component][row];
			}

			/// <summary>
			/// called by the picker to get the number of spinner items
			/// </summary>
			public override int GetComponentCount (UIPickerView picker)
			{
				return this._items.Count;
			}
			
			/// <summary>
			/// called when a row is selected in the spinner
			/// </summary>
			public override void Selected (UIPickerView picker, int row, int component)
			{
				//this._selectedIndex = row;
				if (this.ValueChanged != null)
				{
					this.ValueChanged (this, new EventArgs ());
				}
			}
		}
		
		
	}
}
