using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain
{
    public class AuthorId
    {
        private Guid Value { get; set; }

        public AuthorId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Author id cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(AuthorId self) => self.Value;
    }
}
