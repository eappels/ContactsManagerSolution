using ContactsManager.Interfaces;
using ContactsManager.Services;
using ContactsManager.ViewModels;
using ContactsManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ContactsManager
{

    public partial class App : Application
    {

        public new static App Current => (App)Application.Current;
        /// <summary>
        /// IOC container
        /// </summary>
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var win = new ShellView();
            win.Show();
        }

        /// <summary>
        /// Adding the services to the IOC container
        /// </summary>
        /// <returns>Services</returns>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<MainWindowViewModel>();
            services.AddSingleton<IJSONFileDataService, JSONFileDataService >();
            services.AddSingleton<ISQLdbService, SQLdbService >();
            return services.BuildServiceProvider();
        }
    }
}