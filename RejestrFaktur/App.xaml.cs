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
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                }).Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<InvoicesDbContext>(db => new InvoicesDbContext());
            services.AddSingleton<MainWindowView>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindowView>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }
    }
}
