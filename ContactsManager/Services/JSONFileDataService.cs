using ContactsManager.Interfaces;
using ContactsManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace ContactsManager.Services
{
    /// <summary>
    /// JSON service definition
    /// </summary>
    public class JSONFileDataService  : IJSONFileDataService
    {

        private string _datafile = "ContactsManager_export.json";

        public ObservableCollection<Contact> GetContacts(string inputFile)
        {
            string json = File.ReadAllText(inputFile);
            return JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json);
        }

        public void ExportContactstoFile(ObservableCollection<Contact> contacts)
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