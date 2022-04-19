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

        public ObservableCollection<Contact> GetContacts(string inputFile)
        {
            string json = File.ReadAllText(inputFile);
            return JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json);
        }

        public void ExportContactstoFile(ObservableCollection<Contact> contacts, string outputFile)
        {
            string json = JsonConvert.SerializeObject(contacts);
            try
            {
                File.WriteAllText(outputFile, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}