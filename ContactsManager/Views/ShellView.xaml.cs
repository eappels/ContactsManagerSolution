using ContactsManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ContactsManager.Views
{
    /// <summary>
    /// MVVM pattern - View
    /// </summary>
    public partial class ShellView : Window
    {

        public ShellView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<MainWindowViewModel>();
        }
    }
}