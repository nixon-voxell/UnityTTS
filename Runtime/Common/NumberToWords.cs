/*
This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software Foundation,
Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.

The Original Code is Copyright (C) 2020 Voxell Technologies.
All rights reserved.
*/

using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Voxell.Speech.Common
{
    public static class NumberToWords
    {
        static string[] unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        static string[] tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        public static string CleanText(string text)
        {
            text = text.ToLower();
            text = ToWords(text);
            return text;
        }

        public static string ToWords(string text)
        {
            var match = Regex.Match(text, @"-?\d+");
            while (match.Success)
            {
                var number = Int32.Parse(match.Value);
                var words = Convert(number);

                text = match.Result(words);
                match = Regex.Match(text, @"-?\d+");
            }
            return text;
        }
        static string Convert(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + Convert(Mathf.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += Convert(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += Convert(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += Convert(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}