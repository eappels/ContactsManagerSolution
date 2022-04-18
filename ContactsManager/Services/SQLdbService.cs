using ContactsManager.Interfaces;
using ContactsManager.Models;
using Dapper;
using System.Collections.Generic;
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

        public string _database = "ContactsManager.db";

        public SQLdbService()
        {
            if (File.Exists(_database)) File.Delete(_database);

            if (!File.Exists(_database))
            {
                SQLiteConnection.CreateFile(_database);
                using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
                {
                    var sql = "CREATE TABLE Contact (Id INTEGER PRIMARY KEY, FirstName TEXT, LastName TEXT, Email TEXT NOT NULL UNIQUE, Gender INTEGER);";
                    connection.Execute(sql, new DynamicParameters());
                }
                using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
                {
                    List<Contact> Contacts = new List<Contact>();
                    Contacts.Add(new Contact() { Id = 0, FirstName = "Johnathan", LastName = "Kaur", Email = "Johnathan_Kaur5376@nickia.com", Gender = GenderType.Male });
                    Contacts.Add(new Contact() { Id = 1, FirstName = "Jamie", LastName = "Townend", Email = "Jamie_Townend1075@nanoff.biz", Gender = GenderType.Female });
                    Contacts.Add(new Contact() { Id = 2, FirstName = "Melanie", LastName = "Newman", Email = "Melanie_Newman927@acrit.org", Gender = GenderType.Female });
                    Contacts.Add(new Contact() { Id = 3, FirstName = "Johnathan", LastName = "Cartwright", Email = "Johnathan_Cartwright8101@brety.org", Gender = GenderType.Male });
                    Contacts.Add(new Contact() { Id = 4, FirstName = "Russel", LastName = "Tennant", Email = "Russel_Tennant5329@dionrab.com", Gender = GenderType.Female });

                    foreach (Contact contact in Contacts)
                    {
                        var sql = "INSERT INTO Contact (FirstName, Lastname, Email, Gender) VALUES (@FirstName, @Lastname, @Email, @Gender)";
                        connection.Execute(sql, contact);
                    }
                }
            }
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