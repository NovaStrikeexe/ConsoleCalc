namespace ConsoleCalc.Services.Implementation;

public class Division : OperationBase
{
    public override double Calculate(double leftOperand, double rightOperand)
    {
        if (rightOperand == 0)
            throw new ArgumentException("Division by zero is not allowed.");

        return leftOperand / rightOperand;
    }
    
    public override int Precedence 
        => 2;
}