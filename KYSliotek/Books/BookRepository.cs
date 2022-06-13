using KYSliotek.Domain.Book;
using KYSliotek.Infrastructure;
using Raven.Client.Documents.Session;

namespace KYSliotek.Books
{
    public class BookRepository : RavenDbRepository<Book, BookId>, IBooksRepository
    {
        public BookRepository(IAsyncDocumentSession session)
         : base(session, id => $"Book/{id.Value.ToString()}") { }

        /*
         * private readonly IAsyncDocumentSession _session;

        private static string EntityId(BookId id)   //for ravenDb
         => $"Book/{id.ToString()}";


        public BookRepository(IAsyncDocumentSession session)
        {
            _session = session;
        }
        public Task Add(Book entity)
        {
            return _session.StoreAsync(entity, EntityId(entity.Id));
        }

        public Task<bool> Exists(BookId id)
        {
            return _session.Advanced.ExistsAsync(EntityId(id));
        }

        public Task<Book> Load(BookId id)
        {
            return _session.LoadAsync<Book>(EntityId(id));
        }
        */
    }
}
