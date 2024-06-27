using ConsoleCalc.Services;

public class ExpressionEvaluator
{
    public double Evaluate(string expression)
    {
        var tokens = ExpressionParser.Parse(expression);
        var outputQueue = ShuntingYardAlgorithm(tokens);
        return EvaluatePostfix(outputQueue);
    }

    private Queue<string> ShuntingYardAlgorithm(Queue<string> tokens)
    {
        var outputQueue = new Queue<string>();
        var operatorStack = new Stack<string>();

        while (tokens.Count > 0)
        {
            var token = tokens.Dequeue();

            if (double.TryParse(token, out _))
            {
                outputQueue.Enqueue(token);
            }
            else if (OperationFactory.IsOperator(token[0]))
            {
                while (operatorStack.Count > 0 && OperationFactory.IsOperator(operatorStack.Peek()[0]) &&
                       !OperationFactory.IsHigherPrecedence(token[0], operatorStack.Peek()[0]))
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }
                operatorStack.Push(token);
            }
            else if (token == "(")
            {
                operatorStack.Push(token);
            }
            else if (token == ")")
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }
                if (operatorStack.Count == 0 || operatorStack.Peek() != "(")
                {
                    throw new ArgumentException("Mismatched parentheses.");
                }
                operatorStack.Pop();
            }
        }

        while (operatorStack.Count > 0)
        {
            var op = operatorStack.Pop();
            if (op == "(" || op == ")")
            {
                throw new ArgumentException("Mismatched parentheses.");
            }
            outputQueue.Enqueue(op);
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
            else if (OperationFactory.IsOperator(token[0]))
            {
                if (operandStack.Count < 2)
                {
                    throw new ArgumentException("Unexpected operator.");
                }

                var rightOperand = operandStack.Pop();
                var leftOperand = operandStack.Pop();
                var operation = OperationFactory.GetOperation(token[0]);
                var result = operation.Calculate(leftOperand, rightOperand);
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
}