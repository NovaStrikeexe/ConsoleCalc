using System.Text;

namespace ConsoleCalc.Services;

public static class ExpressionParser
{
    public static Queue<string> Parse(string expression)
    {
        var tokens = new Queue<string>();
        var currentNumber = new StringBuilder();

        for (var index = 0; index < expression.Length; index++)
        {
            var symbol = expression[index];

            if (char.IsDigit(symbol) || symbol == '.')
            {
                currentNumber.Append(symbol);
            }
            else if (OperationFactory.IsOperator(symbol))
            {
                if (symbol is '-' or '+' && (index == 0 || OperationFactory.IsOperator(expression[index - 1]) || expression[index - 1] == '('))
                {
                    tokens.Enqueue("0");
                    tokens.Enqueue(symbol.ToString());
                }
                else
                {
                    if (currentNumber.Length > 0)
                    {
                        tokens.Enqueue(currentNumber.ToString());
                        currentNumber.Clear();
                    }
                    tokens.Enqueue(symbol.ToString());
                }
            }
            else switch (symbol)
            {
                case '(':
                case ')':
                {
                    if (currentNumber.Length > 0)
                    {
                        tokens.Enqueue(currentNumber.ToString());
                        currentNumber.Clear();
                    }
                    tokens.Enqueue(symbol.ToString());
                    break;
                }
                case ' ':
                    continue;
                default:
                    throw new ArgumentException($"Unexpected character '{symbol}' in expression.");
            }
        }

        if (currentNumber.Length > 0)
        {
            tokens.Enqueue(currentNumber.ToString());
        }

        return tokens;
    }
}

