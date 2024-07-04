namespace Shopfloor.Utilities
{
    internal static class RemovePolishCharacters
    {
        public static string Remove(string input)
        {
            string polishChars = "ąćęłńóśźżĄĆĘŁŃÓŚŹŻ";
            string englishChars = "acelnoszzACELNOSZZ";

            for (int i = 0; i < polishChars.Length; i++)
            {
                input = input.Replace(polishChars[i], englishChars[i]);
            }
            return input;
        }
    }
}