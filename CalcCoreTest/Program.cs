using System;

namespace CalcCoreTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CalculatorCore.SetNumber(3);
            CalculatorCore.SetOperation(Operation.Add);
            CalculatorCore.SetNumber(3);
            CalculatorCore.SetOperation(Operation.Add);
            CalculatorCore.SetNumber(3);
            CalculatorCore.SetOperation(Operation.Multiply);
            CalculatorCore.SetNumber(2);

            Console.WriteLine(CalculatorCore.Calculate());
            Console.WriteLine(CalculatorCore.operationString);
        }
    }
}