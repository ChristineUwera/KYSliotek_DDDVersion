using KYSliotek.Domain.Book;
using Raven.Client.Documents.Session;
using System.Threading.Tasks;

namespace KYSliotek.Books
{
    public class BookRepository : IBooksRepository
    {
        private readonly IAsyncDocumentSession _session;

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
    }
}
