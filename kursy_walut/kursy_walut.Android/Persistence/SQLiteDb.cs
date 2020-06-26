using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using kursy_walut.Droid.Persistence;
using kursy_walut.Persistence;
using SQLite;
using Environment = Android.OS.Environment;
using Xamarin.Forms;
using kursy_walut.Droid;
using DependencyAttribute = Xamarin.Forms.DependencyAttribute;

[assembly: Dependency(typeof(SQLiteDb))]

namespace kursy_walut.Droid.Persistence
{
    public class SQLiteDb : ISQLiteDb
    {
		public SQLiteAsyncConnection GetConnection()
		{
			var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			var path = Path.Combine(documentsPath, "MySQLite.db3");

			return new SQLiteAsyncConnection(path);
		}
	}
}