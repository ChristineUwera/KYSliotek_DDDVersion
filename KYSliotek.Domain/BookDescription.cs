using KYSliotek.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain
{
    public class BookDescription : Value<BookDescription>
    {
        public string Value { get; private set; }

        internal BookDescription(string value)
        {
            Value = value;
        }

        //factory method converting a string to value object
        public static BookDescription FromString(string title)
        {
            return new BookDescription(title);
        }

        public static implicit operator string(BookDescription text) =>
           text.Value;


        // Satisfy the serialization requirements
        protected BookDescription() { }
    }
}
