using DegreePlanner.Models;
using DegreePlanner.Services;
using System;
using System.Collections.Generic;
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
			CourseStatus.Title = myCourse.CourseStatus;
			CourseStart.Date = myCourse.CourseStart.Date;
			CourseEnd.Date = myCourse.CourseEnd.Date;
			InstructorName.Text = myCourse.InstName.ToString();
			InstructorEmail.Text = myCourse.InstEmail.ToString();
			InstructorPhone.Text = myCourse.InstPhone.ToString();
			EditNotes.Text = myCourse.Notes;
			NotificationEdit.IsToggled = myCourse.Notification;


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
			
			await DatabaseServices.UpdateCourse(Int32.Parse(CourseId.Text), CourseName.Text, CourseStatus.Title,
									DateTime.Parse(CourseStart.Date.ToString()), DateTime.Parse(CourseEnd.Date.ToString()),
									InstructorName.Text, InstructorEmail.Text, InstructorPhone.Text, EditNotes.Text, NotificationEdit.IsToggled);
			await Navigation.PopAsync();
		}

		async void CancelCourse_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		async void DeleteCourse_Clicked(object sender, EventArgs e)
		{
			var id = int.Parse(CourseId.Text);

			var confirmDelete = await DisplayAlert("Confirm", "Are you sure you wnat to delete this record?", "Ok", "Cancel");

			if (confirmDelete == true)
			{
				await DatabaseServices.RemoveCourse(id);
				await Navigation.PopAsync();
			}
			else
			{
				return;
			}
		}

		private void CourseStart_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

		private void CourseEnd_DateSelected(object sender, DateChangedEventArgs e)
		{

		}
	}
}