using KYSliotek.Framework;
using System;

namespace KYSliotek.Domain.LendingService
{
    public class BookId : Value<BookId>
    {
        Guid Value { get;  }

        public BookId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Book id cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(BookId self) => self.Value;

        public static implicit operator BookId(string value)
            => new BookId(Guid.Parse(value));

        public override string ToString() => Value.ToString(); 
    }
}
