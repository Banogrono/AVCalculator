using System.Globalization;
using System.Text.RegularExpressions;
using AVCalculator.Model;
using ReactiveUI;

namespace AVCalculator.Controller
{
    public class MainWindowController : ReactiveObject
    {
        private string _calcWindowText;

        private bool _isOperationIllegal;

        // if operation is already set, then set this to true and in case of clicking any of operations buttons (add,
        // divide, multiply etc) just replace the operation instead of crashing (as prior to that change). 
        private bool _isOperationSet;

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
                    var illegalCharactersCounter = Regex.Matches(_calcWindowText, @"[a-zA-Z]").Count;
                    if (_calcWindowText.Equals("0") && !value.Equals(".") || illegalCharactersCounter > 0)
                    {
                        if (illegalCharactersCounter > 0)
                        {
                            _isOperationIllegal = true;
                            CalculatorCore.SetOperation(Operation.Clear);
                            CalculatorCore.Calculate();
                            _calcWindowText = "";
                        }

                        _calcWindowText = "";
                    }
                    else
                    {
                        _calcWindowText = value;
                        _isOperationIllegal = false;
                    }
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
            _isOperationSet = false;
        }

        public void ButtonCE_Click()
        {
            CalculatorCore.SetOperation(Operation.ClearEntity);
            CalculatorCore.Calculate();
            CalcWindowText = "0";
        }

        public void ButtonPlus_Click()
        {
            if (_isOperationSet)
            {
                CalculatorCore.SetOperation(Operation.Add);
            }
            else
            {
                if (_isOperationIllegal) return;

                CalculatorCore.SetNumber(double.Parse(CalcWindowText));
                CalculatorCore.SetOperation(Operation.Add);
                _isOperationSet = true;
                CalcWindowText = "0";
            }
        }

        public void ButtonMinus_Click()
        {
            if (_isOperationSet)
            {
                CalculatorCore.SetOperation(Operation.Subtract);
            }
            else
            {
                if (CalcWindowText.Equals("0") || CalcWindowText.Equals("") ||
                    CalcWindowText.Equals("illegal operation"))
                {
                    CalcWindowText = "-";
                }
                else
                {
                    if (_isOperationIllegal) return;

                    CalculatorCore.SetNumber(double.Parse(CalcWindowText));


                    CalculatorCore.SetOperation(Operation.Subtract);
                    _isOperationSet = true;
                    CalcWindowText = "0";
                }
            }
        }

        public void ButtonMultiply_Click()
        {
            if (_isOperationSet)
            {
                CalculatorCore.SetOperation(Operation.Multiply);
            }
            else
            {
                SetZeroIfEmpty();
                if (_isOperationIllegal) return;


                CalculatorCore.SetNumber(double.Parse(CalcWindowText));


                CalculatorCore.SetOperation(Operation.Multiply);
                _isOperationSet = true;
                CalcWindowText = "0";
            }
        }

        public void ButtonDivide_Click()
        {
            if (_isOperationSet)
            {
                CalculatorCore.SetOperation(Operation.Divide);
            }
            else
            {
                SetZeroIfEmpty();
                if (_isOperationIllegal) return;

                CalculatorCore.SetNumber(double.Parse(CalcWindowText));


                CalculatorCore.SetOperation(Operation.Divide);
                _isOperationSet = true;
                CalcWindowText = "0";
            }
        }

        public void ButtonDot_Click()
        {
            CalcWindowText += ".";
        }

        public void ButtonEquals_Click()
        {
            if (_isOperationIllegal) return;

            SetZeroIfEmpty();
            var value = CalcWindowText == "" ? 0 : double.Parse(CalcWindowText);
            CalculatorCore.SetNumber(value); // setting second number
            var result = CalculatorCore.Calculate();
            CalcWindowText = result;
            _isOperationSet = false;
        }

        public void ButtonMemPlus_Click()
        {
            if (_isOperationIllegal) return;

            SetZeroIfEmpty();
            var value = CalcWindowText == "" ? 0 : double.Parse(CalcWindowText); // issue #3 
            CalculatorCore.SetMemory(value);
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
        // TODO: adapt for 'illegal operation'
        private void SetZeroIfEmpty()
        {
            if (CalcWindowText == "" || CalcWindowText.Equals(null)) CalcWindowText = "0";
        }
    }
}