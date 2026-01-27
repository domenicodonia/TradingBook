using System;
using System.IO;
using System.Windows;
using TradingBook.ApplicationLayer.UseCases;
using TradingBook.Infrastructure.Bootstrap;
using TradingBook.Infrastructure.Repositories;
using TradingBook.UI.ViewModels;
using TradingBook.UI.Views;


namespace TradingBook.UI;

public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dir = Path.Combine(appData, "TradingBook");
        Directory.CreateDirectory(dir);
        var dbPath = Path.Combine(dir, "tradingbook.db");

        var dbContextFactory = InfrastructureBootstrapper.CreateDbContextFactory(dbPath);

        try
        {
            InfrastructureBootstrapper.Migrate(dbContextFactory);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Errore inizializzazione database:\n{ex.Message}",
                "TradingBook",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            Shutdown();
            return;
        }

        var statusRepository = new EfStatusRepository(dbContextFactory);
        var getStatusTextUseCase = new GetStatusTextUseCase(statusRepository);
        var mainViewModel = new MainViewModel(getStatusTextUseCase);

        var mainWindow = new MainWindow { DataContext = mainViewModel };
        mainWindow.Show();
    }

}
