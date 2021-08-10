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

        public void ButtonPlus_Click()
        {
            if (CalcWindowText.Length < 1 || CalcWindowText.Equals("-")) return;
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalculatorCore.SetOperation(Operation.Add);

            CalcWindowText = "0";
        }

        // TODO: Fix -0 problem
        public void ButtonMinus_Click()
        {
            if (CalcWindowText.Length < 1)
            {
                CalcWindowText = "-";
                return;
            }

            var content = CalcWindowText.Length < 1 || CalcWindowText.Equals("-") ? "0" : CalcWindowText;
            CalculatorCore.SetNumber(double.Parse(content));
            CalculatorCore.SetOperation(Operation.Subtract);
            CalcWindowText = "0";
        }

        public void ButtonMultiply_Click()
        {
            if (CalcWindowText.Length < 1 || CalcWindowText.Equals("-")) return;
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalculatorCore.SetOperation(Operation.Multiply);
            CalcWindowText = "0";
        }

        public void ButtonDivide_Click()
        {
            if (CalcWindowText.Length < 1 || CalcWindowText.Equals("-")) return;
            CalculatorCore.SetNumber(double.Parse(CalcWindowText));
            CalculatorCore.SetOperation(Operation.Divide);
            CalcWindowText = "0";
        }

        public void ButtonEquals_Click()
        {
            var content = CalcWindowText.Length < 1 || CalcWindowText.Equals("-") ? "0" : CalcWindowText;
            CalculatorCore.SetNumber(double.Parse(content));
            CalcWindowText = CalculatorCore.Calculate();
            CalculatorCore.Clear();
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