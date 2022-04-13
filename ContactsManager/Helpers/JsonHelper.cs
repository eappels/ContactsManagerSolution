using ContactsManager.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace ContactsManager.Helpers
{
    public static class JsonHelper
    {

        private static string _datafile = "Contacts.json";

        public static void WriteToFile(ObservableCollection<Contact> contacts)
        {
            string json = JsonConvert.SerializeObject(contacts);
            File.WriteAllText(_datafile, json);
        }

        public static ObservableCollection<Contact>? ReadFromFile()
        {
            string json = File.ReadAllText(_datafile);
            return JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json);
        }
    }
}