namespace Application.Services
{
    public static class EmailService
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                // Use IdnMapping class to convert Unicode domain names.
                email = System.Text.RegularExpressions.Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      System.Text.RegularExpressions.RegexOptions.None, TimeSpan.FromMilliseconds(200));
                // Return true if strIn is in valid e-mail format.
                return System.Text.RegularExpressions.Regex.IsMatch(email,
                      @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                      System.Text.RegularExpressions.RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (System.Text.RegularExpressions.RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            string DomainMapper(System.Text.RegularExpressions.Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new System.Globalization.IdnMapping();
                string domainName = match.Groups[2].Value;
                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    return string.Empty;
                }
                return match.Groups[1].Value + domainName;
            }
        }
    }
}
