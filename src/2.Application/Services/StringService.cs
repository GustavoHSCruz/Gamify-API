namespace Application.Services
{
    public static class StringService
    {
        public static string RemoveSpecialCharacters(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            var array = new char[str.Length];
            var arrayIndex = 0;
            foreach (var @char in str)
            {
                if (char.IsLetterOrDigit(@char) || char.IsWhiteSpace(@char))
                {
                    array[arrayIndex] = @char;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
