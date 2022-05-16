using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KYSliotek.Projections.ReadModels;
using static KYSliotek.Domain.Book.Book;

namespace KYSliotek.Books
{
    public static class Queries
    {
        public static Task<List<PublicBookListItem>> Query( this IAsyncDocumentSession session,
           QueryModels.GetPublishedBooks query )
        {
            return session.Query<KYSliotek.Domain.Book.Book>()
                .Where(x => x.Status == BookStatus.Active)
                .Select(x => new PublicBookListItem
                {
                    BookId = x.Id.Value,
                    Title = x.Title.Value,
                    AuthorId = x.AuthorId
                }).PagedList(query.Page, query.PageSize);
        }       

        public static Task<List<PublicBookListItem>> Query(this IAsyncDocumentSession session,
           QueryModels.GetBorrowedBooks query)
        {
            return session.Query<KYSliotek.Domain.Book.Book>()
                .Where(x => x.Status == BookStatus.Borrowed)
                .Select(x => new PublicBookListItem
                {
                    BookId = x.Id.Value,
                    Title = x.Title.Value,
                    AuthorId = x.AuthorId
                }).PagedList(query.Page, query.PageSize);
        }

        public static Task<BookDetails> Query(this IAsyncDocumentSession session,
          QueryModels.GetPublicBook query)
        => (from book in session.Query<KYSliotek.Domain.Book.Book>()
            where book.Id.Value == query.BookId
            select new BookDetails
            {
                    BookId = book.Id.Value,
                    Title = book.Title.Value,
                    AuthorId = book.AuthorId,
                    Description = book.Description
            }).SingleAsync();
        

        private static Task<List<T>> PagedList<T>(
           this IRavenQueryable<T> query, int page, int pageSize
       ) =>
           query
               .Skip(page * pageSize)
               .Take(pageSize)
               .ToListAsync();

    }
}
