using System.Collections.Generic;
using System.Globalization;

namespace CalcCoreTest
{
    public static class CalculatorCore
    {
        private static double _memory;
        private static readonly List<double> Numbers;

        private static double _result;
        private static Operation _operation;

        static CalculatorCore()
        {
            Numbers = new List<double>();
        }

        public static string Calculate()
        {
            ConductOperation(_operation);
            return _result.ToString(CultureInfo.InvariantCulture);
        }

        public static void SetNumber(double value)
        {
            Numbers.Add(value);
        }

        public static void SetOperation(Operation value)
        {
            if (Numbers.Count > 1) ConductOperation(_operation);
            _operation = value;
        }

        private static void ConductOperation(Operation value)
        {
            _operation = value;
            switch (value)
            {
                case Operation.Add:
                    _result = Numbers[0] + Numbers[1];
                    break;
                case Operation.Subtract:
                    _result = Numbers[0] - Numbers[1];
                    break;
                case Operation.Divide:
                    _result = Numbers[0] / Numbers[1];
                    break;
                case Operation.Multiply:
                    _result = Numbers[0] * Numbers[1];
                    break;
            }

            Numbers.Clear();
            Numbers.Add(_result);
        }

        public static void ClearEntity()
        {
            _result = 0;
            Numbers.Clear();
        }

        public static void DeleteLast()
        {
            Numbers.Remove(1);
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