using ContactsManager.Helpers;
using ContactsManager.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ContactsManager.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ObservableCollection<Contact>? Contacts { get; }
        public ICommand SaveCommand { get; set; }
        public MainWindowViewModel()
        {
            SaveCommand = new RelayCommand(SaveCommand_Clicked, CanSaveCommandBe_Clicked);
            Contacts = JsonHelper.ReadFromFile();
        }

        private Contact _selectedContact;

        public Contact SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }

        private void SaveCommand_Clicked(object value)
        {
            JsonHelper.WriteToFile(Contacts);
        }

        private bool CanSaveCommandBe_Clicked(object value)
        {
            return true;
        }
    }
}