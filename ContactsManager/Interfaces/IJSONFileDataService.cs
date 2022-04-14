using ContactsManager.Models;
using System.Collections.ObjectModel;

namespace ContactsManager.Interfaces
{
    public interface IJSONFileDataService
    {
        ObservableCollection<Contact> GetContacts();
        void SaveContacts(ObservableCollection<Contact> contacts);
    }
}