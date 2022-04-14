using ContactsManager.Interfaces;
using ContactsManager.Services;
using ContactsManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ContactsManager
{

    public partial class App : Application
    {

        private IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDataService, DataService>();
            services.AddTransient<MainWindowViewModel>();            
            return services.BuildServiceProvider();
        }
    }
}