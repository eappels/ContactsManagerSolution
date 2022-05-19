using ContactsManager.ViewModels;
using System;
using System.Windows.Input;

namespace ContactsManager.Commands
{
    public class ExitCommand : ICommand
    {

        private MainWindowViewModel _viewmodel { get; }

        public ExitCommand(MainWindowViewModel viewmodel)
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
            return true;
        }

        public void Execute(object? parameter)
        {
            _viewmodel.ExitCommand_Clicked(parameter);
        }
    }
}