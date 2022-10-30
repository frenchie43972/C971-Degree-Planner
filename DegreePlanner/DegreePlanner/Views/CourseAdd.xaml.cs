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


			//foreach (var term in termList)
			//{
			//	await DisplayAlert("Error!", term.TermName.ToString(), "Ok");

			//	System.Collections.IList termName = term.TermName.ToList();
			//	AddCourseTerm.ItemsSource = termName;

			//}
		}

		async void SaveCourse_Clicked(object sender, EventArgs e)
		{
			//var t = await DatabaseServices.GetTerm();
			//AddCourseTerm.ItemsSource = (System.Collections.IList)t;

			Term t = (Term)AddCourseTerm.SelectedItem;

			await DatabaseServices.AddCourse(t.Id, AddCourseName.Text, (string)AddCourseStatus.SelectedItem,
									AddCourseStart.Date, AddCourseEnd.Date, AddCourseInst.Text, AddInstEmail.Text, AddInstPhone.Text,
									CourseNotes.Text, NotificationAdd.IsEnabled);
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