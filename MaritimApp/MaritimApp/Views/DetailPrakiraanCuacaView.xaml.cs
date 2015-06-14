using MaritimApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MaritimApp.Views
{
	public partial class DetailPrakiraanCuacaView : TabbedPage
	{
        List<Cuaca> ListCuaca;
        int SelectedIndex = 0;
		public DetailPrakiraanCuacaView (Lokasi lokasi)
		{
			InitializeComponent ();
            Title = "INFORMASI";
            PetaLokasiGrid.BindingContext = lokasi;
            namaLokasi.BindingContext = lokasi;

            initMap(lokasi);
            GetPrakiraanCuaca(lokasi);
		}

        public DetailPrakiraanCuacaView(Lokasi lokasi, int time_index)
        {
            InitializeComponent();
            Title = "INFORMASI";
            PetaLokasiGrid.BindingContext = lokasi;
            namaLokasi.BindingContext = lokasi;

            initMap(lokasi);
            GetPrakiraanCuaca(lokasi, time_index);
        }

        private void initMap(Lokasi lokasi)
        {
            var zoomLevel = 7; // pick a value between 1 and 18
            var latlongdeg = 360 / (Math.Pow(2, zoomLevel));

            var pos = new Position(lokasi.Latitude, lokasi.Longitude);
            var span = new MapSpan(pos, latlongdeg, latlongdeg);
            MainMap.MoveToRegion(span);
            
        }

        void Up_Clicked(object sender, EventArgs e)
        {
            if (ListCuaca == null || ListCuaca.Count == 0) return;

            if (SelectedIndex == 0)
            {
                SelectedIndex = ListCuaca.Count - 1;
            }
            else
            {
                SelectedIndex--;
            }
            UpdateView();
        }

        void Down_Clicked(object sender, EventArgs e)
        {
            if (ListCuaca == null || ListCuaca.Count == 0) return;

            if (SelectedIndex == ListCuaca.Count - 1)
            {
                SelectedIndex = 0;
            }
            else
            {
                SelectedIndex++;
            }
            UpdateView();
        }

        void UpdateView()
        {
            this.BindingContext = ListCuaca[SelectedIndex];
        }

        private async void GetPrakiraanCuaca(Lokasi lokasi)
        {
            loadingIndicator.IsVisible = true;
            loadingIndicator.IsRunning = true;
            ListCuaca = await lokasi.GetPrakiraanCuaca();
            if(ListCuaca != null && ListCuaca.Count > 0)
            {
                //pilih cuaca terdekat
                SelectedIndex = ListCuaca.IndexOf(ListCuaca.Where(c => DateTime.Now.Subtract(c.Waktu).TotalSeconds > 0).
                    OrderByDescending(c => c.Waktu).FirstOrDefault());

                UpdateView();
            }
            else
            {
                await DisplayAlert("Gagal Mendapatkan Data", "Mungkin koneksi anda bermasalah atau layanan sedang sibuk", "OK");
            }
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsRunning = false;
        }


        private async void GetPrakiraanCuaca(Lokasi lokasi, int time_index)
        {
            loadingIndicator.IsVisible = true;
            loadingIndicator.IsRunning = true;
            ListCuaca = await lokasi.GetPrakiraanCuaca();
            if (ListCuaca != null && ListCuaca.Count > 0)
            {
                SelectedIndex = time_index;
                UpdateView();
            }
            else
            {
                await DisplayAlert("Gagal Mendapatkan Data", "Mungkin koneksi anda bermasalah atau layanan sedang sibuk", "OK");
            }
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsRunning = false;
        }
	}
}
