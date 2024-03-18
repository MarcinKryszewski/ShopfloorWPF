namespace Shopfloor.Utilities
{
    internal sealed class RemovePolishCharacters
    {
        public static string Remove(string input)
        {
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