using ContactsManager.Models;
using System.Collections.ObjectModel;

namespace ContactsManager.Interfaces
{
    public interface ISQLDataService
    {
        ObservableCollection<Contact> GetContacts();
        void SaveContacts(ObservableCollection<Contact> contacts);
        void SaveContact(Contact contact);
    }
}