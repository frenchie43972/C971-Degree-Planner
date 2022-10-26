using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DegreePlanner.Models;
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

		async void SaveCourse_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CourseAdd());
		}

		async void CancelCourse_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		private void AddCourseStart_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

		private void AddCourseEnd_DateSelected(object sender, DateChangedEventArgs e)
		{

		}
	}
}