using DataAccess.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataAccess.Data
{
    public static class SqliteContext
    {

        #region Configuration and Properties 
        private static string _dbpath { get; set; }
        private static async void UseSQLite(string databaseName = "sqlite.db")
        {

            await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName, CreationCollisionOption.OpenIfExists);

            _dbpath = $"filename = {Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName)}";

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "CREATE TABLE IF NOT EXISTS Customers (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Created DATETIME NOT NULL);CREATE TABLE IF NOT EXISTS Issues(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, CustomerId INTEGER NOT NULL, Title TEXT NOT NULL, Description TEXT NOT NULL, Status TEXT NOT NULL, Created DATATIME NOT NULL, FOREIGN KEY (CustomerId) REFERENCES Customers(Id)); CREATE TABLE IF NOT EXISTS Comments(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, IssueId INTEGER NOT NULL,  Description TEXT NOT NULL, Created DATATIME NOT NULL, FOREIGN KEY (IssueId) REFERENCES Issues(Id));";
                var cmd = new SqliteCommand(query, db);

                await cmd.ExecuteNonQueryAsync();
                db.Close();
            }
        }
        #endregion

        #region Create Methods

        public static async Task<int> CreateCustomerAsync (Customer customer)
        {
            int id = 0;

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "INSERT INTO  Customers VALUES(null,@Name,@Created);";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);

                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (int)await cmd.ExecuteScalarAsync();



                db.Close();
            }

            return id;
        }


        public static async Task<int> CreateIssueAsync(Issue issue)
        {
            int id = 0;

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "INSERT INTO  Issues VALUES(null,@CustomerId,@Title,@Description,@Status,@Created);";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@CustomerId", issue.CustomerId);
                cmd.Parameters.AddWithValue("@Title", issue.Title);
                cmd.Parameters.AddWithValue("@Description", issue.Description);
                cmd.Parameters.AddWithValue("@Status", "new");
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);

                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (int)await cmd.ExecuteScalarAsync();



                db.Close();
            }

            return id;
        }

        public static async Task CreateCommentAsync(Comment comment)
        {
            

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "INSERT INTO  Comments VALUES(null,@IssueId,@Description,@Created);";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@IssueId", comment.IssueId);
                
                cmd.Parameters.AddWithValue("@Description", comment.Description);
                
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);

                await cmd.ExecuteNonQueryAsync();

                


                db.Close();
            }

            
        }

        #endregion
    }
}
