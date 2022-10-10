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
		private static SQLiteAsyncConnection _dbConnection;

		static async Task Init()
		{
			// Checks is DB exists and will not recreate a new instance
			if (_db != null)
			{
				return;
			}

			var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Term.db");

			_db = new SQLiteAsyncConnection(databasePath);
			_dbConnection = new SQLiteAsyncConnection(databasePath);

			await _db.CreateTableAsync<Term>();
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

	}
}
