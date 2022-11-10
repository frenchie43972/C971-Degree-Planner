using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DegreePlanner.Models;
using DegreePlanner.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace DegreePlanner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssessPage : ContentPage
	{
		public AssessPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			AssessCollectionView.ItemsSource = await DatabaseServices.GetAssessment();
		}

		async void AddAsses_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AssessAdd());
		}

		async void AssessCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.CurrentSelection != null)
			{
				Assessment assess = (Assessment)e.CurrentSelection.FirstOrDefault();
				await Navigation.PushAsync(new AssessEdit(assess));
			}
		}
	}
}