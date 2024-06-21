using ConsoleCalc.Services.Interfaces;

namespace ConsoleCalc.Services.Implementation;

public class Multiplication : IOperation
{
    public double Calculate(double left, double right) 
        => left * right;
}