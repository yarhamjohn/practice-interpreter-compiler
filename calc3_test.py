import unittest
from calc3 import Interpreter


class TestCalc3Interpreter(unittest.TestCase):
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

    def test_multiplication(self):
        interpreter = Interpreter("12 * 5")
        self.assertEqual(interpreter.expr(), 60)

    def test_division(self):
        interpreter = Interpreter("12 / 6")
        self.assertEqual(interpreter.expr(), 2)

    def test_multiple_add_subtract_operations(self):
        interpreter = Interpreter("9 - 5 + 3 + 11")
        self.assertEqual(interpreter.expr(), 18)

    def test_multiple_multiple_divide_operations(self):
        interpreter = Interpreter("9 * 6 / 3 * 2")
        self.assertEqual(interpreter.expr(), 36)


if __name__ == '__main__':
    unittest.main()
