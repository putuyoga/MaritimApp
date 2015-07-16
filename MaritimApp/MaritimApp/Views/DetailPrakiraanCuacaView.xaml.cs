using MaritimApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Lokasi SelectedLokasi;

        int SelectedIndex = -1;

		public DetailPrakiraanCuacaView (Lokasi lokasi)
		{
			InitializeComponent ();
            Title = "INFORMASI";
            SelectedLokasi = lokasi;
		}

        public DetailPrakiraanCuacaView(Lokasi lokasi, int time_index)
        {
            InitializeComponent();
            Title = "INFORMASI";
            SelectedLokasi = lokasi;

            SelectedIndex = time_index;           
        }

        protected override void OnAppearing()
        {
            PetaLokasiGrid.BindingContext = SelectedLokasi;
            namaLokasi.BindingContext = SelectedLokasi;
            initMap(SelectedLokasi);

            if (SelectedIndex == -1)
            {
                GetPrakiraanCuaca(SelectedLokasi);
            }
            else
            {
                GetPrakiraanCuaca(SelectedLokasi, SelectedIndex);
            }
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
            
            SelectedIndex--;
            if(SelectedIndex == 0)
            {
                prevButton.IsEnabled = false;
            }

            if (!nextButton.IsEnabled) nextButton.IsEnabled = true;
            UpdateView();
        }

        void Down_Clicked(object sender, EventArgs e)
        {
            if (ListCuaca == null || ListCuaca.Count == 0) return;

            SelectedIndex++;
            if (SelectedIndex == ListCuaca.Count - 1)
            {
                nextButton.IsEnabled = false;
            }

            if (!prevButton.IsEnabled) prevButton.IsEnabled = true;
            UpdateView();
        }

        void UpdateView()
        {
            this.BindingContext = ListCuaca[SelectedIndex];
        }

        private async void GetPrakiraanCuaca(Lokasi lokasi)
        {
            ErrorGrid.IsVisible = false;
            loadingIndicator.IsVisible = true;
            loadingIndicator.IsRunning = true;

            
            bool debug_mode = App.PengaturanVM.GetValues<bool>("debug_mode");

            //DEBUG MODE
            if(debug_mode)
            {
                Stopwatch timer;
                timer = new Stopwatch();
                timer.Start();
                ListCuaca = await lokasi.GetPrakiraanCuaca();
                timer.Stop();
                long ms = timer.ElapsedMilliseconds;
                await DisplayAlert("Debug Mode", "waktu akses data: " + ms + "ms", "OK");
            }
            else
            {
                ListCuaca = await lokasi.GetPrakiraanCuaca();
            }

            if(ListCuaca != null && ListCuaca.Count > 0)
            {
                //pilih cuaca terdekat
                SelectedIndex = ListCuaca.IndexOf(ListCuaca.Where(c => DateTime.Now.Subtract(c.Waktu).TotalSeconds > 0).
                    OrderByDescending(c => c.Waktu).FirstOrDefault());
                ErrorGrid.IsVisible = false;
                UpdateView();
            }
            else
            {
                await DisplayAlert("Gagal Mendapatkan Data", "Mungkin koneksi anda bermasalah atau layanan sedang sibuk", "OK");
                ErrorGrid.IsVisible = true;
            }
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsRunning = false;
        }


        private async void GetPrakiraanCuaca(Lokasi lokasi, int time_index)
        {
            reloadButton.IsVisible = false;
            loadingIndicator.IsVisible = true;
            loadingIndicator.IsRunning = true;
            ListCuaca = await lokasi.GetPrakiraanCuaca();
            if (ListCuaca != null && ListCuaca.Count > 0)
            {
                SelectedIndex = time_index;
                reloadButton.IsVisible = false;
                UpdateView();
            }
            else
            {
                await DisplayAlert("Gagal Mendapatkan Data", "Mungkin koneksi anda bermasalah atau layanan sedang sibuk", "OK");
                reloadButton.IsVisible = true;
            }
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsRunning = false;
        }

        void Reload_Clicked(object sender, EventArgs e)
        {
            GetPrakiraanCuaca(SelectedLokasi);
        }
	}
}
