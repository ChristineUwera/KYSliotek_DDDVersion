using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain
{
    public static class Events
    {
        public class BookCreated
        {
            public Guid Id { get; set; }
            public Guid AuthorId { get; set; }
        }
        public class BookTitleChanged
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }

        public class BookDescriptionUpdated
        {
            public Guid Id { get; set; }
            public string BookDescription { get; set; }
        }

        public class BookSentForReview
        {
            public Guid Id { get; set; }
        }

        public class BookPublished
        {
            public Guid Id { get; set; }
            public Guid AuthorId { get; set; }
            public Guid ApprovedBy { get; set; }
        }
        
    }
}
