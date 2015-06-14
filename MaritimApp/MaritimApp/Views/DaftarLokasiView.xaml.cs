using MaritimApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaritimApp.Views
{
	public partial class DaftarLokasiView : ContentPage
	{
		public DaftarLokasiView ()
		{
			InitializeComponent ();
            Title = "MARITIM";
            this.BindingContext = App.LokasiVM;
            LokasiListView.ItemTapped += LokasiListView_ItemTapped;
		}

        void LokasiListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var lokasi = (Lokasi)e.Item;

            Navigation.PushAsync(new DetailPrakiraanCuacaView(lokasi));
        }

        void TambahLokasiMenu_Clicked(object sender, EventArgs e)
        {
            var nextView = new TambahLokasiView();
            Navigation.PushAsync(nextView);
        }

        void PengaturanMenu_Clicked(object sender, EventArgs e)
        {
            var nextView = new PengaturanView();
            Navigation.PushAsync(nextView);
        }

        void TentangMenu_Clicked(object sender, EventArgs e)
        {
            //var nextView = new TentangView();
            //Navigation.PushAsync(nextView);
        }
	}
}
