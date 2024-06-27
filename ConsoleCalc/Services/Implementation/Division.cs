using ConsoleCalc.Services.Interfaces;

namespace ConsoleCalc.Services.Implementation;

public class Division : IOperation
{
    public  double Calculate(double leftOperand, double rightOperand)
    {
        if (rightOperand == 0)
            throw new ArgumentException("Division by zero is not allowed.");

        return leftOperand / rightOperand;
    }
    
    public  int Precedence 
        => 2;

    public char Operator 
        => '/';
}