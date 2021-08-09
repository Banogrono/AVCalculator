using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AVCalculator.Model
{
    public static class CalculatorCore
    {
        private static double _firstNumberComponent;
        private static double _secondNumberComponent;
        private static double _memory;
        private static Operation _selectedOperation;
        private static bool _isFirstComponentEntered;

        private static readonly Queue<double> _numberQueue;
        private static readonly Queue<Operation> _operationQueue;

        static CalculatorCore()
        {
            _firstNumberComponent = 0;
            _secondNumberComponent = 0;
            _memory = 0;

            _operationQueue = new Queue<Operation>();
            _numberQueue = new Queue<double>();
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

        public static string CalculateA()
        {
            string result = "";

            if (_operationQueue.Count < 1)
                // alternatively repeat last operation - but that requires no operation cleaning 
                return _numberQueue.Count > 0 ? _numberQueue.Last().ToString(CultureInfo.CurrentCulture) : "0";

            // if there is only one number and one operation
            if (_numberQueue.Count == 1 && _operationQueue.Count == 1)
            {
                var t = _numberQueue.Dequeue();
                result = ConductOperation(t, t, _operationQueue.Dequeue())
                    .ToString(CultureInfo.CurrentCulture);
                _operationQueue.Clear();
                return result;
            }


            var resultDouble =
                ConductOperation(_numberQueue.Dequeue(), _numberQueue.Dequeue(), _operationQueue.Dequeue());
            var numberOfOperations = _operationQueue.Count;

            for (var i = 0; i < numberOfOperations; i++)
            {
                if (_numberQueue.Count < 2) return resultDouble.ToString(CultureInfo.CurrentCulture);

                var operand = _numberQueue.Dequeue();
                var operation = _operationQueue.Dequeue();
                resultDouble += ConductOperation(resultDouble, operand, operation);
            }

            return result;
        }

        // conducts operation and returns numeric result 
        private static double ConductOperation(double a, double b, Operation operation)
        {
            switch (operation)
            {
                case Operation.Add:
                    return a + b;
                case Operation.Subtract:
                    return a - b;
                case Operation.Multiply:
                    return a * b;
                case Operation.Divide:
                    return a / b;
            }

            return 0;
        }

        public static void SetOperation(Operation operation)
        {
            _selectedOperation = operation;
            _isFirstComponentEntered = true;
        }

        public static void SetOperationQ(Operation operation)
        {
            _operationQueue.Enqueue(operation);
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

        public static void SetNumberQ(double value)
        {
            _numberQueue.Enqueue(value);
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