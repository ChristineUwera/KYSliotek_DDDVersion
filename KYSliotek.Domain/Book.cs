using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain
{
    public class Book
    {
        public Guid Id { get;}
        public Guid _authorId { get; set; }
        public string _title { get; set; }
        public string description { get; set; }
        //need a picture as well

        public Book(Guid id, Guid authorId)
        {
            if (id == default)
                throw new ArgumentException("Identity must be specified", nameof(id));
            if(authorId == default)
                throw new ArgumentException("owner id must be specified", nameof(authorId));

            Id = id;
            _authorId = authorId;
        }

    }
}
