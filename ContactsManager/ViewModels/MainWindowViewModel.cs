using ContactsManager.Helpers;
using ContactsManager.Interfaces;
using ContactsManager.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ContactsManager.ViewModels
{
    /// <summary>
    /// MVVM pattern used in a CRUD app
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {

        /// <summary>
        /// Service references
        /// </summary>
        private IJSONFileDataService _jsondataService = App.Current.Services.GetService<IJSONFileDataService>();
        private ISQLdbService _sqldbService = App.Current.Services.GetService<ISQLdbService>(); 
        public ObservableCollection<Contact> Contacts { get; set; }
        private Contact _selectedContact;
        
        /// <summary>
        /// Commands
        /// </summary>
        public ICommand ClearCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public MainWindowViewModel()
        {
            Contacts = _sqldbService.GetContacts();

            ClearCommand = new RelayCommand(ClearCommand_Clicked, CanClearCommandBe_Clicked);
            AddCommand = new RelayCommand(AddCommand_Clicked, CanAddCommandBe_Clicked);
            EditCommand = new RelayCommand(EditCommand_Clicked, CanEditCommandBe_Clicked);
            DeleteCommand = new RelayCommand(DeleteCommand_Clicked, CanDeleteCommandBe_Clicked);                               
        }        

        public Contact SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }

        /// <summary>
        /// Clear fields command
        /// </summary>
        /// <param name="value"></param>
        private void ClearCommand_Clicked(object value)
        {
            SelectedContact = null;            
        }

        private bool CanClearCommandBe_Clicked(object value)
        {
            return SelectedContact != null;
        }

        /// <summary>
        /// Add contact command
        /// </summary>
        /// <param name="value"></param>
        private void AddCommand_Clicked(object value)
        {
            SelectedContact = new Contact();
            Contacts.Add(SelectedContact);
            _sqldbService.AddContact(SelectedContact);
        }

        private bool CanAddCommandBe_Clicked(object value)
        {
            return true;
        }

        /// <summary>
        /// Delete contact command
        /// </summary>
        /// <param name="value"></param>
        private void EditCommand_Clicked(object value)
        {
            _sqldbService.DeleteContact(SelectedContact);
            ReloadContacts();
        }

        private bool CanEditCommandBe_Clicked(object value)
        {
            if (SelectedContact != null) return true;
            else return false;
        }

        /// <summary>
        /// Reload contacts list
        /// </summary>
        /// <param name="value"></param>
        private void DeleteCommand_Clicked(object value)
        {
            Contacts.Clear();
            foreach (Contact contact in _sqldbService.GetContacts())
            {
                Contacts.Add(contact);
            }
        }

        private bool CanDeleteCommandBe_Clicked(object value)
        {
            return _sqldbService.CanSaveToDB();
        }

        private void ReloadContacts()
        {
            Contacts.Clear();
            foreach (Contact c in _sqldbService.GetContacts())
            {
                Contacts.Add(c);
            }
        }
    }
}