using System.Windows;
using System.Windows.Input;
using Calculator.ViewModel;

namespace Calculator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();
        }

        // activating buttons via keyboard
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        { 
            var viewModel = DataContext as CalculatorViewModel;

            if (viewModel == null) return;

            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    viewModel.DigitCommand.Execute("0");
                    break;
                case Key.D1:
                case Key.NumPad1:
                    viewModel.DigitCommand.Execute("1");
                    break;
                case Key.D2:
                case Key.NumPad2:
                    viewModel.DigitCommand.Execute("2");
                    break;
                case Key.D3:
                case Key.NumPad3:
                    viewModel.DigitCommand.Execute("3");
                    break;
                case Key.D4:
                case Key.NumPad4:
                    viewModel.DigitCommand.Execute("4");
                    break;
                case Key.D5:
                case Key.NumPad5:
                    viewModel.DigitCommand.Execute("5");
                    break;
                case Key.D6:
                case Key.NumPad6:
                    viewModel.DigitCommand.Execute("6");
                    break;
                case Key.D7:
                case Key.NumPad7:
                    viewModel.DigitCommand.Execute("7");
                    break;
                case Key.D8:
                case Key.NumPad8:
                    viewModel.DigitCommand.Execute("8");
                    break;
                case Key.D9:
                case Key.NumPad9:
                    viewModel.DigitCommand.Execute("9");
                    break;
                case Key.Add:
                    viewModel.OperationCommand.Execute("+");
                    break;
                case Key.Subtract:
                    viewModel.OperationCommand.Execute("-");
                    break;
                case Key.Multiply:
                    viewModel.OperationCommand.Execute("×");
                    break;
                case Key.Divide:
                    viewModel.OperationCommand.Execute("÷");
                    break;
                case Key.OemPeriod:
                case Key.Decimal:
                    viewModel.DecimalCommand.Execute(null);
                    break;
                case Key.Enter:
                    viewModel.EqualsCommand.Execute(null);
                    break;
                case Key.Delete:
                    viewModel.ClearCommand.Execute(null);
                    break;
            }

            e.Handled = true;
        }
    }
}
