using ConsoleCalc.Services.Interfaces;

namespace ConsoleCalc.Services.Implementation;

public class Multiplication : IOperation
{
    public double Calculate(double left, double right) 
        => left * right;
    
    public int Precedence 
        => 2;
    
    public char Operator 
        => '*';
}