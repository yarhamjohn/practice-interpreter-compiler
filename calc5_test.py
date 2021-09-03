import unittest
from calc5 import Lexer, Interpreter


class TestCalc5Interpreter(unittest.TestCase):
    def test_single_digit_addition(self):
        interpreter = Interpreter(Lexer("1+2"))
        self.assertEqual(interpreter.expr(), 3)

    def test_multiple_digit_addition(self):
        interpreter = Interpreter(Lexer("10+12"))
        self.assertEqual(interpreter.expr(), 22)

    def test_mixed_digit_addition(self):
        interpreter = Interpreter(Lexer("11+3"))
        self.assertEqual(interpreter.expr(), 14)

    def test_addition_with_spaces(self):
        interpreter = Interpreter(Lexer(" 12 + 3 "))
        self.assertEqual(interpreter.expr(), 15)

    def test_subtraction(self):
        interpreter = Interpreter(Lexer("12-5"))
        self.assertEqual(interpreter.expr(), 7)

    def test_multiplication(self):
        interpreter = Interpreter(Lexer("12 * 5"))
        self.assertEqual(interpreter.expr(), 60)

    def test_division(self):
        interpreter = Interpreter(Lexer("12 / 6"))
        self.assertEqual(interpreter.expr(), 2)

    def test_multiple_add_subtract_operations(self):
        interpreter = Interpreter(Lexer("9 - 5 + 3 + 11"))
        self.assertEqual(interpreter.expr(), 18)

    def test_multiple_multiple_divide_operations(self):
        interpreter = Interpreter(Lexer("9 * 6 / 3 * 2"))
        self.assertEqual(interpreter.expr(), 36)

    def test_multiple_mix_operations_one(self):
        interpreter = Interpreter(Lexer("2 + 7 * 4"))
        self.assertEqual(interpreter.expr(), 30)

    def test_multiple_mix_operations_two(self):
        interpreter = Interpreter(Lexer("7 - 8 / 4"))
        self.assertEqual(interpreter.expr(), 5)

    def test_multiple_mix_operations_three(self):
        interpreter = Interpreter(Lexer("14 + 2 * 3 - 6 / 2"))
        self.assertEqual(interpreter.expr(), 17)

    def test_parentheses(self):
        interpreter = Interpreter(Lexer(" 7 + 3 * (10 / (12 / (3 + 1) - 1))"))
        self.assertEqual(interpreter.expr(), 22)


if __name__ == '__main__':
    unittest.main()
