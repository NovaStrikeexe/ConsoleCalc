using System.Text.RegularExpressions;

namespace ConsoleCalc.Services;

public static class ExpressionParser
{
    public static Queue<string> Parse(string expression)
    {
        var tokens = new Queue<string>();
        var matches = Regex.Matches(expression, @"(\d+(\.\d+)?)|[()+\-*/]");
        foreach (Match match in matches)
            tokens.Enqueue(match.Value);
        return tokens;
    }
}