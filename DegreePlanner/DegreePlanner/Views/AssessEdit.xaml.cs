using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DegreePlanner.Models;
using DegreePlanner.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DegreePlanner.Models;
using DegreePlanner.Services;

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
	}
}