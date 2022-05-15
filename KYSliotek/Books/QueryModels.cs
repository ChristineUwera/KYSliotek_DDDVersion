using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYSliotek.Books
{
    public static class QueryModels
    {
        public class GetPublishedBooks
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetBorrowedBooks
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetPublicBook
        {
            public Guid BookId { get; set; }
        }
    }
}
