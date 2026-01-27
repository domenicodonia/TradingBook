using TradingBook.UI.ViewModels;
using System.Windows;

namespace TradingBook.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

}
