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

			//CourseId.Text = selectedCourse.Id.ToString();
			//TermSelect.Title = selectedCourse.TermId.ToString(); // Need to display Term Name
			//CourseName.Text = selectedCourse.CourseName.ToString();
			//CourseStatus.Title = selectedCourse.CourseStatus.ToString();
			//CourseStart.Date = selectedCourse.CourseStart.Date;
			//CourseEnd.Date = selectedCourse.CourseEnd.Date;
			//InstructorName.Text = selectedCourse.InstName.ToString();
			//InstructorEmail.Text = selectedCourse.InstEmail.ToString();
			//InstructorPhone.Text = selectedCourse.InstPhone.ToString();
			//EditNotes.Text = selectedCourse.Notes.ToString();
			//NotificationEdit = selectedCourse.Notification.ToString();
			
			// Set Datasource iterate through and select 
			//TermSelect.SelectedItem = selectedCourse.TermSelect; 
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			var termList = await DatabaseServices.GetTerm();
			TermSelect.ItemsSource = (System.Collections.IList)termList;

			CourseId.Text = myCourse.Id.ToString();
			TermSelect.Title = "Select a Term"; 
			CourseName.Text = myCourse.CourseName.ToString();
			CourseStatus.Title = myCourse.CourseStatus.ToString();
			CourseStart.Date = myCourse.CourseStart.Date;
			CourseEnd.Date = myCourse.CourseEnd.Date;
			InstructorName.Text = myCourse.InstName.ToString();
			InstructorEmail.Text = myCourse.InstEmail.ToString();
			InstructorPhone.Text = myCourse.InstPhone.ToString();
			EditNotes.Text = myCourse.Notes;
			NotificationEdit.IsEnabled = myCourse.Notification;


			foreach (var term in termList)
			{
				if (term.Id == myCourse.TermId)
				{
					TermSelect.SelectedItem = term;
					break;
				}
			}
		}

		private void SaveCourse_Clicked(object sender, EventArgs e)
		{

		}

		async void CancelCourse_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		async  void DeleteCourse_Clicked(object sender, EventArgs e)
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