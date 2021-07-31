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

namespace Voxell.Speech
{
  class NumberToWords
  {
    private static string[] units = {"zero", "one", "two", "three",
    "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
    "twelve", "thirteen", "fourteen", "fifteen", "sixteen",
    "seventeen", "eighteen", "nineteen"};
    private static string[] tens = {"", "", "twenty", "thirty", "forty",
    "fifty", "sixty", "seventy", "eighty", "ninety"};
    
    public static string ConvertAmount(double amount)
    {
      // try
      // {
        Int64 amount_int = (Int64)amount;
        Int64 amount_dec = (Int64)Math.Round((amount - (double)(amount_int)) * 100);
        if (amount_dec == 0)
        {
          return Convert(amount_int);
        }
        else  
        {
          return Convert(amount_int) + " point " + Convert(amount_dec);
        }
      // }
      // catch (Exception e)
      // {
      //   // tODO: handle exception
      //   Debug.LogError(e);
      // }
      // return "";
    }

    public static string ConvertAmount(float amount)
      => ConvertAmount(System.Convert.ToDouble(amount));
    
    public static string Convert(Int64 i)
    {
      if (i < 20)
        return units[i];

      if (i < 100)
        return tens[i / 10] + ((i % 10 > 0) ? " " + Convert(i % 10) : "");

      if (i < 1_000)
        return units[i / 100] + " hundred"  
            + ((i % 100 > 0) ? " and " + Convert(i % 100) : "");

      if (i < 1_000_000)
        return Convert(i / 1_000) + " thousand"
            + ((i % 1_000 > 0) ? ", " + Convert(i % 1_000) : "");

      if (i < 1_000_000_000)
        return Convert(i / 1_000_000) + " million"  
            + ((i % 1_000_000 > 0) ? ", " + Convert(i % 1_000_000) : "");

      if (i < 1_000_000_000_000)
        return Convert(i / 1_000_000_000) + " billion"  
            + ((i % 1_000_000_000 > 0) ? ", " + Convert(i % 1_000_000_000) : "");

      return Convert(i / 1_000_000_000_000) + " trillion"  
          + ((i % 1_000_000_000_000 > 0) ? ", " + Convert(i % 1_000_000_000_000) : "");
    }
  }
}