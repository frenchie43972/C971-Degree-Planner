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

			await DatabaseServices.UpdateAssess(Int32.Parse(AssId.Text), c.Id, AssessType.SelectedItem.ToString(),
											DueDate.Date, NotifyEdit.IsToggled);
			await Navigation.PopAsync();
		}

		async void CancelEdit_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();

		}

		async void DeleteAssess_Clicked(object sender, EventArgs e)
		{
			var id = int.Parse(AssId.Text);

			var confirmDelete = await DisplayAlert("Confirm", "Are you sure you wnat to delete this record?", "Ok", "Cancel");

			if (confirmDelete == true)
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