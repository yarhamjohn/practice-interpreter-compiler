using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SimpleInterpreter
{
    internal sealed class Tokenizer
    {
        private readonly string _input;
        private int _currentPosition;

        public Tokenizer(string input)
        {
            _input = input;
        }

        public Token GetNextToken()
        {
            // Ignore any whitespace characters
            while (InputCharactersRemaining() && CurrentPositionIsWhitespace())
            {
                _currentPosition++;
            }

            // Return EOF once the end of the input is reached
            if (!InputCharactersRemaining())
            {
                return new EndOfFileToken();
            }

            // Check if next token is an integer and if so, return it
            if (TryGetInteger(out var num))
            {
                return new IntegerToken((int) num);
            }
            
            // Check if the next token is an operator and if so, return it
            if (TryGetOperationToken(out var operatorToken))
            {
                _currentPosition++;
                return operatorToken;
            }
            
            // Check if the next token is a parenthesis and if so, return it
            if (TryGetParenthesis(out var parenthesisToken))
            {
                _currentPosition++;
                return parenthesisToken;
            }
            
            throw new InvalidOperationException($"Invalid character found in input: {CurrentChar()}");
        }

        /// <summary>
        /// Checks if the _currentPosition in _input is a whitespace or not.
        /// </summary>
        /// <returns>True when the _currentPosition in _input is whitespace.</returns>
        private bool CurrentPositionIsWhitespace() => CurrentChar() == ' ';

        /// <summary>
        /// Gets the char at the _currentPosition in _input.
        /// </summary>
        /// <returns>The char at the _currentPosition in _input.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the end of the _input string has been reached.
        /// </exception>
        private char CurrentChar()
        {
            if (!InputCharactersRemaining())
            {
                throw new InvalidOperationException("Cannot get the current char because no characters remain");
            }

            return _input[_currentPosition];
        }

        /// <summary>
        /// Determines whether or not all the characters in _input have been parsed.
        /// </summary>
        /// <returns>True when there are any unparsed characters remaining in _input.</returns>
        private bool InputCharactersRemaining() => _currentPosition < _input.Length;

        /// <summary>
        /// Attempts to parse the _currentPosition in the _input as an integer.
        /// Will increment the _currentPosition and expand the integer until a non-integer character is reached.
        /// </summary>
        /// <param name="num">The full integer parsed from _input.</param>
        /// <returns>True when an integer could be parsed from the _input starting at the _currentPosition.</returns>
        private bool TryGetInteger([NotNullWhen(true)] out int? num)
        {
            num = null;
            var result = "";

            while (InputCharactersRemaining() && char.IsDigit(CurrentChar()))
            {
                result += CurrentChar();
                _currentPosition++;
            }

            if (result.Any())
            {
                num = Convert.ToInt32(result);
            }

            return num != null;
        }

        /// <summary>
        /// Attempts to parse the _currentPosition in the _input as an operation.
        /// </summary>
        /// <param name="token">
        /// The parsed operation token. Is null if the _currentPosition in the _input is not a known operation.
        /// </param>
        /// <returns>True when the _currentPosition is an operation.</returns>
        private bool TryGetOperationToken([NotNullWhen(true)] out OperatorToken? token)
        {
            token = CurrentChar() switch
            {
                '+' => new PlusToken(),
                '-' => new MinusToken(),
                '*' => new MultiplyToken(),
                '/' => new DivideToken(),
                _ => null
            };

            return token != null;
        }
        
        
        private bool TryGetParenthesis([NotNullWhen(true)] out ParenthesisToken? token)
        {
            token = CurrentChar() switch
            {
                '(' => new LeftParenthesisToken(),
                ')' => new RightParenthesisToken(),
                _ => null
            };
            
            return token != null;
        }

    }
}