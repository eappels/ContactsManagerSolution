using ContactsManager.Models;
using System.Collections.ObjectModel;

namespace ContactsManager.Interfaces
{
    public interface IJSONFileDataService
    {
        ObservableCollection<Contact> GetContacts(string inputFile);
        void ExportContactstoFile(ObservableCollection<Contact> contacts, string outputFile);
    }
}