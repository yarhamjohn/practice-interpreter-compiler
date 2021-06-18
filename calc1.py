#
# EOF (end-of-file) token is used to indicate that
# there is no more input left for lexical analysis
INTEGER, PLUS, MINUS, EOF = 'INTEGER', 'PLUS', 'MINUS', 'EOF'


class InterpreterException(Exception):
    """Exception raised for errors in the lexical analyzer

    Attributes:
        message -- explanation of the error
    """

    def __init__(self, message="Error parsing input"):
        self.message = message
        super().__init__(self.message)


class Token(object):
    def __init__(self, type, value):
        # token type: INTEGER, PLUS, MINUS or EOF
        self.type = type
        # token value: 0, 1, 2. 3, 4, 5, 6, 7, 8, 9, '+', '-', or None
        self.value = value

    def __str__(self):
        """String representation of the class instance.

        Examples:
            Token(INTEGER, 3)
            Token(PLUS '+')
            Token(MINUS '-')
        """
        return 'Token({type}, {value})'.format(
            type=self.type,
            value=repr(self.value)
        )

    def __repr__(self):
        return self.__str__()


class Interpreter(object):
    def __init__(self, text):
        # client string input, e.g. "3+5"
        self.text = text
        # self.pos is an index into self.text
        self.pos = 0
        # current token instance
        self.current_token = None

    def error(self):
        raise InterpreterException

    def get_next_token(self):
        """Lexical analyzer (also known as scanner or tokenizer)

        This method is responsible for breaking a sentence
        apart into tokens. One token at a time.
        """
        text = self.text

        # is self.pos index past the end of the self.text ?
        # if so, then return EOF token because there is no more
        # input left to convert into tokens
        if self.pos > len(text) - 1:
            return Token(EOF, None)

        # get a character at the position self.pos and decide
        # what token to create based on the single character
        current_char = text[self.pos]

        # ignore whitespace
        if current_char.isspace():
            self.pos += 1
            current_char = text[self.pos]

        # get each consecutive character until a non-digit is reached
        # then create an INTEGER token from the multi-digit integer,
        # increment self.pos index to point to the next character
        # after the integer and return the INTEGER token
        num = ""
        while True:
            if current_char.isdigit():
                num += current_char
                self.pos += 1

                if self.pos > len(text) - 1:
                    return Token(INTEGER, int(num))

                current_char = text[self.pos]
                continue

            if len(num) > 0:
                token = Token(INTEGER, int(num))
                return token

            break

        # get a plus character, create and return a PLUS token
        if current_char == '+':
            token = Token(PLUS, current_char)
            self.pos += 1
            return token

        # get a minus character, create and return a MINUS token
        if current_char == '-':
            token = Token(MINUS, current_char)
            self.pos += 1
            return token

        # throw error if the current character is not valid
        self.error()

    def eat(self, token_type):
        # compare the current token type with the passed token
        # type and if they match then "eat" the current token
        # and assign the next token to the self.current_token,
        # otherwise raise an exception.
        if self.current_token.type == token_type:
            self.current_token = self.get_next_token()
        else:
            self.error()

    def expr(self):
        """expr -> INTEGER PLUS INTEGER"""
        # set current token to the first token taken from the input
        self.current_token = self.get_next_token()

        # we expect the current token to be a single-digit integer
        left = self.current_token
        self.eat(INTEGER)

        # we expect the current token to be a '+' token or a '-' token
        op = self.current_token
        try:
            self.eat(PLUS)
        except InterpreterException:
            self.eat(MINUS)

        # we expect the current token to be a single-digit integer
        right = self.current_token
        self.eat(INTEGER)
        # after the above call the self.current_token is set to
        # EOF token

        # at this point INTEGER PLUS INTEGER sequence of tokens
        # has been successfully found and the method can just
        # return the result of adding two integers, thus
        # effectively interpreting client input
        if op.type is PLUS:
            result = left.value + right.value
        else:
            result = left.value - right.value

        return result


def main():
    while True:
        try:
            # To run under Python3 replace 'raw_input' call
            # with 'input'
            text = input('calc> ')
        except EOFError:
            break
        if not text:
            continue
        interpreter = Interpreter(text)
        result = interpreter.expr()
        print(result)


if __name__ == '__main__':
    main()
