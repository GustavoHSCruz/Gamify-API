namespace Application.Services
{
    public static class InternalService
    {
        public static string HashStringToSHA256(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static string HashObjectToSHA256(object obj)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(obj);
            return HashStringToSHA256(json);
        }


    }
}
