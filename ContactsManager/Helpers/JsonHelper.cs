using ContactsManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace ContactsManager.Helpers
{
    public static class JsonHelper
    {

        private static string _datafile = "Contacts.json";

        public static void WriteToFile(ObservableCollection<Contact> contacts)
        {
            string json = JsonConvert.SerializeObject(contacts);
            try
            {
                File.WriteAllText(_datafile, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static ObservableCollection<Contact>? ReadFromFile()
        {
            string json = File.ReadAllText(_datafile);
            return JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json);
        }
    }
}