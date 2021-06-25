using System;

namespace SimpleInterpreter
{
    internal sealed class Parser
    {
        private readonly Tokenizer _tokenizer;

        public Parser(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public int GetCalculation()
        {
            int? runningTotal = null;
            OperationToken? currentOperation = null;

            void HandleOperationToken(OperationToken token)
            {
                if (runningTotal is null)
                {
                    throw new InvalidOperationException("Cannot have an operation without a left side.");
                }
                
                currentOperation = token;
            }

            void HandleIntegerToken(IntegerToken token)
            {
                if (runningTotal is null)
                {
                    runningTotal = token.Value;
                }
                else if (currentOperation is null)
                {
                    throw new InvalidOperationException("Cannot have a right side integer without an operator.");
                }
                else
                {
                    runningTotal = currentOperation.Calculate((int) runningTotal, token.Value);
                    currentOperation = null;
                }
            }
            
            var currentToken = _tokenizer.GetNextToken();
            while (currentToken is not EndOfFileToken)
            {
                switch (currentToken)
                {
                    case IntegerToken integer:
                        HandleIntegerToken(integer);
                        break;
                    case OperationToken operation:
                        HandleOperationToken(operation);
                        break;
                    default:
                        throw new InvalidOperationException("Unrecognised token found.");
                }
                
                currentToken = _tokenizer.GetNextToken();
            }

            if (currentOperation is not null)
            {
                throw new InvalidOperationException(
                    "Cannot complete calculation as there is an unapplied operation (no right-side integer).");
            }
            
            if (runningTotal is null)
            {
                throw new InvalidOperationException(
                    "No result was calculated, most likely due to invalid input.");
            }
            
            return (int) runningTotal;
        }
    }
}