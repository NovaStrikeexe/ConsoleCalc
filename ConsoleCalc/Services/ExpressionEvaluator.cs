namespace ConsoleCalc.Services;

public class ExpressionEvaluator
{
    public double Evaluate(string expression)
    {
        var tokens = ExpressionParser.Parse(expression);
        var outputQueue = ConvertToPostfix(tokens);
        return EvaluatePostfix(outputQueue);
    }

    private Queue<string> ConvertToPostfix(Queue<string> tokens)
    {
        var outputQueue = new Queue<string>();
        var operatorStack = new Stack<string>();

        while (tokens.Count > 0)
        {
            var token = tokens.Dequeue();

            if (double.TryParse(token, out var number))
            {
                outputQueue.Enqueue(token);
            }
            else if (OperationFactory.IsOperation(token))
            {
                ProcessOperator(token, outputQueue, operatorStack);
            }
            else
            {
                ProcessParentheses(token, outputQueue, operatorStack);
            }
        }

        while (operatorStack.Count > 0)
        {
            var op = operatorStack.Pop();
            if (op is "(" or ")")
                throw new ArgumentException("Mismatched parentheses");
            outputQueue.Enqueue(op);
        }

        return outputQueue;
    }

    private void ProcessOperator(string token, Queue<string> outputQueue, Stack<string> operatorStack)
    {
        while (operatorStack.Count > 0 && OperationFactory.IsOperation(operatorStack.Peek()) &&
               !OperationFactory.IsHigherPrecedence(token, operatorStack.Peek()))
        {
            outputQueue.Enqueue(operatorStack.Pop());
        }
        operatorStack.Push(token);
    }

    private void ProcessParentheses(string token, Queue<string> outputQueue, Stack<string> operatorStack)
    {
        switch (token)
        {
            case "(":
                operatorStack.Push(token);
                break;
            case ")":
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }
                if (operatorStack.Count == 0 || operatorStack.Peek() != "(")
                    throw new ArgumentException("Mismatched parentheses");
                operatorStack.Pop();
                break;
            }
        }
    }

    private double EvaluatePostfix(Queue<string> outputQueue)
    {
        var resultStack = new Stack<double>();

        while (outputQueue.Count > 0)
        {
            var element = outputQueue.Dequeue();

            if (double.TryParse(element, out var number))
            {
                resultStack.Push(number);
            }
            else
            {
                if (resultStack.Count < 2)
                    throw new ArgumentException("Unexpected operator");

                var right = resultStack.Pop();
                var left = resultStack.Pop();
                var operation = OperationFactory.GetOperation(element);
                resultStack.Push(operation.Calculate(left, right));
            }
        }

        if (resultStack.Count != 1)
            throw new ArgumentException("Unexpected operator");

        return resultStack.Pop();
    }
}