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
	public partial class CourseAdd : ContentPage
	{
		public CourseAdd()
		{
			InitializeComponent();
		}

		private void SaveCourse_Clicked(object sender, EventArgs e)
		{

		}

		async void CancelCourse_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
	}
}