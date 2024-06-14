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
            //System.Diagnostics.Debug.WriteLine("PL: " + DateTime.Now);
            return input;
        }
    }
}