using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2_HW
{
    public class ButtonOperations
    {
        public string FirstValue { get; set; }
        public string SecondValue { get; set; }
        public OperationType OperationType { get; set; }
        public string Result { get; set; }

        public ButtonOperations()
        {
            FirstValue = String.Empty;
            SecondValue = String.Empty;
        }

        public void Clear()
        {
            FirstValue = String.Empty;
            SecondValue = String.Empty;
            Result = String.Empty;
        }
        public string Calculate()
        {
            if (String.IsNullOrWhiteSpace(FirstValue))
                return String.Empty;

            if (String.IsNullOrWhiteSpace(SecondValue))
                return FirstValue;


            var firstValue = Double.Parse(FirstValue);
            var secondValue = Double.Parse(SecondValue);
            double result;
            switch (OperationType)
            {
                case OperationType.Procent:
                    result = (firstValue * secondValue) / 100;
                    break;
                case OperationType.Add:
                    result = firstValue + secondValue;
                    break;
                case OperationType.Subtract:
                    result = firstValue - secondValue;
                    break;
                case OperationType.Multiple:
                    result = firstValue * secondValue;
                    break;
                case OperationType.Divide:
                    {
                        if (secondValue == 0)
                            throw new DivideByZeroException("Cannot divide by zero");

                        result = firstValue / secondValue;
                    }
                    break;
                case OperationType.Pow2:
                    result = firstValue * firstValue;
                    break;
                case OperationType.TransponireX:
                    {
                        if (firstValue == 0)
                            throw new DivideByZeroException("Cannot divide by zero");

                        result = 1 / firstValue;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result.ToString(CultureInfo.InvariantCulture);
        }
        public OperationType GetOperationType(string operation)
        {
            switch (operation)
            {
                case "%":
                    return OperationType.Procent;
                case "+":
                    return OperationType.Add;
                case "-":
                    return OperationType.Subtract;
                case "x":
                    return OperationType.Multiple;
                case "/":
                    return OperationType.Divide;
                case "x^2":
                    return OperationType.Pow2;
                case "1/x":
                    return OperationType.TransponireX;
                case "=":
                    return OperationType.Equal;
                default:
                    throw new InvalidOperationException("Unknown operation");
            }
        }

    }
    public enum OperationType
    {
        Procent,
        Add,
        Subtract,
        Multiple,
        Divide,
        Pow2,
        TransponireX,
        Equal
    }

}

