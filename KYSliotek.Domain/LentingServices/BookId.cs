using KYSliotek.Framework;
using System;

namespace KYSliotek.Domain.LentingServices
{
    public class BookId : Value<BookId>
    {
        public Guid Value { get; private set; }

        public BookId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Book id cannot be empty");

            Value = value;
        }

        //BookId - -> Guid
        public static implicit operator Guid(BookId self) => self.Value;

        //string - - BookId
        public static implicit operator BookId(string value)
            => new BookId(Guid.Parse(value));

        //Guid - -> string
        public override string ToString() => Value.ToString(); 
    }
}
