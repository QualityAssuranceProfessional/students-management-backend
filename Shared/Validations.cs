using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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

        public static bool IsValidPhoneUser(byte[] phone)
        {
            try
            {
                if (.IsNullOrEmpty(phone)) return false;

                if (phone.Length < 9 || phone.Length > 13) return false;

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
