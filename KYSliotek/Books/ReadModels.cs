using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYSliotek.Books
{
    public static class ReadModels
    {
        public class BookDetails
        {
            public Guid BookId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public Guid AuthorId { get; set; }
            public string PhotoUrl { get; set; }//coverPhoto
        }

        public class PublicBookListItem
        {
            public Guid BookId { get; set; }
            public string Title { get; set; }
            public Guid AuthorId { get; set; }
            public string PhotoUrl { get; set; }
        }

        public class UserDetails
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}
