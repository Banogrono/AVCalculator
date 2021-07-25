using System;
using System.Globalization;

namespace AVCalculator.Model
{
    public static class CalculatorCore
    {
        private static double _firstNumberComponent;
        private static double _secondNumberComponent;
        private static double _memory;
        private static Operation _selectedOperation;
        private static bool _isFirstComponentEntered;

        static CalculatorCore()
        {
            _firstNumberComponent = 0;
            _secondNumberComponent = 0;
            _memory = 0;
        }

        public static string Calculate()
        {
            string result = "0";
            double tempMemory = 0;
            try
            {
                switch (_selectedOperation)
                {
                    case Operation.Add:
                        tempMemory = _firstNumberComponent + _secondNumberComponent;
                        break;

                    case Operation.Subtract:
                        tempMemory = _firstNumberComponent - _secondNumberComponent;
                        break;

                    case Operation.Divide:
                        if (_firstNumberComponent == 0 || _secondNumberComponent == 0)
                        {
                            result = "NaN";
                            break;
                        }
                        else
                        {
                            tempMemory = _firstNumberComponent / _secondNumberComponent;
                            break;
                        }

                    case Operation.Multiply:
                        tempMemory = _firstNumberComponent * _secondNumberComponent;
                        break;

                    case Operation.Clear:
                        _isFirstComponentEntered = false;
                        _firstNumberComponent = 0;
                        _secondNumberComponent = 0;
                        _selectedOperation = Operation.None;
                        break;

                    case Operation.ClearEntity:
                        _secondNumberComponent = 0;
                        break;

                    case Operation.None:
                        break;
                }

                _firstNumberComponent = tempMemory;
                result = tempMemory.ToString(CultureInfo.CurrentCulture);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result;
        }

        public static void SetOperation(Operation operation)
        {
            _selectedOperation = operation;
            _isFirstComponentEntered = true;
        }

        public static void SetFirstComponent(double value)
        {
            _firstNumberComponent = value;
        }

        public static double GetFirstComponent()
        {
            return _firstNumberComponent;
        }

        public static void SetSecondComponent(double value)
        {
            _secondNumberComponent = value;
        }

        public static void SetNumber(double value)
        {
            if (_isFirstComponentEntered)
                _secondNumberComponent = value;
            else
                _firstNumberComponent = value;
        }

        public static void SetMemory(double value)
        {
            _memory = value;
        }

        public static double GetMemory()
        {
            return _memory;
        }
    }
}