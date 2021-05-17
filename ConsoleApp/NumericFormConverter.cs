using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public static class NumericFormConverter
    {
        private static readonly string[] digits = new[]
        {
             "",
            "One" ,
            "Two" ,
            "Three",
            "Four" ,
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine",
        };

        private static readonly Dictionary<int, string> teens = new Dictionary<int, string>
        {
            {10, "Ten"},
            {11, "Eleven" },
            {12, "Twelve"},
            {13, "Thirteen" },
            {14, "Fourteen" },
            {15, "Fifteen" },
            {16, "Sixteen" },
            {17, "Seventeen" },
            {18, "Eighteen" },
            {19, "Nineteen "}
        };

        private static readonly Dictionary<int, string> tens = new Dictionary<int, string>
        {
            {0, ""},
            //{1, "Ten"}, // 12 -> Ten Two or Onety Two ???
            {2, "Twenty"},
            {3, "Thirty"},
            {4, "Forty"},
            {5, "Fifty"},
            {6, "Sixty"},
            {7, "Seventy"},
            {8, "Eighty"},
            {9, "Ninety"},
        };

        private static readonly string[] suffixes = new[]
        {
           "",
           "Thousand",
           "Million",
           "Billion" ,
           "Trillion" ,
           "Quadrillion" ,
           "Quintillion" ,
           "Sextillion" ,
           "Septillion" ,
           "Octillion" ,
           "Nonillion" ,
           "Decillion" ,
           "Undecillion" ,
           "Duodecillion" ,
           "Tredecillion" ,
           "Quattuordecillion" ,
           "Quindecillion" ,
           "Sexdecillion" ,
           "Septendecillion" ,
           "Octodecillion" ,
           "Novemdecillion" ,
           "Vigintillion"
        };

        private static readonly Regex containsInvalidCharacters = new Regex("[^0-9]");

        private static readonly Regex containsOtherThanZeros = new Regex("[^0]");

        private static readonly Regex splitter = new Regex(@"(?<!^)(?=[A-Z])");

        private static readonly char[] trimLeftZeroArray = new char[] { '0' };

        public static string Convert(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Empty input");

            if (number.Length > 64)
                throw new ArgumentException("Numbers with more than 64 digits is invalid, we don't have this names for them");

            if (containsInvalidCharacters.IsMatch(number))
                throw new ArgumentException("Invalid Charaters, only type numbers");

            if (!containsOtherThanZeros.IsMatch(number))
                return "Zero";

            number = number.TrimStart(trimLeftZeroArray);

            var chunks = new List<string> { "" };

            for (int index = number.Length - 1, group = 0; index >= 0; index--)
            {
                if (chunks[group].Length < 3)
                    chunks[group] = number[index] + chunks[group];
                else
                {
                    chunks.Add("");
                    chunks[++group] = number[index] + chunks[group];
                }
            }

            string numericForm = "";
            for (int suffixIndex = 0; suffixIndex < chunks.Count; suffixIndex++)
            {
                var chunk = int.Parse(chunks[suffixIndex]);

                numericForm = (chunk / 100 > 0 ? $"{digits[chunk / 100]}Hundred" : "")
                    + (chunk % 100 is < 20 and > 9 ? teens[chunk % 100] : $"{tens[chunk / 10 % 10]}{digits[chunk % 10]}")
                    + (chunk > 0 ? suffixes[suffixIndex] : "")
                    + numericForm;
            }

            var blocks = splitter.Split(numericForm);

            return string.Join(' ', blocks);
        }


    }
}
