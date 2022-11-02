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
		public CourseEdit()
		{
			InitializeComponent();
		}

		public CourseEdit(Course selectedCourse)
		{
			InitializeComponent();

			CourseId.Text = selectedCourse.Id.ToString();
			TermSelect.Title = selectedCourse.TermId.ToString(); // Need to display Term Name
			CourseName.Text = selectedCourse.CourseName.ToString();
			CourseStatus.Title = selectedCourse.CourseStatus.ToString();
			CourseStart.Date = selectedCourse.CourseStart.Date;
			CourseEnd.Date = selectedCourse.CourseEnd.Date;
			InstructorName.Text = selectedCourse.InstName.ToString();
			InstructorEmail.Text = selectedCourse.InstEmail.ToString();
			InstructorPhone.Text = selectedCourse.InstPhone.ToString();
			//EditNotes.Text = selectedCourse.Notes.ToString();
			//NotificationEdit = selectedCourse.Notification.ToString();
			
			// Set Datasource iterate through and select 
			//TermSelect.SelectedItem = selectedCourse.TermSelect; 
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
				await DatabaseServices.RemoveTerm(id);
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