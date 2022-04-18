using ContactsManager.Interfaces;
using ContactsManager.Models;
using Dapper;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace ContactsManager.Services
{
    /// <summary>
    /// SQLite service definition
    /// </summary>
    public class SQLdbService : ISQLdbService
    {

        public string _database = "Contacts.db";

        public SQLdbService()
        {
            if (!File.Exists(_database))
                SQLiteConnection.CreateFile(_database);
        }

        public ObservableCollection<Contact> GetContacts()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "SELECT * FROM Contact";
                var output = connection.Query<Contact>(sql, new DynamicParameters());
                return new ObservableCollection<Contact>(output);
            }
        }

        public void SaveContact(Contact contact)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "INSERT INTO Contact (FirstName, Lastname, Email, Gender) VALUES (@FirstName, @Lastname, @Email, @Gender)";
                connection.Execute(sql, contact);
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {                
                var sql = "UPDATE Contact SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Gender = @Gender WHERE Id = @Id";
                connection.Execute(sql, contact);
            }
        }

        public void DeleteContact(Contact contact)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "DELETE FROM Contact WHERE Id = @Id";
                connection.Execute(sql, new { Id = contact.Id });
            }
        }

        private string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}