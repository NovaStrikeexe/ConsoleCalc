using ConsoleCalc.Services.Interfaces;

namespace ConsoleCalc.Services.Implementation;

public class Subtraction : IOperation
{
    public double Calculate(double left, double right) 
        => left - right;
    
    public int Precedence 
        => 1;
    
    public char Operator 
        => '-';
}
