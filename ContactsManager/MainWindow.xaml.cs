using ContactsManager.ViewModels;
using System.Windows;

namespace ContactsManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}