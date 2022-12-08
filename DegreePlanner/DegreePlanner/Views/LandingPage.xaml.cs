using DegreePlanner.Models;
using DegreePlanner.Services;
using Plugin.LocalNotifications;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DegreePlanner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LandingPage : ContentPage
	{
		public LandingPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			await DatabaseServices.LoadSampleData();

			var courseList = await DatabaseServices.GetCourse();
			var assessList = await DatabaseServices.GetAssessment();

			var notifyRandom = new Random();
			var notifyId = notifyRandom.Next(1000);

			foreach (Course listedCourse in courseList)
			{
				if (listedCourse.NotificationStart == true || listedCourse.NotificationEnd == true)
				{
					if (listedCourse.CourseStart == DateTime.Today)
					{
						CrossLocalNotifications.Current.Show("Notice", $"{listedCourse.CourseName} begins today.", notifyId);
					}
					if (listedCourse.CourseEnd == DateTime.Today)
					{
						CrossLocalNotifications.Current.Show("Notice", $"{listedCourse.CourseName} ends today.", notifyId);
					}
				}
			}

			foreach (Assessment listedAssess in assessList)
			{
				if (listedAssess.Notifications == true)
				{
					if (listedAssess.AssessDueDate == DateTime.Today)
					{
						CrossLocalNotifications.Current.Show("Notice", $"{listedAssess.TypeAssess} is due today.", notifyId);
					}
				}
			}
		}

		async void Terms_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new TermPage());
		}

		async void Courses_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CoursePage());
		}

		async void Assessments_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AssessPage());
		}
	}
}