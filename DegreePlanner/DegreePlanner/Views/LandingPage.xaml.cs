﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		async void AddTerm_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new TermAdd());
		}
	}
}