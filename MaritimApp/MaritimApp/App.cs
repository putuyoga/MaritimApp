using MaritimApp.Models;
using MaritimApp.ViewModels;
using MaritimApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MaritimApp
{
	public class App : Application
	{
        public static LokasiViewModel LokasiVM { get; private set; }
        public static PengaturanViewModel PengaturanVM { get; private set; }
		public App ()
		{
            LokasiVM = new LokasiViewModel();
            LokasiVM.GetListLokasi();

            PengaturanVM = new PengaturanViewModel();
            MainPage = new NavigationPage(new DaftarLokasiView());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
