using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Preferences;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MaritimApp.Droid.Libs;

[assembly: Xamarin.Forms.Dependency(typeof(Settings_Android))]

namespace MaritimApp.Droid.Libs
{
    public class Settings_Android : MaritimApp.Libs.ISettings
    {
        public T GetValues<T>(string key)
        {
            using (var sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context))
            {
                object value = null;
                if (typeof(T) == typeof(float))
                {
                    value = sharedPreferences.GetFloat(key, 0.0f);
                }
                else if (typeof(T) == typeof(int))
                {
                    value = sharedPreferences.GetInt(key, 0);
                }
                else if (typeof(T) == typeof(string))
                {
                    value = sharedPreferences.GetString(key, String.Empty);
                }
                else if(typeof(T) == typeof(bool) || typeof(T) == typeof(System.Boolean))
                {
                    value = sharedPreferences.GetBoolean(key, false);
                }
                else
                {
                    throw new Exception("Currenty not suported - " + typeof(T).ToString());
                }
                return (T)value;
            }
        }
        public void SetValue<T>(string key, T value)
        {
            //TODO: Add more data type (double ?)
            using (var sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context))
            {
                using (var sharedPreferencesEditor = sharedPreferences.Edit())
                {
                    if (typeof(T) == typeof(float))
                    {
                        sharedPreferencesEditor.PutFloat(key, Convert.ToSingle(value));
                    }
                    else if (typeof(T) == typeof(int))
                    {
                        sharedPreferencesEditor.PutInt(key, Convert.ToInt32(value));
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        sharedPreferencesEditor.PutString(key, Convert.ToString(value));
                    }
                    else if(typeof(T) == typeof(bool) || typeof(T) == typeof(System.Boolean))
                    {
                        sharedPreferencesEditor.PutBoolean(key, Convert.ToBoolean(value));
                    }
                    else
                    {
                        throw new Exception("Currenty not suported - " + typeof(T).ToString());
                    }
                    sharedPreferencesEditor.Commit();
                }
            }
        }
    }
}