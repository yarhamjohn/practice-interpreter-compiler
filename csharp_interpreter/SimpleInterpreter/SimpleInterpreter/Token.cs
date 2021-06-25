namespace SimpleInterpreter
{
    internal record Token;

    internal record EndOfFileToken : Token;

    internal record IntegerToken(int Value) : Token;

    internal abstract record OperationToken : Token
    {
        internal abstract int Calculate(int left, int right);
    }

    internal record PlusToken : OperationToken
    {
        internal override int Calculate(int left, int right)
        {
            return left + right;
        }
    }

    internal record MinusToken : OperationToken
    {
        internal override int Calculate(int left, int right)
        {
            return left - right;
        }
    }

    internal record MultiplyToken : OperationToken
    {
        internal override int Calculate(int left, int right)
        {
            return left * right;
        }
    }

    internal record DivideToken : OperationToken
    {
        internal override int Calculate(int left, int right)
        {
            return left / right;
        }
    }
}