﻿using ConsoleCalc.Services;

namespace CalculatorApp.Tests
{
    public class ExpressionEvaluatorTests
    {
        private readonly ExpressionEvaluator evaluator = new ExpressionEvaluator();

        [Theory]
        [InlineData("1+2", 3)]
        [InlineData("1+2-3", 0)]
        [InlineData("1+2*3", 7)]
        [InlineData("(1+2)*3", 9)]
        [InlineData("3+4*2/(1-5)", 1)]
        public void Evaluate_ValidExpressions_ReturnsExpectedResult(string expression, double expected)
        {
            var result = evaluator.Evaluate(expression);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("10 / 2", 5)]
        [InlineData("5 - 3", 2)]
        [InlineData("2 * 3", 6)]
        public void Evaluate_BasicOperations_ReturnsCorrectResult(string expression, double expected)
        {
            var result = evaluator.Evaluate(expression);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("(5 - 3) * (2 + 4)", 12)]
        [InlineData("10 / (2 + 3) * 2", 4)]
        [InlineData("2 + 3 * 4 - 5 / (1 + 4)", 13.0)]
        public void Evaluate_ComplexExpressions_ReturnsCorrectResult(string expression, double expected)
        {
            var result = evaluator.Evaluate(expression);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("(1+2", "Mismatched parentheses")]
        [InlineData("3+", "Unexpected operator")]
        public void Evaluate_InvalidExpressions_ThrowsException(string expression, string expectedErrorMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() => evaluator.Evaluate(expression));
            Assert.Contains(expectedErrorMessage, exception.Message);
        }
    }
}