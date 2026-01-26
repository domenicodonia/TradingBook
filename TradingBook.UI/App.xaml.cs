using System;
using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TradingBook.Infrastructure.Persistence;
using TradingBook.UI.Views;
using TradingBook.ApplicationLayer.UseCases;
using TradingBook.Infrastructure.Repositories;
using TradingBook.UI.ViewModels;

namespace TradingBook.UI
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Determine the database path
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appData, "TradingBook");
            Directory.CreateDirectory(dir);
            var dbPath = Path.Combine(dir, "TradingBook.db");

            //Build the DbContext options
            var options = new DbContextOptionsBuilder<TradingBookDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            //Initialize the database
            try
            {
                using var db = new TradingBookDbContext(options);
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error initializing database: {ex.Message}", 
                    "Database Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                Shutdown();
                return;
            }

            var statusRepository = new EfStatusRepository(options);
            var getStatusTextUseCase = new GetStatusTextUseCase(statusRepository);
            var mainViewModel = new MainViewModel(getStatusTextUseCase);

            //Show the main window
            var mainWindow = new MainWindow { DataContext = mainViewModel };
            mainWindow.Show();
        }
    }
}
