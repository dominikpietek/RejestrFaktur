using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RejestrFaktur.Databases;
using RejestrFaktur.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace RejestrFaktur
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindowView();
            mainWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
