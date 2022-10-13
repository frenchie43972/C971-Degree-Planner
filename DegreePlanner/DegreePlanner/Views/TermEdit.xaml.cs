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
			TermStart.Text = selectedTerm.TermStart.ToString();
			TermEnd.Text = selectedTerm.TermEnd.ToString();
		}

		async void SaveTerm_Clicked(object sender, EventArgs e)
		{
			await DatabaseServices.UpdateTerm(Int32.Parse(TermId.Text), TermName.Text, DateTime.Parse(TermStart.Text), DateTime.Parse(TermEnd.Text));

			await Navigation.PopAsync();
		}

		async void CancelTerm_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		async void DeleteTerm_Clicked(object sender, EventArgs e)
		{
			var id = int.Parse(TermId.Text);

			await DatabaseServices.RemoveTerm(id);
			await Navigation.PopAsync();
		}
	}
}