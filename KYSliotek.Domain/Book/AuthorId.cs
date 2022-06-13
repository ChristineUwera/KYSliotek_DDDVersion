using KYSliotek.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain.Book
{
    public class AuthorId : Value<AuthorId>
    {
        public Guid Value { get; private set; }

        public AuthorId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Book id cannot be empty");

            Value = value;
        }

        //BookId - -> Guid
        public static implicit operator Guid(AuthorId self) => self.Value;

        //string - - BookId
        public static implicit operator AuthorId(string value)
            => new AuthorId(Guid.Parse(value));

        //Guid - -> string
        public override string ToString() => Value.ToString();
    }
}
