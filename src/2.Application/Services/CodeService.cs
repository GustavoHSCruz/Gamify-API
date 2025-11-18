namespace Application.Services
{
    public static class CodeService
    {
        public static string GenerateCode(int length = 8, bool isSplited = false)
        {
            var random = new Random();
            var code = string.Empty;
            for (int i = 0; i < length; i++)
            {
                code += random.Next(0, 10).ToString();
            }
            if (isSplited && length > 4)
            {
                code = code.Insert(length / 2, "-");
            }
            return code;
        }

        public static string GenerateAlphanumericCode(int length = 8)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var code = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }

        //    var random = new Random();
        //    var code = string.Empty;
        //        for (int i = 0; i<length; i++)
        //        {
        //            code += random.Next(0, 10).ToString();
        //}
        //        return code;

        //var random = new Random();
        //const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
        //const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //const string digitChars = "0123456789";
        //const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
        //var allChars = lowerChars + upperChars + digitChars + specialChars;

        //var password = new char[length];
        //password[0] = lowerChars[random.Next(lowerChars.Length)];
        //    password[1] = upperChars[random.Next(upperChars.Length)];
        //    password[2] = digitChars[random.Next(digitChars.Length)];
        //    password[3] = specialChars[random.Next(specialChars.Length)];
        //    for (int i = 4; i<length; i++)
        //    {
        //        password[i] = allChars[random.Next(allChars.Length)];
        //    }

        //    return new string (password.OrderBy(x => random.Next()).ToArray());
    }
}
