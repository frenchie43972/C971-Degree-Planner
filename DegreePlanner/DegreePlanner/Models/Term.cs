using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DegreePlanner.Models
{
	public class Term
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string TermName { get; set; }
		public DateTime TermStart { get; set; }
		public DateTime TermEnd { get; set; }
	}
}
