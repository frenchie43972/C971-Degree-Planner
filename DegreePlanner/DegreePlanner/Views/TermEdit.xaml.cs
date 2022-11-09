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
	public partial class TermEdit : ContentPage
	{
		public TermEdit(Term selectedTerm)
		{
			InitializeComponent();

			TermId.Text = selectedTerm.Id.ToString();
			TermName.Text = selectedTerm.TermName.ToString();
			TermStart.Date = selectedTerm.TermStart.Date;
			TermEnd.Date = selectedTerm.TermEnd.Date;
		}

		async void SaveTerm_Clicked(object sender, EventArgs e)
		{
			if (TermName.Text == null)
			{
				await DisplayAlert("Error!", "Term name cannot be empty", "Ok");

				return;
			}
			if (TermStart.Date >= TermEnd.Date)
			{
				await DisplayAlert("Error!", "Start date cannot be greater than end date", "Ok");

				return;
			}
			else
			{
				await DatabaseServices.UpdateTerm(Int32.Parse(TermId.Text), TermName.Text,
											DateTime.Parse(TermStart.Date.ToString()),
											DateTime.Parse(TermEnd.Date.ToString()));

				await Navigation.PopAsync();
			}

		}

		async void CancelTerm_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		async void DeleteTerm_Clicked(object sender, EventArgs e)
		{
			var id = int.Parse(TermId.Text);

			var confirmDelete = await DisplayAlert("Confirm", "Are you sure you wnat to delete this record?", "Ok", "Cancel");

			if (confirmDelete == true)
			{
				await DatabaseServices.RemoveTerm(id);
				await Navigation.PopAsync();
			}
			else
			{
				return;
			}
			
		}

		private void TermStart_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

		private void TermEnd_DateSelected(object sender, DateChangedEventArgs e)
		{

		}
	}
}