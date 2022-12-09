using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using DegreePlanner.Models;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Essentials;

namespace DegreePlanner.Services
{
	public static class DatabaseServices
	{
		private static SQLiteAsyncConnection _db;

		static async Task Init()
		{
			// Checks is DB exists and will not recreate a new instance
			if (_db != null)
			{
				return;
			}

			var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Term03.db");

			_db = new SQLiteAsyncConnection(databasePath);

			await _db.CreateTableAsync<Term>();
			await _db.CreateTableAsync<Course>();
			await _db.CreateTableAsync<Assessment>();
		}

		#region Term Methods
		public static async Task AddTerm(string name, DateTime start, DateTime end)
		{
			await Init();
			var term = new Term()
			{
				TermName = name,
				TermStart = start,
				TermEnd = end,
			};

			await _db.InsertAsync(term);
			var id = term.Id;
		}

		public static async Task RemoveTerm(int id)
		{
			await Init();

			await _db.DeleteAsync<Term>(id);
		}

		public static async Task<IEnumerable<Term>> GetTerm()
		{
			await Init();

			var terms = await _db.Table<Term>().ToListAsync();
			return terms;
		}

		public static async Task UpdateTerm(int id, string name, DateTime start, DateTime end)
		{
			await Init();

			var termQuery = await _db.Table<Term>()
				.Where(i => i.Id == id)
				.FirstOrDefaultAsync();

			if (termQuery != null)
			{
				termQuery.TermName = name;
				termQuery.TermStart = start;
				termQuery.TermEnd = end;

				await _db.UpdateAsync(termQuery);
			}
		}
		#endregion

		#region Course Methods

		public static async Task AddCourse(int termId, string courseName, string courseStatus,
					DateTime courseStart, DateTime courseEnd, string instName, string instEmail,
					string instPhone, string notes, bool notifiyStart, bool notifiyEnd)
		{
			await Init();
			var course = new Course
			{
				TermId = termId,
				CourseName = courseName,
				CourseStatus = courseStatus,
				CourseStart = courseStart,
				CourseEnd = courseEnd,
				InstName = instName,
				InstEmail = instEmail,
				InstPhone = instPhone,
				Notes = notes,
				NotificationStart = notifiyStart,
				NotificationEnd = notifiyEnd,
			};

			await _db.InsertAsync(course);

			var id = course.Id;
		}

		public static async Task RemoveCourse(int id)
		{
			await Init();

			await _db.DeleteAsync<Course>(id);
		}

		public static async Task<IEnumerable<Course>> GetCourse(int termId)
		{
			await Init();

			var courses = await _db.Table<Course>().Where(i => i.TermId == termId).ToListAsync();

			return courses;
		}

		public static async Task<IEnumerable<Course>> GetCourse()
		{
			await Init();

			var courses = await _db.Table<Course>().ToListAsync();

			return courses;
		}

		public static async Task UpdateCourse(int id, int termId, string courseName, string courseStatus,
					DateTime courseStart, DateTime courseEnd, string instName, string instEmail,
					string instPhone, string notes, bool notifiyStart, bool notifiyEnd)
		{
			await Init();

			var courseQuery = await _db.Table<Course>()
				.Where(i => i.Id == id)
				.FirstOrDefaultAsync();

			if (courseQuery != null)
			{
				courseQuery.TermId = termId;
				courseQuery.CourseName = courseName;
				courseQuery.CourseStatus = courseStatus;
				courseQuery.CourseStart = courseStart;
				courseQuery.CourseEnd = courseEnd;
				courseQuery.InstName = instName;
				courseQuery.InstEmail = instEmail;
				courseQuery.InstPhone = instPhone;
				courseQuery.Notes = notes;
				courseQuery.NotificationStart = notifiyStart;
				courseQuery.NotificationEnd = notifiyEnd;

				await _db.UpdateAsync(courseQuery);
			}
		}

		#endregion

		#region Assessment Methods
		public static async Task AddAssess(int courseId, string asessType, DateTime dueDate, bool assessNotify)
		{
			await Init();

			var assessment = new Assessment
			{
				CourseId = courseId,
				TypeAssess = asessType,
				AssessDueDate = dueDate,
				Notifications = assessNotify,
			};

			await _db.InsertAsync(assessment);

			var id = assessment.AssessId;
		}

		public static async Task RemoveAssess(int id)
		{
			await Init();

			await _db.DeleteAsync<Assessment>(id);
		}

		public static async Task<IEnumerable<Assessment>> GetAssessment(int courseId)
		{
			await Init();

			var assessments = await _db.Table<Assessment>().Where(i => i.CourseId == courseId).ToListAsync();

			return assessments;
		}

		public static async Task<IEnumerable<Assessment>> GetAssessment()
		{
			await Init();

			var assessments = await _db.Table<Assessment>().ToListAsync();

			return assessments;
		}

		public static async Task UpdateAssess(int id, int courseId, string asessType, DateTime dueDate, bool assessNotify)
		{
			await Init();

			var assessQuery = await _db.Table<Assessment>()
				.Where(i => i.AssessId == id)
				.FirstOrDefaultAsync();

			if (assessQuery != null)
			{
				assessQuery.CourseId = courseId;
				assessQuery.TypeAssess = asessType;
				assessQuery.AssessDueDate = dueDate;
				assessQuery.Notifications = assessNotify;
				

				await _db.UpdateAsync(assessQuery);
			}
		}

		#endregion

		#region Demo Data
		public static async Task LoadSampleData()
		{
			await Init();

			var terms = await _db.Table<Term>().ToListAsync();
			var courses = await _db.Table<Course>().ToListAsync();
			var assessments = await _db.Table<Assessment>().ToListAsync();


			if (terms.Count > 0 || courses.Count > 0 || assessments.Count > 0)
			{
				return;
			}

			Term term = new Term
			{
				TermName = "Winter Term",
				TermStart = new DateTime(2022, 12, 05),
				TermEnd = new DateTime(2023, 05, 05),
			};
			await _db.InsertAsync(term);

			Course course1 = new Course
			{
				TermId = term.Id,
				CourseName = "Winter Course",
				CourseStatus = "In Progress",
				CourseStart = new DateTime(2022, 12, 05),
				CourseEnd = new DateTime(2023, 01, 23),
				InstName = "Kris French",
				InstEmail = "kfren51@wgu.edu",
				InstPhone = "360-969-0322",
				Notes = " ",
				NotificationStart = false,
				NotificationEnd = false,
			};
			await _db.InsertAsync(course1);

			Assessment assessPA = new Assessment
			{
				CourseId = course1.Id,
				TypeAssess = "Performance Assessment",
				AssessDueDate = new DateTime(2023, 01, 23),
				Notifications = false,
			};
			await _db.InsertAsync(assessPA);

			Assessment assessOA = new Assessment
			{
				CourseId = course1.Id,
				TypeAssess = "Objective Assessment",
				AssessDueDate = new DateTime(2022, 12, 23),
				Notifications = false,
			};
			await _db.InsertAsync(assessOA);

		}

		public static async void ClearSampleData()
		{
			await Init();

			await _db.DropTableAsync<Term>();
			await _db.DropTableAsync<Course>();
			await _db.DropTableAsync<Assessment>();

			_db = null;
		}

		#endregion

	}
}
