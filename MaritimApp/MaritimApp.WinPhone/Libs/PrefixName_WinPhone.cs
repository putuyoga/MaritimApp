using MaritimApp.WinPhone.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(PrefixName_WinPhone))]

namespace MaritimApp.WinPhone.Libs
{
    public class PrefixName_WinPhone : MaritimApp.Libs.IPrefixName
    {
        /// <summary>
        /// Atur prefix resource untuk platform Windows Phone
        /// </summary>
        /// <param name="name">nama asset</param>
        /// <returns>lokasi di platform wp</returns>
        public string GetPrefixName(string name)
        {
            return "Assets/" + name;
        }
    }
}
