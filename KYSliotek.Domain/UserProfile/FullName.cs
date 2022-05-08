using KYSliotek.Framework;
using System;

namespace KYSliotek.Domain.UserProfile
{
    public class FullName : Value<FullName>
    {
        public string Value { get; private set; }

        internal FullName(string value) => Value = value;

        //string --> to fullName value object
        public static FullName FromString(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));

            return new FullName(fullName);
        }

        // value object --> string value
        public static implicit operator string(FullName fullName)
           => fullName.Value;

        // To Satisfy the serialization requirements
        protected FullName() { }
    }
}
