using System.ComponentModel;
using System.Windows.Input;
using Calculator.Commands;
using Calculator.Models;

public class CalculatorViewModel : INotifyPropertyChanged
{
    private CalculatorState _calculatorState;

    public string Display { get; private set; } = "0.";
    public string OperationIndicator { get; private set; } = string.Empty;

    public ICommand DigitCommand { get; }
    public ICommand OperationCommand { get; }
    public ICommand EqualsCommand { get; }
    public ICommand ClearCommand { get; }
    public ICommand DecimalCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public CalculatorViewModel()
    {
        _calculatorState = new CalculatorState();

        DigitCommand = new RelayCommand(param => OnDigitInput(param.ToString()));
        DecimalCommand = new RelayCommand(param => OnDecimal());
        OperationCommand = new RelayCommand(OnOperationInput);
        EqualsCommand = new RelayCommand(param => OnEquals());
        ClearCommand = new RelayCommand(param => OnClear());
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void OnDigitInput(string digit)
    {
        _calculatorState.AddDigit(digit);
        UpdateDisplay();
    }

    public void OnDecimal()
    {
        _calculatorState.SetDecimalMode();
        UpdateDisplay();
    }

    public void OnOperationInput(object parameter)
    {
        if (parameter is string operation)
        {
            _calculatorState.SetOperation(operation);
            UpdateDisplay();
        }
    }

    public void OnEquals()
    {
        _calculatorState.CalculateResult();
        UpdateDisplay();
    }

    public void OnClear()
    {
        _calculatorState.Clear();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        Display = _calculatorState.CurrentDisplay;
        OperationIndicator = _calculatorState.CurrentOperationDisplay;
        OnPropertyChanged(nameof(Display));
        OnPropertyChanged(nameof(OperationIndicator));
    }
}
