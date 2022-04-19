using ContactsManager.Helpers;
using ContactsManager.Interfaces;
using ContactsManager.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ContactsManager.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        private IJSONFileDataService _jsondataService = App.Current.Services.GetService<IJSONFileDataService>();
        private ISQLdbService _sqldbService = App.Current.Services.GetService<ISQLdbService>(); 
        public ObservableCollection<Contact> Contacts { get; set; }
        private Contact _selectedContact;
        private Contact _createContact = new Contact();
        public ICommand viewCommand { get; set; }
        private int _switchView;
        public ICommand SaveContactCommand { get; set; }
        public ICommand ReloadContactsListCommand { get; set; }
        public ICommand UpdateContactCommand { get; set; }
        public ICommand DeleteContactCommand { get; set; }

        public ICommand ExitCommand { get; set; }

        public MainWindowViewModel()
        {
            Contacts = _sqldbService.GetContacts();
            ExitCommand = new RelayCommand(ExitCommand_Clicked, CanExitCommandBe_Clicked);
            viewCommand = new RelayCommand(viewCommand_Clicked, CanviewCommandBe_Clicked);
            SaveContactCommand = new RelayCommand(SaveContactCommand_Click, CanSaveContactCommandBe_Clicked);
            UpdateContactCommand = new RelayCommand(UpdateContactCommand_Click, CanUpdateContactCommandBe_Clicked);
            DeleteContactCommand = new RelayCommand(DeleteContactCommand_Clicked, CanDeleteContactCommandBe_Clicked);
            ReloadContactsListCommand = new RelayCommand(ReloadContactsListCommand_Clicked, CanReloadContactsListCommandBe_Clicked);
            SwitchView = 0;
        }        

        public Contact SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }

        public Contact CreateContact
        {
            get => _createContact;
            set => SetProperty(ref _createContact, value);
        }

        private void ExitCommand_Clicked(object value)
        {
            App.Current.Shutdown();
        }

        private bool CanExitCommandBe_Clicked(object value)
        {
            return true;
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

        private void SaveContactCommand_Click(object value)
        {
            _sqldbService.InsertContact(_createContact);
            ReloadContactsList();
        }

        private bool CanSaveContactCommandBe_Clicked(object value)
        {
            return true;
        }

        private void UpdateContactCommand_Click(object value)
        {
            _sqldbService.UpdateContact(SelectedContact);
            ReloadContactsList();
        }

        private bool CanUpdateContactCommandBe_Clicked(object value)
        {
            return SelectedContact != null;
        }

        private void DeleteContactCommand_Clicked(object value)
        {
            _sqldbService.DeleteContact(SelectedContact);
            SelectedContact = null;
            ReloadContactsList();
        }

        private bool CanDeleteContactCommandBe_Clicked(object value)
        {
            return SelectedContact != null;
        }

        private void ReloadContactsListCommand_Clicked(object value)
        {
            ReloadContactsList();
        }

        private bool CanReloadContactsListCommandBe_Clicked(object value)
        {
            return true;
        }

        private void ReloadContactsList()
        {
            Contacts.Clear();
            ObservableCollection<Contact> contacts = _sqldbService.GetContacts();
            foreach (Contact contact in contacts)
            {
                Contacts.Add(contact);
            }
        }
    }
}