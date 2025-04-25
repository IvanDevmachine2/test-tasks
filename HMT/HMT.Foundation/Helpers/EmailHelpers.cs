using System.Text.RegularExpressions;

namespace HMT.Foundation.Helpers
{
    class EmailHelpers
    {
        public static bool SimpleEmailValidation(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
