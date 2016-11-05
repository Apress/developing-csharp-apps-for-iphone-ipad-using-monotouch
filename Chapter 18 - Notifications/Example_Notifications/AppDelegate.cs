using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Example_Notifications.Screens;

namespace Example_Notifications
{
	//========================================================================
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		//========================================================================
		#region -= declarations and properties =-
		
		protected UIWindow _window;
		protected Screens.iPhone.Home.Home_iPhone _home;
		protected string _deviceToken = string.Empty;
		
		public string DeviceToken { get { return this._deviceToken; } }
		
		#endregion
		//========================================================================
		
		//========================================================================
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			//---- create our window
			this._window = new UIWindow (UIScreen.MainScreen.Bounds);
			this._window.MakeKeyAndVisible ();
		
			this._home = new Screens.iPhone.Home.Home_iPhone();
			this._home.View.Frame = new System.Drawing.RectangleF(0, UIApplication.SharedApplication.StatusBarFrame.Height, UIScreen.MainScreen.ApplicationFrame.Width, UIScreen.MainScreen.ApplicationFrame.Height);			
			this._window.AddSubview(this._home.View);
			
			//---- check for a local notification
			if(options != null)
			{
				if(options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
				{
					UILocalNotification localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
					if(localNotification != null)
					{
						new UIAlertView(localNotification.AlertAction, localNotification.AlertBody, null, "OK", null).Show();
						//---- reset our badge
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					}
				}
			
				//---- check for a remote notification
				if(options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
				{
					NSDictionary remoteNotification = options[UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary;
					if(remoteNotification != null)
					{
						//new UIAlertView(remoteNotification.AlertAction, remoteNotification.AlertBody, null, "OK", null).Show();
					}
				}
			}
			
			//==== register for remote notifications and get the device token
			//---- set what kind of notification types we want
			UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge;
			//---- register for remote notifications
			UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
			
			//----
			return true;
		}
		//========================================================================
		
		
		//========================================================================
		/// <summary>
		/// 
		/// </summary>
		public override void ReceivedLocalNotification (UIApplication application, UILocalNotification notification)
		{
			//---- show an alert
			new UIAlertView(notification.AlertAction, notification.AlertBody, null, "OK", null).Show();
			
			//---- reset our badge
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}
		//========================================================================

		//========================================================================
		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			
		}
		//========================================================================
		
		
		//========================================================================
		/// <summary>
		/// The iOS will call the APNS in the background and issue a device token to the device. when that's 
		/// accomplished, this method will be called.
		/// 
		/// Note: the device token can change, so this needs to register with your server application everytime 
		/// this method is invoked, or at a minimum, cache the last token and check for a change.
		/// </summary>
		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			this._deviceToken = deviceToken.ToString();
		}
		//========================================================================
		
		//========================================================================
		/// <summary>
		/// Registering for push notifications can fail, for instance, if the device doesn't have network access.
		/// 
		/// In this case, this method will be called.
		/// </summary>
		public override void FailedToRegisterForRemoteNotifications (UIApplication application, NSError error)
		{
			new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
		}
		//========================================================================
	
	}
	//========================================================================
}
