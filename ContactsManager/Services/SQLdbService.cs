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

        public static string _database = "ContactsManager.db";

        public SQLdbService()
        {
#if DEBUG
            /// DELETE DB FOR DEVELOPMENT
            if (File.Exists(_database)) File.Delete(_database);
#endif
            ///If the database is not found, we create a new one
            if (!File.Exists(_database))
            {
                ///Create DB
                SQLiteConnection.CreateFile(_database);

                ///Create the table
                using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
                {
                    var sql = "CREATE TABLE Contacts (Id INTEGER PRIMARY KEY, FirstName TEXT, LastName TEXT, Email TEXT NOT NULL UNIQUE, Gender INTEGER);";
                    connection.Execute(sql, new DynamicParameters());
                }

#if DEBUG
                ///Add test data to the DB
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
                        var sql = "INSERT INTO Contacts (FirstName, Lastname, Email, Gender) VALUES (@FirstName, @Lastname, @Email, @Gender)";
                        connection.Execute(sql, contact);
                    }
                }
#endif
            }
        }

        /// <summary>
        /// CRUD get
        /// </summary>
        /// <returns>ObservableCollection<Contact></returns>
        public ObservableCollection<Contact> GetContacts()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "SELECT * FROM Contacts";
                var output = connection.Query<Contact>(sql);
                return new ObservableCollection<Contact>(output);
            }
        }

        /// <summary>
        /// CRUD insert
        /// </summary>
        /// <param name="contact"></param>
        public void InsertContact(Contact contact)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "INSERT INTO Contacts (FirstName, Lastname, Email, Gender) VALUES (@FirstName, @Lastname, @Email, @Gender)";
                connection.Execute(sql, contact);
            }
        }

        /// <summary>
        /// CRUD update
        /// </summary>
        /// <param name="contact"></param>
        public void UpdateContact(Contact contact)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {                
                var sql = "UPDATE Contacts SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Gender = @Gender WHERE Id = @Id";
                connection.Execute(sql, contact);
            }
        }

        /// <summary>
        /// CRUD delete
        /// </summary>
        /// <param name="contact"></param>
        public void DeleteContact(Contact contact)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "DELETE FROM Contacts WHERE Id = @Id";
                connection.Execute(sql, new { Id = contact.Id });
            }
        }

        /// <summary>
        /// DB connection string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public void ProcessImportedContacts(ObservableCollection<Contact> importedContacts)
        {
            ///Wipe DB
            if (File.Exists(_database)) File.Delete(_database);
            ///Create DB
            SQLiteConnection.CreateFile(_database);
            ///Create the table
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = "CREATE TABLE Contacts (Id INTEGER PRIMARY KEY, FirstName TEXT, LastName TEXT, Email TEXT NOT NULL UNIQUE, Gender INTEGER);";
                connection.Execute(sql, new DynamicParameters());
            }

            ///Add imported contacts to the DB
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (Contact contact in importedContacts)
                {
                    var sql = "INSERT INTO Contacts (FirstName, Lastname, Email, Gender) VALUES (@FirstName, @Lastname, @Email, @Gender)";
                    connection.Execute(sql, contact);
                }
            }
        }
    }
}