using ConsoleCalc.Services;
using ConsoleCalc.Services.Implementation;

namespace ConsoleCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterOperations();

            Console.WriteLine("Введите выражение:");
            var input = Console.ReadLine();

            try
            {
                var evaluator = new ExpressionEvaluator();
                var result = evaluator.Evaluate(input);
                Console.WriteLine($"Результат: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void RegisterOperations()
        {
            OperationFactory.RegisterOperation(new Addition());
            OperationFactory.RegisterOperation(new Subtraction());
            OperationFactory.RegisterOperation(new Multiplication());
            OperationFactory.RegisterOperation(new Division());
        }
    }
}
