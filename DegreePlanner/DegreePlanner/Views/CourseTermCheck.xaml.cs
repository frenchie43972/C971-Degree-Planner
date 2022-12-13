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
	public partial class CourseTermCheck : ContentPage
	{
		public CourseTermCheck()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			Term getTerm = new Term();
			Course getCourse = new Course();

			if (getTerm.Id == getCourse.Id)
			{
				TermCourses.ItemsSource = await DatabaseServices.GetCourse();
				return;
			}
		}

		private void TermCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}