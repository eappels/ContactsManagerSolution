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
        public ICommand viewCommand { get; set; }
        private int _switchView;

        /// <summary>
        /// Commands
        /// </summary>
        public ICommand CreateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public MainWindowViewModel()
        {
            Contacts = _sqldbService.GetContacts();
            CreateCommand = new RelayCommand(CreateCommand_Clicked, CanCreateCommandBe_Clicked);
            UpdateCommand = new RelayCommand(UpdateCommand_Clicked, CanUpdateCommandCommandBe_Clicked);
            DeleteCommand = new RelayCommand(DeleteCommand_Clicked, CanDeleteCommandBe_Clicked);
            ExitCommand = new RelayCommand(ExitCommand_Clicked, CanExitCommandBe_Clicked);
            viewCommand = new RelayCommand(viewCommand_Clicked, CanviewCommandBe_Clicked);
            SwitchView = 0;
        }        

        public Contact SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }

        private void ExitCommand_Clicked(object value)
        {
            App.Current.Shutdown();
        }

        private bool CanExitCommandBe_Clicked(object value)
        {
            return true;
        }

        private void CreateCommand_Clicked(object value)
        {
            SelectedContact = new Contact();
            Contacts.Add(SelectedContact);
        }

        private bool CanCreateCommandBe_Clicked(object value)
        {
             return false;
        }

        private void UpdateCommand_Clicked(object value)
        {
            _sqldbService.UpdateContact(SelectedContact);
        }

        private bool CanUpdateCommandCommandBe_Clicked(object value)
        {
            return SelectedContact != null;
        }

        private void DeleteCommand_Clicked(object value)
        {
            _sqldbService.DeleteContact(SelectedContact);
            Contacts.Remove(SelectedContact);
            SelectedContact = null;
        }

        private bool CanDeleteCommandBe_Clicked(object value)
        {
            return SelectedContact != null;
        }

        public int SwitchView
        {
            get => _switchView;
            set => SetProperty(ref _switchView, value);
        }

        private void viewCommand_Clicked(object value)
        {
            SwitchView = int.Parse(value.ToString());
        }

        private bool CanviewCommandBe_Clicked(object value)
        {
            return SwitchView != int.Parse(value.ToString());
        }
    }
}