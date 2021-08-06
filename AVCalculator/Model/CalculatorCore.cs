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
            string result;
            double tempMemory = 0;
            var isOperationIllegal = false;
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
                            isOperationIllegal = true;
                            tempMemory = 0;
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
                if (isOperationIllegal)
                    result = "illegal operation"; // additional information for illegal operations also issue #3
                else
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

        public static void SetNumber(double value)
        {
            if (_isFirstComponentEntered)
            {
                _secondNumberComponent = value;
            }
            else
            {
                if (_firstNumberComponent != 0)
                    _firstNumberComponent += value;
                else
                    _firstNumberComponent = value;
            }
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