using MaritimApp.Helpers;
using MaritimApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MaritimApp.ViewModels
{
    public class LokasiViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Lokasi> ListLokasi { get; set; }

        public void TambahLokasi(Lokasi baru) {
            Database db = new Database();
            db.Insert(baru);

            //insert to collection
            ListLokasi.Add(baru);
            
        }

        public void GetListLokasi()
        {
            Database db = new Database();
            string sql = "SELECT * FROM Lokasi";
            ListLokasi = new ObservableCollection<Lokasi>(db.Query<Lokasi>(sql));
        }

        public void HapusLokasi(Lokasi lokasi)
        {
            ListLokasi.Remove(lokasi);
            Database db = new Database();
            db.Delete<Lokasi>(lokasi);

            //TODO: delete cuaca pisan
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
