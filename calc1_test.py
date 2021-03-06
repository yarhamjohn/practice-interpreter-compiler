import unittest
from calc1 import Interpreter


class TestCalc1Interpreter(unittest.TestCase):
    def test_single_digit_addition(self):
        interpreter = Interpreter("1+2")
        self.assertEqual(interpreter.expr(), 3)

    def test_multiple_digit_addition(self):
        interpreter = Interpreter("10+12")
        self.assertEqual(interpreter.expr(), 22)

    def test_mixed_digit_addition(self):
        interpreter = Interpreter("11+3")
        self.assertEqual(interpreter.expr(), 14)

    def test_addition_with_spaces(self):
        interpreter = Interpreter(" 12 + 3 ")
        self.assertEqual(interpreter.expr(), 15)

    def test_subtraction(self):
        interpreter = Interpreter("12-5")
        self.assertEqual(interpreter.expr(), 7)


if __name__ == '__main__':
    unittest.main()
