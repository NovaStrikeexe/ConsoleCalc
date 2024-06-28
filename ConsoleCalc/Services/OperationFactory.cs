using ConsoleCalc.Services.Implementation;
using ConsoleCalc.Services.Interfaces;

namespace ConsoleCalc.Services;

public static class OperationFactory
{
    private static readonly Dictionary<char, IOperation> operations = new();
    
    public static void RegisterOperation(IOperation operation) 
        => operations[operation.Operator] = operation;

    public static IOperation GetOperation(char operatorSymbol)
    {
        if (operations.TryGetValue(operatorSymbol, out var operation))
        {
            return operation;
        }
        throw new ArgumentException($"Unsupported operator: {operatorSymbol}");
    }

    public static bool IsOperator(char c) 
        => operations.ContainsKey(c);

    public static bool IsHigherPrecedence(char op1, char op2) 
        => operations[op1].Precedence > operations[op2].Precedence;
    
}