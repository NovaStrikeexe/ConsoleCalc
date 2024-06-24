namespace ConsoleCalc.Services.Implementation;

public class Multiplication : OperationBase
{
    public override double Calculate(double leftOperand, double rightOperand) 
        => leftOperand * rightOperand;
    
    public override int Precedence 
        => 2;
}