using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
	public partial class CourseAdd : ContentPage
	{
		public CourseAdd()
		{
			InitializeComponent();
			
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			var termList = await DatabaseServices.GetTerm();
			AddCourseTerm.ItemsSource = (System.Collections.IList)termList;
			
		}

		async void SaveCourse_Clicked(object sender, EventArgs e)
		{
			
			Term t = (Term)AddCourseTerm.SelectedItem;

			//if (IsValidPhoneNumber(AddInstPhone.Text))
			//{
			//	await DisplayAlert("Error!", "Yay", "Ok");
			//	return;

			//}
			//else
			//{
			//	await DisplayAlert("Error!", "Booo", "Ok");
			//	return;

			//}

			await DatabaseServices.AddCourse(t.Id, AddCourseName.Text, (string)AddCourseStatus.SelectedItem,
									AddCourseStart.Date, AddCourseEnd.Date, AddCourseInst.Text, AddInstEmail.Text, AddInstPhone.Text,
									CourseNotes.Text, NotificationAdd.IsToggled, NotifyEnd.IsToggled);
			await Navigation.PopAsync();
		}

		async void CancelCourse_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		// Validates an email address
		public bool validEmail(string address)
		{
			EmailAddressAttribute e = new EmailAddressAttribute();
			if (e.IsValid(address))
				return true;
			else
				return false;
		}

		// Validates that a good phone number is inputted
		public static bool IsValidPhoneNumber(string phoneNumber)
		{
			if (string.IsNullOrWhiteSpace(phoneNumber))
			{
				return false;
			}
			var pattern = @"^[\+]?[{1}]?[(]?[2-9]\d{2}[)]?[-\s\.]?[2-9]\d{2}[-\s\.]?[0-9]{4}$";
			var options = System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase;
			return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern, options);
		}


		public void AddCourseStart_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

		public void AddCourseEnd_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

	}
}