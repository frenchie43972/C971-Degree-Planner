using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace DegreePlanner.Models
{
	public class Course
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public int TermId { get; set; }
		public string CourseName { get; set; }
		public string CourseStatus { get; set; }
		public DateTime CourseStart { get; set; }
		public DateTime CourseEnd { get; set; }
		public string InstName { get; set; }
		public string InstEmail { get; set; }
		public string InstPhone { get; set; }
		public string Notes { get; set; }
		public bool Notification { get; set; }
	}
}
