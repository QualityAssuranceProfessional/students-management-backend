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
                if(string.IsNullOrEmpty(phone)) return false;

                if(phone.Length < 9 || phone.Length > 13) return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password)) return false;

                if (password.Length < 9) return false;
                if (!Regex.IsMatch(password, @"[A-Z]"))
                {
                    return false;
                }

                if (!Regex.IsMatch(password, @"[a-z]"))
                {
                    return false;
                }

                if (!Regex.IsMatch(password, @"[0-9]"))
                {
                    return false;
                }

                if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?\:{ }|<>]"))
        {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
