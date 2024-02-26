using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shopfloor.Utilities
{
    internal sealed class RemovePolishCharacters
    {
        public static string Remove(string input)
        {
            //System.Diagnostics.Debug.WriteLine("PL: " + DateTime.Now);
            string polishChars = "ąćęłńóśźż";
            string englishChars = "acelnoszz";

            for (int i = 0; i < polishChars.Length; i++)
            {
                input = input.Replace(polishChars[i], englishChars[i]);
            }

            return input;
        }
    }
}