using DegreePlanner.Models;
using DegreePlanner.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DegreePlanner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CourseEdit : ContentPage
	{

		Course myCourse { get; set; }
		public CourseEdit(Course selectedCourse)
		{
			InitializeComponent();

			myCourse = selectedCourse;

		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			var termList = await DatabaseServices.GetTerm();
			TermSelect.ItemsSource = (System.Collections.IList)termList;

			CourseId.Text = myCourse.Id.ToString();
			TermSelect.Title = "Term Select"; 
			CourseName.Text = myCourse.CourseName.ToString();
			CourseStatus.SelectedItem = myCourse.CourseStatus;
			CourseStart.Date = myCourse.CourseStart.Date;
			CourseEnd.Date = myCourse.CourseEnd.Date;
			InstructorName.Text = myCourse.InstName.ToString();
			InstructorEmail.Text = myCourse.InstEmail.ToString();
			InstructorPhone.Text = myCourse.InstPhone.ToString();
			EditNotes.Text = myCourse.Notes;
			NotificationEdit.IsToggled = myCourse.NotificationStart;
			NotificationEnd.IsToggled = myCourse.NotificationEnd;


			foreach (var term in termList)
			{
				if (term.Id == myCourse.TermId)
				{
					TermSelect.SelectedItem = term;
					break;
				}
			}

			

		}

		async void SaveCourse_Clicked(object sender, EventArgs e)
		{
			
			Term t = (Term)TermSelect.SelectedItem;

			if (!validEmail(InstructorEmail.Text) && !IsValidPhoneNumber(InstructorPhone.Text))
			{
				await DisplayAlert("Error!", "Enter a valid phone number.", "Ok");
				return;
			}
			else if (TermSelect.SelectedItem == null)
			{
				await DisplayAlert("Error!", "Please select a term.", "Ok");
				return;
			}
			else if (CourseStatus.SelectedItem == null)
			{
				await DisplayAlert("Error!", "Please select a course status.", "Ok");
				return;
			}
			else if (CourseStart.Date > CourseEnd.Date)
			{
				await DisplayAlert("Error!", "Start date cannot be greater than end date", "Ok");

				return;
			}
			else
			{
				await DatabaseServices.UpdateCourse(Int32.Parse(CourseId.Text), t.Id, CourseName.Text, CourseStatus.SelectedItem.ToString(),
									DateTime.Parse(CourseStart.Date.ToString()), DateTime.Parse(CourseEnd.Date.ToString()),
									InstructorName.Text, InstructorEmail.Text, InstructorPhone.Text, EditNotes.Text,
									NotificationEdit.IsToggled, NotificationEnd.IsToggled);

				await Navigation.PopAsync();
			}
		}

		async void CancelCourse_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		async void DeleteCourse_Clicked(object sender, EventArgs e)
		{
			var id = int.Parse(CourseId.Text);

			var confirmDelete = await DisplayAlert("Confirm", "Are you sure you wnat to delete this record?", "Ok", "Cancel");

			if (confirmDelete)
			{
				await DatabaseServices.RemoveCourse(id);
				await Navigation.PopAsync();
			}
			else
			{
				return;
			}
		}

		// Validates an email address
		public bool validEmail(string address)
		{
			EmailAddressAttribute e = new EmailAddressAttribute();
			if (e.IsValid(address))
				return true;
			else
				DisplayAlert("Error!", "Enter a valid email address.", "Ok");
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

		private void CourseStart_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

		private void CourseEnd_DateSelected(object sender, DateChangedEventArgs e)
		{

		}
	}
}