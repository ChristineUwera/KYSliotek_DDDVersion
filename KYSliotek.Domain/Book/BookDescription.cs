using KYSliotek.Framework;

namespace KYSliotek.Domain.Book
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
