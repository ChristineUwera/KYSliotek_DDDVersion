using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain
{
    //BookId value object
    public class BookId : IEquatable<BookId>
    {
        public Guid Value { get; }

        public BookId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Book id cannot be empty");

            Value = value;
        }

        public bool Equals(BookId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BookId)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
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
