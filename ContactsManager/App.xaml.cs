﻿using ContactsManager.Interfaces;
using ContactsManager.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ContactsManager
{

    public partial class App : Application
    {

        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDataService, DataService>();
            return services.BuildServiceProvider();
        }
    }
}