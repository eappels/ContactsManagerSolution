using ContactsManager.Interfaces;
using ContactsManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace ContactsManager.Services
{
    public class JSONFileDataService  : IJSONFileDataService
    {

        private string _datafile = "Contacts.json";

        public ObservableCollection<Contact>? GetContacts()
        {
            string json = File.ReadAllText(_datafile);
            return JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json);
        }

        public void SaveContacts(ObservableCollection<Contact> contacts)
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
    }
}