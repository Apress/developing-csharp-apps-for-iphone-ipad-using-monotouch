using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.CoreFoundation;

namespace Tapku
{
	// by adding the Events=... attribute, btouch will automatically add events from the passed in types to this class.
	// the normal rule applies though, you can use the delegate, or the events, but not both.
	// @interface TKCoverflowView : UIScrollView <UIScrollViewDelegate>
	[BaseType(typeof(UIScrollView), Delegates=new string [] { "Delegate" }, Events=new Type [] { typeof (TKCoverflowViewDelegate)})]
	interface TKCoverflowView
	{
		//==== constructors
		// this isn't in the definition, but it's in the base class, UIView, so we need to implement it
		[Export("initWithFrame:")]
		IntPtr Constructor(RectangleF frame);
	
		
		//==== Properties
		
		// @property (nonatomic, assign) CGSize coverSize; // default 224 x 224
		[Export("coverSize", ArgumentSemantic.Assign)]
		SizeF CoverSize { get; set; }
		
		// @property (nonatomic, assign) int numberOfCovers;
		[Export("numberOfCovers", ArgumentSemantic.Assign)]
		int NumberOfCovers { get; set; }
		
		// @property (nonatomic, assign) float coverSpacing;
		[Export("coverSpacing", ArgumentSemantic.Assign)]
		float CoverSpacing { get; set; }
		
		// @property (nonatomic, assign) float coverAngle;
		[Export("coverAngle", ArgumentSemantic.Assign)]
		float CoverAngle { get; set; }
		
		
		//==== Methods
	
		// - (TKCoverflowCoverView*) dequeueReusableCoverView; // like a tableview
		[Export("dequeueReusableCoverView")]
		TKCoverflowCoverView DequeueReusableCoverView();		
		
		// - (TKCoverflowCoverView*) coverAtIndex:(int)index; // returns nil if cover is outside active range
		[Export("coverAtIndex:")]
		TKCoverflowCoverView GetCover(int index);		
		
		// - (int) indexOfFrontCoverView;
		// bind this as a read-only property
		[Export("indexOfFrontCoverView")]
		int FrontCoverIndex { get; }
		
		// - (void) bringCoverAtIndexToFront:(int)index animated:(BOOL)animated;
		[Export("bringCoverAtIndexToFront:animated:")]
		void BringCoverToFront(int index, bool animated);
		
		
		//==== Delegate properties
		//---- this is the way to do it if you just want to expose only a strongly-typed delegate:
		/*
		// @property (nonatomic, assign) id <TKCoverflowViewDelegate> delegate;
		// hides the underlying delegate property, so we need to add the new attribute
		[Export("delegate", ArgumentSemantic.Assign), New]
		TKCoverflowViewDelegate Delegate { get; set; }
		*/
		
		//---- this is the recommended way to handle delegates, this allows you to 
		// have a weak delegate (any ol' object that implements the methods), or
		// if you wish, a strongly-typed delegate

		// @property (nonatomic, assign) id <TKCoverflowViewDelegate> delegate;
		// hides the underlying delegate property, so we need to add the new attribute
		[Export ("delegate", ArgumentSemantic.Assign), New][NullAllowed]
		NSObject WeakDelegate { get; set; }
		
		[Wrap ("WeakDelegate"), New]
		TKCoverflowViewDelegate Delegate { get; set; }
		
		
		//==== DataSource property
		// @property (nonatomic, assign) id <TKCoverflowViewDataSource> dataSource;
		[Export("dataSource", ArgumentSemantic.Assign)]
		TKCoverflowViewDataSource DataSource { get; set; }

				
	}
	
	// @interface TKCoverflowCoverView : UIView
	[BaseType(typeof(UIView))]
	interface TKCoverflowCoverView
	{		
		//==== constructors
		// this isn't in the definition, but it's in the base class, UIView, so we need to implement it
		[Export("initWithFrame:")]
		IntPtr Constructor(RectangleF frame);
	
		// @property (retain,nonatomic) UIImage *image;
		[Export("image", ArgumentSemantic.Retain)]
		UIImage Image { get; set; }
		
		// @property (assign,nonatomic) float baseline;
		[Export("baseline", ArgumentSemantic.Assign)]
		float Baseline { get; set; }
	}
	
	// @protocol TKCoverflowViewDataSource <NSObject>
	[BaseType (typeof (NSObject))]
	[Model]
	interface TKCoverflowViewDataSource
	{
		// @required
		// - (TKCoverflowCoverView*) coverflowView:(TKCoverflowView*)coverflowView coverAtIndex:(int)index;
		[Export("coverflowView:coverAtIndex:"), Abstract]
		TKCoverflowCoverView GetCover(TKCoverflowView coverflowView, int index);		
	}
	
	// @protocol TKCoverflowViewDelegate <NSObject>
	[BaseType (typeof (NSObject))]
	[Model]
	interface TKCoverflowViewDelegate
	{
		// @required
		// - (void) coverflowView:(TKCoverflowView*)coverflowView coverAtIndexWasBroughtToFront:(int)index;
		[Export("coverflowView:coverAtIndexWasBroughtToFront:"), EventArgs ("Coverflow"), Abstract]
		void CoverWasBroughtToFront(TKCoverflowView coverflowView, int index);

		// @optional
		// - (void) coverflowView:(TKCoverflowView*)coverflowView coverAtIndexWasDoubleTapped:(int)index;
		[Export("coverflowView:coverAtIndexWasDoubleTapped:"), EventArgs ("Coverflow")]
		void CoverWasDoubleTapped(TKCoverflowView coverflowView, int index);
	}
}

// NOTE: Objective-C's strange method (selector) naming syntax:
//- (returntype) selectorPartA:(argtype1)argname selectorPartB:(argtype2)argname2;
// turns into:
// selectorPartA:selectorPartB: