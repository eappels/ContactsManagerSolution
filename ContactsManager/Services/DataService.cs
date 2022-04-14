using ContactsManager.Helpers;
using ContactsManager.Interfaces;
using ContactsManager.Models;
using System.Collections.ObjectModel;

namespace ContactsManager.Services
{
    public class DataService : IDataService
    {
        public ObservableCollection<Contact> GetContacts()
        {
            return JsonHelper.ReadFromFile();
        }

        public void SaveContacts(ObservableCollection<Contact> contacts)
        {
            JsonHelper.WriteToFile(contacts);
        }
    }
}