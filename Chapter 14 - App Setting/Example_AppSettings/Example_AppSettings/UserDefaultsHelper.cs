using System;
using MonoTouch.Foundation;

namespace Example_AppSettings
{
	public static class UserDefaultsHelper
	{
		//========================================================================
		/// <summary>
		/// Loads the default settings from the Settings.bundle/Root.plist file. Also
		/// calls nested settings (referred to in child pane items) recursively, to 
		/// load those defaults.
		/// </summary>
		public static void LoadDefaultSettings ()
		{
			//---- check to see if they've already been loaded for the first time
			if (!NSUserDefaults.StandardUserDefaults.BoolForKey ("__DefaultsLoaded"))
			{
				#if DEBUG
				Console.WriteLine ("Loading settings file for the first time");
				#endif
				
				string rootSettingsFilePath = NSBundle.MainBundle.BundlePath + "/Settings.bundle/Root.plist";
				
				//---- check to see if there is event a settings file
				if (System.IO.File.Exists (rootSettingsFilePath))
				{
					//---- load the settings
					NSDictionary settings = NSDictionary.FromFile (rootSettingsFilePath);
					LoadSettingsFile (settings);
				}
				
				//---- mark them as loaded so this doesn't run again
				NSUserDefaults.StandardUserDefaults.SetBool (true, "__DefaultsLoaded");
			}
		}
		//========================================================================
		
		//========================================================================
		/// <summary>
		/// Recursive version of LoadDefautSetings
		/// </summary>
		private static void LoadSettingsFile (NSDictionary settings)
		{
			//---- declare vars
			bool foundTypeKey;
			bool foundDefaultValue;
			string prefKeyName;
			NSObject prefDefaultValue;
			NSObject key;
			
			//---- get the preference specifiers node
			NSArray prefs = settings.ObjectForKey (new NSString ("PreferenceSpecifiers")) as NSArray;
	
			//---- loop through the settings
			for (uint i = 0; i < prefs.Count; i++)
			{
				//---- reset for each setting
				foundTypeKey = false;
				foundDefaultValue = false;
				prefKeyName = string.Empty;
				prefDefaultValue = new NSObject ();
				
				//----
				NSDictionary pref = new NSDictionary (prefs.ValueAt (i));
				
				#if DEBUG
				Console.WriteLine ("=============");
				#endif
				
				//---- loop through the dictionary of any particular setting
				for (uint keyCount = 0; keyCount < pref.Keys.Length; keyCount++)
				{
					//---- shortcut reference
					key = pref.Keys[keyCount];
					
					//---- get the key name and default value
					if (key.ToString () == "Key")
					{
						foundTypeKey = true;
						prefKeyName = pref[key].ToString ();
					} 
					else if (key.ToString () == "DefaultValue")
					{
						foundDefaultValue = true;
						prefDefaultValue = pref[key];
					}
					else if (key.ToString () == "File")
					{
	
						#if DEBUG
						Console.WriteLine ("calling recursively");
						Console.WriteLine ("<nested>");
						#endif
						
						NSDictionary nestedSettings = NSDictionary.FromFile (
							NSBundle.MainBundle.BundlePath + "/Settings.bundle/" + pref[key].ToString () + ".plist");
						LoadSettingsFile (nestedSettings);
						
						#if DEBUG
						Console.WriteLine ("</nested>");
						#endif
					}
	
					
					//---- if we've found both, set it in our user preferences
					if (foundTypeKey && foundDefaultValue)
					{
						NSUserDefaults.StandardUserDefaults[prefKeyName] = prefDefaultValue;
					}
					
					#if DEBUG
					//---- write to the console, our values
					WriteKeyAndValueToOutput (pref, key);
					#endif
				}
			}
		}
		//========================================================================
		
		//========================================================================
		/// <summary>
		/// Writes out the key and value information to the console, useful for debugging,
		/// or understanding how the user preferences are stored.
		/// </summary>
		private static void WriteKeyAndValueToOutput (NSDictionary dict, NSObject key)
		{
			Console.Write (key.ToString () + ":");
			
			if (dict[key] is NSString)
			{
				Console.WriteLine (dict[key].ToString ());
			} 
			else
			{
				switch (dict[key].GetType ().ToString ())
				{
					case "MonoTouch.Foundation.NSNumber":
						Console.WriteLine ((dict[key] as NSNumber).FloatValue.ToString ());
						break;
					case "MonoTouch.Foundation.NSArray":
						Console.WriteLine ("");
						NSArray items = dict[key] as NSArray;
						
						for (uint j = 0; j < items.Count; j++)
						{
							Console.WriteLine ("\t" + NSString.FromHandle (items.ValueAt (j)).ToString ());
						}
						break;
				}
	
			}
		}
		//========================================================================
	
	}
}
