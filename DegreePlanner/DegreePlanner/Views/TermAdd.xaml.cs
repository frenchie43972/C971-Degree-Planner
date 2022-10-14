using DegreePlanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Check ON APPEARING METHODS!!!
namespace DegreePlanner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TermAdd : ContentPage
	{
		public TermAdd()
		{
			InitializeComponent();
		}

		private void TermStartDate_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

		private void TermEndDate_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

		async void SaveTerm_Clicked(object sender, EventArgs e)
		{
			await DatabaseServices.AddTerm(TermName.Text, TermStartDate.Date, TermEndDate.Date);
			await Navigation.PopAsync();
		}

		async void CancelTerm_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
	}
}