using ContactsManager.ViewModels;
using System;
using System.Windows.Input;

namespace ContactsManager.Commands
{
    public class ExportContactsCommand : ICommand
    {

        private MainWindowViewModel _viewmodel { get; }

        public ExportContactsCommand(MainWindowViewModel viewmodel)
        {
            _viewmodel = viewmodel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _viewmodel.CanExportContactsCommandBe_Clicked(parameter);
        }

        public void Execute(object? parameter)
        {
            _viewmodel.ExportContactsCommand_Clicked(parameter);
        }
    }
}