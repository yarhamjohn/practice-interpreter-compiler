using FluentAssertions;
using NUnit.Framework;
using SimpleInterpreter;

namespace SimpleInterpreterTests
{
    public class InterpreterTests
    {
        [Test]
        public void SingleDigitAddition()
        {
            var result = Program.Interpret("1+2");
            result.Should().Be(3);
        }
        
        [Test]
        public void MultipleDigitAddition()
        {
            var result = Program.Interpret("10+12");
            result.Should().Be(22);
        }
        
        [Test]
        public void MixedDigitAddition()
        {
            var result = Program.Interpret("11+3");
            result.Should().Be(14);
        }
        
        [Test]
        public void AdditionWithSpaces()
        {
            var result = Program.Interpret(" 12 + 3 ");
            result.Should().Be(15);
        }
        
        [Test]
        public void Subtraction()
        {
            var result = Program.Interpret("12-5");
            result.Should().Be(7);
        }
        
        [Test]
        public void Multiplication()
        {
            var result = Program.Interpret("12 * 5");
            result.Should().Be(60);
        }
        
        [Test]
        public void Division()
        {
            var result = Program.Interpret("12 / 6");
            result.Should().Be(2);
        }
        
        [Test]
        public void MultipleAddSubtractOperations()
        {
            var result = Program.Interpret("9 - 5 + 3 + 11");
            result.Should().Be(18);
        }
        
        [Test]
        public void MultipleMultiplyDivideOperations()
        {
            var result = Program.Interpret("9 * 6 / 3 * 2");
            result.Should().Be(36);
        }
        
        [Test]
        public void MultipleMixOperationsOne()
        {
            var result = Program.Interpret("2 + 7 * 4");
            result.Should().Be(30);
        }
        
        [Test]
        public void MultipleMixOperationsTwo()
        {
            var result = Program.Interpret("7 - 8 / 4");
            result.Should().Be(5);
        }
        
        [Test]
        public void MultipleMixOperationsThree()
        {
            var result = Program.Interpret("14 + 2 * 3 - 6 / 2");
            result.Should().Be(17);
        }
        
        [Test]
        public void SimpleParentheses()
        {
            var result = Program.Interpret("(2 + 4) / 2");
            result.Should().Be(3);
        }
        
        [Test]
        public void MediumParentheses()
        {
            var result = Program.Interpret("7 + 3 * (10 / (12 / (3 + 1) - 1))");
            result.Should().Be(22);
        }
        
        [Test]
        public void ComplexParentheses()
        {
            var result = Program.Interpret("7 + 3 * (10 / (12 / (3 + 1) - 1)) / (2 + 3) - 5 - 3 + (8)");
            result.Should().Be(10);
        }
        
        [Test]
        public void MultipleParentheses()
        {
            var result = Program.Interpret("7 + (((3 + 2)))");
            result.Should().Be(12);
        }
    }
}