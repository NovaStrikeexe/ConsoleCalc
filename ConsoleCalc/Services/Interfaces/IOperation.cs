namespace ConsoleCalc.Services.Interfaces;

public interface IOperation
{
    double Calculate(double leftOperand, double rightOperand);

    int Precedence { get; }
}

