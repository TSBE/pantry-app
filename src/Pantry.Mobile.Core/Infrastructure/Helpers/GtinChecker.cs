﻿using System.Text.RegularExpressions;

namespace Pantry.Mobile.Core.Infrastructure.Helpers;

public static partial class GtinChecker
{

    private static readonly Regex _gtinRegex = checkDigits();
    public static bool IsValidGtin(string code)
    {
        if (!_gtinRegex.IsMatch(code)) return false; // check if all digits and with 8, 12, 13 or 14 digits
        code = code.PadLeft(14, '0'); // stuff zeros at start to garantee 14 digits
        var mult = Enumerable.Range(0, 13).Select(i => (code[i] - '0') * (i % 2 == 0 ? 3 : 1)).ToArray(); // STEP 1: without check digit, "Multiply value of each position" by 3 or 1
        var sum = mult.Sum(); // STEP 2: "Add results together to create sum"
        return (10 - sum % 10) % 10 == int.Parse(code[13].ToString()); // STEP 3 Equivalent to "Subtract the sum from the nearest equal or higher multiple of ten = CHECK DIGIT"
    }

    public static bool IsValidEAN13(string input) => IsValidGtin(input, 13);

    private static bool IsValidGtin(ReadOnlySpan<char> input, byte length)
    {
        if (input.Length != length)
        {
            return false;
        }

        if (!char.IsDigit(input[^1]))
        {
            return false;
        }

        var sum = 0d;
        var multiplyByThree = true;
        var inputWithoutCheckDigit = input[..^1];
        for (var i = inputWithoutCheckDigit.Length - 1; i >= 0; i--)
        {
            var currentChar = inputWithoutCheckDigit[i];
            if (!char.IsDigit(currentChar))
            {
                return false;
            }

            var value = char.GetNumericValue(currentChar);
            if (multiplyByThree)
            {
                sum += value * 3;
            }
            else
            {
                sum += value;
            }

            multiplyByThree = !multiplyByThree;
        }

        var checkDigit = char.GetNumericValue(input[^1]);
        return (sum + checkDigit) % 10 == 0;
    }

    [GeneratedRegex("^(\\d{8}|\\d{12,14})$")]
    private static partial Regex checkDigits();
}
