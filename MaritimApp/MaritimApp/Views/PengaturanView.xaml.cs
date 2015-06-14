using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MaritimApp.Views
{
	public partial class PengaturanView : ContentPage
	{
		public PengaturanView ()
		{
			InitializeComponent ();
            Title = "PENGATURAN";
            InitPengaturan();

            //Simpan button Handle
            simpanButton.IsEnabled = false;
            simpanButton.Text = "";
            jarakPandangSlider.ValueChanged += jarakPandangSlider_ValueChanged;
            tinggiGelSlider.ValueChanged += tinggiGelSlider_ValueChanged;
            kondisiCuacaPicker.SelectedIndexChanged += kondisiCuacaPicker_SelectedIndexChanged;
		}

        void kondisiCuacaPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonEnable();
        }

        void tinggiGelSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            ButtonEnable();
        }

        void jarakPandangSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            ButtonEnable();
        }

        void ButtonEnable()
        {
            simpanButton.Text = "Simpan";
            simpanButton.IsEnabled = true;
        }

        private void InitPengaturan()
        {
            //Jarak Pandang Minimum
            float jarakPandangMin = App.PengaturanVM.GetValues<float>("JarakPandangMin");
            float tinggiGelombangMin = App.PengaturanVM.GetValues<float>("TinggiGelombangMin");
            int KondisiCuacaMin = App.PengaturanVM.GetValues<int>("KondisiCuacaMin");

            jarakPandangSlider.Value = jarakPandangMin == 0 ? 6 : jarakPandangMin;
            tinggiGelSlider.Value = tinggiGelombangMin == 0 ? 2 : tinggiGelombangMin;
            kondisiCuacaPicker.SelectedIndex = KondisiCuacaMin;
        }


        void Simpan_Clicked(object sender, EventArgs e)
        {
            App.PengaturanVM.SetValue<float>("JarakPandangMin", (float)jarakPandangSlider.Value);
            App.PengaturanVM.SetValue<float>("TinggiGelombangMin", (float)tinggiGelSlider.Value);
            App.PengaturanVM.SetValue<int>("KondisiCuacaMin", kondisiCuacaPicker.SelectedIndex);
            simpanButton.Text = "Tersimpan";
            simpanButton.IsEnabled = false;  
        }
	}
}
