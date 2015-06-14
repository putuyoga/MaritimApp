using MaritimApp.WinPhone.Libs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_WinPhone))]

namespace MaritimApp.WinPhone.Libs
{
    public class SQLite_WinPhone : MaritimApp.Libs.ISQLite
    {
        public SQLite_WinPhone() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "Maritim.db3";
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);

            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);

            // Return the database connection
            return conn;
        }
    }
}
