using ContactsManager.Models;
using System.Collections.ObjectModel;

namespace ContactsManager.Interfaces
{
    public interface ISQLdbService
    {
        ObservableCollection<Contact> GetContacts();
        void SaveContacts(ObservableCollection<Contact> contacts);
        void SaveContact(Contact contact);
        void DeleteContact(Contact contact);
        bool CanSaveToDB();


        void AddContact(Contact contact);
    }
}