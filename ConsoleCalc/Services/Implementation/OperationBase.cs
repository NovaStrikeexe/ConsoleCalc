using ConsoleCalc.Services.Interfaces;

namespace ConsoleCalc.Services.Implementation;

public abstract class OperationBase : IOperation
{
    public abstract double Calculate(double leftOperand, double rightOperand);
    
    public abstract int Precedence { get; }
}