﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace DegreePlanner.Services
{
	public static class Settings
	{
		public static bool FirstRun
		{
			get => Preferences.Get(nameof(FirstRun), true);
			set => Preferences.Set(nameof(FirstRun), value);
		}
	}
}
