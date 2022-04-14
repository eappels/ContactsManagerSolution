using ContactsManager.Helpers;
using ContactsManager.Interfaces;
using ContactsManager.Models;
using System.Collections.ObjectModel;

namespace ContactsManager.Services
{
    public class DataService : IDataService
    {
        public int SelectedContactId
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public void AddContact(Contact contact)
        {
            throw new System.NotImplementedException();
        }

        public Contact GetContact(int id)
        {
            throw new System.NotImplementedException();
        }

        public ObservableCollection<Contact> GetContacts()
        {
            return JsonHelper.ReadFromFile();
        }

        public void UpdateContact(Contact contact)
        {
            throw new System.NotImplementedException();
        }
    }
}