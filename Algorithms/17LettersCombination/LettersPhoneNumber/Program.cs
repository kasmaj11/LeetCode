using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

namespace LettersPhoneNumber
{
    public class Program
    {
        public static void Main()
        {
            var testData = "2";
            var result = Solution.GetCombinations(testData).ToList();
            result.ForEach(item => Console.WriteLine(item));
        }
    }

    public class Solution
    {
        private static Dictionary<char, string> dict = new Dictionary<char, string>()
        {
            {'2', "abc"},
            {'3', "def"},
            {'4', "ghi"},
            {'5', "jkl"},
            {'6', "mno"},
            {'7', "pqrs"},
            {'8', "tuv"},
            {'9', "wxyz"},
        };

        public static IEnumerable<string> GetCombinations(string phoneNumber)
        {
            if(phoneNumber.Length == 1)
            {
                return dict[phoneNumber[0]].ToCharArray().Select(c => c.ToString());
            } 

            if (!Regex.IsMatch(phoneNumber, "^[2-9]+$"))
            {
                return new string[] { };
            }

            return Recursive(phoneNumber.Substring(0, phoneNumber.Length - 1), phoneNumber[phoneNumber.Length - 1]);
        }

        private static IEnumerable<string> Recursive(string digitsString, char additionalDigit)
        {
            var retVal = new List<string>();
            if (digitsString.Length == 1)
            {
                var headLetters = dict[digitsString[0]];
                var tailLetters = dict[additionalDigit];
                for (var i = 0; i < headLetters.Length; i++)
                {
                    for (var j = 0; j < tailLetters.Length; j++)
                    {
                        retVal.Add($"{headLetters[i]}{tailLetters[j]}");
                    }
                }
                return retVal;

            }
            
            var lastDigit = digitsString[digitsString.Length - 1];
            var resultForDigitsString = Recursive(digitsString.Substring(0, digitsString.Length - 1), lastDigit);

            for (var i = 0; i < resultForDigitsString.Count(); i++)
            {
                var tailLetters = dict[additionalDigit];
                for (var j = 0; j < tailLetters.Length; j++)
                {
                    retVal.Add($"{resultForDigitsString.ElementAt(i)}{tailLetters[j]}");
                }
            }
            
            return retVal;
        }
    }
}