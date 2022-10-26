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

		public CourseEdit(CourseEdit selectedCourse)
		{
			InitializeComponent();

			CourseId.Text = selectedCourse.Id.ToString();
			TermSelect.SelectedItem = selectedCourse.TermSelect; // No Idea how to get this here
		}

		private void SaveCourse_Clicked(object sender, EventArgs e)
		{

		}

		async void CancelCourse_Clicked(object sender, EventArgs e)
		{
			//await Init();
		}

		private void DeleteCourse_Clicked(object sender, EventArgs e)
		{

		}

		private void CourseStart_DateSelected(object sender, DateChangedEventArgs e)
		{

		}

		private void CourseEnd_DateSelected(object sender, DateChangedEventArgs e)
		{

		}
	}
}