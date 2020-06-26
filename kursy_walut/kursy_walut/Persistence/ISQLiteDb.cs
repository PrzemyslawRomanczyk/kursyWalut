using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace kursy_walut.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
