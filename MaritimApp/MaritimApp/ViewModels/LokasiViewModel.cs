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
        
        public bool IsEmpty
        {
            get
            {
                return ListLokasi.Count == 0 ? true : false;
            }
        }

        public void TambahLokasi(Lokasi baru) {
            Database db = new Database();
            db.Insert(baru);

            //insert to collection
            ListLokasi.Add(baru);
            NotifyPropertyChanged("IsEmpty");
            
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
            db.Delete(lokasi);

            //Empty Database Cuaca
            string emptySQL = String.Format("DELETE FROM Cuaca Where IDLokasi = '{0}'", lokasi.ID);
            db.Query<Cuaca>(emptySQL);

            NotifyPropertyChanged("IsEmpty");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
