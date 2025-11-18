namespace Application.Services
{
    public static class PasswordService
    {
        public static string GeneratePassword(int length = 12, bool lowerChar = true, bool upperChar = true, bool digitChar = true, bool specialChar = true)
        {
            var random = new Random();
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
            var allChars = string.Empty;
            if (lowerChar) allChars += lowerChars;
            if (upperChar) allChars += upperChars;
            if (digitChar) allChars += digitChars;
            if (specialChar) allChars += specialChars;
            if (string.IsNullOrEmpty(allChars)) allChars = lowerChars + upperChars + digitChars + specialChars;
            var password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }
            return new string(password);
        }

        public static (string HashedPassword, string Salt) CryptPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return (hashedPassword, salt);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
