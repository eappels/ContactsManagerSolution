using ContactsManager.Models;
using System.Collections.ObjectModel;

namespace ContactsManager.Interfaces
{
    public interface IDataService
    {
        ObservableCollection<Contact> GetContacts();
        Contact GetContact(int id);
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        int SelectedContactId { get; set; }
    }
}