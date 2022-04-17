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
                var output = connection.Query<Contact>("SELECT * FROM Contact", new DynamicParameters());
                return new ObservableCollection<Contact>(output);
            }
        }

        public void SaveContact(Contact contact)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("INSERT INTO Contact (FirstName, Lastname, Email, Gender) VALUES (@FirstName, @Lastname, @Email, @Gender)", contact);
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("UPDATE Contact (FirstName, Lastname, Email, Gender) VALUES (@FirstName, @Lastname, @Email, @Gender) WHERE Id = @Id", contact.Id);
            }
        }

        public void DeleteContact(Contact contact)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("DELETE FROM Contact WHERE Id = @Id", new { Id = contact.Id });
            }
        }

        private string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}