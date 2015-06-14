using MaritimApp.Libs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaritimApp.ViewModels
{
    public class PengaturanViewModel
    {

        private readonly object locker = new object();

        public T GetValues<T> (string key)
        {
            lock (locker)
            {
                return DependencyService.Get<ISettings>().GetValues<T>(key);
            }
        }

        public void SetValue<T>(string key, T value)
        {
            lock (locker)
            {
                DependencyService.Get<ISettings>().SetValue<T>(key, value);
            }
        }
    }
}
