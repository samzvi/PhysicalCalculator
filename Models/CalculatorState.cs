using System.Globalization;

namespace Calculator.Models
{
    public class CalculatorState
    {
        public string CurrentDisplay { get; private set; } = "0.";
        public string CurrentOperationDisplay { get; private set; } = string.Empty;

        private double _firstValue = 0;
        private double _lastValue = 0;
        private string _currentOperation = string.Empty;
        private bool _operationCalcs = false;
        private bool _isDecimalMode = false;
        private bool _enteringFirst = true;
        private bool _resetDisplay = false;

        public void AddDigit(string digit) // entering digits
        {
            double value = 0;

            if (_resetDisplay)
            {
                CurrentDisplay = "0.";
                _isDecimalMode = false;
                _resetDisplay = false;
            }

            if (!_isDecimalMode)
            {
                if (CurrentDisplay == "0.")
                {
                    if (digit == "0" || digit == "00")
                        return;
                    CurrentDisplay = digit + ".";
                }
                else if (CurrentDisplay.Length < 13)
                {
                    CurrentDisplay = CurrentDisplay.TrimEnd('.') + digit + ".";
                }
                value = double.Parse(CurrentDisplay.TrimEnd('.'));

            }
            else if (CurrentDisplay.Length < 15)
            {
                CurrentDisplay += digit;
                value = double.Parse(CurrentDisplay, CultureInfo.InvariantCulture);
            }

            if (_enteringFirst)
                _firstValue = value;
            else
            {
                _lastValue = value;
                _operationCalcs = true;
            }
        }

        public void SetDecimalMode()
        {
            if (_isDecimalMode) return;

            _isDecimalMode = true;
        }

        public void SetOperation(string operation) // any operation button pressed
        {
            if (_operationCalcs) // should operation button calculate?
            {
                CalculateResult();
                _operationCalcs = false;
            }
            if (_currentOperation == "×" && !(operation == "×"))
                (_firstValue, _lastValue) = (_lastValue, _firstValue); // switch values when changing from *
            _currentOperation = operation;
            CurrentOperationDisplay = operation;

            _resetDisplay = true;
            _enteringFirst = false;
            _operationCalcs = false;
        }

        public void CalculateResult()
        {
            if (string.IsNullOrEmpty(_currentOperation))
                return;

            double ans = 0;

            switch (_currentOperation)
            {
                case "+":
                    ans = _firstValue + _lastValue;
                    _firstValue = ans;
                    break;
                case "-":
                    ans = _firstValue - _lastValue;
                    _firstValue = ans;
                    break;
                case "×":
                    ans = _lastValue * _firstValue;
                    _lastValue = ans;
                    break;
                case "÷":
                    ans = _lastValue != 0 ? _firstValue / _lastValue : double.PositiveInfinity;
                    _firstValue = ans;
                    break;
            }

            // format and print the result
            PrintResult(ans);
            _enteringFirst = true;
            _operationCalcs = false;
            _resetDisplay = true;
        }

        public void PrintResult(double result)
        {
            // format/prepare result to print
            string formattedValue;

            if (Math.Abs(result) >= 1e12 || (Math.Abs(result) < 1e-11 && result != 0))
            {
                formattedValue = result.ToString("E", CultureInfo.InvariantCulture);
            }
            else
            {
                formattedValue = result.ToString("G12", CultureInfo.InvariantCulture);

                if (formattedValue.Length > 13)
                {
                    if (formattedValue.Contains("."))
                    {
                        int dotIndex = formattedValue.IndexOf('.');
                        int digitsBeforeDot = dotIndex;

                        int maxAfterDot = 12 - digitsBeforeDot;
                        formattedValue = result.ToString($"F{Math.Max(maxAfterDot, 0)}", CultureInfo.InvariantCulture);
                    }

                    if (formattedValue.Length > 13)
                        formattedValue = result.ToString("0", CultureInfo.InvariantCulture);
                }
            }

            if (!formattedValue.Contains("."))
                formattedValue += ".";

            CurrentDisplay = formattedValue;
            CurrentOperationDisplay = string.Empty;
        }
        public void Clear() // AC button
        {
            CurrentDisplay = "0.";
            CurrentOperationDisplay = string.Empty;
            _firstValue = 0;
            _lastValue = 0;
            _currentOperation = string.Empty;
            _operationCalcs = false;
            _isDecimalMode = false;
            _enteringFirst = true;
        }
    }
}