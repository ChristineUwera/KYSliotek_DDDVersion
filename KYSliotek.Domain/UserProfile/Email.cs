using KYSliotek.Framework;
using System;
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

            if (!IsValidEmail(email))
                throw new ArgumentException($"invalid email detected: {email}"); 

            return new Email(email);
        }

        public static implicit operator string(Email email)
           => email.Value;

        // Satisfy the serialization requirements
        protected Email() { }

        private static bool IsValidEmail(string email)
        {
            try
            {
                Regex emailRegex = new Regex(@"^\s*[\w\-\+_']+(\.[\w\-\+_']+)*\@[A-Za-z0-9]([\w\.-]*[A-Za-z0-9])?\.[A-Za-z][A-Za-z\.]*[A-Za-z]$",                   
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
                return emailRegex.IsMatch(email);
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}