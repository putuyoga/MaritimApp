using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using MaritimApp.Droid.Libs;
using System.Text.RegularExpressions;

[assembly: Dependency(typeof(PrefixName_Android))]

namespace MaritimApp.Droid.Libs
{
    public class PrefixName_Android : MaritimApp.Libs.IPrefixName
    {
        public string GetPrefixName(string name)
        {
            return "Assets/" + Regex.Replace(name, "/", "_");
        }
    }
}