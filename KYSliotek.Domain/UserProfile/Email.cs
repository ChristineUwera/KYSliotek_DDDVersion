using KYSliotek.Framework;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace KYSliotek.Domain.UserProfile
{
    public class Email : Value<Email>
    {
        public string Value { get; private set; }

        internal Email (string email)
        {
            Value = email;
        }

        public static Email FromString(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            if (IsValidEmail(email))
                throw new ArgumentException($"invalid email detected: {email}"); 

            return new Email(email);
        }

        public static implicit operator string(Email email)
           => email.Value;

        // Satisfy the serialization requirements
        protected Email() { }

        private static bool IsValidEmail(string email)
        {
            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w{2,3})+)$)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

            //MailAddress address = new MailAddress(email);
            //string host = address.Host;
            //if(host != "gmail.com")

            string[] words = email.Split('@');
            if(words[0].Length <= 6 && words[1].Trim().Contains("gmail.com"))
            {
                return emailRegex.IsMatch(email);
            }
            return false;
        }
    }
}