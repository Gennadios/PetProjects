using System.Text.RegularExpressions;

namespace PizzaSushiBot
{

    static class Validator
    {
        internal static bool IsValidName(string username)
        {
            if (username.Length >= 4 && username.Length < 16)
                return true;

            return false;
        }

        internal static bool IsValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        internal static bool IsValidPhoneNumber(string phoneNumber)
        {
            bool isNumeric = long.TryParse(phoneNumber, out long phone);

            if (phoneNumber.Length == 10 && isNumeric)
                return true;

            return false;
        }

        internal static bool IsValidAddress(string address)
        {
            Regex regex = new Regex(@"^[А-ЯA-Zа-яa-z]+,\s*[А-ЯA-Zа-яa-z]+,\s*\d+,\s*\d+$");

            if (regex.IsMatch(address))
                return true;

            return false;
        }

        internal static bool IsValidPassword(string password)
        {
            if (password.Length >= 6)
                return true;

            return false;
        }
    }
}
