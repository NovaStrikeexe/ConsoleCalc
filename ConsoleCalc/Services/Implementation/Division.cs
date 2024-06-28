using ConsoleCalc.Services.Interfaces;

namespace ConsoleCalc.Services.Implementation;

public class Division : IOperation
{
    public  double Calculate(double leftOperand, double rightOperand) 
        => leftOperand / rightOperand;
    
    public  int Precedence 
        => 2;

    public char Operator 
        => '/';
}