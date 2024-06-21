using ConsoleCalc.Services.Interfaces;

namespace ConsoleCalc.Services.Implementation;

public class Division : IOperation
{
    public double Calculate(double left, double right) => left / right;
}