using MaritimApp.WinPhone.Libs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaritimApp.ViewModels
{
    public class PengaturanViewModel
    {

        private readonly object locker = new object();

        public T GetValues<T> (string key)
        {
            lock (locker)
            {
                return new Settings_WinPhone().GetValues<T>(key);
            }
        }

        public void SetValue<T>(string key, T value)
        {
            lock (locker)
            {
                new Settings_WinPhone().SetValue<T>(key, value);
            }
        }
    }
}
