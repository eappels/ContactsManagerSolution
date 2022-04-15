﻿using ContactsManager.Helpers;
using ContactsManager.Interfaces;
using ContactsManager.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ContactsManager.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        private IJSONFileDataService? _jsondataService = App.Current.Services.GetService<IJSONFileDataService>();

        public ObservableCollection<Contact>? Contacts { get; }        
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public MainWindowViewModel()
        {
            Contacts = _jsondataService?.GetContacts();
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

        private void AddCommand_Clicked(object value)
        {

        }

        private bool CanAddCommandBe_Clicked(object value)
        {
            return true;
        }
        private void DeleteCommand_Clicked(object value)
        {

        }

        private bool CanDeleteCommandBe_Clicked(object value)
        {
            if (SelectedContact != null) return true;
            else return false;
        }

        private void SaveCommand_Clicked(object value)
        {
            _jsondataService?.SaveContacts(Contacts);
        }

        private bool CanSaveCommandBe_Clicked(object value)
        {
            return true;
        }
    }
}