using System;

namespace SimpleInterpreter
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var result = Interpret(args[0]);
            Console.WriteLine(result);
        }

        public static int Interpret(string input)
        {
            // Lexer - tokenize the input
            var tokenizer = new Tokenizer(input);

            // Parser - understand the structure of the token stream
            var parser = new Parser(tokenizer);
            
            // Interpreter - calculate the result
            return parser.GetCalculation();
        }
    }
}