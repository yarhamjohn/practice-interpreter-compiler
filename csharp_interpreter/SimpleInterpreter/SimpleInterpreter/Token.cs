namespace SimpleInterpreter
{
    internal record Token;

    internal record EndOfFileToken : Token;

    internal record IntegerToken(int Value) : Token;

    internal record ParenthesisToken : Token;
    
    internal record LeftParenthesisToken : ParenthesisToken;
    
    internal record RightParenthesisToken : ParenthesisToken;

    internal abstract record OperatorToken : Token
    {
        internal abstract int Calculate(int left, int right);
    }

    internal record PlusToken : OperatorToken
    {
        internal override int Calculate(int left, int right)
        {
            return left + right;
        }
    }

    internal record MinusToken : OperatorToken
    {
        internal override int Calculate(int left, int right)
        {
            return left - right;
        }
    }

    internal record MultiplyToken : OperatorToken
    {
        internal override int Calculate(int left, int right)
        {
            return left * right;
        }
    }

    internal record DivideToken : OperatorToken
    {
        internal override int Calculate(int left, int right)
        {
            return left / right;
        }
    }
}