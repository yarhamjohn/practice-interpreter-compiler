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
            // expr          : add_sub_expr
            // add_sub_expr  : mul_div_expr ((ADD | SUB) mul_div_expr)*
            // mul_div_expr  : factor ((MUL | DIV) factor)*
            // factor        : INTEGER
            //

            var result = DoMulDivExpr();
            
            while (_currentToken is PlusToken or MinusToken)
            {
                result = _currentToken switch
                {
                    PlusToken plus => plus.Calculate(result, DoMulDivExpr()),
                    MinusToken sub => sub.Calculate(result, DoMulDivExpr()),
                    _ => throw new InvalidOperationException("Shouldn't ever end up here but can't declare variable in while loop expression :(")
                };
            }

            return result;
        }

        private int DoMulDivExpr()
        {
            var result = GetNextInteger();

            _currentToken = _tokenizer.GetNextToken();
            while (_currentToken is MultiplyToken or DivideToken)
            {
                result = _currentToken switch
                {
                    MultiplyToken mul => mul.Calculate(result, GetNextInteger()),
                    DivideToken div => div.Calculate(result, GetNextInteger()),
                    _ => throw new InvalidOperationException("Shouldn't ever end up here but can't declare variable in while loop expression :(")
                };

                _currentToken = _tokenizer.GetNextToken();
            }

            return result;
        }

        private int GetNextInteger()
        {
            _currentToken = _tokenizer.GetNextToken();
            if (_currentToken is IntegerToken integerToken)
            {
                return integerToken.Value;
            }

            throw new InvalidOperationException("Next token was not an integer.");
        }
    }
}