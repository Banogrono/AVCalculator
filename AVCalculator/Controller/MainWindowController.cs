using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Input;
using AVCalculator.Model;
using ReactiveUI;

namespace AVCalculator.Controller
{
    public class MainWindowController : ReactiveObject
    {
        // private bool _hasBeenSet = false;

        public delegate void Handle(object source, KeyEventArgs e);

        private string _calcWindowText;

        public MainWindowController()
        {
            _calcWindowText = "0";
            CalcWindowText = "0";
        }

        public string CalcWindowText
        {
            get => _calcWindowText;
            set
            {
                this.RaiseAndSetIfChanged(ref _calcWindowText,
                    value); // setting up event so UI can refresh accordingly 
                const string pattern = "([0-9]*[.]?[0-9]*)";
                var result = Regex.IsMatch(value, pattern);
                if (result)
                {
                    if (_calcWindowText.Equals("0") && !value.Equals("."))
                        _calcWindowText = "";
                    else
                        _calcWindowText = value;
                }
            }
        }


        // Button commands

        // numbers
        public void Button0_Click()
        {
            CalcWindowText += "0";
        }

        public void Button1_Click()
        {
            CalcWindowText += "1";
        }

        public void Button2_Click()
        {
            CalcWindowText += "2";
        }

        public void Button3_Click()
        {
            CalcWindowText += "3";
        }

        public void Button4_Click()
        {
            CalcWindowText += "4";
        }

        public void Button5_Click()
        {
            CalcWindowText += "5";
        }

        public void Button6_Click()
        {
            CalcWindowText += "6";
        }

        public void Button7_Click()
        {
            CalcWindowText += "7";
        }

        public void Button8_Click()
        {
            CalcWindowText += "8";
        }

        public void Button9_Click()
        {
            CalcWindowText += "9";
        }

        // functions
        public void ButtonC_Click()
        {
            CalculatorCore.SetOperation(Operation.Clear);
            CalculatorCore.Calculate();
            CalcWindowText = "0";
        }

        public void ButtonCE_Click()
        {
            CalculatorCore.SetOperation(Operation.ClearEntity);
            CalculatorCore.Calculate();
            CalcWindowText = "0";
        }

        public void ButtonPlus_Click()
        {
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalculatorCore.SetOperation(Operation.Add);
            CalcWindowText = "0";
        }

        public void ButtonMinus_Click()
        {
            if (CalcWindowText.Equals("0") || CalcWindowText.Equals(""))
            {
                CalcWindowText = "-";
            }
            else
            {
                CalculatorCore.SetNumber(double.Parse(CalcWindowText));
                CalculatorCore.SetOperation(Operation.Subtract);
                CalcWindowText = "0";
            }
        }

        public void ButtonMultiply_Click()
        {
            SetZeroIfEmpty();
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalculatorCore.SetOperation(Operation.Multiply);
            CalcWindowText = "0";
        }

        public void ButtonDivide_Click()
        {
            SetZeroIfEmpty();
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalculatorCore.SetOperation(Operation.Divide);
            CalcWindowText = "0";
        }

        public void ButtonDot_Click()
        {
            CalcWindowText += ".";
        }

        // TODO: make that dividing by 0 displays information about illegal operation
        public void ButtonEquals_Click()
        {
            SetZeroIfEmpty();
            var value = CalcWindowText == "" ? 0 : double.Parse(CalcWindowText);
            CalculatorCore.SetNumber(value); // setting second number
            var result = CalculatorCore.Calculate();
            CalcWindowText = result;
        }

        public void ButtonMemPlus_Click()
        {
            SetZeroIfEmpty();
            CalculatorCore.SetMemory(double.Parse(CalcWindowText));
        }

        public void ButtonMemRec_Click()
        {
            CalcWindowText = CalculatorCore.GetMemory().ToString(CultureInfo.CurrentCulture);
        }

        /*Primary use for backspace*/
        public void RemoveLastDigit()
        {
            if (CalcWindowText.Length > 1)
                CalcWindowText = CalcWindowText.Remove(CalcWindowText.Length - 1);
            else if (CalcWindowText.Length == 1) CalcWindowText = "0";
        }

        // TODO: Why aren't you working
        private void SetZeroIfEmpty()
        {
            if (CalcWindowText == "" || CalcWindowText.Equals(null)) CalcWindowText = "0";
        }
    }
}