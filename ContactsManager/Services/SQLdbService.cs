using ContactsManager.Interfaces;
using ContactsManager.Models;
using Dapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ContactsManager.Services
{
    public class SQLdbService : ISQLdbService
    {

        public string _database = "Contacts.db";
        private readonly SQLiteConnection _connection;

        public SQLdbService()
        {
            if (!File.Exists(_database))
                SQLiteConnection.CreateFile(_database);
            _connection = new SQLiteConnection("Data Source=.\\Contacts.db;Version=3;");
        }

        public ObservableCollection<Contact> GetContacts()
        {
            _connection.Open();
            var result = _connection.Query<Contact>(@"SELECT * FROM Contact").ToList();
            _connection.Close();
            return new ObservableCollection<Contact>(result.ToList());
        }

        public void SaveContact(Contact contact)
        {
            var sql = "insert into Contact (FirstName, LastName) values ('" + contact.FirstName + "', '" + contact.LastName + "')";
            _connection.Open();
            _connection.Execute(sql);
            _connection.Close();
        }

        public void SaveContacts(ObservableCollection<Contact> contacts)
        {
            SaveContact(contacts[0]);
        }

        public void DeleteContact(Contact contact)
        {
            var sql = "DELETE FROM Contact WHERE Id = @Id";
            _connection.Open();
            _connection.Execute(sql, new { Id = contact.Id });
            _connection.Close();
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }    

        public bool CanSaveToDB()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
                return true;
            else
                return false;
        }
    }
}