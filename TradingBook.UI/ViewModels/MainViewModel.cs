using System.Threading.Tasks;
using System.Windows.Input;
using TradingBook.ApplicationLayer.UseCases;
using TradingBook.UI.Commands;

namespace TradingBook.UI.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    private readonly GetStatusTextUseCase _getStatusTextUseCase;

    private string _resultText = string.Empty;
    public string ResultText
    {
        get => _resultText;
        set
        {
            _resultText = value;
            OnPropertyChanged();
        }
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }

    public ICommand ReadStatusCommand { get; }

    public MainViewModel(GetStatusTextUseCase getStatusTextUseCase)
    {
        _getStatusTextUseCase = getStatusTextUseCase;
        ReadStatusCommand = new AsyncRelayCommand(ReadStatusAsync, () => !IsBusy);
    }

    private async Task ReadStatusAsync()
    {
        IsBusy = true;

        try
        {
            ResultText = await _getStatusTextUseCase.ExecuteAsync();
        }
        finally
        {
            IsBusy = false;

            if (ReadStatusCommand is AsyncRelayCommand cmd)
                cmd.RaiseCanExecuteChanged();
        }
    }
}
