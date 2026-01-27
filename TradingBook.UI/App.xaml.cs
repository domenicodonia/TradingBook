using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using TradingBook.ApplicationLayer.Repositories;
using TradingBook.ApplicationLayer.UseCases;
using TradingBook.Infrastructure.Bootstrap;
using TradingBook.Infrastructure.Persistence;
using TradingBook.Infrastructure.Repositories;
using TradingBook.UI.ViewModels;
using TradingBook.UI.Views;

namespace TradingBook.UI
{
    public partial class App : Application
    {
        private static void ConfigureServices(IServiceCollection services)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appData, "TradingBook");
            Directory.CreateDirectory(dir);
            var dbPath = Path.Combine(dir, "tradingbook.db");

            // 1) crea UNA factory
            var dbContextFactory = InfrastructureBootstrapper.CreateDbContextFactory(dbPath);

            // 2) migra UNA volta
            InfrastructureBootstrapper.Migrate(dbContextFactory);

            // 3) registra l'istanza (overload che non sbaglia mai)
            services.AddSingleton(typeof(IDbContextFactory<TradingBookDbContext>), dbContextFactory);

            // repository + use case + vm + view
            services.AddScoped<IStatusRepository, EfStatusRepository>();
            services.AddScoped<GetStatusTextUseCase>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            using var provider = services.BuildServiceProvider();

            var mainWindow = provider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
