using ContactsManager.Models;
using System.Collections.ObjectModel;

namespace ContactsManager.Interfaces
{
    public interface ISQLdbService
    {
        ObservableCollection<Contact> GetContacts();
        void InsertContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);
    }
}