using System.Collections.Generic;
using System.Globalization;

namespace CalcCoreTest
{
    public static class CalculatorCore
    {
        private static double _memory;
        private static Queue<double> _outputQueue;
        private static Stack<Operation> _operationsStack;

        private static double _number1;
        private static double _number2;
        private static double result;
        private static Operation _operation;
        public static string operationString = "";


        public static string Calculate()
        {
            ConductOperation(_operation);
            return result.ToString(CultureInfo.InvariantCulture);
        }

        public static void SetNumber(double value)
        {
            operationString += value;
            _number1 = value;
        }

        public static void SetOperation(Operation value)
        {
            ConductOperation(value);
            _operation = value;
        }

        private static void ConductOperation(Operation value)
        {
            switch (value)
            {
                case Operation.Add:
                    operationString += "+";
                    result += _number1;
                    break;
                case Operation.Subtract:
                    operationString += "-";
                    result -= _number1;
                    break;
                case Operation.Divide:
                    operationString += "/";
                    result /= _number1;
                    break;
                case Operation.Multiply:
                    operationString += "*";
                    result *= _number1;
                    break;
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