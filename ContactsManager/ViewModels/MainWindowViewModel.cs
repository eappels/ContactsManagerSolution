using ContactsManager.Commands;
using ContactsManager.Helpers;
using ContactsManager.Interfaces;
using ContactsManager.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ContactsManager.ViewModels
{
    /// <summary>
    /// MVVM pattern - View Model
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {

        private IJSONFileDataService? _jsondataService = App.Current.Services.GetService<IJSONFileDataService>();
        private ISQLdbService? _sqldbService = App.Current.Services.GetService<ISQLdbService>(); 
        public ObservableCollection<Contact>? Contacts { get; set; }
        private Contact? _selectedContact;
        private Contact? _createContact = new Contact();
        public ICommand ViewCommand { get; set; }
        private int _switchView;
        public ICommand SaveContactCommand { get; set; }
        public ICommand ReloadContactsListCommand { get; set; }
        public ICommand UpdateContactCommand { get; set; }
        public ICommand DeleteContactCommand { get; set; }
        public ICommand ImportContactsCommand { get; }
        public ICommand ExportContactsCommand { get; }
        public ICommand ExitCommand { get; }

        public MainWindowViewModel()
        {
            Contacts = _sqldbService?.GetContacts();
            ExitCommand = new ExitCommand(this);
            ViewCommand = new ViewCommand(this);
            SaveContactCommand = new SaveContactCommand(this);
            UpdateContactCommand = new UpdateContactCommand(this);
            DeleteContactCommand = new DeleteCommand(this);
            ReloadContactsListCommand = new ReloadContactsListCommand(this);
            ImportContactsCommand = new ImportContactsCommand(this);
            ExportContactsCommand = new ExportContactsCommand(this);
            SwitchView = 0;
        }

        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }

        public Contact? CreateContact
        {
            get => _createContact;
            set => SetProperty(ref _createContact, value);
        }

        public void ExitCommand_Clicked(object value)
        {
            App.Current.Shutdown();
        }

        public int SwitchView
        {
            get => _switchView;
            set => SetProperty(ref _switchView, value);
        }

        public void ViewCommand_Clicked(object value)
        {
            Debug.WriteLine(value);
            SwitchView = int.Parse((string)value);
        }

        public bool CanviewCommandBe_Clicked(object value)
        {
            return SwitchView != int.Parse((string)value);
        }

        /// <summary>
        /// CRUD
        /// </summary>

        public void CreateContactCommand_Click(object value)
        {
            if (_createContact != null)
            {
                _sqldbService?.InsertContact(_createContact);
                ReloadContactsList();
            }
        }

        public void UpdateContactCommand_Click(object value)
        {
            Contact? tmpContact = SelectedContact;
            if (SelectedContact != null)
            {
                _sqldbService?.UpdateContact(SelectedContact);
                ReloadContactsList();
                SelectedContact = tmpContact;
            }
        }

        public bool CanUpdateContactCommandBe_Clicked(object value)
        {
            return SelectedContact != null;
        }

        public void DeleteContactCommand_Clicked(object value)
        {
            if (SelectedContact != null)
            {
                _sqldbService?.DeleteContact(SelectedContact);
                SelectedContact = null;
                ReloadContactsList();
            }
        }

        public bool CanDeleteContactCommandBe_Clicked(object value)
        {
            return SelectedContact != null;
        }

        public void ReloadContactsListCommand_Clicked(object value)
        {
            ReloadContactsList();
        }

        public void ImportContactsCommand_Clicked(object value)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                if (_jsondataService != null)
                {
                    ObservableCollection<Contact> contacts = _jsondataService.GetContacts((string)openFileDialog.FileName);
                    _sqldbService?.ProcessImportedContacts((ObservableCollection<Contact>)contacts);
                    ReloadContactsList();
                }
            }
        }

        public void ExportContactsCommand_Clicked(object value)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json files (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                if (Contacts != null && _jsondataService != null)
                {
                    ReloadContactsList();
                    _jsondataService?.ExportContactstoFile(Contacts, (string)saveFileDialog.FileName);
                }
            }
        }

        public bool CanExportContactsCommandBe_Clicked(object value)
        {
            if (Contacts != null)
                return Contacts.Count > 0;
            else
                return false;
        }

        private void ReloadContactsList()
        {
            if (Contacts != null)
            {
                Contacts.Clear();
                ObservableCollection<Contact>? contacts = _sqldbService?.GetContacts();
                if (contacts != null)
                {
                    foreach (Contact contact in contacts)
                    {
                        Contacts.Add(contact);
                    }
                }
            }
        }
    }
}