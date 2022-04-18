using ContactsManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ContactsManager.Views
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.Current.Services.GetService<MainWindowViewModel>();
        }
    }
}