using System;
using System.Collections.Generic;

namespace App.Core.Securities
{
    public static class DetectBot
    {
        public static List<string> Chaptcha()
        {
            var rnd = new Random();
            const string operators = "+--++-";
            var digits = new List<string>();
            var num = rnd.Next(0, operators.Length - 1);
            digits.Add(rnd.Next(1, 20).ToString());
            digits.Add(rnd.Next(1, 20).ToString());
            digits.Add(operators[num].ToString());
            digits.Add(operators[num].ToString() == "+"
                ? (Convert.ToInt32(digits[0]) + Convert.ToInt32(digits[1])).ToString()
                : (Convert.ToInt32(digits[0]) - Convert.ToInt32(digits[1])).ToString());
            return digits;
        }
    }
}
