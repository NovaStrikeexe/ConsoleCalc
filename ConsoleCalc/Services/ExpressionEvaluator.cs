using ConsoleCalc.Services;
using ConsoleCalc.Services.Implementation;
using ConsoleCalc.Services.Interfaces;

public class ExpressionEvaluator
{
    private readonly Dictionary<char, IOperation> operations = new()
    {
        {'+', new Addition()},
        {'-', new Subtraction()},
        {'*', new Multiplication()},
        {'/', new Division()}
    };

    public double Evaluate(string expression)
    {
        var tokens = ExpressionParser.Parse(expression);
        var outputQueue = ConvertToPostfix(tokens);
        return EvaluatePostfix(outputQueue);
    }

    private Queue<string> ConvertToPostfix(Queue<string> tokens)
    {
        var outputQueue = new Queue<string>();
        var operatorStack = new Stack<char>();

        while (tokens.Count > 0)
        {
            var token = tokens.Dequeue();

            if (double.TryParse(token, out _))
            {
                outputQueue.Enqueue(token);
            }
            else if (token == "(")
            {
                operatorStack.Push('(');
            }
            else if (token == ")")
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                {
                    outputQueue.Enqueue(operatorStack.Pop().ToString());
                }
                if (operatorStack.Count == 0 || operatorStack.Peek() != '(')
                {
                    throw new ArgumentException("Mismatched parentheses.");
                }
                operatorStack.Pop();
            }
            else if (IsOperator(token[0]))
            {
                while (operatorStack.Count > 0 && IsOperator(operatorStack.Peek()) &&
                       operations[token[0]].Precedence <= operations[operatorStack.Peek()].Precedence)
                {
                    outputQueue.Enqueue(operatorStack.Pop().ToString());
                }
                operatorStack.Push(token[0]);
            }
            else
            {
                throw new ArgumentException($"Unexpected token '{token}' in expression.");
            }
        }

        while (operatorStack.Count > 0)
        {
            var op = operatorStack.Pop();
            if (op == '(' || op == ')')
            {
                throw new ArgumentException("Mismatched parentheses.");
            }
            outputQueue.Enqueue(op.ToString());
        }

        return outputQueue;
    }

    private double EvaluatePostfix(Queue<string> outputQueue)
    {
        var operandStack = new Stack<double>();

        while (outputQueue.Count > 0)
        {
            var token = outputQueue.Dequeue();

            if (double.TryParse(token, out double number))
            {
                operandStack.Push(number);
            }
            else if (IsOperator(token[0]))
            {
                if (operandStack.Count < 2)
                {
                    throw new ArgumentException("Unexpected operator.");
                }

                var rightOperand = operandStack.Pop();
                var leftOperand = operandStack.Pop();
                var result = operations[token[0]].Calculate(leftOperand, rightOperand);
                operandStack.Push(result);
            }
            else
            {
                throw new ArgumentException($"Unexpected token '{token}' in expression.");
            }
        }

        if (operandStack.Count != 1)
        {
            throw new ArgumentException("Invalid expression.");
        }

        return operandStack.Pop();
    }





    private bool IsOperator(char ch)
    {
        return operations.ContainsKey(ch);
    }
}
