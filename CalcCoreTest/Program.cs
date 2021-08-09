using System;

namespace CalcCoreTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // A test field - works like a charm
            CalculatorCore.SetNumber(2);
            CalculatorCore.SetOperation(Operation.Multiply);
            CalculatorCore.SetNumber(3);
            CalculatorCore.Calculate();
            CalculatorCore.SetOperation(Operation.Add);
            CalculatorCore.SetNumber(3);


            Console.WriteLine(CalculatorCore.Calculate());
        }
    }
}