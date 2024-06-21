using ConsoleCalc.Services.Implementation;
using ConsoleCalc.Services.Interfaces;

namespace ConsoleCalc.Services;

public static class OperationFactory
{
    private static readonly Dictionary<string, IOperation> operations = new()
    {
        { "+", new Addition() },
        { "-", new Subtraction() },
        { "*", new Multiplication() },
        { "/", new Division() }
    };

    public static IOperation GetOperation(string symbol)
    {
        if (operations.TryGetValue(symbol, out var value))
            return value;
        throw new ArgumentException($"Unsupported operation: {symbol}");
    }

    public static bool IsOperation(string symbol) => operations.ContainsKey(symbol);

    public static bool IsHigherPrecedence(string currentOperator, string stackOperator)
    {
        var precedence = new Dictionary<string, int>
        {
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "/", 2 }
        };

        return precedence[currentOperator] > precedence[stackOperator];
    }
}