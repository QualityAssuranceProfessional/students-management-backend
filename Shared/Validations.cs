using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared
{
    public class Validations
    {
        public Validations() { }

        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhone(string phone)
        {
            try
            {
                if (string.IsNullOrEmpty(phone)) return false;

                if (phone.Length < 9 || phone.Length > 13) return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static (bool isComplex, List<string> messages) IsPasswordComplex(string password)
        {
            List<string> validationMessages = new List<string>();

            var uppercaseRegex = new Regex(@"[A-Z]");
            var lowercaseRegex = new Regex(@"[a-z]");
            var digitRegex = new Regex(@"\d");
            var specialCharRegex = new Regex(@"[!@#$%^&*()_+}{:;'?/>.<,|~`]");

            if (password.Length < 8)
            {
                validationMessages.Add("Password must be at least 8 characters long.");
            }
            if (!uppercaseRegex.IsMatch(password))
            {
                validationMessages.Add("Password must contain at least one uppercase letter.");
            }
            if (!lowercaseRegex.IsMatch(password))
            {
                validationMessages.Add("Password must contain at least one lowercase letter.");
            }
            if (!digitRegex.IsMatch(password))
            {
                validationMessages.Add("Password must contain at least one digit.");
            }
            if (!specialCharRegex.IsMatch(password))
            {
                validationMessages.Add("Password must contain at least one special character.");
            }

            bool isComplex = validationMessages.Count == 0;

            return (isComplex, validationMessages);
        }

        public static bool IsStringOnly(object value)
        {
            if (value == null)
            {
                return false;
            }

            string strValue = value.ToString();

            if (string.IsNullOrWhiteSpace(strValue))
            {
                return false;
            }

         
            foreach (char c in strValue)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}