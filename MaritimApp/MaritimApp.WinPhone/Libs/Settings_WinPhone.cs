using MaritimApp.WinPhone.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Settings_WinPhone))]

namespace MaritimApp.WinPhone.Libs
{
    public class Settings_WinPhone : MaritimApp.Libs.ISettings
    {
        public T GetValues<T>(string key)
        {
            var isoSettings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
            if (isoSettings.Contains(key))
            {
                var tempValue = isoSettings[key];
                if (tempValue != null)
                {
                    return (T)tempValue;
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

        public void SetValue<T>(string key, T value)
        {
            var isoSettings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
            bool valueChanged = false;
            if (isoSettings.Contains(key))
            {
                if (isoSettings[key] != (object)value)
                {
                    isoSettings[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                isoSettings.Add(key, value);
                valueChanged = true;
            }

            //simpan ke isolated storage settings
            if (valueChanged)
            {
                isoSettings.Save();
            }
        }
    }
}
