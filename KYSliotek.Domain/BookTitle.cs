using KYSliotek.Framework;
using System;
using System.Text.RegularExpressions;

namespace KYSliotek.Domain
{
    public class BookTitle : Value<BookTitle>
    {
        public string Value { get; private set; }

        internal BookTitle(string value)
        {
            Value = value;
        }

        //factory method converting a string to value object
        public static BookTitle FromString(string title)
        {
            CheckValidity(title);
            return new BookTitle(title);
        }

        //a new factory function to convert HTML into value object instance in case we were to get title as html and not as a string..
        public static BookTitle FromHtml(string htmTitle)
        {
            var supportedTagsReplaced = htmTitle
                .Replace("<i>", "*")
                .Replace("</i>", "*")
                .Replace("<b>", "**")
                .Replace("</b>", "**");

            var value = Regex.Replace(supportedTagsReplaced, "<.*?>", string.Empty);

            CheckValidity(value);

            return new BookTitle(value);
        }

        private static void CheckValidity(string value)
        {
            if (value.Length > 200)
                throw new ArgumentOutOfRangeException(
                    "Title cannot be longer that 100 characters",
                    nameof(value));
        }

        public static implicit operator string(BookTitle title) =>
           title.Value;


        // Satisfy the serialization requirements
        protected BookTitle() { }
    }
}
