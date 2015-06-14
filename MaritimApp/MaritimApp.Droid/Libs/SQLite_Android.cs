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
using System.Runtime.CompilerServices;
using MaritimApp.Droid.Libs;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]

namespace MaritimApp.Droid.Libs
{
    
    // ...
    public class SQLite_Android : MaritimApp.Libs.ISQLite
    {
        public SQLite_Android() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);

            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);

            // Return the database connection
            return conn;
        }
    }
}