﻿namespace ConsoleCalc.Services.Interfaces;

public interface IOperation
{
    double Calculate(double left, double right);
    
    int Precedence { get; }
    
    char Operator { get; }
}


