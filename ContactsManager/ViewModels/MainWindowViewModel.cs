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
        private IJSONFileDataService? _jsondataService = App.Current.Services.GetService<IJSONFileDataService>();
        private ISQLdbService _sqldbService = App.Current.Services.GetService<ISQLdbService>(); 

        public ObservableCollection<Contact>? Contacts { get; set; }
        /// <summary>
        /// Commands
        /// </summary>
        public ICommand ReloadCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public MainWindowViewModel()
        {
            Contacts = _sqldbService.GetContacts();
            ReloadCommand = new RelayCommand(ReloadCommand_Clicked, CanReloadCommandBe_Clicked);
            AddCommand = new RelayCommand(AddCommand_Clicked, CanAddCommandBe_Clicked);
            DeleteCommand = new RelayCommand(DeleteCommand_Clicked, CanDeleteCommandBe_Clicked);
            SaveCommand = new RelayCommand(SaveCommand_Clicked, CanSaveCommandBe_Clicked);            
        }

        private Contact _selectedContact;

        public Contact SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }

        private void ReloadCommand_Clicked(object value)
        {
            Contacts.Clear();
            foreach (Contact contact in _sqldbService.GetContacts())
            {
                Contacts.Add(contact);
            }
        }

        private bool CanReloadCommandBe_Clicked(object value)
        {
            return _sqldbService.CanSaveToDB();
        }

        private void AddCommand_Clicked(object value)
        {
           SelectedContact = new Contact();
            Contacts.Add(SelectedContact);
        }

        private bool CanAddCommandBe_Clicked(object value)
        {
            return true;
        }

        private void DeleteCommand_Clicked(object value)
        {
            _sqldbService.DeleteContact(SelectedContact);
            Contacts.Clear();
            foreach (Contact contact in _sqldbService.GetContacts())
            {
                Contacts.Add(contact);
            }
        }

        private bool CanDeleteCommandBe_Clicked(object value)
        {
            if (SelectedContact != null) return true;
            else return false;
        }

        private void SaveCommand_Clicked(object value)
        {
             _sqldbService?.SaveContact(Contacts[Contacts.Count-1]);
            Contacts.Clear();
            foreach (Contact contact in _sqldbService.GetContacts())
            {
                Contacts.Add(contact);
            }
        }

        private bool CanSaveCommandBe_Clicked(object value)
        {
            return _sqldbService.CanSaveToDB();
        }
    }
}