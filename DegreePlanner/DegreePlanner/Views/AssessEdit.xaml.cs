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
	public partial class AssessEdit : ContentPage
	{
		Assessment myAssessment { get; set; }

		public AssessEdit(Assessment selectedAssess)
		{
			InitializeComponent();
			myAssessment = selectedAssess;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			var courseList = await DatabaseServices.GetCourse();
			CourseSelect.ItemsSource = (System.Collections.IList)courseList;

			AssId.Text = myAssessment.AssessId.ToString();
			AssessType.SelectedItem = myAssessment.TypeAssess;
			CourseSelect.Title = "Course Select";
			DueDate.Date = myAssessment.AssessDueDate.Date;
			NotifyEdit.IsToggled = myAssessment.Notifications;

			foreach (var course in courseList)
			{
				if (course.Id == myAssessment.CourseId)
				{
					CourseSelect.SelectedItem = course;
					break;
				}
			}
		}

		async void SaveEdit_Clicked(object sender, EventArgs e)
		{
			Course c = (Course)CourseSelect.SelectedItem;

			if (AssessType.SelectedItem == null)
			{
				await DisplayAlert("Error!", "Please select an assessment type.", "Ok");
				return;
			}
			else if (CourseSelect.SelectedItem == null)
			{
				await DisplayAlert("Error!", "Please select a course.", "Ok");
				return;
			}

			var getList = await DatabaseServices.GetAssessment(c.Id);
			int o = 0;
			int p = 0;

			foreach (Assessment a in getList)
			{
				// Validates Assessment IDs and ignores if they're the same or else you can 
				// save duplicate assessments
				if (a.AssessId != a.AssessId)
				{
					continue;
				}
				else
				{
					if (a.TypeAssess == "Objective Assessment")
					{
						o++;
					}
					if (a.TypeAssess == "Performance Assessment")
					{
						p++;
					}
				}
			}

			if (AssessType.SelectedItem.ToString() == "Objective Assessment" && o > 0)
			{
				await DisplayAlert("Error!", "You may only have one Objective Assessment for this course.", "Ok");
				return;
			}
			if (AssessType.SelectedItem.ToString() == "Performance Assessment" && p > 0)
			{
				await DisplayAlert("Error!", "You may only have one Performance Assessment for this course.", "Ok");
				return;
			}
			else
			{
				await DatabaseServices.UpdateAssess(Int32.Parse(AssId.Text), c.Id, AssessType.SelectedItem.ToString(),
											DueDate.Date, NotifyEdit.IsToggled);
				await Navigation.PopAsync();
			}
		}

		async void CancelEdit_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();

		}

		async void DeleteAssess_Clicked(object sender, EventArgs e)
		{
			var id = int.Parse(AssId.Text);

			var confirmDelete = await DisplayAlert("Confirm", "Are you sure you wnat to delete this record?", "Ok", "Cancel");

			if (confirmDelete)
			{
				await DatabaseServices.RemoveAssess(id);
				await Navigation.PopAsync();
			}
			else
			{
				return;
			}
		}

		private void DueDate_DateSelected(object sender, DateChangedEventArgs e)
		{

		}
	}
}