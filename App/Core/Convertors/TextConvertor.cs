using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Core.Convertors
{
    public class TextConvertor
    {
        public static string FixingText(string text)
        {
            var correctText = Regex.Replace(text, " {2,}", " ");
            return correctText.Trim().ToLower();
        }

        public static List<string> TextToArray(string text, string seprator)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            var textArray = text.Split(seprator);
            var arrayText = textArray.ToList();
            return arrayText;
        }

        public static string ReplaceLetters(string text, char oldChar, char newChar)
        {
            var stringForFormat = text;
            return stringForFormat.Replace(oldChar, newChar);
        }
    }
}
