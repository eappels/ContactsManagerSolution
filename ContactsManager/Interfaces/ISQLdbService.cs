using ContactsManager.Models;
using System.Collections.ObjectModel;

namespace ContactsManager.Interfaces
{
    public interface ISQLdbService
    {
        ObservableCollection<Contact> GetContacts();
        void SaveContacts(ObservableCollection<Contact> contacts);
        ObservableCollection<Contact> SaveContact();
        bool CanSaveToDB();
    }
}