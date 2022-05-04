using ContactsManager.ViewModels;
using System;
using System.Windows.Input;

namespace ContactsManager.Commands
{
    public class DeleteCommand : ICommand
    {

        private MainWindowViewModel _viewmodel { get; }

        public DeleteCommand(MainWindowViewModel viewmodel)
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
            return _viewmodel.CanDeleteContactCommandBe_Clicked(parameter);
        }

        public void Execute(object? parameter)
        {
            _viewmodel.DeleteContactCommand_Clicked(parameter);
        }
    }
}