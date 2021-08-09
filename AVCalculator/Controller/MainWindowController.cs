using System.Globalization;
using System.Text.RegularExpressions;
using AVCalculator.Model;
using ReactiveUI;

// ===========================================================
// Middle man between MainWindow and CalculatorCore 
// Maps buttons to methods in CalculatorCore 
// ===========================================================

namespace AVCalculator.Controller
{
    public class MainWindowController : ReactiveObject
    {
        private string _calcWindowText;

        // ===========================================================
        // Constructor(s)
        // ===========================================================
        public MainWindowController()
        {
            _calcWindowText = "0";
            CalcWindowText = "0";
        }

        // ===========================================================
        // CalcWindowText custom setter
        // ===========================================================
        public string CalcWindowText
        {
            get => _calcWindowText;
            set
            {
                this.RaiseAndSetIfChanged(ref _calcWindowText,
                    value); // setting up event so UI can refresh accordingly 

                var illegalCharactersCounter = Regex.Matches(_calcWindowText, @"[a-zA-Z]").Count;
                if (_calcWindowText.Equals("0") && !value.Equals(".") || illegalCharactersCounter > 0)
                {
                    if (illegalCharactersCounter > 0) _calcWindowText = "";

                    _calcWindowText = "";
                }
                else
                {
                    _calcWindowText = value;
                }
            }
        }

        // ===========================================================
        // BUTTON HANDLING
        // ===========================================================

        // -----------------------------------------------------------
        //  NUMBERS (and a dot)
        // -----------------------------------------------------------
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

        public void ButtonDot_Click()
        {
            CalcWindowText += ".";
        }

        // -----------------------------------------------------------
        //  OPERATIONS
        // -----------------------------------------------------------
        public void ButtonC_Click()
        {
            CalculatorCore.Clear();
            CalcWindowText = "0";
        }

        public void ButtonCE_Click()
        {
            CalculatorCore.DeleteLast();
            CalcWindowText = "0";
        }

        // URGENT
        // TODO: When one tries to perform an operation on result (after pressing enter/ equals), the result is getting 
        // TODO: sucked as an operand, which results in false result
        // TODO: eg. after performing: 3+1, the result is 4 - when one wants to add to the result say 1, the 4 form 
        // TODO: previous equation will be sucked in, and in consequence the equation will be interpreted as 4+4 rather
        // TODO: than 4+1 and result in false statement that 4+1=8. 
        public void ButtonPlus_Click()
        {
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalculatorCore.SetOperation(Operation.Add);

            CalcWindowText = "0";
        }

        public void ButtonMinus_Click()
        {
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalculatorCore.SetOperation(Operation.Subtract);
            CalcWindowText = "0";
        }

        public void ButtonMultiply_Click()
        {
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalculatorCore.SetOperation(Operation.Multiply);
            CalcWindowText = "0";
        }

        // TODO: Add necessary guard statements
        public void ButtonDivide_Click()
        {
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalculatorCore.SetOperation(Operation.Divide);
            CalcWindowText = "0";
        }

        public void ButtonEquals_Click()
        {
            // TODO: when enter/ equals pressed after setting operation, program crashes
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalcWindowText = CalculatorCore.Calculate();
        }

        public void ButtonMemPlus_Click()
        {
            var value = CalcWindowText == "" ? 0 : double.Parse(CalcWindowText); // issue #3 
            CalculatorCore.SetMemory(value);
        }

        public void ButtonMemRec_Click()
        {
            CalcWindowText = CalculatorCore.GetMemory().ToString(CultureInfo.CurrentCulture);
        }

        public void RemoveLastDigit()
        {
            if (CalcWindowText.Length > 1)
                CalcWindowText = CalcWindowText.Remove(CalcWindowText.Length - 1);
            else if (CalcWindowText.Length == 1) CalcWindowText = "0";
        }
    }
}

// =====================================================================================================================
// OLD VERSION BELOW 
// TODO: Fix new code and remove this crap
// =====================================================================================================================

/*
 
  
  OLD ONE 
  
  
  
 * using System;
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
            CalculatorCore.Clear();
            CalculatorCore.Calculate();
            CalcWindowText = "0";
            _isOperationSet = false;
        }

        public void ButtonCE_Click()
        {
            CalculatorCore.DeleteLast();
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

               // CalculatorCore.SetNumber(double.Parse(CalcWindowText));
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

                   // CalculatorCore.SetNumber(double.Parse(CalcWindowText));


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


                //CalculatorCore.SetNumber(double.Parse(CalcWindowText));


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

               // CalculatorCore.SetNumber(double.Parse(CalcWindowText));


                CalculatorCore.SetOperation(Operation.Divide);
                _isOperationSet = true;
                CalcWindowText = "0";
            }
        }

        public void ButtonDot_Click()
        {
            CalcWindowText += ".";
        }

        public void ButtonEquals_ClickOLD()
        {
            if (_isOperationIllegal) return;

            SetZeroIfEmpty();
            var value = CalcWindowText == "" ? 0 : double.Parse(CalcWindowText);
          //  CalculatorCore.SetNumber(value); // setting second number
            var result = CalculatorCore.Calculate();
            CalcWindowText = result;
            _isOperationSet = false;
        }
        
        public void ButtonEquals_Click()
        {
            CalcWindowText = CalculatorCore.Calculate();
         
        }

        public void ButtonMemPlus_Click()
        {
            // if (_isOperationIllegal) return;

            SetZeroIfEmpty();
            var value = CalcWindowText == "" ? 0 : double.Parse(CalcWindowText); // issue #3 
            CalculatorCore.SetMemory(value);
        }

        public void ButtonMemRec_Click()
        {
            CalcWindowText = CalculatorCore.GetMemory().ToString(CultureInfo.CurrentCulture);
        }

       
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
            if (CalcWindowText == ""
                || CalcWindowText.Equals(null)
                || CalcWindowText.ToLower().Contains("illegal operation"))
                CalcWindowText = "0";
        }
    }
}
*/