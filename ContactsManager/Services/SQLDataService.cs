using ContactsManager.Interfaces;
using ContactsManager.Models;
using System.Collections.ObjectModel;

namespace ContactsManager.Services
{
    public class SQLDataService : ISQLDataService
    {
        public ObservableCollection<Contact> GetContacts()
        {
            throw new System.NotImplementedException();
        }

        public void SaveContact(Contact contact)
        {
            throw new System.NotImplementedException();
        }

        public void SaveContacts(ObservableCollection<Contact> contacts)
        {
            throw new System.NotImplementedException();
        }
    }
}