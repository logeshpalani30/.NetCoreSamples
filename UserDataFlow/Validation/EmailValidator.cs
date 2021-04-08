using System.Text.RegularExpressions;

namespace UserDataFlow
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(email);
            return match.Success;
        }
    }
}