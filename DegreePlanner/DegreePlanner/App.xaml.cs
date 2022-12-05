using DegreePlanner.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using DegreePlanner.Services;

namespace DegreePlanner
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			if (Settings.FirstRun)
			{
				DatabaseServices.LoadSampleData();
				Settings.FirstRun = false;
			}

			var landingPage = new LandingPage();
			var navPage = new NavigationPage(landingPage);
			MainPage = navPage;
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
