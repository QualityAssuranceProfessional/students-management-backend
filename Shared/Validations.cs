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

        public static bool IsPasswordComplex(string password)
        {
                return password.Length >= 6
                && password.Any(char.IsUpper)
                && password.Any(char.IsLower)
                && password.Any(char.IsDigit)
                && password.Any(ch => !char.IsLetterOrDigit(ch));
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