using System;

namespace SimpleInterpreter
{
    internal sealed class Parser
    {
        private readonly Tokenizer _tokenizer;
        private Token? _currentToken;

        public Parser(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public int Calculate()
        {
            //
            // expr   : term ((ADD | SUB) term)*
            // term   : factor ((MUL | DIV) factor)*
            // factor : INTEGER | LPARENS expr RPARENS
            //

            return GetExpr();
        }

        private int GetExpr()
        {
            var result = GetTerm();

            while (_currentToken is PlusToken or MinusToken)
            {
                result = _currentToken switch
                {
                    PlusToken plus => plus.Calculate(result, GetTerm()),
                    MinusToken sub => sub.Calculate(result, GetTerm()),
                    _ => throw new InvalidOperationException(
                        "Shouldn't ever end up here but can't declare variable in while loop expression :(")
                };
            }

            return result;
        }

        private int GetTerm()
        {
            var result = GetFactor();

            _currentToken = _tokenizer.GetNextToken();
            while (_currentToken is MultiplyToken or DivideToken)
            {
                result = _currentToken switch
                {
                    MultiplyToken mul => mul.Calculate(result, GetFactor()),
                    DivideToken div => div.Calculate(result, GetFactor()),
                    _ => throw new InvalidOperationException("Shouldn't ever end up here but can't declare variable in while loop expression :(")
                };

                _currentToken = _tokenizer.GetNextToken();
            }

            return result;
        }

        private int GetFactor()
        {
            _currentToken = _tokenizer.GetNextToken();
            if (_currentToken is IntegerToken integerToken)
            {
                return integerToken.Value;
            }

            if (_currentToken is LeftParenthesisToken)
            {
                var result = GetExpr();
                if (_currentToken is not RightParenthesisToken)
                {
                    throw new InvalidOperationException("Found unclosed left parenthesis.");
                }

                return result;
            }

            throw new InvalidOperationException("The next factor was not valid.");
        }
    }
}