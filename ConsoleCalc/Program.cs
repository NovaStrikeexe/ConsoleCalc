using ConsoleCalc.Services;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите выражение:");
        var expression = Console.ReadLine();
        var evaluator = new ExpressionEvaluator();
        try
        {
            var result = evaluator.Evaluate(expression);
            Console.WriteLine($"Результат: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}