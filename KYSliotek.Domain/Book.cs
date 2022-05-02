using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain
{
    public class Book
    {
        public BookId Id { get;}
        public AuthorId AuthorId { get; private set; }
        public string _title { get; set; }
        public string description { get; set; }
        //need a picture as well

        public Book(BookId id, AuthorId authorId)
        {
            if (id == default)
                throw new ArgumentException("Identity must be specified", nameof(id));
            if(authorId == default)
                throw new ArgumentException("owner id must be specified", nameof(authorId));

            Id = id;
            AuthorId = authorId;
        }

    }
}
