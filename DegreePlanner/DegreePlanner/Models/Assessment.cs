using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DegreePlanner.Models
{
	public class Assessment
	{
		[PrimaryKey, AutoIncrement]
		public int AssessId { get; set; }
		public int CourseId { get; set; }
		public string TypeAssess { get; set; }
		public DateTime AssessDueDate { get; set; }
		public bool Notifications { get; set; }
	}
}
