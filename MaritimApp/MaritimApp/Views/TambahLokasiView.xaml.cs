﻿using MaritimApp.Helpers;
using MaritimApp.Models;
using MaritimApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MaritimApp.Views
{
	public partial class TambahLokasiView : ContentPage
	{
        private Position LastPin;

		public TambahLokasiView ()
		{
			InitializeComponent ();
            Title = "TAMBAH LOKASI";

            var zoomLevel = 6; // pick a value between 1 and 18
            var latlongdeg = 360 / (Math.Pow(2, zoomLevel));

            var pos = new Position(-4.702151, 111.727294);
            var span = new MapSpan(pos, latlongdeg, latlongdeg);
            MainMap.MoveToRegion(span);

            MainMap.Tap += MainMap_Tap;
            SaveButton.Clicked += SaveButton_Clicked;
        }

        void SaveButton_Clicked(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(NamaEntry.Text))
            {
                DisplayAlert("Belum lengkap", "Isian 'nama' harus diisi", "OK");
            }
            else if (LastPin.Latitude == 0 && LastPin.Longitude == 0)
            {
                DisplayAlert("Belum Lengkap", "Pilih lokasi di 'peta' dengan tap, terlebih dahulu.", "OK");
            }
            else
            {
                Lokasi lokasi = new Lokasi() {
                    ID = 1,
                    Latitude = LastPin.Latitude,
                    Longitude = LastPin.Longitude,
                    Nama = NamaEntry.Text
                };
                App.LokasiVM.TambahLokasi(lokasi);
                Navigation.PopAsync(true);
            }
        }

        void MainMap_Tap(object sender, Libs.TapEventArgs e)
        {

            LastPin = e.Position;

            //var item = e.Position;
            /*var position = e.Position; // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = position,
                Label = "Lokasi Baru"
            };
            MainMap.Pins.Clear();
            MainMap.Pins.Add(pin);*/
        }
	}
}
