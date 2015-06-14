using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MaritimApp.Models;
using SQLite;
using Xamarin.Forms;
using MaritimApp.Libs;

namespace MaritimApp.Helpers
{
    public class Database
    {
        /// <summary>
        /// Objek penanda ketika database sedang dipakai
        /// </summary>
        static object locker = new object();

        /// <summary>
        /// Koneksi database
        /// </summary>
        SQLiteConnection database;

        public Database()
        {
            database = database = DependencyService.Get<ISQLite>().GetConnection();

            //buat table sebagai inisialisasi
            database.CreateTable<Lokasi>();
            database.CreateTable<Cuaca>();
        }

        /// <summary>
        /// Melakukan query sql ke db
        /// </summary>
        /// <typeparam name="T">tipe data generic</typeparam>
        /// <param name="query">sql query</param>
        /// <returns>objek list dari tipe data T</returns>
        public IEnumerable<T> Query<T>(string query) where T : new()
        {
            lock (locker)
            {
                return database.Query<T>(query);
            }
        }

        /// <summary>
        /// Menghapus objek T dari db
        /// </summary>
        /// <typeparam name="T">tipe data generic</typeparam>
        /// <param name="item">item yang akan dihapus</param>
        public void Delete<T>(T item)
        {
            lock (locker)
            {
                database.Delete<T>(item);
            }
        }

        /// <summary>
        /// Memasukan objek T baru ke database
        /// </summary>
        /// <typeparam name="T">tipe data generic</typeparam>
        /// <param name="item">item yang akan dimasukkan</param>
        public void Insert<T>(T item)
        {
            lock(locker)
            {
                database.Insert(item);
            }
        }

        /// <summary>
        /// Update objek T pada database
        /// </summary>
        /// <typeparam name="T">tipe data generic</typeparam>
        /// <param name="item">item yang diupdate</param>
        public void Update<T>(T item)
        {
            lock(locker)
            {
                database.Update(item);
            }
        }

        /// <summary>
        /// Memasukan objek List T baru ke database
        /// </summary>
        /// <typeparam name="T">tipe data generic</typeparam>
        /// <param name="item">list item yang akan dimasukkan</param>
        public void Insert<T>(List<T> items)
        {
            lock (locker)
            {
                database.InsertAll(items);
            }
        }
    }
}
