using ConsoleCalc.Services.Interfaces;

namespace ConsoleCalc.Services.Implementation;

public class Addition : OperationBase
{
    public override double Calculate(double leftOperand, double rightOperand) 
        => leftOperand + rightOperand;
    
    public override int Precedence 
        => 1;
}
